using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //�÷��̾��� ��ġ�� ���� ��ġ �񱳸� ����
    public PlayerController player;
    int playerRoomPos; //�÷��̾ �ִ� ��
    int playerFloorPos;//�÷��̾ �ִ� ��

    int EnemyRoomPos; //���� �ִ� ��
    int EnemyFloorPos; //���� �ִ� ��

    public bool SameRoom = false;
    public bool SameFloor = false;
    //=========================================


    SpriteRenderer sr;
    string direction = ""; //���»��ڰ� ������ ����

    public int movementSpeed = 3;

    //==============================
    //���� ������ �˸��� ����
    public bool isChasing;
    float time;
    float maxtime = 4f;
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
        //�÷��̾�� ���� ������ ��� ����
        if(EnemyFloorPos == playerFloorPos)
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
    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (isChasing)
        {
            //���� �߰� ���̶��.
            time += Time.deltaTime;
            if(time>maxtime)
            {
                CheckDirec();// maxtime�� ���� �÷��̾��� ��ġ �޾ƿ�.
                maxtime = Random.Range(2, 7);
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



    private void OnTriggerEnter2D(Collider2D collision)
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
