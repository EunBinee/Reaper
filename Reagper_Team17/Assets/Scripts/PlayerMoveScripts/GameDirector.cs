using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    float maxTime = 7.0f;
    //=====================
    //�޼��� �ڽ�(����뵵)
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
                //���� max Time��ŭ ������ ������ ��� �ð��� ������..
                LifeCount++;

                //�޽��� �ڽ� UI=============================================
                ExplanationBox.SetActive(true);
                StartCoroutine("ExBox_FadeIn");
                TextForExplanation.text = "������ ������ �����.";
                StartCoroutine("ExBox_FadeOut");
                //===========================================================

                if (curLife.name== "Life02")
                {
                    //���� Life02�� ������� ���
                    //Quest02 ��Ʈ �ֱ�
                    questManager.Start_Quest02();

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

}
