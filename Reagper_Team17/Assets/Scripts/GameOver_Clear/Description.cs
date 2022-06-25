using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Description : MonoBehaviour
{
    //���� ����â�� �����ϴ� ��ũ��Ʈ
    //������ ��� ���߰� ������ �Ǹ�
    //���� ȭ��ǥ�� ������ ���� �������� ������ ȭ��ǥ�� ������ ���� �������� �Ѿ��
    //tabŰ�� ������ �� �� �ְ� �� �Ŵϱ�.. �Ȱ��� tabŰ ������ ���� �� �ֵ����ϱ�

    public Sprite[] Des_arr; //���� ���� 
    Image img;
    //���� �̹��� ����
    int cur_ImageNum;
    
    void Start()
    {
        Time.timeScale = 0;
        img = GetComponent<Image>();
        cur_ImageNum = 0; //ó�� �̹��� ������ 0��
        img.sprite = Des_arr[cur_ImageNum];
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
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }
    }

    void Pre_Image()
    {
        //���� �̹����� ���� �ڵ�
        if (cur_ImageNum == 0)
        {
            //���� ���� �̹����� ��ó�� �̹������...�ȳѾ����;
        }
        else if (cur_ImageNum != 0) 
        {
            //0���� �ƴ϶��..
            cur_ImageNum--;
            img.sprite = Des_arr[cur_ImageNum];
           
        }

    }
    void Next_Image()
    {
        //���� �̹����� ���� �ڵ�

        if (cur_ImageNum == (Des_arr.Length - 1))
        {
            //���� �̹����� ������ �̹����� ���
        }
        else if(cur_ImageNum != (Des_arr.Length - 1))
        {
            //������ �̹����� �ƴ϶��
            cur_ImageNum++;
            img.sprite = Des_arr[cur_ImageNum];
        }

    }

}
