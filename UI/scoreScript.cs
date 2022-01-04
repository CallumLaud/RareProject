using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    void Start()
    {
        scoreText.text = "Score: " + score;//initialises score UI  
    }

    public void changeScore(int newScore)//displays new score
    {
        score = newScore;
        scoreText.text = "Score: " + score;
    }
}
