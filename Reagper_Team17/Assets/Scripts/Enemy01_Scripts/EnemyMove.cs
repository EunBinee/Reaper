using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject player;

    SpriteRenderer sr;
    string direction = ""; //���»��ڰ� ������ ����

    public int movementSpeed = 3;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        CheckDirec();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirec();
        Move();
    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

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

    void CheckDirec()
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
