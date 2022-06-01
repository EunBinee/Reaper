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

    //������ ����
    public int LifeCount = 0;
    public Text LifeCountText;
    public bool GetLife = false;
    public GameObject curLife;
    float time;
    float maxTime = 7.0f;
    //=====================
    //Quest02�� ����

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
            if(time>maxTime)
            {
                //���� max Time��ŭ ������ ������ ��� �ð��� ������..
                LifeCount++;

                if(curLife.name== "Life02")
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
            time = 0;
        }

        playerPosText.text = playercontroller.playerPos_Floor.ToString() + "��" +" Room"+ playercontroller.playerPos_Room.ToString();
        LifeCountText.text = LifeCount + "/3";
    }


}
