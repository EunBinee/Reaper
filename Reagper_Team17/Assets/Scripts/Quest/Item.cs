using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //Key는 사용자가 가지고 사용하기위해 인벤토리에 들고다니는 물건
    //Lock 그 Key를 사용할 수 있는 아이템
    Key,
    Lock,
    Object
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType itemType; //아이템의 타입.. 자물쇠인지, 열쇠인지
    public string itemName; //아이템의 이름
    public Sprite itemImage; //아이템의 이미지

    public int pair; //Lock과 Key의 pair가 맞을 때, 사용 가능.

    public bool notMoving;//화면상에 존재하지만, 이제 아이템으로써 움직이지않겠다는 의미
    public bool haveEventAsObject;
    public bool Use()
    {
        //아이템 사용의 성공 여부를 반환하기 위해..
        return false;
    }
}
