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

                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "���� ������ ����Ű�� �̵��� �� �ֽ��ϴ�.\nZŰ�� ���� �뽬�� �� �� �ֽ��ϴ�. ü���� ������ �����ϼ���.";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
            case 1:
                //isLadder�� true�϶�

                if (!arr.Contains(num))
                {
                    arr.Add(num);

                    Debug.Log("text" + num);
                    Ex_text.text = "��(��),��(��) ����Ű�� ������ �ö� �� �ֽ��ϴ�.";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                
                break;
            case 2:
                //ù��° �������� ������ ���
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "C�� ������ ������ ȹ���� �� �ֽ��ϴ�.";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
            case 3:
                //ù��° ������ ���� ��ҿ� ����..
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "C�� ���� �������� ����� �� �ֽ��ϴ�.\nŹ���� ���ʿ��� C�� ����������.";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
            case 4:
                //ù��° ��Ʈ�� �ٰ�����.
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "����(��) ����Ű�� ���� ��Ʈ�� ��� Ǯ �� �ֽ��ϴ�";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
            case 5:
                //���� ���� �ٰ�����,
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "��Ʈ �� �׸����� � ������ �����غ���, �ùٸ� ������� �������� ���߼���. ";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;

            case 6:
                //�ι�° ��Ʈ�� �ٰ����ٸ�..
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "������ ����, ���踦 ã�ư�����.";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
            case 7:
                //����� ��Ī ���� ����..
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "���� ��Ī���� ���弼��.";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
            case 8:
                //hide�� ������...
                if (!arr.Contains(num))
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "�Ʒ���(��) ����Ű�� ���� ����";

                    StopCoroutine("Ex_Text_FadeOut");
                    StopCoroutine("Ex_Text_FadeIn");

                    StartCoroutine("Ex_Text_FadeIn");
                }
                break;
        }


        
    }

    public void ReMoveText_Case(int num)
    {
        if (arr.Contains(num))
        {
            arr.Remove(num);
        }
    }




    //=====================================================
    //���� �� ���̵��ξƿ�

    IEnumerator Ex_Text_FadeOut()
    {
        yield return new WaitForSeconds(4f); //4�ʵ��� ��ٸ��� ��

        int i = 10;
        while (i >= 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color color = Ex_text.color;
            color.a = f;
            Ex_text.color = color;
            yield return new WaitForSeconds(0.02f);
        }
        
    }

    IEnumerator Ex_Text_FadeIn()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            Color color = Ex_text.color;
            color.a = f;
            Ex_text.color = color;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(Ex_Text_FadeOut());
    }





}