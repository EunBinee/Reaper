using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    EnemyGanerator enemyGanerator;
    GameObject Portal;
    Portal portalScript;

    GameObject Enemy;
    EnemyController enemyController;
    void Start()
    {
        enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
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
    }

    // Update is called once per frame
    void Update()
    {
        Portal = enemyGanerator.curPortal;

        if (Portal != null)
        {
            portalScript = Portal.GetComponent<Portal>();
            Enemy = portalScript.CurEnemy;

            if (Enemy != null)
            {
                enemyController = Enemy.GetComponent<EnemyController>();

                this.transform.position = new Vector3(enemyController.transform.position.x, enemyController.transform.position.y , enemyController.transform.position.z);
            }
        }
    }
}
