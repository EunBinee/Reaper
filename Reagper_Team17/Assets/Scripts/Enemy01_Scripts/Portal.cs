using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerController playerController;

    SpriteRenderer sr;
    float time = 0;
    float maxtime = 7.0f;

    bool EndPortal = false; //���� false�� ���, �� ó�� ���� �� ���, true�� ��� ���� ����� ��Ż

    //���� �� ������Ʈ
    EnemyGanerator enemyGanerator; //existEnemy�� �����������. ���߿�
    GameObject Enemy_Prefab;
    GameObject Enemy_Prefab_collider;
    GameObject Enemy_Prefab_Light;


    public GameObject CurEnemy;
    EnemyController enemyController;
    GameObject CurEnemyCollider;
    Vector3 EnemyPos; //���� ��ġ


    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerController = Player.GetComponent<PlayerController>();

        sr = GetComponent<SpriteRenderer>();
        maxtime = Random.Range(4, 7);
        enemyGanerator = GameObject.Find("EnemyGanerator").GetComponent<EnemyGanerator>();
        Enemy_Prefab = enemyGanerator.enemyPrefab[0];
        Enemy_Prefab_collider= enemyGanerator.enemyPrefab[1];
        Enemy_Prefab_Light = enemyGanerator.enemyPrefab[2];
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

                CurEnemy = Instantiate(Enemy_Prefab, EnemyPos, Quaternion.identity); //������
                CurEnemyCollider = Instantiate(Enemy_Prefab_collider, EnemyPos, Quaternion.identity); //�� �ݶ��̴� ����
                Enemy_Prefab_Light = Instantiate(Enemy_Prefab_Light, EnemyPos, Quaternion.identity); //�� ����Ʈ ����
                enemyController = CurEnemy.GetComponent<EnemyController>();
                EndPortal = true;
                enemyController.StopPortal = true;
                sr.color = new Color(1, 1, 1, 0);//�����ϰ� ����

                time = 0;
            }
        }
        if (EndPortal) //�ѹ��̻� �¾ ���
        {
            if (enemyController.StopPortal) //true �϶�, ����� �����̰� �հ�, ���� ��������� ���� �ž�.
            {

            }
            else if (!enemyController.StopPortal)//false �϶�, ����� ���� ����� �ž�.
            {
                if(CurEnemy!=null)
                {
                    this.transform.position = new Vector3(CurEnemy.transform.position.x, this.transform.position.y, this.transform.position.z);
                }
                sr.color = new Color(1, 1, 1, 1);//�����ϰ� ����

                //������� �� �Լ�
                Destroy(CurEnemy);
                Destroy(CurEnemyCollider);
                Invoke("Destroy_Portal", 3);
            }
        }
    }

    void Destroy_Portal()
    {
        enemyGanerator.existEnemy = false;
        Destroy(gameObject);
    }

}