using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Inventory inventory;
    public PlayerController playerController;
    //���� �κ��丮�� �ִ� ������ ���� ������ ����
    GameObject curInventor_Item;
    //���� �÷��̾ �´���ִ� Lock�ڹ����� ����.
    GameObject curLockItem;
    bool usingItem;

    //item ��ũ��Ʈ�� �޾ƿð�
    Item KeyItem;
    Item LockItem;

    //�´� ¦�� �ִٸ�.. ���� ����Ʈ�� ������, �ڹ���� Ű�� pair��
    int match_Pair = -1;


    //����Ʈ�� ���� �����~~
    public GameObject[] keys;


    void Start()
    {
        curInventor_Item = inventory.GetInventoryItem();
        curLockItem = playerController.GetLockItem();
    }
    void Update()
    {
        usingItem = playerController.isUsing;
        if(usingItem)
        {
            //���� ����Ѵٰ� ���� ���..
            curInventor_Item = inventory.GetInventoryItem();
            curLockItem = playerController.GetLockItem();
            //�ٽ� �޾ƿ´�!!
            if (curInventor_Item != null && curLockItem != null) 
            {
                //�κ��丮 �����۰�, �� ������ �Ѵ� �ȿ� ���� ���� �� �ߵ�
                KeyItem = curInventor_Item.GetComponent<Item>();
                LockItem = curLockItem.GetComponent<Item>();

                if(KeyItem.pair==LockItem.pair)
                {
                    //���� Key�����۰� Lock�������� pair�� �´ٸ�...
                    if(match_Pair != KeyItem.pair)
                    {
                        //mathcpair�� Ű�������� ¦�� �Ȱ����ʴٸ�..
                        match_Pair = KeyItem.pair;
                        OpenQuest();
                    }
                    
                }
            }

        }
    }

    void OpenQuest()
    {
        switch(match_Pair)
        {
            case 0:
                Debug.Log("0����Ʈ�Դϴ�.");
                inventory.Destroy_item();
                keys[0].SetActive(true);
                break;
            default:
                Debug.Log("1����Ʈ�Դϴ�.");

                break;
        }
    }
    
}
