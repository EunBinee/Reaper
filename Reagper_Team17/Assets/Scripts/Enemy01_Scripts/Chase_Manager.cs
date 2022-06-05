using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Manager : MonoBehaviour
{
    public EnemyMove enemyMove;
    public PlayerController playerController;

    bool checking = false; //Stay2d����enemyMove.CheckDirec();�� ������ �ݺ��ϴ°� �Ⱦ // ������ Ǯ���� checking�� �ٽ� Ǭ��.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(enemyMove.transform.position.x, enemyMove.transform.position.y + 1.5f, enemyMove.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            //���� Chase�� �������� player�� ���´ٸ� �ٷ� ���� �����̿�~
            enemyMove.CheckDirec();
            if(enemyMove.isChasing == false && playerController.ishiding == true)
            {
                //���� ���� ���¿��� ���´ٸ�, ���� ���ϵ���
                enemyMove.isChasing = false;
            }
            else
            {
                enemyMove.isChasing = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(checking)
            {
                enemyMove.CheckDirec();
                checking = true;
            }
            if (enemyMove.isChasing == false && playerController.ishiding == true)
            {
                //�������ϴ� ���� �ƴϰ�, �����ִ� ���̿��߸�.. ��������.
                enemyMove.isChasing = false;
            }
            else
            {
                //���� ���� ���̶��, ��� ������.
                //ishiding�� true�� �ӵ� ��� ����
                enemyMove.isChasing = true;
            }
        }
    }
}
