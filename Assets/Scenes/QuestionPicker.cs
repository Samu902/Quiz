using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MathOp
{
    Sum, Sub, Mul, Div
}

public class QuestionPicker : MonoBehaviour
{
    public static QuestionPicker Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public QuestionData Generate()
    {
        int firstOperand = Random.Range(0, 100);
        int secondOperand = Random.Range(0, 100);

        MathOp operation = (MathOp)Random.Range(0, 3);

        int result = 0;
        string question = "Quanto fa " + firstOperand + " ";

        switch (operation)
        {
            case MathOp.Sum:
                result = firstOperand + secondOperand;
                question += "+";
                break;
            case MathOp.Sub:
                result = firstOperand - secondOperand;
                question += "-";
                break;
            case MathOp.Mul:
                result = firstOperand * secondOperand;
                question += "*";
                break;
            case MathOp.Div:
                result = firstOperand / secondOperand;
                question += "/";
                break;
            default:
                break;
        }
        question += " " + secondOperand + "?";

        int[] wrongResults = new int[3];
        for (int i = 0; i < wrongResults.Length; i++)
            wrongResults[i] = result - (i + 1) * Random.Range(1, 5);

        QuestionData data = (QuestionData)ScriptableObject.CreateInstance(typeof(QuestionData));
        data.question = question;
        data.correctAnswer = result.ToString();
        data.wrongAnswers = System.Array.ConvertAll(wrongResults, x => x.ToString());

        return data;
        //return new QuestionData(question, result.ToString(), System.Array.ConvertAll(wrongResults, x => x.ToString()));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Debug.Log(Generate());
    }
}
