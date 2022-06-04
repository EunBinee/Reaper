using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject ItemInSlot;
    Item_DragDrop item_DragDrop;
    private void Start()
    {
        item_DragDrop = GetComponent<Item_DragDrop>();
        //�ڱ� �ڽ��� ������
    }
    public void OnDrop(PointerEventData eventData)
    {
        //�������� ���Կ��� ������ ����� ���
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            ItemInSlot = eventData.pointerDrag;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        
            if(item_DragDrop.color== ItemInSlot.GetComponent<Item_DragDrop>().color)
            {
                //���� ¦�� �´� ���
                ItemInSlot.GetComponent<Item_DragDrop>().Matching = true;
            }
        
        }
    }

}
