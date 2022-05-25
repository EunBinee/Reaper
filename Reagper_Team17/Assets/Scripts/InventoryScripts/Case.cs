using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="1F_Floor")
        {
            //만약 바닥에 닿았을 경우
            //1. collision 비활성화
            //2. sprite 바꾸기
            //3. 카메라 흔들림.

            gameObject.GetComponent<CompositeCollider2D>().enabled = false; //1. collider 비활성화! player가 밖으로 나갈 수 있게..
            playerController.inCase = false;
            //2. 스프라이트 바꾸기는 나중에..

            //3. 카메라 흔들림

        }
    }
}
