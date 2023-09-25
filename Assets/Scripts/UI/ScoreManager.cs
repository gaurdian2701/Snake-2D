using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private float currentScore;
    private float scoreIncrease;

    private void Awake()
    {
        scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        PlayerController.sizeGrown += IncreaseScore;
        PlayerController.sizeDecreased += DecreaseScore;

        currentScore = 0f;
        scoreIncrease = 5f;
    }

    private void OnDestroy()
    {
        PlayerController.sizeGrown -= IncreaseScore;
        PlayerController.sizeDecreased -= DecreaseScore;
        currentScore = 0f;
        RefreshScoreText();
    }

    public float GetDefaultScoringValue()
    {
        return scoreIncrease;
    }

    public void SetDefaultScoringValue(float newScore)
    {
        scoreIncrease = newScore;
    }

    private void IncreaseScore()
    {
        currentScore += scoreIncrease;
        RefreshScoreText();
    }

    private void DecreaseScore()
    {
        if(currentScore - 1 <= 0f)
            currentScore = 0f;
        else
            currentScore -= 1f;
        RefreshScoreText();
    }

    private void RefreshScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
