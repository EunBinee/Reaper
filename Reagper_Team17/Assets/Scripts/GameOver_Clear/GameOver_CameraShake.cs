using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_CameraShake : MonoBehaviour
{
    //카메라 흔들기
    public float ShakeAmount;
    public float ShakeTime;

    Vector3 initialPosition;
    Vector3 curPos;

    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            curPos = transform.position;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0;
            this.transform.position = Vector3.Lerp(curPos, initialPosition, Time.deltaTime * 2f);
        }
    }
}
