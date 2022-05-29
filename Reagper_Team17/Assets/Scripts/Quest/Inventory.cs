using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    // �÷��̾ ���� Item�� ������ �����ְ�,
    // ������ �ٸ� ��ũ��Ʈ���� �޾ƾ� �� �ֵ��� �����ϴ� ��.
    
    public List<Item> item;
    public List<GameObject> item_Object;
    public GameObject preItem; //�κ��丮�� ������ִ� ���� �������� ����

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
            //���� ������ �ִ� �������� ������
            SlotItem_Img.sprite = null; // �ƹ� ��������Ʈ�� UI�� ����� ���ſ�

        }
    }

    public void AddItem(GameObject _itemObject, Item _item)
    {
        if (_item.notMoving == false)
        {
            //�����̴� �������� ��쿡��.. ����
            if (item.Count < slot_size)
            {
                //���� �ƹ� �͵� �ȵ���ִٸ�..
                item.Add(_item);
                item_Object.Add(_itemObject);

                SlotItem_Img.sprite = item_Object[0].GetComponent<SpriteRenderer>().sprite; // �κ��丮�� ��������Ʈ �̹��� �ٲٱ�

                item_Object[0].SetActive(false);

                Debug.Log(_itemObject.name + " ����");

                 //�κ��丮�� ó������ �ƹ��͵� �����⿡.. ���� �������� ���� ���..
            }
            else
            {

                //UI�� ���� �ƴ��� Ȯ�� UI

                //���� á�� ��, ���� �������� ������ ���ο� �������� ������ �´�.
                preItem = item_Object[0];

                item.Remove(item[0]);//������ ������ ����
                item_Object.Remove(item_Object[0]);

                item.Add(_item);
                item_Object.Add(_itemObject);

                //====================================
                //�κ��丮 UI ��������Ʈ �κ�            
                SlotItem_Img.sprite = item_Object[0].GetComponent<SpriteRenderer>().sprite;
                //====================================

                preItem.SetActive(true);
                preItem.transform.position = item_Object[0].transform.position;

                item_Object[0].SetActive(false);

                Debug.Log(preItem + " ���� " + _itemObject.name + " ����");

                //�κ��丮�� ���� �������� �־.. ������ �� ���..
            }
        }
    }

    public GameObject GetInventoryItem()
    {
        if (item.Count < slot_size)
        {
            //���� �κ��丮 ���Կ� �ƹ� �͵� �ȵ����� ���
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

        Debug.Log("���� �Ϸ�");
        Destroy(usedKey);
    }

    public void Destory_onlyList()
    {
        item_Object.Remove(item_Object[0]);
        item.Remove(item[0]);
        preItem = null;
        Debug.Log("���� �Ϸ�_onlyList");
    }
}
