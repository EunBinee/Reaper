using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �÷��̾ ���� Item�� ������ �����ְ�,
    // ������ �ٸ� ��ũ��Ʈ���� �޾ƾ� �� �ֵ��� �����ϴ� ��.

    public List<Item> item;
    public List<GameObject> item_Object;
    public GameObject preItem; //�κ��丮�� ������ִ� ���� �������� ����

    private int slot_size=1;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem( GameObject _itemObject, Item _item)
    {

        if (item.Count < slot_size)
        {
            //���� �ƹ� �͵� �ȵ���ִٸ�..
            item.Add(_item);
            item_Object.Add(_itemObject);

            Debug.Log(_itemObject.name + " ����");

            return false; //�κ��丮�� ó������ �ƹ��͵� �����⿡.. ���� �������� ���� ���..
        }
        else
        {
            //���� á�� ��, ���� �������� ������ ���ο� �������� ������ �´�.
            preItem = item_Object[0];

            item.Remove(item[0]);//������ ������ ����
            item_Object.Remove(item_Object[0]);

            item.Add(_item);
            item_Object.Add(_itemObject);

            Debug.Log(preItem + " ���� " + _itemObject.name + " ����");

            return true; //�κ��丮�� ���� �������� �־.. ������ �� ���..
        }
            
    }
}
