using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public Inventory inventory;
    public PlayerController playerController;
    public GameObject player;

    public GameDirector gameDirector;
    public EnemyGanerator enemyGanerator;

    public CameraShake cameraShake; //ī�޶��� ��鸲

    SpriteRenderer sp;//���߿� openQuest()���� lock�� sp�� �ҷ����µ� ���δ�.

    //���� �κ��丮�� �ִ� ������ ���� ������ ����
    [SerializeField] private GameObject curInventor_Item;
    //���� �÷��̾ �´���ִ� Lock�ڹ����� ����.
    [SerializeField] private GameObject curLockItem;
    //���� �÷��̾ �´���ִ� (�̺�Ʈ�� ����)������Ʈ�� ����.
    [SerializeField] private GameObject curObject;

    bool usingItem;
    bool usingObject;
    //item ��ũ��Ʈ�� �޾ƿð�
    Item KeyItem;
    Item LockItem;
    Item ObjectItem;

    //�´� ¦�� �ִٸ�.. ���� ����Ʈ�� ������, �ڹ���� Ű�� pair��
    public int match_Pair = -1;

    //����Ʈ�� ���� �����~~
    public GameObject[] keys;

    public Transform[] Locks_And_Object; //��ư ��ġ�� �����ؾ� �� ���.. ���
    public GameObject[] changeTag; //�̺�Ʈ �߻��� �±װ��� �ٲ�� �ϴ� ��� ���.

    //Quest 01 ��==========================================================================
    //������
    //������
    public bool color_Quest01 = false;
    public GameObject color_Quest01_room01; //������ UI ������Ʈ

    public GameObject[] colorBall;
    public int trueNum=0;
    //�������� ��Ʈ ����
    public bool Hint01_Quest01 = false;
    public GameObject hint01_UI;
    //Quest 02 ��==========================================================================
    //Hint02
    public bool Hint02_Quest02 = false;
    public GameObject hint02; //ȭ�鿡 ������ hint
    public GameObject hint02_UI;

    //���ڸ� ���� ���
   // public bool PressTheButton;
    public GameObject ExplanationBox;
    public Text TextForExplanation;

    public Sprite brokenCrack; //�μ��� ũ�� ��������Ʈ
    public GameObject[] crack_object;//�ֺ� ����� ����Ʈ, 

    //========================
    //��Ÿ�� //������Ʈ ����
    float time;
    float maxTime = 1f;
    //=========================================
    //Quest03 ��Ī����
    int count_Quest03 = 0;
    public GameObject Life03;

    //==========================================
    //��������Ʈ ����
    public Sprite[] ChangeSprite;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        curInventor_Item = inventory.GetInventoryItem();
        curLockItem = playerController.GetLockAndObjectItem();
      
    }
    void Update()
    {
        usingItem = playerController.isUsingItem;
        usingObject = playerController.isUsingObject;

        curInventor_Item = inventory.GetInventoryItem();

        if (curInventor_Item != null)
        {
            KeyItem = curInventor_Item.GetComponent<Item>();//Key
            if (KeyItem.haveEventAsObject)
            {
                UseObject(curInventor_Item, KeyItem); //���� ������Ʈ�� �̺�Ʈ�� ���ԵǾ��ִٸ�!
            }
        }
        if(curLockItem!=null)
        {
            LockItem = curLockItem.GetComponent<Item>();
            if (LockItem.haveEventAsObject)
            {
                UseObject(curLockItem, LockItem); //���� ������Ʈ�� �̺�Ʈ�� ���ԵǾ��ִٸ�!
            }
        }

        time += Time.deltaTime;

        if (usingItem)
        {
            curLockItem = playerController.GetLockAndObjectItem();
            //�ٽ� �޾ƿ´�!!
            if (curInventor_Item != null && curLockItem != null) 
            {
                //�κ��丮 �����۰�, �� ������ �Ѵ� �ȿ� ���� ���� �� �ߵ�
                LockItem = curLockItem.GetComponent<Item>();

                if (KeyItem.pair==LockItem.pair)
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

        } //�������� ����ϱ�����..

        if(usingObject)
        {

            playerController.isUsingObject = false;

            curObject = playerController.GetLockAndObjectItem();
            ObjectItem = curObject.GetComponent<Item>();

            if(time>maxTime)
            {
                UseObject();
                time = 0;
            }
        }

        //Quest01======================================================
        if (Hint01_Quest01) 
        {
            //��Ʈ 1�� ����
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                //������ ��ư ������.. ��Ʈ �����ֱ�
                if (!playerController.dontMove)
                {
                    GameObject.Find("Hint_S").GetComponent<AudioSource>().Play();
                    //���� dontMove�� false�� hint�����ְ�
                    playerController.dontMove = true;
                    hint01_UI.SetActive(true);
                }
                else
                {
                   // GameObject.Find("Hint_S").GetComponent<AudioSource>().Play();
                    //true��
                    playerController.dontMove = false;
                    hint01_UI.SetActive(false);
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
        //Quest02======================================================
        if (Hint02_Quest02) //Quest02 ��Ʈ ����
        {
            //��Ʈ 1�� ����
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //������ ��ư ������.. ��Ʈ �����ֱ�
                if (!playerController.dontMove)
                {
                    GameObject.Find("Hint_S").GetComponent<AudioSource>().Play();
                    //���� dontMove�� false�� hint�����ְ�
                    playerController.dontMove = true;
                    hint02_UI.SetActive(true);
                }
                else
                {
                    //true��
                    playerController.dontMove = false;
                    hint02_UI.SetActive(false);
                }
            }
        }//Quest02 ��Ʈ ����
        //Quest03======================================================
        if(count_Quest03==3)
        {
            cameraShake.ShakeTime(0.3f, 0.5f);
            cameraShake.Shake = true;

            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "������ ������ ��Ÿ�����ϴ�.";
            StartCoroutine("ExBox_FadeOut");

            //===========================================================
            //������ ���� true, ���� true;
            Life03.SetActive(true);
            keys[6].SetActive(true);
            count_Quest03 = -1; //���� ����ȵǵ���
        }

        //==========================================================
        
    }

    void OpenQuest()
    {
        switch (match_Pair)
        {
            case 0:
                
                Debug.Log("������ ���� ��ġ�� �δ� ����Ʈ.");

                //�����
                GameObject.Find("GetItem_S").GetComponent<AudioSource>().Play();

                keys[0].transform.position = Locks_And_Object[0].position;

                inventory.Destory_onlyList();

                keys[0].tag = "canJump";
                //**�ʼ� !!
                playerController.isUsingItem = false;

                keys[0].SetActive(true);
                break;
            case 1:
                Debug.Log("1����Ʈ�Դϴ�.");
                //���� ����� ����.
                //��������Ʈ�� �ٲ۴�.(������ ���� �ٲٴ� ������ �����)
                sp = curLockItem.GetComponent<SpriteRenderer>();
                sp.sprite = ChangeSprite[0];
                inventory.Destroy_item();//�κ��丮�� �ִ� Key�����
                curLockItem.tag = "object_Item"; //���� �±׸� Lock>>object_Item���� ����;

                //**�ʼ� !!
                playerController.isUsingItem = false;

                break;
            case 2:
                Debug.Log("2����Ʈ�Դϴ�.");

                cameraShake.ShakeTime(0.3f, 0.4f);
                cameraShake.Shake = true;
                //���� �������.
                inventory.Destroy_item();//�κ��丮�� �ִ� Key�����
                Destroy(curLockItem);// Lock�� ���� ���ش�.
                //�޽��� �ڽ� UI=============================================
                ExplanationBox.SetActive(true);
                StartCoroutine("ExBox_FadeIn");
                TextForExplanation.text = "���� �������.";
                StartCoroutine("ExBox_FadeOut");
                //===========================================================
                //���� ������� �ڽ�Key�� �к�key�� ���� tagȰ��ȭ
                changeTag[3].tag = "key";//���� ����
                changeTag[4].tag = "key";//�к�
                changeTag[3].GetComponent<Item>().pair = 4;
                changeTag[3].GetComponent<Item>().haveEventAsObject = true;

                //**�ʼ� !!
                playerController.isUsingItem = false;

                break;
            case 3:
                //���ھ���!!!
                Debug.Log("3����Ʈ�� �����Դϴ�.");
                count_Quest03++;
                keys[3].transform.position = Locks_And_Object[3].position;

                cameraShake.ShakeTime(0.3f, 0.3f);
                cameraShake.Shake = true;
                //���� �������.
                keys[3].tag = "nothing";
                inventory.Destory_onlyList(); //Key������Ʈ�� ȭ�鿡�ְ�, �κ��丮 ����Ʈ������ �����. 

                //**�ʼ� !!
                playerController.isUsingItem = false;
                keys[3].SetActive(true);
                break;
            case 4:
                //���� ����
                count_Quest03++;
                Debug.Log("3����Ʈ�� ���ǻ����Դϴ�.");
                keys[4].transform.position = Locks_And_Object[4].position;

                cameraShake.ShakeTime(0.3f, 0.3f);
                cameraShake.Shake = true;
                //���� �������.
                keys[4].tag = "canJump";
                inventory.Destory_onlyList(); //Key������Ʈ�� ȭ�鿡�ְ�, �κ��丮 ����Ʈ������ �����. 
                //key�������� tag�� Destory_onlyList()�ȿ��� nothing���� �ٲ�

                //**�ʼ� !!
                playerController.isUsingItem = false;
                keys[4].SetActive(true);
                break;
            case 5:
                //�к�
                count_Quest03++;
                Debug.Log("3����Ʈ�� �к��Դϴ�.");
                keys[5].transform.position = Locks_And_Object[5].position;

                cameraShake.ShakeTime(0.3f, 0.3f);
                cameraShake.Shake = true;
                //���� �������.
                keys[5].tag = "nothing";
                inventory.Destory_onlyList(); //Key������Ʈ�� ȭ�鿡�ְ�, �κ��丮 ����Ʈ������ �����. 
                //key�������� tag�� Destory_onlyList()�ȿ��� nothing���� �ٲ�

                //**�ʼ� !!
                playerController.isUsingItem = false;
                keys[5].SetActive(true);
                break;
            case 6:
                Debug.Log("Lock06_Door02_LastLock ������Ʈ �̺�Ʈ ����!!");
                sp = curLockItem.GetComponent<SpriteRenderer>();
                sp.sprite = ChangeSprite[0];
                inventory.Destroy_item();//�κ��丮�� �ִ� Key�����
                curLockItem.tag = "object_Item";

                playerController.isUsingItem = false;

                break;

            default:
                break;
        }
    } //����Ʈ �������� �����Ѵ�.

    void UseObject()
    {
        //������Ʈ (�� ��) ���!
        if (ObjectItem.itemName == "Key01_lock_And_Door01_Quest01")
        {
            //ObjectItem.itemName�� ���� ������Ʈ�� Item ��ũ��Ʈ�� �ް��ִ�.
            //Quest1�� ���̶��..
            float ObectPosX = Locks_And_Object[2].position.x;

            player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);

            Debug.Log("Key01_lock_And_Door01_Quest01 ������Ʈ �̺�Ʈ ����!!");
        }
        if (ObjectItem.itemName == "Door02_Quest01")
        {
            float ObectPosX = Locks_And_Object[1].position.x;

            player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);


            Debug.Log("Door02_Quest01 ������Ʈ �̺�Ʈ ����!!");
        }

        if (ObjectItem.itemName == "Crack")
        {
            //Quest02�� crack
            //���踦 ��.
            crack_object[1].SetActive(false);//��..
            keys[2].SetActive(true);
            changeTag[1].tag = "nothing"; //ũ���� tag�� ���� ������Ʈ���� �׳� �ƹ��͵� �ƴ� ������ �ٲ���
        }

        //Quest03_ ��Ī��_ Lock�� ó������ object�� �з�
        if (ObjectItem.itemName == "Lock02_Quest02")
        {
            //Quest03�� ������ �ʴ� ��
            cameraShake.ShakeTime(0.3f, 0.3f);
            cameraShake.Shake = true;
            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "���� ������ �ִ�.\n���ڿ� ���� �����Ͱ���.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }

        //������ ��---------------------------------------
        //������Ʈ (�� ��) ���!

        if (ObjectItem.itemName == "Lock06_Door03_LastLock")
        {
            if (gameDirector.LifeCount < 3)
            {
                //������ ������ �����ϴٸ�
                //item.haveEventAsObject = false;

                //�޽��� �ڽ� UI=============================================
                ExplanationBox.SetActive(true);
                StartCoroutine("ExBox_FadeIn");
                TextForExplanation.text = "������ ������ �����մϴ�.";
                StartCoroutine("ExBox_FadeOut");
                //===========================================================
            }
            else
            {
                float ObectPosX = Locks_And_Object[7].position.x;
                // enemyGanerator.SetActive(false); //������ ���� ����, �� ������ ����
                enemyGanerator.stop_Ganerator = true;
                player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);

                Debug.Log("Lock06_Door02_LastLock ������Ʈ �̺�Ʈ ����!!");
            }

        }

    }
    void UseObject(GameObject curItem_, Item item)
    {
        //Key00_Quest00_����
        if (item.itemName == "chair_Key00_Quest00")
        {
            item.haveEventAsObject = false;

            cameraShake.ShakeTime(0.3f, 0.3f);
            cameraShake.Shake = true;

            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "��򰡿� ��� �ö� �� ���� �� ����.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }
        //Key01_Quest01_�� ����
        if (item.itemName == "Key01_Quest01")
        {
            item.haveEventAsObject = false;
            changeTag[1].tag = "lock";
            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "���踦 �����.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }
        //������02(��Ī�� ���� �뵵
        if (item.itemName == "key02_Quest02")
        {
            item.haveEventAsObject = false;
            changeTag[2].tag = "lock";
            //�������ʴ� �� tag. Lock���� ����

            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "���踦 �����.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }
        //���� �̺�Ʈ_Key03_��Ī�� ����.
        if (item.itemName == "PhotoPrame_Key03_Quest03")
        {
            item.haveEventAsObject = false;

            cameraShake.ShakeTime(0.3f, 0.4f);
            cameraShake.Shake = true;
            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "���𰡰� �μ����� �Ҹ��� ����.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================

            SpriteRenderer Sr = changeTag[1].GetComponent<SpriteRenderer>();
            Sr.sprite = brokenCrack; //ũ���� ��������Ʈ�� �μ��� ������ ��ü���ش�.

            crack_object[0].SetActive(true);//����
            crack_object[1].SetActive(true);//��..

            changeTag[1].tag = "object_Item"; //crack�� �±׸� �ٲ��ش�.
        }
        //������ �������� ���� Ű
        if (item.itemName == "Key06_LastKey")
        {
            item.haveEventAsObject = false;

            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "������ ���踦 �����.. ���� Ż������!";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================

        }

        //��
        if (item.itemName == "Lock06_Door03_LastLock")
        {
            item.haveEventAsObject = false;

            cameraShake.ShakeTime(0.3f, 0.3f);
            cameraShake.Shake = true;

            //�޽��� �ڽ� UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "������ ���� 3������ ���谡 �ʿ��մϴ�.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }






    }
    //�޼��� �ڽ� ���̵��� ���̵� �ƿ�
    IEnumerator ExBox_FadeOut()
    {
        yield return new WaitForSeconds(2f); //4�ʵ��� ��ٸ��� ��

        int i = 10;
        while (i >= 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color color = ExplanationBox.GetComponent<Image>().color;
            color.a = f;
            ExplanationBox.GetComponent<Image>().color = color;

            if(i<=0)
            {
                ExplanationBox.SetActive(false);
            }
            
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator ExBox_FadeIn()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            Color color = ExplanationBox.GetComponent<Image>().color;
            color.a = f;
            ExplanationBox.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.02f);
        }
    }

    //Quest 01 �� ���� �޼ҵ�-----------------------------------------------
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
    //Quest 02 ����(����)ã��
    public void Start_Quest02()
    {
        //������ ���⵵��
        hint02.SetActive(true);
        changeTag[0].tag = "key"; //2�� ����tag�� key�� 
    }


}
