using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    //�÷��̾ �ִ� ��ġ�� �游 ���..�ϴ� ��ũ��Ʈ

    //�÷��̾ �ִ� ���� �޾ƿ´�.
    public PlayerController playerController;
    public int playerRoom;

    public SpriteRenderer[] Rooms_SR;

    //======
    //�÷��̾ ���� �̵��� ��쿡�� ������ �ϵ���
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
        playerRoom = playerController.GetRoom(); //��� �޾ƿ´�.

        if(playerRoom != curRoom)
        {
            preRoom = curRoom; 
            curRoom = playerRoom;

            changeRoom = true;
        }



        if(changeRoom)
        {

            //curRoom-1�� ���� ���� �÷��̾ ��ġ�� ���̴ϱ� padein
            //preRoom-1�� ���� ���� �÷��̾ ��ġ�����ʴ� ���̴ϰ� pade out
            Stop_Coroutine_Padeout();
            Stop_Coroutine_Padein();

            StartCoroutine("ExBox_FadeOut");
            StartCoroutine("ExBox_FadeIn");

            changeRoom = false;
        }
        

    }



    IEnumerator ExBox_FadeIn()
    {

        //����������.. ��Ӱ� �ٽ�.. ���ܳ���,..
        float FadeCount = 0; //ó�� ���İ�

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

        //�����ϰ� �ٽ� �������
        float FadeCount = 1; //ó�� ���İ�

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
