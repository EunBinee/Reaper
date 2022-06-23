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
    //게임 시작 화면 버튼
    public void GameStart_B()
    {
        GameObject.Find("Button_S").GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("GameScene");
    }
    //==============================================
    //게임 오버, 게임 클리어
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
