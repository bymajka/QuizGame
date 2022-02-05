using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float TimeToCompleteQuestion;
    [SerializeField] private float TimeToShowCorrectAnswer;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion;
    
    private float timerValue;

    private void Update()
    {
        UpdateTimer();
    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / TimeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = TimeToShowCorrectAnswer;
            }
        }
        else
        {
            timerValue -= Time.deltaTime;
            
            if (timerValue > 0)
            {
                fillFraction = timerValue / TimeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                loadNextQuestion = true;
                timerValue = TimeToCompleteQuestion;
            }
        }
    }
    public void CancelTimer()
    {
        timerValue = 0;
    }
}
