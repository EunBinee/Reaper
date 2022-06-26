using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject Portal_Prefab;
    public GameObject[] enemyPrefab; //���� �������� �޾ƿ´�.
    public Transform Pos1F_Enemy; //���� 1�� ��ġ(y�� z�� ���ؼ�)
    public Transform Pos2F_Enemy; //���� 2�� ��ġ

    //�׻� �ʿ� ���� �ִ��� ������ Ȯ��
    public bool existEnemy = false;//���� ������ ture, ������ false;
    //player�� ��ġ 
    int playerPosFloor;
    //���� �� ������Ʈ
    public GameObject curPortal;
    Vector3 EnemyPos; //���� ��ġ

    //=--------------------
    //��Ż�� side������ �������ʱ� ���ؼ�
    public GameObject Board_Map_1F;
    BoxCollider2D Board_Map_Collider_1F; //��ü ���� �����ִ� collider�� �޾ƿ��� ���ؼ�
    public GameObject Board_Map_2F;
    BoxCollider2D Board_Map_Collider_2F; //��ü ���� �����ִ� collider�� �޾ƿ��� ���ؼ�
    //---------------------------
    //endScene
    public bool stop_Ganerator = false; //������ �� ���� ����, ���� generator�� �����.
    public bool End_Enemy_Ganerator = false;
    public Transform EndEnemy_pos;
    bool startGanerator = false;
    bool StopStartGaner = false;
    bool check = false;
    void Start()
    {
        curPortal = null;
        Board_Map_Collider_1F = Board_Map_1F.GetComponent<BoxCollider2D>();
        Board_Map_Collider_2F = Board_Map_2F.GetComponent<BoxCollider2D>();
    }
    void Update()
    {

        if (startGanerator&& stop_Ganerator)
        {
            StopStartGaner = true;
            existEnemy = false;
            startGanerator=false;
        }

        if (stop_Ganerator && !check)
        {
            check = true;
            // existEnemy = false;
            if (curPortal != null)
            {
                //���� curPortal�ȿ� �ִٸ�..
                Debug.Log("���� �����Ƿ� ����ϴ�,");
                curPortal.GetComponent<Portal>().Destroy_All();
            }
        }

        if (!existEnemy && End_Enemy_Ganerator)
        {
            existEnemy = true;
            Debug.Log("���� �����մϴ�.");
            createEnemy_EndEnemy();
        }

        else if (!existEnemy && !stop_Ganerator)
        {
            //���� ���� ������..����!
            StartEnemy();
            Debug.Log("�� ���� ����");
        }
    }

    void StartEnemy()
    {
        //�ʿ� ���� ������ 10~15�� ���̿� �����Ѵ�.
        startGanerator = true;
        existEnemy = true;
        int rand = Random.Range(20, 38);
        Debug.Log(rand + "�� �� �� ����");
        Invoke("createEnemy", rand);
    }
    void createEnemy()
    {
        if(!StopStartGaner)
        {
            startGanerator = false;
            Debug.Log("�� ����!!!!!!!!!!!!");
            //���� ��ġ ���ϱ�, 
            playerPosFloor = playerController.GetFloor();
            if (playerPosFloor == 1)
            {

                int Random_X = Random.Range(10, 15);
                int Random_Oper = Random.Range(1, 3); //1�̸� -, 2�� +

                Debug.Log(Random_X);

                if (Random_Oper == 1)
                {
                    //��Ż�� �÷��̾��� ���ʿ� ����ٴ¶�.
                    // ���� ���ʿ� ����µ�, BoundsMin.x���� �� �۾�����.. +�� �ٲٱ�
                    if (playerController.transform.position.x - Random_X < Board_Map_Collider_1F.bounds.min.x)
                    {
                        Debug.Log("-���� +�� �ٲ�����ϴ�.., �÷��̾��� �����ʿ��� ����ϴ�");
                        EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
                    }
                    else
                    {
                        EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);

                    }
                }
                else
                {
                    //��Ż�� �÷��̾��� �����ʿ� ����ٴ¶�.
                    // ���� �����ʿ� ����µ�, BoundsMax.x���� �� Ŀ����.. -�� �ٲٱ�
                    if (playerController.transform.position.x - Random_X > Board_Map_Collider_1F.bounds.max.x)
                    {
                        Debug.Log("+���� -�� �ٲ�����ϴ�., �÷��̾��� ���ʿ��� ����ϴ�.");
                        EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
                    }
                    else
                    {
                        EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);

                    }
                }

                curPortal = Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //��Ż ����
            }
            if (playerPosFloor == 2) //2��
            {
                //���� 1���̸�..
                int Random_X = Random.Range(15, 25);
                int Random_Oper = Random.Range(1, 3); //1�̸� -, 2�� +

                Debug.Log(Random_X);
                if (Random_Oper == 1)
                {
                    //��Ż�� �÷��̾��� ���ʿ� ����ٴ¶�.
                    // ���� ���ʿ� ����µ�, BoundsMin.x���� �� �۾�����.. +�� �ٲٱ�
                    if (playerController.transform.position.x - Random_X < Board_Map_Collider_2F.bounds.min.x)
                    {
                        Debug.Log("-���� +�� �ٲ�����ϴ�.., �÷��̾��� �����ʿ��� ����ϴ�");
                        EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
                    }
                    else
                    {
                        EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);

                    }
                }
                else
                {
                    //��Ż�� �÷��̾��� �����ʿ� ����ٴ¶�.
                    // ���� �����ʿ� ����µ�, BoundsMax.x���� �� Ŀ����.. -�� �ٲٱ�
                    if (playerController.transform.position.x - Random_X > Board_Map_Collider_2F.bounds.max.x)
                    {
                        Debug.Log("+���� -�� �ٲ�����ϴ�., �÷��̾��� ���ʿ��� ����ϴ�.");
                        EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
                    }
                    else
                    {
                        EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);

                    }
                }

                curPortal = Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //��Ż ����


            }
        }
       
    }

    void createEnemy_EndEnemy()
    {
        Debug.Log("�� ����createEnemy_EndEnemy!!!!!!!!!!!!");
        existEnemy = true;
        Vector3 EnemyPos = new Vector3(EndEnemy_pos.position.x, EndEnemy_pos.position.y, EndEnemy_pos.position.z);
        curPortal = Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //��Ż ����



    }
}