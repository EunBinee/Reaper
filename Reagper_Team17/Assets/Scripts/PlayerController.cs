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
    public float jumpPower = 10f;
    public GameObject condiBar; //ĳ������ ü�¹ٸ� ���� ����
    public bool condiZero = false; //�����BAr.. �ʹ� �޷��� ü���� 0�� ��.
    bool isJumping = false; //ĳ���Ͱ� ������ �ϰ��ִ��� �ƴ���..
    public int playerPos_Floor = 1;//ĳ������ ��ġ_����_ 1�� _2��
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        
        if (Input.GetButtonDown("Jump"))
        {
            //���� �����̽� �ٸ� ������, ������ �ȵ����� ���!.. ����!
            Jump();
        }
    }
    private void FixedUpdate()
    {
        //������ ĳ���Ͱ� ��ġ�� ���� ��ġ�ľ��� ����..
        LayerMask mask = LayerMask.GetMask("1F") | LayerMask.GetMask("2F");

        if (rigid.velocity.y < 0) //ĳ���Ͱ� �����ؼ� velocity.y�� �������� ����
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //������ �󿡼��� ���̸� �׷��ش�
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, mask);
            if (rayHit.collider != null) // �ٴ� ������ ���ؼ� �������� ���! 
            {
                isJumping = false;
                if (rayHit.collider.tag == "1F")
                {
                    playerPos_Floor = 1;
                }
                else if (rayHit.collider.tag == "1F")
                {
                    playerPos_Floor = 2;
                }

            }
        }
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
    void Jump()
    {
        if(isJumping)
        {
            //���� ������ �ϴ� ���̸�.. true;
            return;
        }

        rigid.velocity = Vector3.zero;

        Vector3 jumpVelocity = new Vector3(0, jumpPower, 0);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = true;
    }
}
