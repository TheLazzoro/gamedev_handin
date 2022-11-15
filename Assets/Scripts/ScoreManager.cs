using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{   
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    int scoreTeam1 = 0;
    int scoreTeam2 = 0;


    private void Awake()    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scoreTeam1.ToString() + " - " + scoreTeam2.ToString();
    }


    public void AddPointToTeam(string GoalTag)
    {
        if (GoalTag == "Goal1") scoreTeam1++;
        else scoreTeam2++;
        scoreText.text = scoreTeam1.ToString() + " - " + scoreTeam2.ToString();
        //source.Play(clip);
    }
}
