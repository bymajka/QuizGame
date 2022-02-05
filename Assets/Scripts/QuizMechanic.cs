using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizMechanic : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI Questiontext;
    [SerializeField] QuestionObject currentQuestion;
    [SerializeField] private List<QuestionObject> questions = new List<QuestionObject>();
    
    [Header("Answers")]
    [SerializeField] private GameObject[] AnswerButtons = new GameObject[4];
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;
    private int correctAnswerIndex;
    
    [Header("Timer")]
    [SerializeField] private Image timerImage;
    private Timer timer;
    private bool hasAnsweredEarly = true;
    
    [Header("Score")] 
    [SerializeField] private TextMeshProUGUI ScoreText;
    private ScoreCollector scoreCollector;

    [Header("ProgressBar")] 
    [SerializeField] private Slider slider;

    public bool levelComplete = false;


    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreCollector = FindObjectOfType<ScoreCollector>();
        slider.maxValue = questions.Count;
        slider.value = 0;
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (slider.value == slider.maxValue)
            {
                levelComplete = true;
                return;
            }
            
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            InteractableButtons(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        InteractableButtons(false);
        timer.CancelTimer();
        ScoreText.text = "Score: " + scoreCollector.CalculateScore() + "%";
    }
    private void GetQuestion()
    {
        Questiontext.text = currentQuestion.GetQuestion();
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
        }
    }
    private void InteractableButtons(bool state)
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].GetComponent<Button>().interactable = state;
        }
    }
    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            InteractableButtons(true);
            SetDefaultSprites();
            GetRandomQuestion();
            GetQuestion();
            scoreCollector.IncrementQuestionSeen();
            slider.value++;
        }
    }
    private void SetDefaultSprites()
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }
    private void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (currentQuestion.GetCorrectAnswerIndex() == index)
        {
            Questiontext.text = "Correct!";
            buttonImage = AnswerButtons[index].GetComponent<Image>();
            //ScoreText.text = "Score: " + (CurrentScore + ScorePoints) + "%";
            //CurrentScore += ScorePoints;
            scoreCollector.IncrementCorrectAnswers();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            Questiontext.text = "Sorry, correct answer is " + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            AnswerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>().sprite = correctAnswerSprite;
        }
    }
    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
}
