using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCollector : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void IncrementQuestionSeen()
    {
        questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt((correctAnswers * 100) / (float)questionSeen);
    }
}
