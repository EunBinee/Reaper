using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //==========================================
    //���� ���� ȭ�� ��ư
    public void GameStart_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("GameScene");
    }
    //==============================================
    //���� ����, ���� Ŭ����
    public void GoStartScene_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("StartScene");
    }
    public void ReStart_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("GameScene");
    }
    public void Exit_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        Application.Quit();
    }
}
