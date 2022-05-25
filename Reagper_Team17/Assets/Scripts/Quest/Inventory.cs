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

    public bool AddItem( GameObject _itemObject, Item _item)
    {
 
        if (item.Count < slot_size)
        {
            //���� �ƹ� �͵� �ȵ���ִٸ�..
            item.Add(_item);
            item_Object.Add(_itemObject);

            SlotItem_Img.sprite = item_Object[0].GetComponent<SpriteRenderer>().sprite; // �κ��丮�� ��������Ʈ �̹��� �ٲٱ�

            item_Object[0].SetActive(false);

            Debug.Log(_itemObject.name + " ����");

            return false; //�κ��丮�� ó������ �ƹ��͵� �����⿡.. ���� �������� ���� ���..
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

            return true; //�κ��丮�� ���� �������� �־.. ������ �� ���..
        }
            
    }
}
