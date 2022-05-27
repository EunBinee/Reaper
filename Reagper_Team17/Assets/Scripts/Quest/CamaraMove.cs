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

        curCameraPos = playerController.GetFloor(); //�÷��̾ ��ġ�� ���� �޾ƿ´�.
    }
    void LateUpdate()
    {
        //Update���� ���� Update

        /*        if (playerController.isLadder && Input.GetKey(KeyCode.X))
                {
                    //��ٸ��� Ÿ�� ���� ��쿡�� ī�޶��� ��ġ�� �ű��.
                    //float v = Input.GetAxisRaw("Vertical");
                    //tr.position = new Vector3(target.position.x - 0.52f, (target.position.y + (v * playerController.movementSpeed)), tr.position.z);

                    //���� ��ٸ� Ÿ��.. �׳� 2������ ī�޶� �ű��
                    //12 ���ϱ�
                    //���� 
                    if()
                    tr.position = new Vector3(target.position.x - 0.52f, tr.position.y+ tr_2F_1F, tr.position.z);
                }
                else
                {*/
        
        if(playerController.isLadder && Input.GetKey(KeyCode.X) || curCameraPos != playerController.playerPos_Floor)
        {
            //���� ī�޶� ��ġ�� ���̶� player�� ������ġ�� �ٸ���
            curCameraPos = playerController.GetFloor(); // �ٽ� ��ġ�� �޾ƿ�
            //���� player�� Position�� (5�̻�) ���� ���� �ö󰡰ų� �������� �Ʒ��� ������ ���� ��ұ�?

            if(curCameraPos==1)
            {
                //1���̸�..

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
                //2���̸�..

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
