using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Transform[] Locks;
    //Quest 01 ��
    //������
    public bool color_Quest01 = false;
    public GameObject color_Quest01_room01;

    public GameObject[] colorBall;
    public int trueNum=0;
    //�������� ��Ʈ ����
    public bool Hint01_Quest01 = false;
    public GameObject hint01_Room01;


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


        if (Hint01_Quest01) 
        {
            //��Ʈ 1�� ����
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //������ ��ư ������.. ��Ʈ �����ֱ�
                if (!playerController.dontMove)
                {
                    //���� dontMove�� false�� hint�����ְ�
                    playerController.dontMove = true;
                    hint01_Room01.SetActive(true);
                }
                else
                {
                    //true��
                    playerController.dontMove = false;
                    hint01_Room01.SetActive(false);
                }
            }
        } //�� ��Ʈ

        if (color_Quest01 && trueNum < colorBall.Length)
        {
            //�÷� ����Ʈ UI
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //������ ��ư ������.. ��Ʈ �����ֱ�
                if (!playerController.dontMove)
                {
                    //���� dontMove�� false�� hint�����ְ�
                    playerController.dontMove = true;
                    color_Quest01_room01.SetActive(true);
                }
            }
        } //�� ����

    }

    void OpenQuest()
    {
        switch(match_Pair)
        {
            case 0:
                Debug.Log("0����Ʈ�Դϴ�.");
                keys[0].transform.position = Locks[0].position;
         
                inventory.Destory_onlyList();

                //ȭ��� ����������, ���� ���������ν� ���������ʰڴٴ� �ǹ�
                keys[0].GetComponent<Item>().notMoving = true;

                keys[0].SetActive(true);
                break;
            case 1:
                Debug.Log("1����Ʈ�Դϴ�.");
                
                break;
            default:

                break;
        }
    }
    

    //�� ���� �޼ҵ�-----------------------------------------------
    public void Finish_Quest01_B()
    {
        //���� ���� �� ���߰� ������ �� ������ ��ư
        for (int i = 0; i < colorBall.Length; i++) 
        {
            if (colorBall[i].GetComponent<Item_DragDrop>().Matching == true) 
            {
                trueNum++;
            }
        }
        if (trueNum >= colorBall.Length) 
        {
            //���࿡ Matching�� ���� �Ǿ�������..
            //�����ֱ�

            Debug.Log(trueNum);
            Debug.Log(colorBall.Length);
            Debug.Log("���� ��~");

            keys[1].SetActive(true);

            Exit_Quest01_B();
        }
        else
        {
            //�ʱ�ȭ
            trueNum = 0;
            Debug.Log("�ʱ�ȭ!");
            Replace_Quest01();
        }
    }
    public void Replace_Quest01_B()
    {
        //�ʱ�ȭ ��ư
        Replace_Quest01();
    }
    public void Exit_Quest01_B()
    {
        //�ʱ�ȭ
        Replace_Quest01();
        
        playerController.dontMove = false;
        color_Quest01_room01.SetActive(false);//UI�ݱ�

    }
    void Replace_Quest01()
    {
        //���� ���� �ٽ� �����ϱ�
        for (int i = 0; i < colorBall.Length; i++)
        {
            colorBall[i].GetComponent<DragDrop>().Replace();  
        }
    }
    //=====================================================================
}
