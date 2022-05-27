using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    //카메라가 플레이어를 따라다니도록 구현

    //플레이어
    PlayerController playerController;

    public Transform target; //따라다닐 타겟
    private Transform tr; //카메라 자신의 transform

    //private bool check_Change=false;
    private float tr_1F;
    private float tr_2F;
    private int curCameraPos;

    void Start()
    {
        tr = GetComponent<Transform>();
        //tr_1F -= 12;
        //tr_2F += 12;
        playerController = target.GetComponent<PlayerController>();

        curCameraPos = playerController.GetFloor(); //플레이어가 위치한 층을 받아온다.
    }
    void LateUpdate()
    {
        //Update보다 느린 Update

        if (playerController.isLadder && Input.GetKey(KeyCode.X))
        {
            //사다리를 타고 있을 경우에는 카메라의 위치를 옮긴다.
            //float v = Input.GetAxisRaw("Vertical");
            //tr.position = new Vector3(target.position.x - 0.52f, (target.position.y + (v * playerController.movementSpeed)), tr.position.z);

            //만약 사다를 타면.. 그냥 2층으로 카메라를 옮기기
            //12 더하기
            //만약 
            if (curCameraPos !=playerController.playerPos_Floor)
            {
                //현재 카메라 위치와 player가 위치한 층이 다를 경우
                //1층에서 ->2층ㅇ
                if(playerController.playerPos_Floor==2)
                {
                    curCameraPos = playerController.GetFloor(); // 다시 위치를 받아옴

                    tr.position = new Vector3(target.position.x - 0.52f, tr.position.y + 12, tr.position.z);
                }
                else
                {

                }

            }
        }
        else

        {
            tr.position = new Vector3(target.position.x - 0.52f, tr.position.y, tr.position.z);
        }


    }
}
