using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //게임에서 쓰일 Item들의 data를 정리하고 관리합니다.

    public static ItemDatabase instance;
    //다른 스크립트에서 이 스크립트에 쉽게 접근할 수 있도록.. static

    private void Awake()
    {
        instance = this;
    }
    public List<GameObject> itemDB = new List<GameObject>();
}
