using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public float shakeAmount;
    float shakeTime;
    Vector3 initiaIpostion;
    Vector3 curPos;

    public bool Shake = false;
    bool getPos = false;
    void Start()
    {
        shakeTime = 0.15f;
        initiaIpostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Shake)
        {
            if(!getPos)
            {
                initiaIpostion = transform.position;
                //Getpos();
                getPos = true;
            }
            else if (shakeTime > 0)
            {
                transform.position = new Vector3(transform.position.x, Random.insideUnitSphere.y * shakeAmount + initiaIpostion.y, transform.position.z);
                curPos = transform.position;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                Shake = false;
                getPos = false;
                shakeTime = 0;
                transform.position = Vector3.Lerp(curPos, initiaIpostion, Time.deltaTime * 2f);
                
            }
        }
        
        
    }
    public void ShakeTime(float time,float amount )
    {
        shakeTime = time;
        shakeAmount = amount;
    }
    public void Getpos()
    {
        initiaIpostion = transform.position;
    }
    public void Stop()
    {
        shakeTime = 0;
    }
}
