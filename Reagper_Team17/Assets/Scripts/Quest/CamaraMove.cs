using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    //ī�޶� �÷��̾ ����ٴϵ��� ����

    //�÷��̾�
    PlayerController playerController;

    public Transform target; //����ٴ� Ÿ��
    private Transform tr; //ī�޶� �ڽ��� transform

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

        curCameraPos = playerController.GetFloor(); //�÷��̾ ��ġ�� ���� �޾ƿ´�.
    }
    void LateUpdate()
    {
        //Update���� ���� Update

        if (playerController.isLadder && Input.GetKey(KeyCode.X))
        {
            //��ٸ��� Ÿ�� ���� ��쿡�� ī�޶��� ��ġ�� �ű��.
            //float v = Input.GetAxisRaw("Vertical");
            //tr.position = new Vector3(target.position.x - 0.52f, (target.position.y + (v * playerController.movementSpeed)), tr.position.z);

            //���� ��ٸ� Ÿ��.. �׳� 2������ ī�޶� �ű��
            //12 ���ϱ�
            //���� 
            if (curCameraPos !=playerController.playerPos_Floor)
            {
                //���� ī�޶� ��ġ�� player�� ��ġ�� ���� �ٸ� ���
                //1������ ->2����
                if(playerController.playerPos_Floor==2)
                {
                    curCameraPos = playerController.GetFloor(); // �ٽ� ��ġ�� �޾ƿ�

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
