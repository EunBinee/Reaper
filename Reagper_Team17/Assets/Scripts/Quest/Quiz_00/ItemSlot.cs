using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject ItemInSlot;
    private void Start()
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        //�������� ���Կ��� ������ ����� ���
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            ItemInSlot = eventData.pointerDrag;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

}
