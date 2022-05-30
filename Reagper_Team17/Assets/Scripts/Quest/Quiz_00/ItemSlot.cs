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
        //자기 자신의 아이템
    }
    public void OnDrop(PointerEventData eventData)
    {
        //아이템을 슬롯에다 가져다 드랍한 경우
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            ItemInSlot = eventData.pointerDrag;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        
            if(item_DragDrop.color== ItemInSlot.GetComponent<Item_DragDrop>().color)
            {
                //만약 짝이 맞는 경우
                ItemInSlot.GetComponent<Item_DragDrop>().Matching = true;
            }
        
        }
    }

}
