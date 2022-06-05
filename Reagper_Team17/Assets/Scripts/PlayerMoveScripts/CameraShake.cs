using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    public float shakeAmount;
    float shakeTime;
    Vector3 initiaIpostion;
    Vector3 curPos;
    

    public bool Shake = false;
    bool getPos = false;
    void Start()
    {
        shakeTime = 0.15f;
        //initiaIpostion = transform.position;
        initiaIpostion = new Vector3(player.transform.position.x - 0.15f, player.transform.position.y+2.3f, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = new Vector3(player.transform.position.x - 0.15f, player.transform.position.y + 2.3f, this.transform.position.z);


        if (Shake)
        {
            if (!getPos)
            {
                //initiaIpostion = transform.position;
                Getpos();
                getPos = true;
            }
            else if (shakeTime > 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, Random.insideUnitSphere.y * shakeAmount + initiaIpostion.y, this.transform.position.z);
                curPos = transform.position;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                Shake = false;
                getPos = false;
                shakeTime = 0;
                this.transform.position = Vector3.Lerp(curPos, initiaIpostion, Time.deltaTime * 2f);
                


            }
        }
        else
        {
           }

    }
    public void ShakeTime(float time,float amount )
    {
        shakeTime = time;
        shakeAmount = amount;
    }
    public void Getpos()
    {
        //initiaIpostion = transform.position;
        initiaIpostion = new Vector3(player.transform.position.x - 0.15f, player.transform.position.y + 2.3f, this.transform.position.z);
    }
    public void Stop()
    {
        shakeTime = 0;
    }
}
