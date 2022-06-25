using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour
{
    // �÷��̾��� ��ġ UI, PlayerController���� �޾ƿ�
    public Text playerPosText;
    PlayerController playercontroller;
    QuestManager questManager;
    public CameraShake cameraShake; //ī�޶� ��鸲
    bool ShakeStart;

    //������ ����
    public int LifeCount = 0;
    public Text LifeCountText;
    public bool GetLife = false;
    public GameObject curLife;
    float time;
    float maxTime = 5.0f;
    //=====================
    //�޼��� �ڽ�(����뵵)
    public GameObject ExplanationBox;
    public Text TextForExplanation;


    //==========================
    //����
    public EnemyGenerator enemyGenerator;
    public GameObject GameClearBoard; //ȭ���� ��� ����..
    
    public GameObject GameClearLight;//�۷ι� ����Ʈ;
    public GameObject Light;//�۷ι� ����Ʈ;

    //===========================
    //���� ��� �� �Ͻ�����
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
            //�Ͻ�����,�ϰ� ���� ��ư �����ֱ�

            Time.timeScale = 0;
            Button_panel.SetActive(true);
            isPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            //�Ͻ�����,�ϰ� ���� ��ư �����ֱ�

            Time.timeScale = 1;
            Button_panel.SetActive(false);
            isPause = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //�Ͻ�����gkrh, ����â�����ֱ�

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
                //���� max Time��ŭ ������ ������ ��� �ð��� ������..
                LifeCount++;

                

                if (curLife.name== "Life02")
                {
                    //���� Life02�� ������� ���
                    //Quest02 ��Ʈ �ֱ�
                    ExplanationBox.SetActive(true);
                    StartCoroutine("ExBox_FadeIn");
                    TextForExplanation.text = "������ ������ �����, ��Ʈ�� ��Ÿ����.";
                    StartCoroutine("ExBox_FadeOut");

                    questManager.Start_Quest02();

                }
                else
                {
                    //�޽��� �ڽ� UI=============================================
                    ExplanationBox.SetActive(true);
                    StartCoroutine("ExBox_FadeIn");
                    TextForExplanation.text = "������ ������ �����.";
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
            //GetLife�� false��
            if(ShakeStart)
            {
                cameraShake.Stop();
                ShakeStart = false;
            }
            //cameraShake.Stop();
            time = 0;
        }

        playerPosText.text = playercontroller.playerPos_Floor.ToString() + "��" +" Room"+ playercontroller.playerPos_Room.ToString();
        LifeCountText.text = LifeCount + "/3";
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



    //���� ��..1 GameOver=======================================================================================
    public void GameOver()
    {

        SceneManager.LoadScene("GameOverScene");

    }

    //���� ��..2 GameClear=======================================================================================
    public void End_Scene()
    {
        //�� ����
        enemyGenerator.End_Enemy_Ganerator = true;
        enemyGenerator.stop_Ganerator = true;
        ChasingTrue();
    }
    void ChasingTrue()
    {
        cameraShake.ShakeTime(0.4f, 0.4f);
        cameraShake.Shake = true;
        //�޽��� �ڽ� UI=============================================
        ExplanationBox.SetActive(true);
        StartCoroutine("ExBox_FadeIn");
        TextForExplanation.text = "������ �޸�����.";
        StartCoroutine("ExBox_FadeOut");
        //===========================================================
    }
    //========================
    //�÷��̾ GameClearcollider�� ��Ҵ�!

    //
    //1. ������ ����(�����, ������. �ѹ��Ҹ�) ���� �����.
    //3. ���� ���ش�..
    //2.board�� �ҷ��ͼ�, alpha���� ������ �ø���.
    //�׸��� alpha���� �� �߸�.. �÷��̾��� ��������Ʈ �������� �ҷ��ͼ� ���İ��� �÷��ش�.
    //ī�޶� ����ŷ�� �ұ�.. ��εǳ�.�Ф�

    //�۷ι� ����Ʈ�� ���־���.

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
        yield return new WaitForSeconds(2f); //4�ʵ��� ��ٸ��� ��

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
    //esc������ �Ͻ������� ������ ��ư��
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
