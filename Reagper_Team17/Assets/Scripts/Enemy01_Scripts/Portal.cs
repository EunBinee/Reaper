using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    float time = 0;
    float maxtime = 7.0f;

    bool EndPortal = false; //���� false�� ���, �� ó�� ���� �� ���, true�� ��� ���� ����� ��Ż
    void Start()
    {
        maxtime = Random.Range(4, 7);
    }

    // Update is called once per frame
    void Update()
    { 
        if(!EndPortal) //�� �¾ ��Ż�� ���
        {
            time += Time.deltaTime;
            
            if()

            if (time >maxtime)
            {
                time = 0;
            }
        }

    }
}
