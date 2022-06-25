using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Description : MonoBehaviour
{
    //게임 설명창을 조작하는 스크립트
    //게임을 모두 멈추고 실행이 되며
    //왼쪽 화살표를 누르면 이전 페이지로 오른쪽 화살표를 누르면 이후 페이지로 넘어가게
    //tab키를 눌러서 볼 수 있게 할 거니깐.. 똑같이 tab키 눌러서 닫을 수 있도록하기

    public Sprite[] Des_arr; //게임 설명 
    Image img;
    //현재 이미지 순서
    int cur_ImageNum;
    
    void Start()
    {
        img = GetComponent<Image>();
        cur_ImageNum = 0; //처음 이미지 순서는 0번
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Pre_Image();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Next_Image();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            this.gameObject.SetActive(false);
        }
    }

    void Pre_Image()
    {
        //이전 이미지로 가는 코드
        if (cur_ImageNum == 0)
        {
            //지금 현재 이미지가 맨처음 이미지라면...안넘어가도록;
        }
        else if (cur_ImageNum != 0) 
        {
            //0번이 아니라면..
            cur_ImageNum--;
            img.sprite = Des_arr[cur_ImageNum];
           
        }

    }
    void Next_Image()
    {
        //이후 이미지로 가는 코드

        if (cur_ImageNum == (Des_arr.Length - 1))
        {
            //현재 이미지가 마지막 이미지일 경우
        }
        else if(cur_ImageNum != (Des_arr.Length - 1))
        {
            //마지막 이미지가 아니라면
            cur_ImageNum++;
            img.sprite = Des_arr[cur_ImageNum];
        }

    }

}
