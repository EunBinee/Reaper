using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    float time = 0;
    float maxtime = 7.0f;

    bool EndPortal = false; //만약 false인 경우, 막 처음 생성 된 경우, true인 경우 이제 사라질 포탈
    void Start()
    {
        maxtime = Random.Range(4, 7);
    }

    // Update is called once per frame
    void Update()
    { 
        if(!EndPortal) //갓 태어난 포탈일 경우
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
