using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quest_Explanation : MonoBehaviour
{
   // public GameObject text;
    public Text Ex_text; //������ ���� text..
    public List<int> arr = new List<int>();
    void Start()
    {
        Ex_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }



























   public void Text_Case(int num)
    {
        switch (num)
        {
            case 0:
                //������ �ȿ� ���� ��. (��ó�� ����)
               
                if (arr.Contains(num))
                {
                    //���� 0�� �̹� ����ִٸ�..
                    //Ex_text.text = "";
                }
                else
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "���� ������ ����Ű�� �̵��� �� �ֽ��ϴ�.\nZŰ�� ���� �뽬�� �� �� �ֽ��ϴ�. ü���� ������ �����ϼ���.";
                }
                break;
            case 1:
                //isLadder�� true�϶�
                
                if (arr.Contains(num))
                {
                    //���� 0�� �̹� ����ִٸ�..

                    //Ex_text.text = "";
                }
                else
                {
                    arr.Add(num);

                    Debug.Log("text" + num);
                    Ex_text.text = "��(��),��(��) ����Ű�� ������ �ö� �� �ֽ��ϴ�.";
                }
                
                break;
            case 2:
                //ù��° �������� ������ ���
                Ex_text.text = "C�� ������ ������ ȹ���ϰ�, ����� �� �ֽ��ϴ�.";
                break;
            case 3:
                //ù��° ������ ���� ��ҿ� ����..
                Ex_text.text = "C�� ����������. �������� ����� �� �ֽ��ϴ�.";
                break;
            case 4:
                //ù��° ��Ʈ�� �ٰ�����.
                Ex_text.text = "����(��) ����Ű�� ���� ��Ʈ�� ��� Ǯ �� �ֽ��ϴ�";
                break;
            case 5:
                //���� ���� �ٰ�����,
                Ex_text.text = "��Ʈ �� �׸����� � ������ �����غ���, �ùٸ� ������� �������� ���߼���. ";
                break;

            case 6:
                //�ι�° ��Ʈ�� �ٰ����ٸ�..
                Ex_text.text = "������ ����, ���踦 ã�ư�����.";
                break;
            case 7:
                //����� ��Ī ���� ����..
                Ex_text.text = "���� ��Ī���� ���弼��.";
                break;
            case 8:
                //hide�� ������...
                Ex_text.text = "�Ʒ���(��) ����Ű�� ���� ���� �� �ֽ��ϴ�.";
                break;
        }



        //StartCoroutine("");
    }
}
