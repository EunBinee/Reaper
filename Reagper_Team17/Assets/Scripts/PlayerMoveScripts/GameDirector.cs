using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    float maxTime = 7.0f;
    //=====================
    //메세지 박스(설명용도)
    public GameObject ExplanationBox;
    public Text TextForExplanation;

    void Start()
    {
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
        questManager= GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetLife)
        {
            time += Time.deltaTime;
            Debug.Log((int)time);


            ShakeStart = true;
            cameraShake.ShakeTime(maxTime, 0.05f);
            cameraShake.Shake = true;

            if (time>maxTime)
            {
                //만약 max Time만큼 생명의 조각에 닿고 시간이 지나면..
                LifeCount++;

                //메시지 박스 UI=============================================
                ExplanationBox.SetActive(true);
                StartCoroutine("ExBox_FadeIn");
                TextForExplanation.text = "생명의 조각을 얻었다.";
                StartCoroutine("ExBox_FadeOut");
                //===========================================================

                if (curLife.name== "Life02")
                {
                    //만약 Life02가 사라졌을 경우
                    //Quest02 힌트 주기
                    questManager.Start_Quest02();

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

}
