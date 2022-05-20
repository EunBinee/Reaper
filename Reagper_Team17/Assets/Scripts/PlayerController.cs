using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerController
    //5�� 20��: EunBin ĳ���� �̵� ����

    Rigidbody2D rigid;
    SpriteRenderer sr;

    public float movementSpeed = 3.0f;

    public GameObject condiBar; //ĳ������ ü�¹ٸ� ���� ����
    public bool condiZero = false; //�����BAr.. �ʹ� �޷��� ü���� 0�� ��.

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        /*        if(!condiZero)//false�϶�, �� ü���� �ٴ� �����ʾ�����.. ����
                {
                    //���� condiZero�� true�̸�, �����������Ѵ�.
                    Move();
                }
                else if (condiZero) //true�϶�, �� ü���� �ٴ� ������.. ����
                {
                    //������ϴ� �ִϸ��̼� �߰�
                    //!!
                }*/
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



        if (!condiZero)//false�϶�, �� ü���� �ٴ� �����ʾ�����.. ����
        {
            if (Input.GetKey(KeyCode.Z))
            {
                movementSpeed = 7;
                condiBar.GetComponent<ConditionBar>().currentHP -= 0.5f;
            }
            else
            {
                movementSpeed = 3;

                condiBar.GetComponent<ConditionBar>().currentHP += 0.2f;
            }
        }
        else if (condiZero) //true�϶�, �� ü���� �ٴ� ������.. ����
        {
            //������ϴ� �ִϸ��̼� �߰�
            movementSpeed = 2;//������ ���ǵ�..

            condiBar.GetComponent<ConditionBar>().currentHP += 0.2f;
        }


        
        

        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }
}
