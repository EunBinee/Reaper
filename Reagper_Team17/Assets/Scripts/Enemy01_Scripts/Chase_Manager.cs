using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Manager : MonoBehaviour
{
    public EnemyMove enemyMove;
    public PlayerController playerController;

    bool checking = false; //Stay2d에서enemyMove.CheckDirec();을 여러번 반복하는게 싫어서 // 추적이 풀리면 checking을 다시 푼다.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(enemyMove.transform.position.x, enemyMove.transform.position.y + 1.5f, enemyMove.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            //만약 Chase의 범위내에 player가 들어온다면 바로 추적 시작이여~
            enemyMove.CheckDirec();
            if(enemyMove.isChasing == false && playerController.ishiding == true)
            {
                //만약 숨은 상태에서 들어온다면, 추적 안하도록
                enemyMove.isChasing = false;
            }
            else
            {
                enemyMove.isChasing = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(checking)
            {
                enemyMove.CheckDirec();
                checking = true;
            }
            if (enemyMove.isChasing == false && playerController.ishiding == true)
            {
                //추적당하는 중이 아니고, 숨어있는 중이여야만.. 추적안함.
                enemyMove.isChasing = false;
            }
            else
            {
                //만약 추적 중이라면, 계속 추적함.
                //ishiding이 true든 머든 계쏙 추적
                enemyMove.isChasing = true;
            }
        }
    }
}
