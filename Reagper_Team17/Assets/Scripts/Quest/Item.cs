using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //Key�� ����ڰ� ������ ����ϱ����� �κ��丮�� ���ٴϴ� ����
    //Lock �� Key�� ����� �� �ִ� ������
    Key,
    Lock,
    Object
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType itemType; //�������� Ÿ��.. �ڹ�������, ��������
    public string itemName; //�������� �̸�
    public Sprite itemImage; //�������� �̹���

    public int pair; //Lock�� Key�� pair�� ���� ��, ��� ����.

    public bool notMoving;//ȭ��� ����������, ���� ���������ν� ���������ʰڴٴ� �ǹ�
    public bool haveEventAsObject;
    public bool Use()
    {
        //������ ����� ���� ���θ� ��ȯ�ϱ� ����..
        return false;
    }
}
