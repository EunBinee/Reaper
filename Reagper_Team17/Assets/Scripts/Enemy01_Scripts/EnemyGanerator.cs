using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanerator : MonoBehaviour
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
   public  GameObject curPortal;
    Vector3 EnemyPos; //���� ��ġ

    //=--------------------
    //��Ż�� side������ �������ʱ� ���ؼ�
    public GameObject Board_Map;
    BoxCollider2D Board_Map_Collider; //��ü ���� �����ִ� collider�� �޾ƿ��� ���ؼ�
    //---------------------------
    //endScene
    public bool stop_Ganerator = false; //������ �� ���� ����, ���� generator�� �����.
    public bool End_Enemy_Ganerator = false;
    public Transform EndEnemy_pos;

    bool check = false;
    void Start()
    {
        curPortal = null;
        Board_Map_Collider = Board_Map.GetComponent<BoxCollider2D>();
    }
    void Update()
    {

        
        if(!existEnemy && !stop_Ganerator)
        {
            //���� ���� ������..����!
            StartEnemy();
            Debug.Log("�� ���� ����");
        }
        if(stop_Ganerator&& !check)
        {
            check = true;
           // existEnemy = false;
            if (curPortal != null)
            {
                //���� curPortal�ȿ� �ִٸ�..

                curPortal.GetComponent<Portal>().Destroy_All();
            }
        }
        
        if(!existEnemy && End_Enemy_Ganerator)
        {
            
            createEnemy_EndEnemy();
        }
    }

    void StartEnemy()
    {
        //�ʿ� ���� ������ 10~15�� ���̿� �����Ѵ�.
        existEnemy = true;
        int rand = Random.Range(7, 10);
        Debug.Log(rand+"�� �� �� ����");
        Invoke("createEnemy", rand);
    }
    void createEnemy()
    {
        Debug.Log("�� ����!!!!!!!!!!!!");
        //���� ��ġ ���ϱ�, 
        playerPosFloor = playerController.GetFloor();
        if( playerPosFloor==1)
        {
            int Random_X = Random.Range(10, 15);
            int Random_Oper = Random.Range(1, 3); //1�̸� -, 2�� +

            Debug.Log(Random_X);

            if (Random_Oper == 1)
            {
                //��Ż�� �÷��̾��� ���ʿ� ����ٴ¶�.
                // ���� ���ʿ� ����µ�, BoundsMin.x���� �� �۾�����.. +�� �ٲٱ�
                if (playerController.transform.position.x - Random_X < Board_Map_Collider.bounds.min.x)
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
                if (playerController.transform.position.x - Random_X > Board_Map_Collider.bounds.max.x)
                {
                    Debug.Log("+���� -�� �ٲ�����ϴ�., �÷��̾��� ���ʿ��� ����ϴ�.");
                    EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
                }
                else
                {
                    EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);

                }
            }

            curPortal=Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //��Ż ����
        }
        if (playerPosFloor == 2)
        {
            //���� 1���̸�..
            int Random_X = Random.Range(15, 25);
            int Random_Oper = Random.Range(1, 3); //1�̸� -, 2�� +

            Debug.Log(Random_X);
            if (Random_Oper == 1)
            {
                //��Ż�� �÷��̾��� ���ʿ� ����ٴ¶�.
                // ���� ���ʿ� ����µ�, BoundsMin.x���� �� �۾�����.. +�� �ٲٱ�
                if (playerController.transform.position.x - Random_X < Board_Map_Collider.bounds.min.x)
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
                if (playerController.transform.position.x - Random_X > Board_Map_Collider.bounds.max.x)
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

    void createEnemy_EndEnemy()
    {
        Debug.Log("�� ����!!!!!!!!!!!!");
        existEnemy = true;
        Vector3 EnemyPos = new Vector3(EndEnemy_pos.position.x, EndEnemy_pos.position.y, EndEnemy_pos.position.z);
        curPortal = Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //��Ż ����


        
    }
}
