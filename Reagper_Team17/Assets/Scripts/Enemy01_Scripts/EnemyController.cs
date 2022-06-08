using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //�÷��̾��� ��ġ�� ���� ��ġ �񱳸� ����
    PlayerController player;
    public int playerRoomPos; //�÷��̾ �ִ� ��
    public int playerFloorPos;//�÷��̾ �ִ� ��

    EnemyGanerator enemyGanerator;

    public int EnemyRoomPos; //���� �ִ� ��
    public int EnemyFloorPos; //���� �ִ� ��

    public bool SameRoom = false;
    public bool SameFloor = true;
    //=======================================
    //=========================================
    SpriteRenderer sr;
    public string direction = ""; //���»��ڰ� ������ ����

    public float movementSpeed = 3;
    float plus = 0;

    //==============================
    //���� ������ �˸��� ����
    public bool isChasing;
    float time; //
    float maxtime = 3f; //�߰���_�÷��̾��� ��ġ�� �޾ƿ� Ÿ�̹�

    float hidingTime = 0;
    float maxhidingTime = 10f;

    //======================================
    //����������� ����
    float NotChasingTime;
    float NotChasingMaxTime = 18;
    public bool StopPortal = true;

    //�����
    // �ѹ��� ����ǵ���..
    bool AudioStart_Walk = false;
    bool AudioStart_Chase = false;

    //=======================================
    //�÷��̾� ���� �߿��� �ݶ��̴� Ű��.
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
            //������ ���� ������ ���� ���,
            //�ٷ� ���� ������
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

        //������ ������ enemy���� Ȯ��
        


        //�÷��̾�� ���� ������ ��� ����
        if (EnemyFloorPos == playerFloorPos)
        {
            SameFloor = true;
        }
        else
        {
            SameFloor = false;
        }
        //�÷��̾�� ���� ������ ��� ����
        if (EnemyRoomPos == playerRoomPos)
        {
            SameRoom = true;
        }
        else
        {
            SameRoom = false;
        }

        Move();

        //���� ���� ���� �ƴϰų�, chasing���� 10~15�� ���� ���

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
                Debug.Log("�÷��̾ " + (int)NotChasingMaxTime + "���� ã�� ���߽��ϴ�. ������ϴ�.");
                Invoke("EnemyDestroy", 3f);
            }
        }
        
        if(!SameFloor)
        {
            Debug.Log("���� ���� �ƴմϴ�.������ϴ�");
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
                //���� player�� �����ٸ�..
                if(!enemyGanerator.End_Enemy_Ganerator)
                {
                    //���� ������ ���ߴ� ���� , ������ ���� ���� �ƴϰ�, �÷��̾ ������ ���...^^..
                    hidingTime += Time.deltaTime;
                    if (hidingTime > maxhidingTime)
                    {
                        //�����µ� 15�ʵ��� ������.. 

                        isChasing = false;
                        AudioStart_Chase = false;
                        GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Stop();
                        Debug.Log("������ ���߾����ϴ�.");
                        hidingTime = 0;
                    }
                }
                
            }
            //���� �߰� ���̶��.
            time += Time.deltaTime;
            if (time > maxtime)
            {
                CheckDirec();// maxtime�� ���� �÷��̾��� ��ġ �޾ƿ�.
                maxtime = Random.Range(1, 4);
                time = 0;
            }
            movementSpeed = (7+ plus);//����������
        }
        else
        {
            time = 0;
            movementSpeed = (4 + plus);
        }

        if (direction == "Left")
        {
            //���� �÷��̾ ���ʿ� �־.. ���� �̵��ؾ��Ѵٸ�..
            moveVelocity = Vector3.left;
            sr.flipX = true;
        }
        if (direction == "Right")
        {
            //���� �÷��̾ �����ʿ� �־.. ������ �̵��ؾ��Ѵٸ�..
            moveVelocity = Vector3.right;
            sr.flipX = false;
        }
        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }

    public void CheckDirec()
    {
        //ó�� ���ܳ��� ��, �÷��̾ � ���⿡ �ִ��� Ȯ���ϰ�, �� �������� �����δ�.
        if (player.transform.position.x < this.transform.position.x)
        {
            direction = "Left";
        }
        if (player.transform.position.x > this.transform.position.x)
        {
            direction = "Right";

        }
    } //�÷��̾ ���� ���ʿ� �ִ��� �����ʿ� �ִ���.

    void EnemyDestroy()
    {
        NotChasingMaxTime = Random.Range(15, 20);

        //���������
        AudioStart_Chase = false;
        AudioStart_Walk = false;
        GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Stop();
        GameObject.Find("Enemy_Walk").GetComponent<AudioSource>().Stop();
        StopPortal = false;

    }
    void EnemyDestroy_NotSameFloor()
    {
        NotChasingMaxTime = Random.Range(15, 20);

        //���������
        if (!SameFloor)
        {
            Debug.Log("(�ѹ��� Ȯ���� �ϴ�)���� ���� �ƴմϴ�.������ϴ�");
            AudioStart_Chase = false;
            AudioStart_Walk = false;
            GameObject.Find("Enemy_Chase").GetComponent<AudioSource>().Stop();
            GameObject.Find("Enemy_Walk").GetComponent<AudioSource>().Stop();
            StopPortal = false;
        }
        else
        {

            Debug.Log("(�ѹ��� Ȯ���� �ϴ�)���� ���Դϴ�.��������ʽ��ϴ�.");
        }
    }
    
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sidewall"))
        {
            //���� �翷 ���� ����� �ε�������.
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
            Debug.Log("�� ���� �� : " + EnemyFloorPos);
        }
        if (collision.CompareTag("2F_Floor"))
        {
            EnemyFloorPos = 2;
            Debug.Log("���� �� : " + EnemyFloorPos);
        }
        for (int i = 1; i < 11; i++)
        {
            //���� �ִ� �� ��ġ �ľ�
            if (collision.CompareTag("room" + i.ToString()))
            {
                EnemyRoomPos = i;
            }
        }

    }
}
