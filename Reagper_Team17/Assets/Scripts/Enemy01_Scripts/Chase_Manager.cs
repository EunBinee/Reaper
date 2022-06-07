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

    bool checking = false; //Stay2d����enemyMove.CheckDirec();�� ������ �ݺ��ϴ°� �Ⱦ // ������ Ǯ���� checking�� �ٽ� Ǭ��.
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
            //���� Chase�� �������� player�� ���´ٸ� �ٷ� ���� �����̿�~
            enemyController.CheckDirec();
            if(enemyController.isChasing == false && playerController.ishiding == true)
            {
                //���� ���� ���¿��� ���´ٸ�, ���� ���ϵ���
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
                //�������ϴ� ���� �ƴϰ�, �����ִ� ���̿��߸�.. ��������.
                enemyController.isChasing = false;
            }
            else
            {
                //���� ���� ���̶��, ��� ������.
                //ishiding�� true�� �ӵ� ��� ����
                enemyController.isChasing = true;
            }
        }
    }
}
