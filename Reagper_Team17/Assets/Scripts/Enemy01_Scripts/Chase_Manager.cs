using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Manager : MonoBehaviour
{
    public EnemyMove enemyMove;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            //만약 Chase의 범위내에 player가 들어온다면 바로 추적 시작이여~
            enemyMove.CheckDirec();
            enemyMove.isChasing = true;
        }
    }
}
