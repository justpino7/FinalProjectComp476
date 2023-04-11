using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public int highScore;
    public Text scoreText; // Reference to the UI Text component

    private void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void AddPoints(int points)
    {
        score += points;
    }

    private void Update()
    {
        // Update the score text
        scoreText.text = "Score: " + score;
    }

    private void OnDestroy()
    {
        // Save the high score to PlayerPrefs if the current score is higher
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
