using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public Inventory inventory;
    public PlayerController playerController;
    public GameObject player;

    //현재 인벤토리에 있는 열쇠의 정보 가지고 오기
    [SerializeField] private GameObject curInventor_Item;
    //현재 플레이어가 맞닿아있는 Lock자물쇠의 정보.
    [SerializeField] private GameObject curLockItem;
    //현재 플레이어가 맞닿아있는 (이벤트를 위한)오브젝트의 정보.
    [SerializeField] private GameObject curObject;

    bool usingItem;
    bool usingObject;
    //item 스크립트를 받아올것
    Item KeyItem;
    Item LockItem;
    Item ObjectItem;

    //맞는 짝이 있다면.. 현재 퀘스트의 정보값, 자물쇠와 키의 pair값
    int match_Pair = -1;

    //퀘스트에 사용될 열쇠들~~
    public GameObject[] keys;

    public Transform[] Locks_And_Object; //암튼 위치값 저장해야 할 경우.. 사용
    public GameObject[] changeTag; //이벤트 발생시 태그값을 바꿔야 하는 경우 사용.

    //Quest 01 번==========================================================================
    //색퍼즐
    //색퍼즐
    public bool color_Quest01 = false;
    public GameObject color_Quest01_room01; //색퀴즈 UI 오브젝트

    public GameObject[] colorBall;
    public int trueNum=0;
    //색퍼즐의 힌트 보기
    public bool Hint01_Quest01 = false;
    public GameObject hint01_UI;
    //Quest 02 번==========================================================================
    //Hint02
    public bool Hint02_Quest02 = false;
    public GameObject hint02; //화면에 나오는 hint
    public GameObject hint02_UI;

    //========================
    //쿨타임 //오브젝트 사용시
    float time;
    float maxTime = 1f;
    void Start()
    {

        curInventor_Item = inventory.GetInventoryItem();
        curLockItem = playerController.GetLockAndObjectItem();
    }
    void Update()
    {
        usingItem = playerController.isUsingItem;
        usingObject = playerController.isUsingObject;

        time += Time.deltaTime;

        if (usingItem)
        {
            //만약 사용한다고 됐을 경우..
            curInventor_Item = inventory.GetInventoryItem();
            curLockItem = playerController.GetLockAndObjectItem();
            //다시 받아온다!!
            if (curInventor_Item != null && curLockItem != null) 
            {
                //인벤토리 아이템과, 락 아이템 둘다 안에 무언가 있을 때 발동
                KeyItem = curInventor_Item.GetComponent<Item>();
                LockItem = curLockItem.GetComponent<Item>();

                if(KeyItem.pair==LockItem.pair)
                {
                    //만약 Key아이템과 Lock아이템의 pair가 맞다면...
                    if(match_Pair != KeyItem.pair)
                    {
                        //mathcpair와 키아이템의 짝이 똑같지않다면..
                        match_Pair = KeyItem.pair;
                        OpenQuest();
                    }
                    
                }
            }

        } //아이템을 사용하기위한..

        if(usingObject)
        {

            playerController.isUsingObject = false;

            curObject = playerController.GetLockAndObjectItem();
            ObjectItem = curObject.GetComponent<Item>();

            if(time>maxTime)
            {
                UseObject();
                time = 0;
            }
        }

        //Quest01======================================================
        if (Hint01_Quest01) 
        {
            //힌트 1일 볼때
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //위방향 버튼 누르면.. 힌트 보여주기
                if (!playerController.dontMove)
                {
                    //만약 dontMove가 false면 hint보여주고
                    playerController.dontMove = true;
                    hint01_UI.SetActive(true);
                }
                else
                {
                    //true면
                    playerController.dontMove = false;
                    hint01_UI.SetActive(false);
                }
            }
        } //색 힌트

        if (color_Quest01 && trueNum < colorBall.Length)
        {
            //컬러 퀘스트 UI
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //위방향 버튼 누르면.. 힌트 보여주기
                if (!playerController.dontMove)
                {
                    //만약 dontMove가 false면 hint보여주고
                    playerController.dontMove = true;
                    color_Quest01_room01.SetActive(true);
                }
            }
        } //색 퍼즐
        //Quest02======================================================
        if (Hint02_Quest02) //Quest02 힌트 보기
        {
            //힌트 1일 볼때
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //위방향 버튼 누르면.. 힌트 보여주기
                if (!playerController.dontMove)
                {
                    //만약 dontMove가 false면 hint보여주고
                    playerController.dontMove = true;
                    hint02_UI.SetActive(true);
                }
                else
                {
                    //true면
                    playerController.dontMove = false;
                    hint02_UI.SetActive(false);
                }
            }
        }//Quest02 힌트 보기
    }

    void OpenQuest()
    {
        switch(match_Pair)
        {
            case 0:
                Debug.Log("0퀘스트입니다.");
                keys[0].transform.position = Locks_And_Object[0].position;
                
                inventory.Destory_onlyList();

                //화면상에 존재하지만, 이제 아이템으로써 움직이지않겠다는 의미
                keys[0].GetComponent<Item>().notMoving = true;

                //**필수 !!
                playerController.isUsingItem = false;

                keys[0].SetActive(true);
                break;
            case 1:
                Debug.Log("1퀘스트입니다.");
                //문의 잠금을 연다.
                //스프라이트를 바꾼다.(지금은 색을 바꾸는 것으로 대신함)
                SpriteRenderer sp = curLockItem.GetComponent<SpriteRenderer>();
                sp.color = new Color(0.3f, 0.5f, 0.5f, 1);
                inventory.Destroy_item();//인벤토리에 있는 Key지우기
                curLockItem.tag = "object_Item"; //문의 태그를 Lock>>object_Item으로 변경;

                //**필수 !!
                playerController.isUsingItem = false;

                break;
            default:

                break;
        }
    } //퀘스트 아이템을 조정한다.

    void UseObject()
    {
        //오브젝트 (문 등) 사용!
        if(ObjectItem.itemName== "Key01_lock_And_Door01_Quest01")
        {
            //ObjectItem.itemName은 현재 오브젝트의 Item 스크립트를 받고있다.
            //Quest1번 문이라면..
            float ObectPosX = Locks_And_Object[2].position.x;

            player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);

            Debug.Log("Key01_lock_And_Door01_Quest01 오브젝트 이벤트 실행!!");
        }
        if (ObjectItem.itemName == "Door02_Quest01")
        {
            float ObectPosX = Locks_And_Object[1].position.x;

            player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);


            Debug.Log("Door02_Quest01 오브젝트 이벤트 실행!!");
        }

    }
    

    //Quest 01 색 퍼즐 메소드-----------------------------------------------
    public void Finish_Quest01_B()
    {
        //색깔 퍼즐 다 맞추고 다했을 때 누르는 버튼
        for (int i = 0; i < colorBall.Length; i++) 
        {
            if (colorBall[i].GetComponent<Item_DragDrop>().Matching == true) 
            {
                trueNum++;
            }
        }
        if (trueNum >= colorBall.Length) 
        {
            //만약에 Matching이 전부 되어있으면..
            //열쇠주기

            Debug.Log(trueNum);
            Debug.Log(colorBall.Length);
            Debug.Log("열쇠 줌~");

            keys[1].SetActive(true);

            Exit_Quest01_B();
        }
        else
        {
            //초기화
            trueNum = 0;
            Debug.Log("초기화!");
            Replace_Quest01();
        }
    }
    public void Replace_Quest01_B()
    {
        //초기화 버튼
        Replace_Quest01();
    }
    public void Exit_Quest01_B()
    {
        //초기화
        Replace_Quest01();
        
        playerController.dontMove = false;
        color_Quest01_room01.SetActive(false);//UI닫기

    }
    void Replace_Quest01()
    {
        //색깔 퍼즐 다시 시작하기
        for (int i = 0; i < colorBall.Length; i++)
        {
            colorBall[i].GetComponent<DragDrop>().Replace();  
        }
    }
    //=====================================================================
    //Quest 02 보물(열쇠)찾기
    public void Start_Quest02()
    {
        //지도가 생기도록
        hint02.SetActive(true);
        changeTag[0].tag = "key"; //2층 액자tag를 key로 
    }


}
