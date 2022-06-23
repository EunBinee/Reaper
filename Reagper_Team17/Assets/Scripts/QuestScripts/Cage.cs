using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cage : MonoBehaviour
{
    public CameraShake cameraShake;
    public GameObject EnemyGenerator;

    SpriteRenderer sr;
    public Sprite brokenCase;

    //오디오 소스
    /*AudioSource audioSource; //케이지 안의 오디오 소스
    public AudioClip Falling_S;*/


    private void Start()
    {
        /*audioSource = GetComponent<AudioSource>();*/
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="1F_Floor")
        {
            /*audioSource.clip = Falling_S;
            audioSource.Play();*/

            //오디오
            GameObject.Find("Falling_S").GetComponent<AudioSource>().Play();

            cameraShake.ShakeTime(0.15f,0.4f);
            cameraShake.Shake = true;

            //부서진 스프라이트로 바꾸기
            sr.sprite = brokenCase;


            Invoke("StartEnemyGenerator", 15);
        }
    }

    void StartEnemyGenerator()
    {
        EnemyGenerator.SetActive(true);
    }

}
