using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public float score;
    public float maxScore;
    public Image ScoreBar;
    public TMP_Text ScoreText;
    public TMP_Text VictoryText;

    void Start()
    {
        if (VictoryText != null)
        {
            VictoryText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        UpdateScore(score, maxScore);
        CheckVictory();
    }

    private void CheckVictory()
    {
        if (score >= maxScore && VictoryText != null)
        {
            VictoryText.gameObject.SetActive(true);
            VictoryText.text = "Player 1 Wins!";
        }
    }

    public void UpdateScore(float currentScore, float maxPoints)
    {
        score = Mathf.Min(currentScore, maxPoints);
        ScoreBar.fillAmount = score / maxPoints;
        ScoreText.text = $"{score} / {maxPoints}";
    }
}
