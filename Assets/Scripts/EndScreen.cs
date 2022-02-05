using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FinalScoreText;
    [SerializeField] private Button ReplayButton;
    private ScoreCollector _scoreCollector;

    private void Awake()
    {
        _scoreCollector = FindObjectOfType<ScoreCollector>();
    }

    public void ShowFinalScore()
    {
        FinalScoreText.text = "Congratulations!\n Score: " + _scoreCollector.CalculateScore() + "%"; 
    }
}
