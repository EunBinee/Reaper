using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Case : MonoBehaviour
{

    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="1F_Floor")
        {
            //���� �ٴڿ� ����� ���
            //1. collision ��Ȱ��ȭ
            //2. sprite �ٲٱ�
            //3. ī�޶� ��鸲.

            //gameObject.GetComponent<CompositeCollider2D>().enabled = false; //1. collider ��Ȱ��ȭ! player�� ������ ���� �� �ְ�..
           // gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            //2. ��������Ʈ �ٲٱ�� ���߿�..

            //3. ī�޶� ��鸲
        }
    }

}
