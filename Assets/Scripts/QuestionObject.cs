using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuizQuestion", fileName = "NewQuestion")]
public class QuestionObject : ScriptableObject
{
   [TextArea(2,6)]
   [SerializeField] string Question = "Enter new question here";

   [SerializeField] private string[] Answers = new string[4];
   [SerializeField] private int CorrectAnswer;

   public string GetQuestion()
   {
      return Question;
   }
   
   public int GetCorrectAnswerIndex()
   {
      return CorrectAnswer;
   }

   public string GetAnswer(int index)
   {
      return Answers[index];
   }
}