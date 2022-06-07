using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerController playerController;

    SpriteRenderer sr;
    float time = 0;
    float maxtime = 7.0f;

    bool EndPortal = false; //만약 false인 경우, 막 처음 생성 된 경우, true인 경우 이제 사라질 포탈

    //현재 적 오브젝트
    EnemyGanerator enemyGanerator; //existEnemy을 변경해줘야함. 나중에
    GameObject Enemy_Prefab;
    GameObject Enemy_Prefab_collider;
    GameObject Enemy_Prefab_Light;


    public GameObject CurEnemy;
    EnemyController enemyController;
    GameObject CurEnemyCollider;
    Vector3 EnemyPos; //적의 위치


    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();

        sr = GetComponent<SpriteRenderer>();
        maxtime = Random.Range(4, 7);
        enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
        Enemy_Prefab = enemyGanerator.enemyPrefab[0];
        Enemy_Prefab_collider= enemyGanerator.enemyPrefab[1];
        Enemy_Prefab_Light = enemyGanerator.enemyPrefab[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndPortal) //갓 태어난 포탈일 경우
        {
            time += Time.deltaTime;
            Debug.Log("maxtime : " + maxtime);
            if (time > maxtime)
            {

                EnemyPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

                CurEnemy = Instantiate(Enemy_Prefab, EnemyPos, Quaternion.identity); //적생성
                CurEnemyCollider = Instantiate(Enemy_Prefab_collider, EnemyPos, Quaternion.identity); //적 콜라이더 생성
                Enemy_Prefab_Light = Instantiate(Enemy_Prefab_Light, EnemyPos, Quaternion.identity); //적 라이트 생성
                enemyController = CurEnemy.GetComponent<EnemyController>();
                EndPortal = true;
                enemyController.StopPortal = true;
                sr.color = new Color(1, 1, 1, 0);//투명하게 만듬

                time = 0;
            }
        }
        if (EndPortal) //한번이상 태어난 경우
        {
            if (enemyController.StopPortal) //true 일때, 사신은 움직이고 잇고, 아직 사라지지는 않을 거야.
            {

            }
            else if (!enemyController.StopPortal)//false 일때, 사신은 이제 사라질 거야.
            {
                if(CurEnemy!=null)
                {
                    this.transform.position = new Vector3(CurEnemy.transform.position.x, this.transform.position.y, this.transform.position.z);
                }
                sr.color = new Color(1, 1, 1, 1);//투명하게 만듬

                //사라지게 할 함수
                Destroy(CurEnemy);
                Destroy(CurEnemyCollider);
                Invoke("Destroy_Portal", 3);
            }
        }
    }

    void Destroy_Portal()
    {
        enemyGanerator.existEnemy = false;
        Destroy(gameObject);
    }

}