using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quest_Explanation : MonoBehaviour
{
   // public GameObject text;
    public Text Ex_text; //설명을 위한 text..
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
                //케이지 안에 있을 때. (맨처음 시작)
               
                if (arr.Contains(num))
                {
                    //만약 0이 이미 들어있다면..
                    //Ex_text.text = "";
                }
                else
                {
                    arr.Add(num);
                    Debug.Log("text" + num);
                    Ex_text.text = "왼쪽 오른쪽 방향키로 이동할 수 있습니다.\nZ키를 눌러 대쉬를 할 수 있습니다. 체력이 있으니 조심하세요.";
                }
                break;
            case 1:
                //isLadder이 true일때
                
                if (arr.Contains(num))
                {
                    //만약 0이 이미 들어있다면..

                    //Ex_text.text = "";
                }
                else
                {
                    arr.Add(num);

                    Debug.Log("text" + num);
                    Ex_text.text = "상(↑),하(↓) 방향키를 눌러서 올라갈 수 있습니다.";
                }
                
                break;
            case 2:
                //첫번째 아이템을 만났을 경우
                Ex_text.text = "C를 눌러서 아이템 획득하고, 사용할 수 있습니다.";
                break;
            case 3:
                //첫번째 아이템 지정 장소에 가면..
                Ex_text.text = "C를 눌러보세요. 아이템을 사용할 수 있습니다.";
                break;
            case 4:
                //첫번째 힌트에 다가가면.
                Ex_text.text = "위쪽(↑) 방향키를 눌러 힌트와 퀴즈를 풀 수 있습니다";
                break;
            case 5:
                //색깔 퍼즐에 다가가면,
                Ex_text.text = "힌트 속 그림들이 어떤 색인지 생각해보고, 올바른 순서대로 색구슬을 맞추세요. ";
                break;

            case 6:
                //두번째 힌트에 다가간다면..
                Ex_text.text = "지도를 보고, 열쇠를 찾아가세요.";
                break;
            case 7:
                //열쇠로 대칭 방을 열면..
                Ex_text.text = "방을 대칭으로 만드세요.";
                break;
            case 8:
                //hide에 닿으면...
                Ex_text.text = "아래쪽(↓) 방향키를 눌러 숨을 수 있습니다.";
                break;
        }



        //StartCoroutine("");
    }
}
