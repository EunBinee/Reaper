using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sr;

    public EnemyGanerator enemyGanerator;
    Portal portalScript;
    GameObject Enemy;
    GameObject Portal;
    EnemyController enemyController;

    public  QuestManager questManager;
    public GameDirector gameDirector;
    //�������� ���ߴ� ����
    public bool dontMove = false;
    //=======================
    public Vector3 moveVelocity;
    public float movementSpeed = 3.0f;
    public float jumpPower = 13f;
    public GameObject condiBar; //ĳ������ ü�¹ٸ� ���� ����
    public bool condiZero = false; //�����BAr.. �ʹ� �޷��� ü���� 0�� ��.
    public  bool isJumping = false; //ĳ���Ͱ� ������ �ϰ��ִ��� �ƴ���..
    public int playerPos_Floor = 1;//ĳ������ ��ġ_����_ 1�� _2��
    
    public int playerPos_Room = 1;//ĳ������ ��ġ_room��

    public bool inLadder = false;
    bool isLadder = false; //��ٸ��� Ÿ�� �ִ��� �ƴ��� ����
    //====================================
    //������ �κ��丮
    public Inventory inventory;
    public GameObject _item;
    private bool isCilck = false;

    public GameObject _lock_And_Object_Item;
    public bool isUsingItem; //�������� ����ϰڴٴ� �ǹ�
    public bool isUsingObject;//�̺�Ʈ�� �Ͼ���ϴ� ������Ʈ�� ����ϰڴٴ��ǹ�.


    //====================================

    //��ó��.. ���� ������ ���̶��, z�� �����߸� �����̰�...
    public bool inCase = true;

    //==============================================
    //�������� Ȯ��
    public bool ishiding = false;
    //===================================
    //==================================
    Animator anim;
    //==================================================
    //������ ����..
    public Quest_Explanation quest_Explanation;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
         //�� ��ũ��Ʈ
         //enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
         Portal = enemyGanerator.curPortal;

        if (Portal != null)
        {
            portalScript = Portal.GetComponent<Portal>();
            Enemy = portalScript.CurEnemy;

            if (Enemy != null)
            {
                enemyController = Enemy.GetComponent<EnemyController>();
            }
        }

    }

    void Update()
    {
        if (Portal != null)
        {
            portalScript = Portal.GetComponent<Portal>();
            Enemy = portalScript.CurEnemy;

            if (Enemy != null)
            {
                enemyController = Enemy.GetComponent<EnemyController>();
            }
        }

        if (isLadder)
        {
            //���� ��ٸ��� Ÿ�� �ִٸ�...?
            float v = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0; //��ٸ��� Ÿ��������, �߷� ����
            rigid.velocity = new Vector2(rigid.velocity.x, v * movementSpeed);
            condiBar.GetComponent<ConditionBar>().currentHP += 0.3f;
            // Debug.Log(transform.position.y);


            inLadder = true; //��ٸ��� Ÿ�� ���̿���

        }
        else
        {

            rigid.gravityScale = 2f;
        }

        Move();
        if (!inCase)
        {
            quest_Explanation.Text_Case(0);//
            if (Input.GetButtonDown("Jump"))
            {
                //���� �����̽� �ٸ� ������, ������ �ȵ����� ���!.. ����!
                Jump(); //��ٸ��� Ÿ������ ���� ��, �߷� �ְ�
            }
        }
        //}

    }

    void Move()
    {
        if(!dontMove) 
        {
            Vector3 moveVelocity = Vector3.zero;
           // bool Dash = false; //������ �޸��� �ִ���..

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
                    anim.SetBool("player_Move", false);
                    anim.SetBool("player_Run", true);
                    movementSpeed = 8;
                    condiBar.GetComponent<ConditionBar>().currentHP -= 0.13f;
                }
                else
                {
                    movementSpeed = 4;
                    anim.SetBool("player_Move", true);
                    anim.SetBool("player_Run", false);
                    condiBar.GetComponent<ConditionBar>().currentHP += 0.3f;
                }
            }
            else if (condiZero) //true�϶�, �� ü���� �ٴ� ������.. ����
            {
                //������ϴ� �ִϸ��̼� �߰�
                anim.SetBool("player_Move", true);
                anim.SetBool("player_Run", false);
                movementSpeed = 2;//������ ���ǵ�..

                condiBar.GetComponent<ConditionBar>().currentHP += 0.3f;
            }




            if (!inCase)
            {
                transform.position += moveVelocity * movementSpeed * Time.deltaTime;
            }
            /*else if (inCase && Dash)
            {
                movementSpeed /= 2;
                transform.position += moveVelocity * movementSpeed * Time.deltaTime;
            }*/
            else if (inCase)
            {
                movementSpeed /= 4;
                transform.position += moveVelocity * movementSpeed * Time.deltaTime;
            }

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

    public int GetFloor()
    {
        //�÷��̾ ���ִ� �� ��ġ�� ��ȯ�Ѵ�.
        if(playerPos_Floor==1)
        {
            //1���� �ִٸ�..
            return 1;
        }
        else
        {
            //2���� �ִٸ�..
            return 2;
        }
    }
    public int GetRoom()
    {
        return playerPos_Room;
    }
    public GameObject GetLockAndObjectItem()
    {
        return _lock_And_Object_Item;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "1F_Floor")
        {
            playerPos_Floor = 1;
            Debug.Log("���� �� : " + playerPos_Floor);
            isJumping = false;
        }
        if (collision.transform.tag == "2F_Floor")
        {
            playerPos_Floor = 2;
            Debug.Log("���� �� : " + playerPos_Floor);
            isJumping = false;
        }
        if(collision.transform.tag == "canJump"|| collision.transform.tag == "key")
        {
            isJumping = false;
        }
        //���� �������ȿ� �÷��̾ �����ִٸ�..
        if (collision.gameObject.tag=="playerCase")
        {
            inCase = true;
        }
        if (collision.gameObject.tag == "GameClear")
        {
            Debug.Log("���� ��");
            gameDirector.GameClear();
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "lock")
        {
            //���� �ڹ��迡 �´�� �ִٸ�.
            _lock_And_Object_Item = collision.gameObject;
            if (Input.GetKey(KeyCode.C))
            {
                isUsingItem = true;
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                isUsingItem = false;
            }//�̰� ���� ���� �� ����..
        }
        if (collision.gameObject.tag == "object_Item")
        {
            //���� �̺�Ʈ�� ����Ǿ���ϴ� ������Ʈ �����ۿ� ����ִٸ�.. 
            _lock_And_Object_Item = collision.gameObject;
            if (Input.GetKey(KeyCode.C))
            {
                isUsingObject = true;
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                isUsingObject = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //���� �������ȿ� �÷��̾ ���´ٸ�..
        if (collision.gameObject.tag == "playerCase")
        {
            inCase = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("1F_Floor"))
        {
            playerPos_Floor = 1;
            Debug.Log("���� �� : " + playerPos_Floor);
            isJumping = false;
        }
        if (collision.CompareTag("2F_Floor"))
        {
            playerPos_Floor = 2;
            Debug.Log("���� �� : " + playerPos_Floor);
            isJumping = false;
        }

        if (collision.CompareTag("Ladder"))
        {
            //��ٸ��� �꿴����

  
            isLadder = true;
        }
        for(int i=1;i<11;i++)
        {
            //�÷��̾ �ִ� �� ��ġ �ľ�
            if (collision.CompareTag("room"+i.ToString()))
            {
               
                playerPos_Room = i;
                if(playerPos_Room == 5)
                {
                    //ù��° �������� ���ڸ� ������ ���..
                    quest_Explanation.Text_Case(7);
                }
                else
                {
                    quest_Explanation.ReMoveText_Case(7);

                }
            }
        }

        if (collision.CompareTag("end_Collider"))
        {
            //end�� ���� collider�� ��´ٸ�..?
            gameDirector.End_Scene();

            //�÷��̾� ü�� ���� ä���ֱ�
            condiBar.GetComponent<ConditionBar>().MaxHP();
        }
      //====================
        //��
        if (collision.CompareTag("Enemy"))
        {
            gameDirector.GameOver();

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            quest_Explanation.Text_Case(1);
            //��ٸ��� �꿴����
            isLadder = true;
        }

        if (collision.CompareTag("key"))
        {
            //���� �÷��̾�� ����ִ� Key���� shift�� ������.. �κ��丮�� ����
            _item = collision.gameObject;
            if(_item.GetComponent<Item>().itemName== "chair_Key00_Quest00")
            {
                //ù��° �������� ���ڸ� ������ ���..
                quest_Explanation.Text_Case(2);
            }




            if (Input.GetKey(KeyCode.C))
            {
                if(!isCilck)
                {
                    isCilck = true;

                    /*audioSource.clip = getItem_S;
                    audioSource.Play();*/
                    GameObject.Find("GetItem_S").GetComponent<AudioSource>().Play();

                    inventory.AddItem(_item.gameObject, _item.gameObject.GetComponent<Item>());
                    Invoke("isCilck_Return", 1); //1�ʵڿ� ���� �������� Ŭ�� �� �� ����.
                }
            }
        }
        if (collision.CompareTag("lock"))
        {
            //���� �ڹ��迡 �´�� �ִٸ�.
            _lock_And_Object_Item = collision.gameObject;

            if (_lock_And_Object_Item.GetComponent<Item>().itemName == "Key00_lock")
            {
                //ù��° �������� ���ڸ� ������ ���..
                quest_Explanation.Text_Case(3);
            }

            if (Input.GetKey(KeyCode.C))
            {
                isUsingItem = true;
            }
            if(Input.GetKeyUp(KeyCode.C))
            {
                isUsingItem = false;
            }//�̰� ���� ���� �� ����..
        }
        if (collision.CompareTag("object_Item"))
        {
            //���� �̺�Ʈ�� ����Ǿ���ϴ� ������Ʈ �����ۿ� ����ִٸ�.. 
            _lock_And_Object_Item = collision.gameObject;
            if (Input.GetKey(KeyCode.C))
            {
                isUsingObject = true;
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                isUsingObject = false;
            }
        }
        //
        if (collision.CompareTag("Hide"))
        {

            quest_Explanation.Text_Case(8);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //��������?
                //��¦ ���帰 �ִϸ��̼� �߰�
                sr.color = new Color(0.55f, 0.5f, 0.5f, 0.7f);
                if(Enemy!=null)
                {
                    if (enemyController.SameRoom)
                    {
                        //���� ���� �濡 enemy�� �ִٸ�, ishiding����
                        ishiding = false;
                    }
                    else
                    {
                        //���� ���� �濡 enemy�� ���ٸ�, ishiding����
                        ishiding = true;
                    }
                }
               else
                {
                    //���� ���� �濡 enemy�� ���ٸ�, ishiding����
                    ishiding = true;
                }
            }
        }

        //Quest01
        if (collision.CompareTag("Hint01_Quest01"))
        {
            Debug.Log(collision.tag);
            //ù��° ��Ʈ�� �������
            quest_Explanation.Text_Case(4);

            questManager.Hint01_Quest01 = true;
 
        }
        if (collision.CompareTag("Color_Quest01"))
        {
            //�� ������ ���� ���
            quest_Explanation.Text_Case(5);
            questManager.color_Quest01 = true;
        }
        //Quest02
        if (collision.CompareTag("Hint02_Quest02"))
        {
            //�ι�° ��Ʈ�� �������
            quest_Explanation.Text_Case(6);
            //��Ʈ ����
            questManager.Hint02_Quest02 = true;
        }
            
        //������ ����
        if (collision.CompareTag("life"))
        {
            //���� ������..
            gameDirector.curLife = collision.gameObject;
            gameDirector.GetLife = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }

        if (collision.CompareTag("lock"))
        {
            //���� �ڹ��迡�� ����ٸ�...
            _lock_And_Object_Item = null;
            isUsingItem = false;
        }
        if (collision.CompareTag("object_Item"))
        {
            //���� (�̺�Ʈ�� �Ͼ)������Ʈ���� ����ٸ�...
            _lock_And_Object_Item = null;
            isUsingObject = false;
        }
        if (collision.CompareTag("Hide"))
        {
            quest_Explanation.ReMoveText_Case(8);
            sr.color = new Color(1, 1, 1, 1);
            ishiding = false;
        }

        //Quest01======================================
        if (collision.CompareTag("Hint01_Quest01"))
        {
            quest_Explanation.ReMoveText_Case(4);
            questManager.Hint01_Quest01 = false;

        }
        if (collision.CompareTag("Color_Quest01"))
        {
            quest_Explanation.ReMoveText_Case(5);
            questManager.color_Quest01 = false;
        }

        //Quest02======================================
        if (collision.CompareTag("Hint02_Quest02"))
        {
            quest_Explanation.ReMoveText_Case(6);
            //��Ʈ ����
            questManager.Hint02_Quest02 = false;
        }
        //������ ����======================================
        if (collision.CompareTag("life"))
        {
            //������ �������Լ� ��������.
            gameDirector.curLife = null;
            gameDirector.GetLife = false;
        }
    }

    void isCilck_Return()
    {
        isCilck = false;
    }
}
