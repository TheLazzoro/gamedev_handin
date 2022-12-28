using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.enabled = false;
        Timer.OnTimerEnd += Timer_OnTimerEnd;
    }

    private void Timer_OnTimerEnd()
    {
        textMesh.enabled = true;
        int t1 = ScoreManager.scoreTeam1;
        int t2 = ScoreManager.scoreTeam2;

        if (t1 > t2)
            textMesh.text = "Team 1 Wins!";
        else if (t1 < t2)
            textMesh.text = "Team 2 Wins!";
        else
            textMesh.text = "Tie!";
    }

    private void OnDestroy()
    {
        Timer.OnTimerEnd -= Timer_OnTimerEnd;
    }

}
