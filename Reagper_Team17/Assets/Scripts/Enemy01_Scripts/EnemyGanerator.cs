using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanerator : MonoBehaviour
{
    public GameObject enemyPrefab; //���� �������� �޾ƿ´�.
    //public Sprite[] sprites;//2���ǽ�������Ʈ�� �޾ƿ´�.

    //�׻� �ʿ� ���� �ִ��� ������ Ȯ��
    bool existEnemy = false;//���� ������ ture, ������ false;


    void Start()
    {
        
    }
    void Update()
    {
        if(!existEnemy)
        {
            //���� ���� ������..����!

        }
    }

    void StartEnemy()
    {
        //�ʿ� ���� ������ 10~15�� ���̿� �����Ѵ�.
        existEnemy = true;
        int rand = Random.Range(10, 16);
        Invoke("createEnemy", rand);
    }
    void createEnemy()
    {
        //���� ��ġ ���ϱ�, 
    }
}
