using System.Collections.Generic;
using UnityEngine;

public enum QuestionType
{
    All, Numeric, Fixed
}

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

    public string this[int index]
    {
        get => index >= 0 && index <= 2 ? wrongAnswers[index] : index <= 3 ? correctAnswer : question;
    }

    public override string ToString()
    {
        return question + "\n" + correctAnswer + "\n" + wrongAnswers[0] + "\n" + wrongAnswers[1] + "\n" + wrongAnswers[2];
    }
}
