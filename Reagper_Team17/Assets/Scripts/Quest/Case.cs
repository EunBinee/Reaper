using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Case : MonoBehaviour
{
    public CameraShake cameraShake;
    public GameObject EnemyGenerator;
    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="1F_Floor")
        {
            cameraShake.ShakeTime(0.15f,0.4f);
            cameraShake.Shake = true;

            Invoke("StartEnemyGenerator", 15);
        }
    }

    void StartEnemyGenerator()
    {
        EnemyGenerator.SetActive(true);
    }

}
