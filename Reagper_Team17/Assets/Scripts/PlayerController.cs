using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerController
    //5�� 20��: EunBin ĳ���� �̵� ����

    public GameObject condiBar; //ĳ������ ü�¹ٸ� ���� ����

    Rigidbody2D rigid;
    SpriteRenderer sr;

    public float movementSpeed = 3.0f;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        Move();
    }
    
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        // left
        if (Input.GetAxisRaw("Horizontal") < 0)  //����
        {
            moveVelocity = Vector3.left;

            sr.flipX = true;
        }
        // right
        else if (Input.GetAxisRaw("Horizontal") > 0) //������
        {
            moveVelocity = Vector3.right;

            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            movementSpeed = 7;
            condiBar.GetComponent<ConditionBar>().currentHP -= 1f;
        }
        else
        {
            movementSpeed = 3;

            condiBar.GetComponent<ConditionBar>().currentHP += 1f;
        }
        

        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }
}
