using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Light : MonoBehaviour
{
    EnemyGanerator enemyGanerator;
    void Start()
    {
        enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
