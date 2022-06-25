using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour
{
    // 플레이어의 위치 UI, PlayerController에서 받아옴
    public Text playerPosText;
    PlayerController playercontroller;
    QuestManager questManager;
    public CameraShake cameraShake; //카메라 흔들림
    bool ShakeStart;

    //생명의 조각
    public int LifeCount = 0;
    public Text LifeCountText;
    public bool GetLife = false;
    public GameObject curLife;
    float time;
    float maxTime = 5.0f;
    //=====================
    //메세지 박스(설명용도)
    public GameObject ExplanationBox;
    public Text TextForExplanation;


    //==========================
    //엔딩
    public EnemyGenerator enemyGenerator;
    public GameObject GameClearBoard; //화면을 밝게 해줄..
    
    public GameObject GameClearLight;//글로벌 라이트;
    public GameObject Light;//글로벌 라이트;

    //===========================
    //게임 방법 및 일시정지
    public GameObject Desc;
    public GameObject Button_panel;
    bool isPause = false;

    void Start()
    {
        Time.timeScale = 1;
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
        questManager= GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !isPause)
        {
            //일시정지,하고 종료 버튼 보여주기

            Time.timeScale = 0;
            Button_panel.SetActive(true);
            isPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            //일시정지,하고 종료 버튼 보여주기

            Time.timeScale = 1;
            Button_panel.SetActive(false);
            isPause = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //일시정지gkrh, 설명창보여주기

            Time.timeScale = 0;
            Desc.SetActive(true);
        }


        if (GetLife)
        {
            time += Time.deltaTime;
            Debug.Log((int)time);


            ShakeStart = true;
            cameraShake.ShakeTime(maxTime, 0.3f);
            cameraShake.Shake = true;

            if (time>maxTime)
            {
                //만약 max Time만큼 생명의 조각에 닿고 시간이 지나면..
                LifeCount++;

                

                if (curLife.name== "Life02")
                {
                    //만약 Life02가 사라졌을 경우
                    //Quest02 힌트 주기
                    ExplanationBox.SetActive(true);
                    StartCoroutine("ExBox_FadeIn");
                    TextForExplanation.text = "생명의 조각을 얻었고, 힌트가 나타났다.";
                    StartCoroutine("ExBox_FadeOut");

                    questManager.Start_Quest02();

                }
                else
                {
                    //메시지 박스 UI=============================================
                    ExplanationBox.SetActive(true);
                    StartCoroutine("ExBox_FadeIn");
                    TextForExplanation.text = "생명의 조각을 얻었다.";
                    StartCoroutine("ExBox_FadeOut");
                    //===========================================================

                }
                Destroy(curLife);

                curLife = null;
                time = 0;
            }
        }
        else if(!GetLife)
        {
            //GetLife가 false면
            if(ShakeStart)
            {
                cameraShake.Stop();
                ShakeStart = false;
            }
            //cameraShake.Stop();
            time = 0;
        }

        playerPosText.text = playercontroller.playerPos_Floor.ToString() + "층" +" Room"+ playercontroller.playerPos_Room.ToString();
        LifeCountText.text = LifeCount + "/3";
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

            if (i <= 0)
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



    //엔딩 씬..1 GameOver=======================================================================================
    public void GameOver()
    {

        SceneManager.LoadScene("GameOverScene");

    }

    //엔딩 씬..2 GameClear=======================================================================================
    public void End_Scene()
    {
        //적 생성
        enemyGenerator.End_Enemy_Ganerator = true;
        enemyGenerator.stop_Ganerator = true;
        ChasingTrue();
    }
    void ChasingTrue()
    {
        cameraShake.ShakeTime(0.4f, 0.4f);
        cameraShake.Shake = true;
        //메시지 박스 UI=============================================
        ExplanationBox.SetActive(true);
        StartCoroutine("ExBox_FadeIn");
        TextForExplanation.text = "끝까지 달리세요.";
        StartCoroutine("ExBox_FadeOut");
        //===========================================================
    }
    //========================
    //플레이어가 GameClearcollider에 닿았다!

    //
    //1. 무서운 음악(배경음, 추적음. 뚜벅소리) 전부 멈춘다.
    //3. 적을 없앤다..
    //2.board를 불러와서, alpha값을 서서히 올린다.
    //그리고 alpha값이 다 뜨면.. 플레이어의 스프라이트 랜더러도 불러와서 알파값을 올려준다.
    //카메라 쉐이킹도 할까.. 고민되네.ㅠㅠ

    //글로벌 라이트도 없애야함.

    public void GameClear()
    {
        enemyGenerator.End_Enemy_Ganerator = true;
        enemyGenerator.stop_Ganerator = true;
        GameClearLight.SetActive(false);
        Light.SetActive(true);
        GameObject.Find("Background_M").GetComponent<AudioSource>().Stop();

        //Invoke("GameClearScene_Load", 5.0f);
        StartCoroutine("GameClearBoard_FadeIn");

    }

    IEnumerator GameClearBoard_FadeIn()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            Color color = GameClearBoard.GetComponent<SpriteRenderer>().color;
            color.a = f;
            GameClearBoard.GetComponent<SpriteRenderer>().color = color;

            
            yield return new WaitForSeconds(0.2f);
        }

        if (i >= 10)
        {
            StartCoroutine("GameClearPlayer_Fadeout");
        }
    }

    IEnumerator GameClearPlayer_Fadeout() 
    {
        yield return new WaitForSeconds(2f); //4초동안 기다리는 뜻

        int i = 10;
        while (i >= 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color color = GameObject.Find("Player").GetComponent<SpriteRenderer>().color;
            color.a = f;
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;


            yield return new WaitForSeconds(0.2f);
        }

        SceneManager.LoadScene("GameClearScene");

    }



    //=====================================================================
    //esc누르고 일시정지후 나오는 버튼들
    public void GoStartScene_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("StartScene");
    }
    public void ReStart_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("GameScene");
    }
    public void Exit_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        Application.Quit();
    }


}
