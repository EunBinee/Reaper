using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot_Return : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //아이템을 슬롯에다 가져다 드랍한 경우
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //만약 그 아이템이 null이 아닌경우
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().other_rectTransform.anchoredPosition;
        }
    }
}
