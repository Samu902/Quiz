using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New question", menuName = "Question")]
public class QuestionData : ScriptableObject
{
    public string question;
    public string correctAnswer;
    public string[] wrongAnswers;

    public QuestionData()
    {
        question = "Question";
        correctAnswer = "Correct";
        wrongAnswers = new string[] { "Wrong 1", "Wrong 2", "Wrong 3" };
    }

    public QuestionData(string question, string correctAnswer, string[] wrongAnswers)
    {
        this.question = question;
        this.correctAnswer = correctAnswer;
        this.wrongAnswers = wrongAnswers;
    }
}
