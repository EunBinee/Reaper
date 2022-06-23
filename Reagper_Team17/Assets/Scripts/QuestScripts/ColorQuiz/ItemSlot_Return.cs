using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot_Return : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //�������� ���Կ��� ������ ����� ���
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //���� �� �������� null�� �ƴѰ��
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().other_rectTransform.anchoredPosition;
        }
    }
}
