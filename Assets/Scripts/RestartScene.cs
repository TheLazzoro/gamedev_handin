using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity;

public class RestartScene : MonoBehaviour
{
    public void StartScene()    {
        ScoreManager scoreboard = new ScoreManager();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
