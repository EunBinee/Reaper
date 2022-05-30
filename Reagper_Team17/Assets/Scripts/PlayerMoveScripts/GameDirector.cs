using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameDirector : MonoBehaviour
{
    // 플레이어의 위치 UI, PlayerController에서 받아옴
    public Text playerPosText;
    PlayerController playercontroller;
    void Start()
    {
        //playerPosText = GetComponent<Text>();
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosText.text = playercontroller.playerPos_Floor.ToString() + "층" +" Room"+ playercontroller.playerPos_Room.ToString();
    }


}
