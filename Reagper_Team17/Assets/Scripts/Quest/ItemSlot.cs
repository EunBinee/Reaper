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
        //아이템을 슬롯에다 가져다 드랍한 경우
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //만약 그 아이템이 null이 아닌경우
            ItemInSlot = eventData.pointerDrag.GetComponent<GameObject>();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public GameObject GetItem()
    {
        return ItemInSlot;
    }
}
