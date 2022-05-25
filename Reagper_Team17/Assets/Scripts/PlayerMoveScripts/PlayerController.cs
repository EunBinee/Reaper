using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sr;

    public float movementSpeed = 3.0f;
    public float jumpPower = 30f;
    public GameObject condiBar; //ĳ������ ü�¹ٸ� ���� ����
    public bool condiZero = false; //�����BAr.. �ʹ� �޷��� ü���� 0�� ��.
    bool isJumping = false; //ĳ���Ͱ� ������ �ϰ��ִ��� �ƴ���..
    public int playerPos_Floor = 1;//ĳ������ ��ġ_����_ 1�� _2��
    
    public int playerPos_Room = 0;//ĳ������ ��ġ_����_ 1�� _2��


    public bool isLadder = false; //��ٸ��� Ÿ�� �ִ��� �ƴ��� ����
    public bool wantDown; //�Ʒ��� ������������� ����
    //====================================
    //������ �κ��丮
    public Inventory inventory;
    public GameObject _item;
    private bool isCilck = false;
    //====================================

    //��ó��.. ���� ������ ���̶��, z�� �����߸� �����̰�...
    bool inCase = false;

    //==============================================

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        if (isLadder && Input.GetKey(KeyCode.X))
        {
            //���� ��ٸ��� Ÿ�� �ִٸ�...?
            float v = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0; //��ٸ��� Ÿ��������, �߷� ����
            rigid.velocity = new Vector2(rigid.velocity.x, v * movementSpeed);
        }
        else
        {
            Move();

            if (Input.GetButtonDown("Jump"))
            {
                //���� �����̽� �ٸ� ������, ������ �ȵ����� ���!.. ����!
                Jump(); //��ٸ��� Ÿ������ ���� ��, �߷� �ְ�
            }
            rigid.gravityScale = 3;
        }


    }
    private void FixedUpdate()
    {
        //������ ĳ���Ͱ� ��ġ�� ���� ��ġ�ľ��� ����..
        LayerMask mask = LayerMask.GetMask("1F") | LayerMask.GetMask("2F");

        if (rigid.velocity.y < 0) //ĳ���Ͱ� �����ؼ� velocity.y�� �������� ����
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //������ �󿡼��� ���̸� �׷��ش�
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, mask);
            if (rayHit.collider != null) // �ٴ� ������ ���ؼ� �������� ���! 
            {
                //isJumping = false;
                Debug.Log(rayHit.collider.name);

                if (rayHit.collider.CompareTag("1F_Floor"))
                {
                    playerPos_Floor = 1;
                    Debug.Log("���� �� : " + playerPos_Floor);
                }
                else if (rayHit.collider.CompareTag("2F_Floor"))
                {
                    playerPos_Floor = 2;
                    Debug.Log("���� �� : " + playerPos_Floor);
                }

            }
        }
    }
    void Move()
    {
        
        Vector3 moveVelocity = Vector3.zero;
        bool Dash = false; //������ �޸��� �ִ���..

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
                Dash = true;
                movementSpeed = 7;
                condiBar.GetComponent<ConditionBar>().currentHP -= 0.5f;
            }
            else
            {
                Dash = false;
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




        if (!inCase)
        {
            transform.position += moveVelocity * movementSpeed * Time.deltaTime;
        }
        else if (inCase && Dash)
        {
            movementSpeed = 2;
            transform.position += moveVelocity * movementSpeed * Time.deltaTime;
        }
        else if (inCase)
        {
            movementSpeed = 0.1f;
            transform.position += moveVelocity * movementSpeed * Time.deltaTime;
        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Ground_1F")
        {
            isJumping = false;
        }
        if (collision.transform.name == "Ground_2F")
        {
            isJumping = false;
        }
        //���� �������ȿ� �÷��̾ �����ִٸ�..
        if (collision.gameObject.tag=="playerCase")
        {
            inCase = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            //��ٸ��� �꿴����
            isLadder = true;
        }
        for(int i=1;i<5;i++)
        {
            //�÷��̾ �ִ� �� ��ġ �ľ�
            if (collision.CompareTag("room"+i.ToString()))
            {
                playerPos_Room = i;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("key"))
        {
            //���� �÷��̾�� ����ִ� Key���� shift�� ������.. �κ��丮�� ����
            _item = collision.gameObject;
            
            if (Input.GetKey(KeyCode.C))
            {
                if(!isCilck)
                {
                    isCilck = true;
                    inventory.AddItem(_item.gameObject, _item.gameObject.GetComponent<Item>());
                    Invoke("isCilck_Return", 1); //1�ʵڿ� ���� �������� Ŭ�� �� �� ����.
                }

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }

    void isCilck_Return()
    {
        isCilck = false;
    }
}
