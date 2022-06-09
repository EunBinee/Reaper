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

    public CameraShake cameraShake; //카메라의 흔들림

    SpriteRenderer sp;//나중에 openQuest()에서 lock의 sp를 불러오는데 쓰인다.

    //현재 인벤토리에 있는 열쇠의 정보 가지고 오기
    [SerializeField] private GameObject curInventor_Item;
    //현재 플레이어가 맞닿아있는 Lock자물쇠의 정보.
    [SerializeField] private GameObject curLockItem;
    //현재 플레이어가 맞닿아있는 (이벤트를 위한)오브젝트의 정보.
    [SerializeField] private GameObject curObject;

    bool usingItem;
    bool usingObject;
    //item 스크립트를 받아올것
    Item KeyItem;
    Item LockItem;
    Item ObjectItem;

    //맞는 짝이 있다면.. 현재 퀘스트의 정보값, 자물쇠와 키의 pair값
    public int match_Pair = -1;

    //퀘스트에 사용될 열쇠들~~
    public GameObject[] keys;

    public Transform[] Locks_And_Object; //암튼 위치값 저장해야 할 경우.. 사용
    public GameObject[] changeTag; //이벤트 발생시 태그값을 바꿔야 하는 경우 사용.

    //Quest 01 번==========================================================================
    //색퍼즐
    //색퍼즐
    public bool color_Quest01 = false;
    public GameObject color_Quest01_room01; //색퀴즈 UI 오브젝트

    public GameObject[] colorBall;
    public int trueNum=0;
    //색퍼즐의 힌트 보기
    public bool Hint01_Quest01 = false;
    public GameObject hint01_UI;
    //Quest 02 번==========================================================================
    //Hint02
    public bool Hint02_Quest02 = false;
    public GameObject hint02; //화면에 나오는 hint
    public GameObject hint02_UI;

    //액자를 얻은 경우
   // public bool PressTheButton;
    public GameObject ExplanationBox;
    public Text TextForExplanation;

    public Sprite brokenCrack; //부서진 크랙 스프라이트
    public GameObject[] crack_object;//주변 파편과 라이트, 

    //========================
    //쿨타임 //오브젝트 사용시
    float time;
    float maxTime = 1f;
    //=========================================
    //Quest03 대칭게임
    int count_Quest03 = 0;
    public GameObject Life03;

    //==========================================
    //스프라이트 모음
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
                UseObject(curInventor_Item, KeyItem); //만약 오브젝트에 이벤트가 포함되어있다면!
            }
        }
        if(curLockItem!=null)
        {
            LockItem = curLockItem.GetComponent<Item>();
            if (LockItem.haveEventAsObject)
            {
                UseObject(curLockItem, LockItem); //만약 오브젝트에 이벤트가 포함되어있다면!
            }
        }

        time += Time.deltaTime;

        if (usingItem)
        {
            curLockItem = playerController.GetLockAndObjectItem();
            //다시 받아온다!!
            if (curInventor_Item != null && curLockItem != null) 
            {
                //인벤토리 아이템과, 락 아이템 둘다 안에 무언가 있을 때 발동
                LockItem = curLockItem.GetComponent<Item>();

                if (KeyItem.pair==LockItem.pair)
                {
                    //만약 Key아이템과 Lock아이템의 pair가 맞다면...
                    if(match_Pair != KeyItem.pair)
                    {
                        //mathcpair와 키아이템의 짝이 똑같지않다면..
                       
                        match_Pair = KeyItem.pair;
                        OpenQuest();
                    }
                    
                }
            }

        } //아이템을 사용하기위한..

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
            //힌트 1일 볼때
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                //위방향 버튼 누르면.. 힌트 보여주기
                if (!playerController.dontMove)
                {
                    GameObject.Find("Hint_S").GetComponent<AudioSource>().Play();
                    //만약 dontMove가 false면 hint보여주고
                    playerController.dontMove = true;
                    hint01_UI.SetActive(true);
                }
                else
                {
                   // GameObject.Find("Hint_S").GetComponent<AudioSource>().Play();
                    //true면
                    playerController.dontMove = false;
                    hint01_UI.SetActive(false);
                }
            }
        } //색 힌트

        if (color_Quest01 && trueNum < colorBall.Length)
        {
            //컬러 퀘스트 UI
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //위방향 버튼 누르면.. 힌트 보여주기
                if (!playerController.dontMove)
                {
                    //만약 dontMove가 false면 hint보여주고
                    playerController.dontMove = true;
                    color_Quest01_room01.SetActive(true);
                }
            }
        } //색 퍼즐
        //Quest02======================================================
        if (Hint02_Quest02) //Quest02 힌트 보기
        {
            //힌트 1일 볼때
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                //위방향 버튼 누르면.. 힌트 보여주기
                if (!playerController.dontMove)
                {
                    GameObject.Find("Hint_S").GetComponent<AudioSource>().Play();
                    //만약 dontMove가 false면 hint보여주고
                    playerController.dontMove = true;
                    hint02_UI.SetActive(true);
                }
                else
                {
                    //true면
                    playerController.dontMove = false;
                    hint02_UI.SetActive(false);
                }
            }
        }//Quest02 힌트 보기
        //Quest03======================================================
        if(count_Quest03==3)
        {
            cameraShake.ShakeTime(0.3f, 0.5f);
            cameraShake.Shake = true;

            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "생명의 조각이 나타났습니다.";
            StartCoroutine("ExBox_FadeOut");

            //===========================================================
            //생명의 조각 true, 열쇠 true;
            Life03.SetActive(true);
            keys[6].SetActive(true);
            count_Quest03 = -1; //이제 실행안되도록
        }

        //==========================================================
        
    }

    void OpenQuest()
    {
        switch (match_Pair)
        {
            case 0:
                
                Debug.Log("발판을 옳은 위치에 두는 퀘스트.");

                //오디오
                GameObject.Find("GetItem_S").GetComponent<AudioSource>().Play();

                keys[0].transform.position = Locks_And_Object[0].position;

                inventory.Destory_onlyList();

                keys[0].tag = "canJump";
                //**필수 !!
                playerController.isUsingItem = false;

                keys[0].SetActive(true);
                break;
            case 1:
                Debug.Log("1퀘스트입니다.");
                //문의 잠금을 연다.
                //스프라이트를 바꾼다.(지금은 색을 바꾸는 것으로 대신함)
                sp = curLockItem.GetComponent<SpriteRenderer>();
                sp.sprite = ChangeSprite[0];
                inventory.Destroy_item();//인벤토리에 있는 Key지우기
                curLockItem.tag = "object_Item"; //문의 태그를 Lock>>object_Item으로 변경;

                //**필수 !!
                playerController.isUsingItem = false;

                break;
            case 2:
                Debug.Log("2퀘스트입니다.");

                cameraShake.ShakeTime(0.3f, 0.4f);
                cameraShake.Shake = true;
                //벽이 사라진다.
                inventory.Destroy_item();//인벤토리에 있는 Key지우기
                Destroy(curLockItem);// Lock인 벽도 없앤다.
                //메시지 박스 UI=============================================
                ExplanationBox.SetActive(true);
                StartCoroutine("ExBox_FadeIn");
                TextForExplanation.text = "벽이 사라졌다.";
                StartCoroutine("ExBox_FadeOut");
                //===========================================================
                //벽이 사라지고 박스Key와 촛불key를 이제 tag활성화
                changeTag[3].tag = "key";//발판 상자
                changeTag[4].tag = "key";//촛불
                changeTag[3].GetComponent<Item>().pair = 4;
                changeTag[3].GetComponent<Item>().haveEventAsObject = true;

                //**필수 !!
                playerController.isUsingItem = false;

                break;
            case 3:
                //액자액자!!!
                Debug.Log("3퀘스트의 액자입니다.");
                count_Quest03++;
                keys[3].transform.position = Locks_And_Object[3].position;

                cameraShake.ShakeTime(0.3f, 0.3f);
                cameraShake.Shake = true;
                //벽이 사라진다.
                keys[3].tag = "nothing";
                inventory.Destory_onlyList(); //Key오브젝트는 화면에있고, 인벤토리 리스트에서만 지운다. 

                //**필수 !!
                playerController.isUsingItem = false;
                keys[3].SetActive(true);
                break;
            case 4:
                //발판 상자
                count_Quest03++;
                Debug.Log("3퀘스트의 발판상자입니다.");
                keys[4].transform.position = Locks_And_Object[4].position;

                cameraShake.ShakeTime(0.3f, 0.3f);
                cameraShake.Shake = true;
                //벽이 사라진다.
                keys[4].tag = "canJump";
                inventory.Destory_onlyList(); //Key오브젝트는 화면에있고, 인벤토리 리스트에서만 지운다. 
                //key아이템의 tag도 Destory_onlyList()안에서 nothing으로 바뀜

                //**필수 !!
                playerController.isUsingItem = false;
                keys[4].SetActive(true);
                break;
            case 5:
                //촛불
                count_Quest03++;
                Debug.Log("3퀘스트의 촛불입니다.");
                keys[5].transform.position = Locks_And_Object[5].position;

                cameraShake.ShakeTime(0.3f, 0.3f);
                cameraShake.Shake = true;
                //벽이 사라진다.
                keys[5].tag = "nothing";
                inventory.Destory_onlyList(); //Key오브젝트는 화면에있고, 인벤토리 리스트에서만 지운다. 
                //key아이템의 tag도 Destory_onlyList()안에서 nothing으로 바뀜

                //**필수 !!
                playerController.isUsingItem = false;
                keys[5].SetActive(true);
                break;
            case 6:
                Debug.Log("Lock06_Door02_LastLock 오브젝트 이벤트 실행!!");
                sp = curLockItem.GetComponent<SpriteRenderer>();
                sp.sprite = ChangeSprite[0];
                inventory.Destroy_item();//인벤토리에 있는 Key지우기
                curLockItem.tag = "object_Item";

                playerController.isUsingItem = false;

                break;

            default:
                break;
        }
    } //퀘스트 아이템을 조정한다.

    void UseObject()
    {
        //오브젝트 (문 등) 사용!
        if (ObjectItem.itemName == "Key01_lock_And_Door01_Quest01")
        {
            //ObjectItem.itemName은 현재 오브젝트의 Item 스크립트를 받고있다.
            //Quest1번 문이라면..
            float ObectPosX = Locks_And_Object[2].position.x;

            player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);

            Debug.Log("Key01_lock_And_Door01_Quest01 오브젝트 이벤트 실행!!");
        }
        if (ObjectItem.itemName == "Door02_Quest01")
        {
            float ObectPosX = Locks_And_Object[1].position.x;

            player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);


            Debug.Log("Door02_Quest01 오브젝트 이벤트 실행!!");
        }

        if (ObjectItem.itemName == "Crack")
        {
            //Quest02의 crack
            //열쇠를 줌.
            crack_object[1].SetActive(false);//빛..
            keys[2].SetActive(true);
            changeTag[1].tag = "nothing"; //크랙의 tag를 이제 오브젝트에서 그냥 아무것도 아닌 것으로 바꿔줌
        }

        //Quest03_ 대칭방_ Lock은 처음에는 object로 분류
        if (ObjectItem.itemName == "Lock02_Quest02")
        {
            //Quest03의 보이지 않는 벽
            cameraShake.ShakeTime(0.3f, 0.3f);
            cameraShake.Shake = true;
            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "열쇠 구멍이 있다.\n벽뒤에 방이 있을것같다.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }

        //마지막 문---------------------------------------
        //오브젝트 (문 등) 사용!

        if (ObjectItem.itemName == "Lock06_Door03_LastLock")
        {
            if (gameDirector.LifeCount < 3)
            {
                //생명의 조각이 부족하다면
                //item.haveEventAsObject = false;

                //메시지 박스 UI=============================================
                ExplanationBox.SetActive(true);
                StartCoroutine("ExBox_FadeIn");
                TextForExplanation.text = "생명의 조각이 부족합니다.";
                StartCoroutine("ExBox_FadeOut");
                //===========================================================
            }
            else
            {
                float ObectPosX = Locks_And_Object[7].position.x;
                // enemyGanerator.SetActive(false); //마지막 문을 열면, 적 생성기 없앰
                enemyGanerator.stop_Ganerator = true;
                player.transform.position = new Vector3(ObectPosX, player.transform.position.y, player.transform.position.z);

                Debug.Log("Lock06_Door02_LastLock 오브젝트 이벤트 실행!!");
            }

        }

    }
    void UseObject(GameObject curItem_, Item item)
    {
        //Key00_Quest00_의자
        if (item.itemName == "chair_Key00_Quest00")
        {
            item.haveEventAsObject = false;

            cameraShake.ShakeTime(0.3f, 0.3f);
            cameraShake.Shake = true;

            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "어딘가에 밟고 올라갈 수 있을 것 같다.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }
        //Key01_Quest01_문 열쇠
        if (item.itemName == "Key01_Quest01")
        {
            item.haveEventAsObject = false;
            changeTag[1].tag = "lock";
            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "열쇠를 얻었다.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }
        //문열쇠02(대칭방 여는 용도
        if (item.itemName == "key02_Quest02")
        {
            item.haveEventAsObject = false;
            changeTag[2].tag = "lock";
            //보이지않는 벽 tag. Lock으로 변경

            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "열쇠를 얻었다.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }
        //액자 이벤트_Key03_대칭에 쓰임.
        if (item.itemName == "PhotoPrame_Key03_Quest03")
        {
            item.haveEventAsObject = false;

            cameraShake.ShakeTime(0.3f, 0.4f);
            cameraShake.Shake = true;
            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "무언가가 부서지는 소리가 났다.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================

            SpriteRenderer Sr = changeTag[1].GetComponent<SpriteRenderer>();
            Sr.sprite = brokenCrack; //크랙의 스프라이트를 부서진 것으로 교체해준다.

            crack_object[0].SetActive(true);//파편
            crack_object[1].SetActive(true);//빛..

            changeTag[1].tag = "object_Item"; //crack의 태그를 바꿔준다.
        }
        //마지막 엔딩으로 가는 키
        if (item.itemName == "Key06_LastKey")
        {
            item.haveEventAsObject = false;

            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "마지막 열쇠를 얻었다.. 이제 탈출하자!";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================

        }

        //문
        if (item.itemName == "Lock06_Door03_LastLock")
        {
            item.haveEventAsObject = false;

            cameraShake.ShakeTime(0.3f, 0.3f);
            cameraShake.Shake = true;

            //메시지 박스 UI=============================================
            ExplanationBox.SetActive(true);
            StartCoroutine("ExBox_FadeIn");
            TextForExplanation.text = "생명의 조각 3조각과 열쇠가 필요합니다.";
            StartCoroutine("ExBox_FadeOut");
            //===========================================================
        }






    }
    //메세지 박스 페이드인 페이드 아웃
    IEnumerator ExBox_FadeOut()
    {
        yield return new WaitForSeconds(2f); //4초동안 기다리는 뜻

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

    //Quest 01 색 퍼즐 메소드-----------------------------------------------
    public void Finish_Quest01_B()
    {
        //색깔 퍼즐 다 맞추고 다했을 때 누르는 버튼
        for (int i = 0; i < colorBall.Length; i++) 
        {
            if (colorBall[i].GetComponent<Item_DragDrop>().Matching == true) 
            {
                trueNum++;
            }
        }
        if (trueNum >= colorBall.Length) 
        {
            //만약에 Matching이 전부 되어있으면..
            //열쇠주기

            Debug.Log(trueNum);
            Debug.Log(colorBall.Length);
            Debug.Log("열쇠 줌~");

            keys[1].SetActive(true);

            Exit_Quest01_B();
        }
        else
        {
            //초기화
            trueNum = 0;
            Debug.Log("초기화!");
            Replace_Quest01();
        }
    }
    public void Replace_Quest01_B()
    {
        //초기화 버튼
        Replace_Quest01();
    }
    public void Exit_Quest01_B()
    {
        //초기화
        Replace_Quest01();
        
        playerController.dontMove = false;
        color_Quest01_room01.SetActive(false);//UI닫기

    }
    void Replace_Quest01()
    {
        //색깔 퍼즐 다시 시작하기
        for (int i = 0; i < colorBall.Length; i++)
        {
            colorBall[i].GetComponent<DragDrop>().Replace();  
        }
    }
    //=====================================================================
    //Quest 02 보물(열쇠)찾기
    public void Start_Quest02()
    {
        //지도가 생기도록
        hint02.SetActive(true);
        changeTag[0].tag = "key"; //2층 액자tag를 key로 
    }


}
