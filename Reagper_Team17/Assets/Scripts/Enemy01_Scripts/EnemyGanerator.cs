using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanerator : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject[] enemyPrefab; //���� �������� �޾ƿ´�.
    public Transform Pos1F_Enemy; //���� 1�� ��ġ(y�� z�� ���ؼ�)
    public Transform Pos2F_Enemy; //���� 2�� ��ġ

    //�׻� �ʿ� ���� �ִ��� ������ Ȯ��
    bool existEnemy = false;//���� ������ ture, ������ false;
    //player�� ��ġ 
    int playerPosFloor;
    //���� �� ������Ʈ
    public GameObject CurEnemy;
    GameObject CurEnemyCollider;
    Vector3 EnemyPos; //���� ��ġ


    void Start()
    {
        CurEnemy = null;
    }
    void Update()
    {
        if(!existEnemy)
        {
            //���� ���� ������..����!
            StartEnemy();
            Debug.Log("�� ���� ����");
        }
    }

    void StartEnemy()
    {
        //�ʿ� ���� ������ 10~15�� ���̿� �����Ѵ�.
        existEnemy = true;
        int rand = Random.Range(1, 2);
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
            int Random_X = Random.Range(10, 20);
            int Random_Oper = Random.Range(1, 3); //1�̸� -, 2�� +

            Debug.Log(Random_X);
            
            if (Random_Oper == 1)
            {
                
                EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
            }
            else
            {
                
                EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
            }

           
            CurEnemy = Instantiate(enemyPrefab[0], EnemyPos, Quaternion.identity); //������
            CurEnemyCollider = Instantiate(enemyPrefab[1], EnemyPos, Quaternion.identity); //������
        }
        if (playerPosFloor == 2)
        {
            //���� 1���̸�..
            int Random_X = Random.Range(10, 20);
            int Random_Oper = Random.Range(1, 3); //1�̸� -, 2�� +

            Debug.Log(Random_X);

            if (Random_Oper == 1)
            {
                

                EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
            }
            else
            {

                EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
            }

            CurEnemy = Instantiate(enemyPrefab[0], EnemyPos, Quaternion.identity); //������
            CurEnemyCollider = Instantiate(enemyPrefab[1], EnemyPos, Quaternion.identity); //������

            CurEnemy.GetComponent<EnemyController>().portalStatus = true;
        }
    }
}
