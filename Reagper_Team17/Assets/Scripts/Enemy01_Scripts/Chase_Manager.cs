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
            //���� Chase�� �������� player�� ���´ٸ� �ٷ� ���� �����̿�~
            enemyMove.CheckDirec();
            enemyMove.isChasing = true;
        }
    }
}
