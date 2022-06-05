using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Manager : MonoBehaviour
{
    EnemyGanerator enemyGanerator;
    GameObject Portal;
    Portal portalScript;

    GameObject Enemy;
    EnemyController enemyController;
    PlayerController playerController;

    bool checking = false; //Stay2d에서enemyMove.CheckDirec();을 여러번 반복하는게 싫어서 // 추적이 풀리면 checking을 다시 푼다.
    void Start()
    {
        enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        Portal = enemyGanerator.curPortal;

        if (Portal != null)
        {
            portalScript = Portal.GetComponent<Portal>();
            Enemy = portalScript.CurEnemy;

            if (Enemy != null)
            {
                enemyController = Enemy.GetComponent<EnemyController>();
            }
        }
/*
        Enemy = portalScript.CurEnemy;
        enemyController = Enemy.GetComponent<EnemyController>();*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Portal != null)
        {
            portalScript = Portal.GetComponent<Portal>();
            Enemy = portalScript.CurEnemy;

            if (Enemy != null)
            {
                enemyController = Enemy.GetComponent<EnemyController>();

                this.transform.position = new Vector3(enemyController.transform.position.x, enemyController.transform.position.y + 1.5f, enemyController.transform.position.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //만약 Chase의 범위내에 player가 들어온다면 바로 추적 시작이여~
            enemyController.CheckDirec();
            if(enemyController.isChasing == false && playerController.ishiding == true)
            {
                //만약 숨은 상태에서 들어온다면, 추적 안하도록
                enemyController.isChasing = false;
            }
            else
            {
                enemyController.isChasing = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(checking)
            {
                enemyController.CheckDirec();
                checking = true;
            }
            if (enemyController.isChasing == false && playerController.ishiding == true)
            {
                //추적당하는 중이 아니고, 숨어있는 중이여야만.. 추적안함.
                enemyController.isChasing = false;
            }
            else
            {
                //만약 추적 중이라면, 계속 추적함.
                //ishiding이 true든 머든 계쏙 추적
                enemyController.isChasing = true;
            }
        }
    }
}
