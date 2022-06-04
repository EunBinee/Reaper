using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject player;

    SpriteRenderer sr;
    string direction = ""; //���»��ڰ� ������ ����

    public int movementSpeed = 3;

    //==============================
    //���� ������ �˸��� ����
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
            //���� �߰� ���̶��.
            time += Time.deltaTime;
            if(time>maxtime)
            {
                CheckDirec();// maxtime�� ���� �÷��̾��� ��ġ �޾ƿ�.
                maxtime = Random.Range(2, 7);
                time = 0;
            }
            movementSpeed = 6;//����������
        }
        else
        {
            time = 0;
            movementSpeed = 3;
        }
        
        if(direction == "Left")
        {
            //���� �÷��̾ ���ʿ� �־.. ���� �̵��ؾ��Ѵٸ�..
            moveVelocity = Vector3.left;
            sr.flipX = true;
        }
        if (direction == "Right")
        {
            //���� �÷��̾ �����ʿ� �־.. ������ �̵��ؾ��Ѵٸ�..
            moveVelocity = Vector3.right;
            sr.flipX = false;
        }
        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }

    public void CheckDirec()
    {
        //ó�� ���ܳ��� ��, �÷��̾ � ���⿡ �ִ��� Ȯ���ϰ�, �� �������� �����δ�.
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
