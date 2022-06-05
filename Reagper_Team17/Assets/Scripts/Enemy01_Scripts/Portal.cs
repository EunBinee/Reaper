using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    SpriteRenderer sr;
    float time = 0;
    float maxtime = 7.0f;

    bool EndPortal = false; //���� false�� ���, �� ó�� ���� �� ���, true�� ��� ���� ����� ��Ż
    bool StopPortal = true;
    //���� �� ������Ʈ
    public GameObject[] enemyPrefab; //���� �������� �޾ƿ´�.

    public GameObject CurEnemy;
    GameObject CurEnemyCollider;
    Vector3 EnemyPos; //���� ��ġ


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        maxtime = Random.Range(4, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndPortal) //�� �¾ ��Ż�� ���
        {
            time += Time.deltaTime;
            Debug.Log("maxtime : " + maxtime);
            if (time > maxtime)
            {

                EnemyPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

                CurEnemy = Instantiate(enemyPrefab[0], EnemyPos, Quaternion.identity); //������
                CurEnemyCollider = Instantiate(enemyPrefab[1], EnemyPos, Quaternion.identity); //������

                EndPortal = true;
                StopPortal = true;
                sr.color = new Color(1, 1, 1, 0);//�����ϰ� ����

                time = 0;
            }
        }
        if (EndPortal) //�ѹ��̻� �¾ ���
        {
            if (StopPortal) //true �϶�, ����� �����̰� �հ�, ���� ��������� ���� �ž�.
            {

            }
            else if (!StopPortal)//false �϶�, ����� ���� ����� �ž�.
            {
                this.transform.position = new Vector3(CurEnemy.transform.position.x, this.transform.position.y, this.transform.position.z);
                sr.color = new Color(1, 1, 1, 1);//�����ϰ� ����

                //������� �� �Լ�
            }
        }
    }
}