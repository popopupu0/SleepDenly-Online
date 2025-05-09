using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Opponent2Score : MonoBehaviour
{
    public static float playerScore;
    public static float playerMaxScore;
    public float score;
    public float maxScore;
    public Image ScoreBar;
    public TMP_Text ScoreText;

    void Start()
    {
        playerScore = score;
        playerMaxScore = maxScore;
    }

    void Update()
    {
        UpdateScore(score, playerMaxScore);
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
}
