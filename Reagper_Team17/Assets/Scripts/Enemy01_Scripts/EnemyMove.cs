using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //플레이어의 위치와 적의 위치 비교를 위함
    public PlayerController player;
    public int playerRoomPos; //플레이어가 있는 방
    public int playerFloorPos;//플레이어가 있는 층

    public int EnemyRoomPos; //적이 있는 방
    public int EnemyFloorPos; //적이 있는 층

    public bool SameRoom = false;
    public bool SameFloor = true;
    //========================================
    //만약 같은 층이 아니면 5초뒤에 사라지게 하기위한 변수
    bool EnemyDestory = false;
    //=========================================
    SpriteRenderer sr;
    public string direction = ""; //저승사자가 움직일 방향

    public int movementSpeed = 3;

    //==============================
    //추적 시작을 알리는 변수
    public bool isChasing;
    float time;
    float maxtime = 4f;

    float hidingTime = 0;
    float maxhidingTime = 15f;
    
    //======================================



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerRoomPos = player.GetRoom();
        playerFloorPos = player.GetFloor();
        CheckDirec();
    }

    // Update is called once per frame
    void Update()
    {
        playerRoomPos = player.GetRoom();
        playerFloorPos = player.GetFloor();

        //플레이어와 같은 층인지 계속 연산
        if (EnemyFloorPos == playerFloorPos)
        {
            SameFloor = true;
        }
        else
        {
            SameFloor = false;
            EnemyDestory = true;
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

        //만약 같은 층이 아니면 10초뒤에 사라지게 하기위한 변수

        if(EnemyDestory)
        {
            Invoke("EnemyDestroy", 10f);
            EnemyDestory = false;
        }
    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (isChasing)
        {
            if(player.ishiding)
            {
                //만약 player가 숨었다면..
                hidingTime += Time.deltaTime;
                if(hidingTime>maxhidingTime)
                {
                    //숨었는데 15초동안 숨으면.. 

                    isChasing = false;
                    Debug.Log("추적이 멈추었습니다.");
                    hidingTime = 0;
                }
            }
            //만약 추격 중이라면.
            time += Time.deltaTime;
            if(time>maxtime)
            {
                CheckDirec();// maxtime초 마다 플레이어의 위치 받아옴.
                maxtime = Random.Range(2, 6);
                time = 0;
            }
            movementSpeed = 6;//ㅈㄴ빠르게
        }
        else
        {
            time = 0;
            movementSpeed = 3;
        }
        
        if(direction == "Left")
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
        if (SameFloor)
        {
            //다시한번더 체크 지금도 같은 층인지
            Debug.Log("지금은 같은 층입니다. 사라지지않습니다");
        }
        else if (!SameFloor)
        {
            Debug.Log("플레이어와 다른 층입니다. 사라집니다.");
            Destroy(gameObject);
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "sidewall")
        {
            //만약 양옆 벽에 사신이 부딪쳤을때.
            if (direction == "Left")
            {
                direction = "Right";
            }
            if (direction == "Right")
            {
                direction = "Left";
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "1F_Floor")
        {
            EnemyFloorPos = 1;
            Debug.Log("적 현재 층 : " + EnemyFloorPos);
        }
        if (collision.transform.tag == "2F_Floor")
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
