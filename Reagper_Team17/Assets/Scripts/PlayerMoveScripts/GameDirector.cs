using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameDirector : MonoBehaviour
{
    // �÷��̾��� ��ġ UI, PlayerController���� �޾ƿ�
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
        playerPosText.text = playercontroller.playerPos_Floor.ToString() + "��" +" Room"+ playercontroller.playerPos_Room.ToString();
    }


}
