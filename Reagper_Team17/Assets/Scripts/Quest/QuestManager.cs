using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public Inventory inventory;
    public PlayerController playerController;
    //현재 인벤토리에 있는 열쇠의 정보 가지고 오기
    GameObject curInventor_Item;
    //현재 플레이어가 맞닿아있는 Lock자물쇠의 정보.
    GameObject curLockItem;
    bool usingItem;

    //item 스크립트를 받아올것
    Item KeyItem;
    Item LockItem;

    //맞는 짝이 있다면.. 현재 퀘스트의 정보값, 자물쇠와 키의 pair값
    int match_Pair = -1;


    //퀘스트에 사용될 열쇠들~~
    public GameObject[] keys;

    public Transform[] Locks;
    //Quest 01 번
    //색퍼즐
    public bool color_Quest01 = false;
    public GameObject color_Quest01_room01;

    public GameObject[] colorBall;
    public int trueNum=0;
    //색퍼즐의 힌트 보기
    public bool Hint01_Quest01 = false;
    public GameObject hint01_Room01;


    void Start()
    {
        curInventor_Item = inventory.GetInventoryItem();
        curLockItem = playerController.GetLockItem();
    }
    void Update()
    {
        usingItem = playerController.isUsing;
        if(usingItem)
        {
            //만약 사용한다고 됐을 경우..
            curInventor_Item = inventory.GetInventoryItem();
            curLockItem = playerController.GetLockItem();
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

        }


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
                    hint01_Room01.SetActive(true);
                }
                else
                {
                    //true면
                    playerController.dontMove = false;
                    hint01_Room01.SetActive(false);
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

    }

    void OpenQuest()
    {
        switch(match_Pair)
        {
            case 0:
                Debug.Log("0퀘스트입니다.");
                keys[0].transform.position = Locks[0].position;
         
                inventory.Destory_onlyList();

                //화면상에 존재하지만, 이제 아이템으로써 움직이지않겠다는 의미
                keys[0].GetComponent<Item>().notMoving = true;

                keys[0].SetActive(true);
                break;
            case 1:
                Debug.Log("1퀘스트입니다.");
                
                break;
            default:

                break;
        }
    }
    

    //색 퍼즐 메소드-----------------------------------------------
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
}
