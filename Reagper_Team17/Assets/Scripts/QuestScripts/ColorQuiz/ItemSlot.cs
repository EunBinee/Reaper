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
            GameObject.Find("Quest_01_Click").GetComponent<AudioSource>().Play();
            ItemInSlot = eventData.pointerDrag;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        
            if(item_DragDrop.color== ItemInSlot.GetComponent<Item_DragDrop>().color)
            {
                //���� ¦�� �´� ���
                ItemInSlot.GetComponent<Item_DragDrop>().Matching = true;
            }
        
        }
    }

}