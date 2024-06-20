using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesMove : MonoBehaviour
{

    public void GameScene_StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GameScene_Prologue()
    {
        SceneManager.LoadScene("Prologue");
    }
    public void GameScene_GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameScene_Stage01()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void GameScene_Stage02()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void GameScene_Stage03()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void GameScene_Stage04()
    {
        SceneManager.LoadScene("Stage4");
    }

    public void GameScene_Stage05()
    {
        SceneManager.LoadScene("Stage5");
    }

    public void GameScene_Stage06()
    {
        SceneManager.LoadScene("Stage6");
    }

    public void GameScene_Stage07()
    {
        SceneManager.LoadScene("Stage7");
    }


}
