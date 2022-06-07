using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanerator : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject Portal_Prefab;
    public GameObject[] enemyPrefab; //적의 프리펩을 받아온다.
    public Transform Pos1F_Enemy; //적의 1층 위치(y와 z를 위해서)
    public Transform Pos2F_Enemy; //적의 2층 위치

    //항상 맵에 적이 있는지 없는지 확인
    public bool existEnemy = false;//적이 있으면 ture, 없으면 false;
    //player의 위치 
    int playerPosFloor;
    //현재 적 오브젝트
   public  GameObject curPortal;
    Vector3 EnemyPos; //적의 위치

    //=--------------------
    //포탈이 side밖으로 나가지않기 위해서
    public GameObject Board_Map;
    BoxCollider2D Board_Map_Collider; //전체 맵을 덮고있는 collider을 받아오기 위해서
    //---------------------------
    //endScene
    public bool stop_Ganerator = false; //마지막 방 문을 열면, 이제 generator가 멈춘다.
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
            //만약 적이 없으면..생성!
            StartEnemy();
            Debug.Log("적 생성 시작");
        }
        if(stop_Ganerator&& !check)
        {
            check = true;
           // existEnemy = false;
            if (curPortal != null)
            {
                //만약 curPortal안에 있다면..

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
        //맵에 적이 없으면 10~15초 사이에 생성한다.
        existEnemy = true;
        int rand = Random.Range(7, 10);
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
            int Random_X = Random.Range(10, 15);
            int Random_Oper = Random.Range(1, 3); //1이면 -, 2면 +

            Debug.Log(Random_X);

            if (Random_Oper == 1)
            {
                //포탈이 플레이어의 왼쪽에 생긴다는뜻.
                // 만약 왼쪽에 생기는데, BoundsMin.x보다 더 작아지면.. +로 바꾸기
                if (playerController.transform.position.x - Random_X < Board_Map_Collider.bounds.min.x)
                {
                    Debug.Log("-에서 +로 바뀌었습니다.., 플레이어의 오른쪽에서 생깁니다");
                    EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
                }
                else
                {
                    EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);

                }
            }
            else
            {
                //포탈이 플레이어의 오른쪽에 생긴다는뜻.
                // 만약 오른쪽에 생기는데, BoundsMax.x보다 더 커지면.. -로 바꾸기
                if (playerController.transform.position.x - Random_X > Board_Map_Collider.bounds.max.x)
                {
                    Debug.Log("+에서 -로 바뀌었습니다., 플레이어의 왼쪽에서 생깁니다.");
                    EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);
                }
                else
                {
                    EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos1F_Enemy.position.y, Pos1F_Enemy.position.z);

                }
            }

            curPortal=Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //포탈 생성
        }
        if (playerPosFloor == 2)
        {
            //만약 1층이면..
            int Random_X = Random.Range(15, 25);
            int Random_Oper = Random.Range(1, 3); //1이면 -, 2면 +

            Debug.Log(Random_X);
            if (Random_Oper == 1)
            {
                //포탈이 플레이어의 왼쪽에 생긴다는뜻.
                // 만약 왼쪽에 생기는데, BoundsMin.x보다 더 작아지면.. +로 바꾸기
                if (playerController.transform.position.x - Random_X < Board_Map_Collider.bounds.min.x)
                {
                    Debug.Log("-에서 +로 바뀌었습니다.., 플레이어의 오른쪽에서 생깁니다");
                    EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
                }
                else
                {
                    EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);

                }
            }
            else
            {
                //포탈이 플레이어의 오른쪽에 생긴다는뜻.
                // 만약 오른쪽에 생기는데, BoundsMax.x보다 더 커지면.. -로 바꾸기
                if (playerController.transform.position.x - Random_X > Board_Map_Collider.bounds.max.x)
                {
                    Debug.Log("+에서 -로 바뀌었습니다., 플레이어의 왼쪽에서 생깁니다.");
                    EnemyPos = new Vector3(playerController.transform.position.x - Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);
                }
                else
                {
                    EnemyPos = new Vector3(playerController.transform.position.x + Random_X, Pos2F_Enemy.position.y, Pos1F_Enemy.position.z);

                }
            }

            curPortal = Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //포탈 생성


        }
    }

    void createEnemy_EndEnemy()
    {
        Debug.Log("적 생성!!!!!!!!!!!!");
        existEnemy = true;
        Vector3 EnemyPos = new Vector3(EndEnemy_pos.position.x, EndEnemy_pos.position.y, EndEnemy_pos.position.z);
        curPortal = Instantiate(Portal_Prefab, EnemyPos, Quaternion.identity); //포탈 생성


        
    }
}
