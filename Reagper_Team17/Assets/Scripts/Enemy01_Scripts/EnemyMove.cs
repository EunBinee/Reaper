using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject player;

    SpriteRenderer sr;
    string direction = ""; //저승사자가 움직일 방향

    public int movementSpeed = 3;

    //==============================
    //추적 시작을 알리는 변수
    public bool isChasing;
    float time;
    float maxtime = 4f;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        CheckDirec();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (isChasing)
        {
            //만약 추격 중이라면.
            time += Time.deltaTime;
            if(time>maxtime)
            {
                CheckDirec();// maxtime초 마다 플레이어의 위치 받아옴.
                maxtime = Random.Range(2, 7);
                time = 0;
            }
            movementSpeed = 6;//ㅈㄴ빠르게
        }
        else
        {
            time = 0;
            movementSpeed = 3;
        }
        
        if(direction == "Left")
        {
            //만약 플레이어가 왼쪽에 있어서.. 왼쪽 이동해야한다면..
            moveVelocity = Vector3.left;
            sr.flipX = true;
        }
        if (direction == "Right")
        {
            //만약 플레이어가 오른쪽에 있어서.. 오른쪽 이동해야한다면..
            moveVelocity = Vector3.right;
            sr.flipX = false;
        }
        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }

    public void CheckDirec()
    {
        //처음 생겨났을 때, 플레이어가 어떤 방향에 있는지 확인하고, 그 방향으로 움직인다.
        if (player.transform.position.x < this.transform.position.x)
        {
            direction = "Left";
        }
        if (player.transform.position.x > this.transform.position.x)
        {
            direction = "Right";

        }
    }

}
