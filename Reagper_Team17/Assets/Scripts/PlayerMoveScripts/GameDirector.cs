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

    //생명의 조각
    public int LifeCount = 0;
    public Text LifeCountText;
    public bool GetLife = false;
    public GameObject curLife;
    float time;
    float maxTime = 7.0f;
    //=====================
    //Quest02의 시작

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
                //만약 max Time만큼 생명의 조각에 닿고 시간이 지나면..
                LifeCount++;

                if(curLife.name== "Life02")
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
            time = 0;
        }

        playerPosText.text = playercontroller.playerPos_Floor.ToString() + "층" +" Room"+ playercontroller.playerPos_Room.ToString();
        LifeCountText.text = LifeCount + "/3";
    }


}
