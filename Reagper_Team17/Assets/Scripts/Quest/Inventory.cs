using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    // 플레이어가 가진 Item의 정보를 보여주고,
    // 정보를 다른 스크립트에서 받아쓸 수 있도록 관리하는 곳.
    
    public List<Item> item;
    public List<GameObject> item_Object;
    public GameObject preItem; //인벤토리에 담겨져있던 이전 아이템의 종류

    private int slot_size=1;
    //==========================
    public Image SlotItem_Img;
    //============================

    GameObject usedKey;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (item.Count < slot_size)
        {
            //만약 가지고 있는 아이템이 없으면
            SlotItem_Img.sprite = null; // 아무 스프라이트도 UI에 띄우지 마셔용

        }
    }

    public void AddItem(GameObject _itemObject, Item _item)
    {
        if (_item.notMoving == false)
        {
            //움직이는 아이템일 경우에만.. 진행
            if (item.Count < slot_size)
            {
                //만약 아무 것도 안들어있다면..
                item.Add(_item);
                item_Object.Add(_itemObject);

                SlotItem_Img.sprite = item_Object[0].GetComponent<SpriteRenderer>().sprite; // 인벤토리에 스프라이트 이미지 바꾸기

                item_Object[0].SetActive(false);

                Debug.Log(_itemObject.name + " 넣음");

                 //인벤토리에 처음부터 아무것도 없었기에.. 이전 아이템이 없는 경우..
            }
            else
            {

                //UI할 건지 아닌지 확인 UI

                //가득 찼을 때, 안의 아이템을 버리고 새로운 아이템을 가지고 온다.
                preItem = item_Object[0];

                item.Remove(item[0]);//기존의 아이템 없앰
                item_Object.Remove(item_Object[0]);

                item.Add(_item);
                item_Object.Add(_itemObject);

                //====================================
                //인벤토리 UI 스프라이트 부분            
                SlotItem_Img.sprite = item_Object[0].GetComponent<SpriteRenderer>().sprite;
                //====================================

                preItem.SetActive(true);
                preItem.transform.position = item_Object[0].transform.position;

                item_Object[0].SetActive(false);

                Debug.Log(preItem + " 빼고 " + _itemObject.name + " 넣음");

                //인벤토리에 이전 아이템이 있어서.. 버려야 할 경우..
            }
        }
    }

    public GameObject GetInventoryItem()
    {
        if (item.Count < slot_size)
        {
            //만약 인벤토리 슬롯에 아무 것도 안들어가있을 경우
            return null;
        }
        else
            return item_Object[0];
    }

    public void Destroy_item()
    {
        usedKey = item_Object[0];
        item_Object.Remove(item_Object[0]);
        item.Remove(item[0]);
        preItem = null;

        Debug.Log("삭제 완료");
        Destroy(usedKey);
    }

    public void Destory_onlyList()
    {
        item_Object.Remove(item_Object[0]);
        item.Remove(item[0]);
        preItem = null;
        Debug.Log("삭제 완료_onlyList");
    }
}
