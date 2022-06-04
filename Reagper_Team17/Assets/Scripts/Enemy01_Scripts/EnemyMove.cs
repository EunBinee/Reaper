using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //�÷��̾��� ��ġ�� ���� ��ġ �񱳸� ����
    public PlayerController player;
    public int playerRoomPos; //�÷��̾ �ִ� ��
    public int playerFloorPos;//�÷��̾ �ִ� ��

    public int EnemyRoomPos; //���� �ִ� ��
    public int EnemyFloorPos; //���� �ִ� ��

    public bool SameRoom = false;
    public bool SameFloor = true;
    //========================================
    //���� ���� ���� �ƴϸ� 5�ʵڿ� ������� �ϱ����� ����
    bool EnemyDestory = false;
    //=========================================
    SpriteRenderer sr;
    public string direction = ""; //���»��ڰ� ������ ����

    public int movementSpeed = 3;

    //==============================
    //���� ������ �˸��� ����
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

        //�÷��̾�� ���� ������ ��� ����
        if (EnemyFloorPos == playerFloorPos)
        {
            SameFloor = true;
        }
        else
        {
            SameFloor = false;
            EnemyDestory = true;
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

        //���� ���� ���� �ƴϸ� 10�ʵڿ� ������� �ϱ����� ����

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
                //���� player�� �����ٸ�..
                hidingTime += Time.deltaTime;
                if(hidingTime>maxhidingTime)
                {
                    //�����µ� 15�ʵ��� ������.. 

                    isChasing = false;
                    Debug.Log("������ ���߾����ϴ�.");
                    hidingTime = 0;
                }
            }
            //���� �߰� ���̶��.
            time += Time.deltaTime;
            if(time>maxtime)
            {
                CheckDirec();// maxtime�� ���� �÷��̾��� ��ġ �޾ƿ�.
                maxtime = Random.Range(2, 6);
                time = 0;
            }
            movementSpeed = 6;//����������
        }
        else
        {
            time = 0;
            movementSpeed = 3;
        }
        
        if(direction == "Left")
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
        if (SameFloor)
        {
            //�ٽ��ѹ��� üũ ���ݵ� ���� ������
            Debug.Log("������ ���� ���Դϴ�. ��������ʽ��ϴ�");
        }
        else if (!SameFloor)
        {
            Debug.Log("�÷��̾�� �ٸ� ���Դϴ�. ������ϴ�.");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "sidewall")
        {
            //���� �翷 ���� ����� �ε�������.
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "sidewall")
        {
            //���� �翷 ���� ����� �ε�������.
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
            Debug.Log("�� ���� �� : " + EnemyFloorPos);
        }
        if (collision.transform.tag == "2F_Floor")
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