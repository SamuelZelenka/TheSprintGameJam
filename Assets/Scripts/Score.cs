using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public Text highScore;

    public int tempScore = 2;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void setScore()
    {
        // fetch score by end of game
        // compare with highScore, if bigger than highScore, set highScore to score

        score.text = tempScore.ToString();

        if (tempScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", tempScore);
        }

    }
}
