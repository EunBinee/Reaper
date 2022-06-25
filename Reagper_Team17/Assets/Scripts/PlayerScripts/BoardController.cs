using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    //플레이어가 있는 위치의 방만 밝게..하는 스크립트

    //플레이어가 있는 방을 받아온다.
    public PlayerController playerController;
    public int playerRoom;

    public SpriteRenderer[] Rooms_SR;

    //======
    //플레이어가 방을 이동한 경우에만 연산을 하도록
    public bool changeRoom =false;
    public int curRoom;
    public int preRoom;

    void Start()
    {

        playerRoom = 1;
        
        curRoom = 1;
        preRoom = 1;

        for(int i=0;i<Rooms_SR.Length;i++)
        {
            Rooms_SR[i].color = new Color(0, 0, 0, 1);
        }


        Rooms_SR[playerRoom-1].color= new Color(0, 0, 0, 0);



    }

    // Update is called once per frame
    void Update()
    {
        playerRoom = playerController.GetRoom(); //계속 받아온다.

        if(playerRoom != curRoom)
        {
            preRoom = curRoom; 
            curRoom = playerRoom;

            changeRoom = true;
        }



        if(changeRoom)
        {

            //curRoom-1의 방은 현재 플레이어가 위치한 방이니깐 padein
            //preRoom-1의 방은 현재 플레이어가 위치하지않는 방이니간 pade out
            Stop_Coroutine_Padeout();
            Stop_Coroutine_Padein();

            StartCoroutine("ExBox_FadeOut");
            StartCoroutine("ExBox_FadeIn");

            changeRoom = false;
        }
        

    }



    IEnumerator ExBox_FadeIn()
    {

        //검정색으로.. 어둡게 다시.. 생겨나게,..
        float FadeCount = 0; //처음 알파값

        while (FadeCount < 1.0f)
        {
            FadeCount += 0.01f;
            Rooms_SR[preRoom - 1].color = new Color(0, 0, 0, FadeCount);

            yield return new WaitForSeconds(0.01f);
        }
        if (FadeCount <= 1.0f)
        {
            Stop_Coroutine_Padein();
        }
    }

    IEnumerator ExBox_FadeOut()
    {

        //투명하게 다시 사라지게
        float FadeCount = 1; //처음 알파값

        while (FadeCount > 0f)
        {
            FadeCount -= 0.01f;
            Rooms_SR[curRoom - 1].color = new Color(0, 0, 0, FadeCount);

            yield return new WaitForSeconds(0.01f);
        }
        if (FadeCount <= 1.0f)
        {
           Stop_Coroutine_Padeout();
        }
    }


    void Stop_Coroutine_Padeout()
    {
        StopCoroutine("ExBox_FadeOut");
    }
    void Stop_Coroutine_Padein()
    {
        StopCoroutine("ExBox_FadeIn");
    }
}
