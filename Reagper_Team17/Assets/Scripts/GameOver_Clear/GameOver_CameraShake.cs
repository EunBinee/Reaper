using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_CameraShake : MonoBehaviour
{
    //ƒ´∏ﬁ∂Û »ÁµÈ±‚
    public float ShakeAmount;
    public float ShakeTime;

    Vector3 initialPosition;
    Vector3 curPos;


    public GameObject canvas;
    public GameObject description;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShakeTime!=-1)
        {
            if (ShakeTime > 0)
            {
                transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
                curPos = transform.position;
                ShakeTime -= Time.deltaTime;
            }
            else
            {
                ShakeTime = 0;
                if (ShakeTime == 0)
                {
                    Invoke("UI_SetTrue", 3f);//1√  µÙ∑π¿Ã
                    ShakeTime = -1;
                }

                this.transform.position = Vector3.Lerp(curPos, initialPosition, Time.deltaTime * 2f);

            }
        }
       
    }

    void UI_SetTrue()
    {
        //GameObject.Find("Monster_Sound").GetComponent<AudioSource>().Play();
        canvas.SetActive(true);
        description.SetActive(false);
    }

}
