using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sr;

    EnemyGanerator enemyGanerator;
    Portal portalScript;
    GameObject Enemy;
    EnemyController enemyController;

    public  QuestManager questManager;
    public GameDirector gameDirector;
    //움직임을 멈추는 변수
    public bool dontMove = false;
    //=======================
    public Vector3 moveVelocity;
    public float movementSpeed = 3.0f;
    public float jumpPower = 13f;
    public GameObject condiBar; //캐릭터의 체력바를 위한 선언
    public bool condiZero = false; //컨디션BAr.. 너무 달려서 체력이 0이 됨.
    public  bool isJumping = false; //캐릭터가 점프를 하고있는지 아닌지..
    public int playerPos_Floor = 1;//캐릭터의 위치_층별_ 1층 _2층
    
    public int playerPos_Room = 0;//캐릭터의 위치_room별

    public bool inLadder = false;
    bool isLadder = false; //사다리를 타고 있는지 아닌지 여부
    //====================================
    //아이템 인벤토리
    public Inventory inventory;
    public GameObject _item;
    private bool isCilck = false;

    public GameObject _lock_And_Object_Item;
    public bool isUsingItem; //아이템을 사용하겠다는 의미
    public bool isUsingObject;//이벤트가 일어나야하는 오브젝트를 사용하겠다는의미.


    //====================================

    //맨처음.. 만약 케이지 안이라면, z를 눌러야만 움직이게...
    public bool inCase = true;

    //==============================================
    //숨었는지 확인
    public bool ishiding = false;
    //===================================
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //적 스크립트
        //enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
        portalScript = GameObject.Find("Portal").GetComponent<Portal>();
        //Enemy = enemyGanerator.CurEnemy;
        Enemy = portalScript.CurEnemy;
        if (Enemy != null)
        {
            enemyController = Enemy.GetComponent<EnemyController>();
        }
    }

    void Update()
    {
        // Enemy = enemyGanerator.CurEnemy;
        Enemy = portalScript.CurEnemy;
        if (Enemy!=null)
        {
            enemyController = Enemy.GetComponent<EnemyController>();
        }

        if (isLadder && Input.GetKey(KeyCode.X))
        {
            //만약 사다리를 타고 있다면...?
            float v = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0; //사다리를 타고있을땐, 중력 없게
            rigid.velocity = new Vector2(rigid.velocity.x, v * movementSpeed);
            condiBar.GetComponent<ConditionBar>().currentHP += 0.3f;
            Debug.Log(transform.position.y);


            inLadder = true; //사다리를 타는 중이예여

        }
        else
        {
            inLadder = false;
            Move();
            if (!inCase)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    //만약 스페이스 바를 눌렀고, 점프가 안되있을 경우!.. 점프!
                    Jump(); //사다리를 타고있지 않을 땐, 중력 있게
                }
            }
            rigid.gravityScale = 2f;
        }

    }
    private void FixedUpdate()
    {
    }
    void Move()
    {
        if(!dontMove) 
        {
            Vector3 moveVelocity = Vector3.zero;
           // bool Dash = false; //빠르게 달리고 있는지..

            // left
            if (Input.GetAxisRaw("Horizontal") < 0)  //왼쪽
            {
                moveVelocity = Vector3.left;

                sr.flipX = true;
            }
            // right
            else if (Input.GetAxisRaw("Horizontal") > 0) //오른쪽
            {
                moveVelocity = Vector3.right;

                sr.flipX = false;
            }



            if (!condiZero)//false일때, 즉 체력이 바닥 나지않았을때.. 실행
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    //Dash = true;
                    movementSpeed = 8;
                    //condiBar.GetComponent<ConditionBar>().currentHP -= 0.3f;
                }
                else
                {
                    //Dash = false;
                    movementSpeed = 4;

                    condiBar.GetComponent<ConditionBar>().currentHP += 0.3f;
                }
            }
            else if (condiZero) //true일때, 즉 체력이 바닥 났을때.. 실행
            {
                //힘들어하는 애니메이션 추가
                movementSpeed = 2;//느려진 스피드..

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
            //만약 점프를 하는 중이면.. true;
            return;
        }

        rigid.velocity = Vector3.zero;

        Vector3 jumpVelocity = new Vector3(0, jumpPower, 0);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = true;

    }

    public int GetFloor()
    {
        //플레이어가 서있는 층 위치를 반환한다.
        if(playerPos_Floor==1)
        {
            //1층에 있다면..
            return 1;
        }
        else
        {
            //2층에 있다면..
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
            Debug.Log("현재 층 : " + playerPos_Floor);
            isJumping = false;
        }
        if (collision.transform.tag == "2F_Floor")
        {
            playerPos_Floor = 2;
            Debug.Log("현재 층 : " + playerPos_Floor);
            isJumping = false;
        }
        if(collision.transform.tag == "canJump"|| collision.transform.tag == "key")
        {
            isJumping = false;
        }
        //만약 케이지안에 플레이어가 갇혀있다면..
        if (collision.gameObject.tag=="playerCase")
        {
            inCase = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "lock")
        {
            //만약 자물쇠에 맞닿아 있다면.
            _lock_And_Object_Item = collision.gameObject;
            if (Input.GetKey(KeyCode.C))
            {
                isUsingItem = true;
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                isUsingItem = false;
            }//이거 만약 수정 될 수도..
        }
        if (collision.gameObject.tag == "object_Item")
        {
            //만약 이벤트가 실행되어야하는 오브젝트 아이템에 닿아있다면.. 
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
        //만약 케이지안에 플레이어가 나온다면..
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
            Debug.Log("현재 층 : " + playerPos_Floor);
            isJumping = false;
        }
        if (collision.CompareTag("2F_Floor"))
        {
            playerPos_Floor = 2;
            Debug.Log("현재 층 : " + playerPos_Floor);
            isJumping = false;
        }

        if (collision.CompareTag("Ladder"))
        {
            //사다리에 닿였는지
            isLadder = true;
        }
        for(int i=1;i<11;i++)
        {
            //플레이어가 있는 방 위치 파악
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
            //만약 플레이어와 닿아있는 Key에서 shift를 누르면.. 인벤토리에 저장
            _item = collision.gameObject;
            
            if (Input.GetKey(KeyCode.C))
            {
                if(!isCilck)
                {
                    isCilck = true;
                    inventory.AddItem(_item.gameObject, _item.gameObject.GetComponent<Item>());
                    Invoke("isCilck_Return", 1); //1초뒤에 이제 아이템을 클릭 할 수 있음.
                }
            }
        }
        if (collision.CompareTag("lock"))
        {
            //만약 자물쇠에 맞닿아 있다면.
            _lock_And_Object_Item = collision.gameObject;
            if(Input.GetKey(KeyCode.C))
            {
                isUsingItem = true;
            }
            if(Input.GetKeyUp(KeyCode.C))
            {
                isUsingItem = false;
            }//이거 만약 수정 될 수도..
        }
        if (collision.CompareTag("object_Item"))
        {
            //만약 이벤트가 실행되어야하는 오브젝트 아이템에 닿아있다면.. 
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
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //숨었나요?
                //납짝 업드린 애니메이션 추가
                sr.color = new Color(0.55f, 0.5f, 0.5f, 0.7f);
                if(Enemy!=null)
                {
                    if (enemyController.SameRoom)
                    {
                        //만약 같은 방에 enemy가 있다면, ishiding실패
                        ishiding = false;
                    }
                    else
                    {
                        //만약 같은 방에 enemy가 없다면, ishiding성공
                        ishiding = true;
                    }
                }
               else
                {
                    //만약 같은 방에 enemy가 없다면, ishiding성공
                    ishiding = true;
                }
            }
        }

        //Quest01
        if (collision.CompareTag("Hint01_Quest01"))
        {
            Debug.Log(collision.tag);
            questManager.Hint01_Quest01 = true;
 
        }
        if (collision.CompareTag("Color_Quest01"))
        {
            questManager.color_Quest01 = true;
        }
        //Quest02
        if (collision.CompareTag("Hint02_Quest02"))
        {
            //힌트 보기
            questManager.Hint02_Quest02 = true;
        }
            
        //생명의 조각
        if (collision.CompareTag("life"))
        {
            //만약 닿으면..
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
            //만약 자물쇠에서 벗어난다면...
            _lock_And_Object_Item = null;
            isUsingItem = false;
        }
        if (collision.CompareTag("object_Item"))
        {
            //만약 (이벤트가 일어날)오브젝트에서 벗어난다면...
            _lock_And_Object_Item = null;
            isUsingObject = false;
        }
        if (collision.CompareTag("Hide"))
        {
            sr.color = new Color(1, 1, 1, 1);
            ishiding = false;
        }

        //Quest01======================================
        if (collision.CompareTag("Hint01_Quest01"))
        {
            questManager.Hint01_Quest01 = false;

        }
        if (collision.CompareTag("Color_Quest01"))
        {
            questManager.color_Quest01 = false;
        }

        //Quest02======================================
        if (collision.CompareTag("Hint02_Quest02"))
        {
            //힌트 보기
            questManager.Hint02_Quest02 = false;
        }
        //생명의 조각======================================
        if (collision.CompareTag("life"))
        {
            //생명의 조각에게서 떨어지면.
            gameDirector.curLife = null;
            gameDirector.GetLife = false;
        }
    }

    void isCilck_Return()
    {
        isCilck = false;
    }
}
