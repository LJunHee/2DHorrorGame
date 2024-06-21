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

    public void GameScene_Stage()
    {
        SceneManager.LoadScene("Stage1");
    }


}
