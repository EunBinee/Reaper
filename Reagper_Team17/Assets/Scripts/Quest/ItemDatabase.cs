using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    //���ӿ��� ���� Item���� data�� �����ϰ� �����մϴ�.

    public static ItemDatabase instance;
    //�ٸ� ��ũ��Ʈ���� �� ��ũ��Ʈ�� ���� ������ �� �ֵ���.. static

    private void Awake()
    {
        instance = this;
    }
    public List<GameObject> itemDB = new List<GameObject>();
}
