using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public static bool gameIsPaused;

    void Update()   {
        if (Input.GetKeyDown(KeyCode.Escape))   {
            
            gameIsPaused = !gameIsPaused;
            Pause();
        }
    }
    
    void Pause()    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Pause game");
        }
        else 
        {
            Time.timeScale = 1;
            Debug.Log("Unpause game");
        }
    }
}
