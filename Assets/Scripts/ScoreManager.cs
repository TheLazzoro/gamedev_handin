using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    int scoreTeam1 = 0;
    int scoreTeam2 = 0;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scoreTeam1.ToString() + " - " + scoreTeam2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
