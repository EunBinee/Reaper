using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void OpenQuest()
    {
        switch(match_Pair)
        {
            case 0:
                Debug.Log("0퀘스트입니다.");
                inventory.Destroy_item();
                keys[0].SetActive(true);
                break;
            default:
                Debug.Log("1퀘스트입니다.");

                break;
        }
    }
    
}
