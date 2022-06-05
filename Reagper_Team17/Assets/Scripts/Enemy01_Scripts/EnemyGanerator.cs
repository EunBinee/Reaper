using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanerator : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject[] enemyPrefab; //적의 프리펩을 받아온다.
    public Transform Pos1F_Enemy; //적의 1층 위치(y와 z를 위해서)
    public Transform Pos2F_Enemy; //적의 2층 위치

    //항상 맵에 적이 있는지 없는지 확인
    bool existEnemy = false;//적이 있으면 ture, 없으면 false;
    //player의 위치 
    int playerPosFloor;
    //현재 적 오브젝트
    public GameObject CurEnemy;
    GameObject CurEnemyCollider;
    Vector3 EnemyPos; //적의 위치


    void Start()
    {
        CurEnemy = null;
    }
    void Update()
    {
        if(!existEnemy)
        {
            //만약 적이 없으면..생성!
            StartEnemy();
            Debug.Log("적 생성 시작");
        }
    }

    void StartEnemy()
    {
        //맵에 적이 없으면 10~15초 사이에 생성한다.
        existEnemy = true;
        int rand = Random.Range(1, 2);
        Debug.Log(rand+"초 뒤 적 생성");
        Invoke("createEnemy", rand);
    }
    void createEnemy()
    {
        Debug.Log("적 생성!!!!!!!!!!!!");
        //적의 위치 정하기, 
        playerPosFloor = playerController.GetFloor();
        if( playerPosFloor==1)
        {
            int Random_X = Random.Range(10, 20);
            int Random_Oper = Random.Range(1, 3); //1이면 -, 2면 +

            Debug.Log(Random_X);
            
            if (Random_Oper == 1)
            {
                
                EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
            }
            else
            {
                
                EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
            }

           
            CurEnemy = Instantiate(enemyPrefab[0], EnemyPos, Quaternion.identity); //적생성
            CurEnemyCollider = Instantiate(enemyPrefab[1], EnemyPos, Quaternion.identity); //적생성
        }
        if (playerPosFloor == 2)
        {
            //만약 1층이면..
            int Random_X = Random.Range(10, 20);
            int Random_Oper = Random.Range(1, 3); //1이면 -, 2면 +

            Debug.Log(Random_X);

            if (Random_Oper == 1)
            {
                

                EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
            }
            else
            {

                EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
            }

            CurEnemy = Instantiate(enemyPrefab[0], EnemyPos, Quaternion.identity); //적생성
            CurEnemyCollider = Instantiate(enemyPrefab[1], EnemyPos, Quaternion.identity); //적생성

            CurEnemy.GetComponent<EnemyController>().portalStatus = true;
        }
    }
}
