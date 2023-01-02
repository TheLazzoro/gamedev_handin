using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()  {
        Application.Quit();
    }
}
