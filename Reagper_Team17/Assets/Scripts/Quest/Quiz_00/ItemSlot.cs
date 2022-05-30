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
        //아이템을 슬롯에다 가져다 드랍한 경우
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            ItemInSlot = eventData.pointerDrag;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

}
