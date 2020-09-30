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
    private Dictionary<MathOp, string> opToString;

    private void Awake()
    {
        Instance = this;
        opToString = new Dictionary<MathOp, string>() { { MathOp.Sum, "+" }, { MathOp.Sub, "-" }, { MathOp.Mul, "*" }, { MathOp.Div, "/" } };
    }

    public QuestionData Generate()
    {
        MathOp operation = (MathOp)Random.Range(0, 4);
        int firstOperand = Random.Range(0, 100);
        int secondOperand = operation == MathOp.Div ? Random.Range(1, 100) : Random.Range(0, 100);
        int result = 0;

        switch (operation)
        {
            case MathOp.Sum:
                result = firstOperand + secondOperand;
                break;
            case MathOp.Sub:
                result = firstOperand - secondOperand;
                break;
            case MathOp.Mul:
                result = firstOperand * secondOperand;
                break;
            case MathOp.Div:
                result = firstOperand / secondOperand;
                break;
            default:
                break;
        }

        string question = string.Format("Quanto fa {0} {1} {2}?", firstOperand, opToString[operation], secondOperand);

        int[] wrongResults = new int[3];
        for (int i = 0; i < wrongResults.Length; i++)
            wrongResults[i] = result - (i + 1) * Random.Range(1, 5);

        QuestionData data = (QuestionData)ScriptableObject.CreateInstance(typeof(QuestionData));
        data.question = question;
        data.correctAnswer = result.ToString();
        data.wrongAnswers = System.Array.ConvertAll(wrongResults, x => x.ToString());

        return data;
    }
}
