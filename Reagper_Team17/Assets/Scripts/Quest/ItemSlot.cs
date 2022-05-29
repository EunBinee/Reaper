using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    //Quiz1
    GameObject ItemInSlot;
    public void OnDrop(PointerEventData eventData)
    {
        //�������� ���Կ��� ������ ����� ���
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //���� �� �������� null�� �ƴѰ��
            ItemInSlot = eventData.pointerDrag.GetComponent<GameObject>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public GameObject GetItem()
    {
        return ItemInSlot;
    }
}
