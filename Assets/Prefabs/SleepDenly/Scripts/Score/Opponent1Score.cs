using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Opponent1Score : MonoBehaviour
{
    public static float playerScore;
    public static float playerMaxScore;
    public float score;
    public float maxScore;
    public Image ScoreBar;
    public TMP_Text ScoreText;
    public TMP_Text VictoryText; // Add victory text reference

    void Start()
    {
        playerScore = score;
        playerMaxScore = maxScore;
    }

    void Update()
    {
        UpdateScore(score, playerMaxScore);
        CheckVictory();
    }

    public void UpdateScore(float score, float playerMaxScore)
    {
        ScoreBar.fillAmount = score / playerMaxScore;

        if (score >= playerMaxScore)
        {
            score = playerMaxScore;
        }

        ScoreText.text = score + " / " + playerMaxScore;
    }

    private void CheckVictory()
    {
        if (score >= playerMaxScore && VictoryText != null)
        {
            VictoryText.gameObject.SetActive(true);
            VictoryText.text = "Player 2 Wins!";
        }
    }
}
