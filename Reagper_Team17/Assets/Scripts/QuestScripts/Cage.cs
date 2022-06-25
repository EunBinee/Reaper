using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cage : MonoBehaviour
{
    public CameraShake cameraShake;
    public GameObject EnemyGenerator;

    SpriteRenderer sr;
    public Sprite brokenCase;

    //����� �ҽ�
    /*AudioSource audioSource; //������ ���� ����� �ҽ�
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

            //�����
            GameObject.Find("Falling_S").GetComponent<AudioSource>().Play();

            cameraShake.ShakeTime(0.15f,0.4f);
            cameraShake.Shake = true;

            //�μ��� ��������Ʈ�� �ٲٱ�
            sr.sprite = brokenCase;


            Invoke("StartEnemyGenerator", 15);
        }
    }

    void StartEnemyGenerator()
    {
        EnemyGenerator.SetActive(true);
    }

}
