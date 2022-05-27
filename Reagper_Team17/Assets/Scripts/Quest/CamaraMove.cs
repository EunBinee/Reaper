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

    private bool check_Change=false;
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

        /*        if (playerController.isLadder && Input.GetKey(KeyCode.X))
                {
                    //사다리를 타고 있을 경우에는 카메라의 위치를 옮긴다.
                    //float v = Input.GetAxisRaw("Vertical");
                    //tr.position = new Vector3(target.position.x - 0.52f, (target.position.y + (v * playerController.movementSpeed)), tr.position.z);

                    //만약 사다를 타면.. 그냥 2층으로 카메라를 옮기기
                    //12 더하기
                    //만약 
                    if()
                    tr.position = new Vector3(target.position.x - 0.52f, tr.position.y+ tr_2F_1F, tr.position.z);
                }
                else
                {*/
        
        if(playerController.isLadder && Input.GetKey(KeyCode.X) || curCameraPos != playerController.playerPos_Floor)
        {
            //만약 카메라가 위치한 층이랑 player의 현재위치가 다르면
            curCameraPos = playerController.GetFloor(); // 다시 위치를 받아옴
            //만약 player의 Position이 (5이상) 일정 수준 올라가거나 내려가면 아래로 내리는 것은 어떠할까?

            if(curCameraPos==1)
            {
                //1층이면..

                if(!check_Change)
                {
                    check_Change = true;
                    tr_1F = tr.position.y - 12;
                }

                if (target.position.y>=5.0f)
                {
                    tr.position = new Vector3(target.position.x - 0.52f, tr_1F, tr.position.z);
                }
            }
            if (curCameraPos == 2)
            {
                //2층이면..

                if (!check_Change)
                {
                    check_Change = true;
                    tr_2F = tr.position.y + 12;
                }

                if (target.position.y <= 5.0f)
                {
                    tr.position = new Vector3(target.position.x - 0.52f, tr_2F, tr.position.z);
                }
            }
        }
        else
        {

            check_Change = false;
            tr.position = new Vector3(target.position.x - 0.52f, tr.position.y, tr.position.z);
        }


    }
}
