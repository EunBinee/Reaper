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
            //���� �ٴڿ� ����� ���
            //1. collision ��Ȱ��ȭ
            //2. sprite �ٲٱ�
            //3. ī�޶� ��鸲.

            gameObject.GetComponent<CompositeCollider2D>().enabled = false; //1. collider ��Ȱ��ȭ! player�� ������ ���� �� �ְ�..
            playerController.inCase = false;
            //2. ��������Ʈ �ٲٱ�� ���߿�..

            //3. ī�޶� ��鸲

        }
    }
}
