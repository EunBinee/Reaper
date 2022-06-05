using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanerator : MonoBehaviour
{
    public GameObject enemyPrefab; //적의 프리펩을 받아온다.
    //public Sprite[] sprites;//2개의스프라이트를 받아온다.

    //항상 맵에 적이 있는지 없는지 확인
    bool existEnemy = false;//적이 있으면 ture, 없으면 false;


    void Start()
    {
        
    }
    void Update()
    {
        if(!existEnemy)
        {
            //만약 적이 없으면..생성!

        }
    }

    void StartEnemy()
    {
        //맵에 적이 없으면 10~15초 사이에 생성한다.
        existEnemy = true;
        int rand = Random.Range(10, 16);
        Invoke("createEnemy", rand);
    }
    void createEnemy()
    {
        //적의 위치 정하기, 
    }
}
