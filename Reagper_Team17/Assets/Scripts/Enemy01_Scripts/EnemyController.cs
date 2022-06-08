using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //플레이어의 위치와 적의 위치 비교를 위함
    PlayerController player;
    public int playerRoomPos; //플레이어가 있는 방
    public int playerFloorPos;//플레이어가 있는 층

    EnemyGanerator enemyGanerator;

    public int EnemyRoomPos; //적이 있는 방
    public int EnemyFloorPos; //적이 있는 층

    public bool SameRoom = false;
    public bool SameFloor = true;
    //=======================================
    //=========================================
    SpriteRenderer sr;
    public string direction = ""; //저승사자가 움직일 방향

    public float movementSpeed = 3;
    float plus = 0;

    //==============================
    //추적 시작을 알리는 변수
    public bool isChasing;
    float time; //
    float maxtime = 3f; //추격중_플레이어의 위치를 받아올 타이밍

    float hidingTime = 0;
    float maxhidingTime = 10f;

    //======================================
    //사라지기위한 변수
    float NotChasingTime;
    float NotChasingMaxTime = 18;
    public bool StopPortal = true;

    //오디오
    // 한번만 실행되도록..
    bool AudioStart_Walk = false;
    bool AudioStart_Chase = false;

    //=======================================
    //플레이어 추적 중에만 콜라이더 키기.
    BoxCollider2D boxCollider2D;

    void Start()
    {
        boxCollider2D = FindObjectOfType<BoxCollider2D>();

        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<PlayerController>();
        enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
        sr = GetComponent<SpriteRenderer>();
        playerRoomPos = player.GetRoom();
        playerFloorPos = player.GetFloor();
        CheckDirec();

        if (enemyGanerator.End_Enemy_Ganerator)
        {
            //마지막 씬에 나오는 적일 경우,
            //바로 추적 ㄱㄱㄱ
            isChasing = true;
            plus += 1.5f;


        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.Find("Player");
        player = Player.GetComponent<PlayerController>();
        playerRoomPos = player.GetRoom();
        playerFloorPos = player.GetFloor();

        //마지막 엔딩씬 enemy인지 확인
        


        //플레이어와 같은 층인지 계속 연산
        if (EnemyFloorPos == playerFloorPos)
        {
            SameFloor = true;
        }
        else
        {
            SameFloor = false;
        }
        //플레이어와 같은 방인지 계속 연산
        if (EnemyRoomPos == playerRoomPos)
        {
            SameRoom = true;
        }
        else
        {
            SameRoom = false;
        }

        Move();

        //만약 같은 층이 아니거나, chasing한지 10~15초 지난 경우

        if (isChasing)
        {
            boxCollider2D.enabled = true;
            NotChasingTime = 0;
        }
        else
        {
            boxCollider2D.enabled = false;
            NotChasingTime += Time.deltaTime;

            if (NotChasingTime > NotChasingMaxTime)
            {
                Debug.Log("플레이어를 " + (int)NotChasingMaxTime + "동안 찾지 못했습니다. 사라집니다.");
                Invoke("EnemyDestroy", 3f);
            }
        }
        
        if(!SameFloor)
        {
            Debug.Log("같은 층이 아닙니다.사라집니다");
            Invoke("EnemyDestroy_NotSameFloor", 15f);
        }



    }
    private void Move()
    {
        if(!AudioStart_Walk)
        {
            GameObject.Find("Enemy_Walk").GetComponent<AudioSource>().Play();
            AudioStart_Walk = true;
        }
        Vector3 moveVelocity = Vector3.zero;
        if (isChasing)
        {
            if(!AudioStart_Chase)
            {

                GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Play();
                AudioStart_Chase = true;
            }
            
            if (player.ishiding)
            {
                //만약 player가 숨었다면..
                if(!enemyGanerator.End_Enemy_Ganerator)
                {
                    //만약 추적이 멈추는 때는 , 마지막 씬의 적이 아니고, 플레이어가 숨엇을 경우...^^..
                    hidingTime += Time.deltaTime;
                    if (hidingTime > maxhidingTime)
                    {
                        //숨었는데 15초동안 숨으면.. 

                        isChasing = false;
                        AudioStart_Chase = false;
                        GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Stop();
                        Debug.Log("추적이 멈추었습니다.");
                        hidingTime = 0;
                    }
                }
                
            }
            //만약 추격 중이라면.
            time += Time.deltaTime;
            if (time > maxtime)
            {
                CheckDirec();// maxtime초 마다 플레이어의 위치 받아옴.
                maxtime = Random.Range(1, 4);
                time = 0;
            }
            movementSpeed = (7+ plus);//ㅈㄴ빠르게
        }
        else
        {
            time = 0;
            movementSpeed = (4 + plus);
        }

        if (direction == "Left")
        {
            //만약 플레이어가 왼쪽에 있어서.. 왼쪽 이동해야한다면..
            moveVelocity = Vector3.left;
            sr.flipX = true;
        }
        if (direction == "Right")
        {
            //만약 플레이어가 오른쪽에 있어서.. 오른쪽 이동해야한다면..
            moveVelocity = Vector3.right;
            sr.flipX = false;
        }
        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }

    public void CheckDirec()
    {
        //처음 생겨났을 때, 플레이어가 어떤 방향에 있는지 확인하고, 그 방향으로 움직인다.
        if (player.transform.position.x < this.transform.position.x)
        {
            direction = "Left";
        }
        if (player.transform.position.x > this.transform.position.x)
        {
            direction = "Right";

        }
    } //플레이어가 적의 왼쪽에 있는지 오른쪽에 있는지.

    void EnemyDestroy()
    {
        NotChasingMaxTime = Random.Range(15, 20);

        //사라지기전
        AudioStart_Chase = false;
        AudioStart_Walk = false;
        GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Stop();
        GameObject.Find("Enemy_Walk").GetComponent<AudioSource>().Stop();
        StopPortal = false;

    }
    void EnemyDestroy_NotSameFloor()
    {
        NotChasingMaxTime = Random.Range(15, 20);

        //사라지기전
        if (!SameFloor)
        {
            Debug.Log("(한번더 확인을 하니)같은 층이 아닙니다.사라집니다");
            AudioStart_Chase = false;
            AudioStart_Walk = false;
            GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Stop();
            GameObject.Find("Enemy_Walk").GetComponent<AudioSource>().Stop();
            StopPortal = false;
        }
        else
        {

            Debug.Log("(한번더 확인을 하니)같은 층입니다.사라지지않습니다.");
        }
    }
    
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sidewall"))
        {
            //만약 양옆 벽에 사신이 부딪쳤을때.
            if (direction == "Left")
            {
                direction = "Right";
            }
            else if (direction == "Right")
            {
                direction = "Left";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("1F_Floor"))
        {
            EnemyFloorPos = 1;
            Debug.Log("적 현재 층 : " + EnemyFloorPos);
        }
        if (collision.CompareTag("2F_Floor"))
        {
            EnemyFloorPos = 2;
            Debug.Log("현재 층 : " + EnemyFloorPos);
        }
        for (int i = 1; i < 11; i++)
        {
            //적이 있는 방 위치 파악
            if (collision.CompareTag("room" + i.ToString()))
            {
                EnemyRoomPos = i;
            }
        }

    }
}
