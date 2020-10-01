using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPicker : MonoBehaviour
{
    public static QuestionPicker Instance { get; private set; }

    public List<QuestionData> premadeQuestions;

    private Dictionary<MathOp, string> mathOpToString;

    private void Awake()
    {
        Instance = this;
        mathOpToString = new Dictionary<MathOp, string>() { { MathOp.Sum, "+" }, { MathOp.Sub, "-" }, { MathOp.Mul, "*" }, { MathOp.Div, "/" } };
    }

    public QuestionData Generate(QuestionType type)
    {
        string question = "";
        string correctAnswer = "";
        string[] wrongAnswers = new string[3];

        if (type == QuestionType.Any)
            type = (QuestionType)Random.Range(1, 3);

        switch (type)
        {
            case QuestionType.Numeric:
                MathOp operation = (MathOp)Random.Range(0, 4);
                int firstOperand = Random.Range(-20, 21);
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

                question = string.Format("Quanto fa {0} {1} {2}?", firstOperand, mathOpToString[operation], secondOperand);
                correctAnswer = result.ToString();
                for (int i = 0; i < 3; i++)
                    wrongAnswers[i] = (result - (i + 1) * Random.Range(1, 5)).ToString();
                break;
            case QuestionType.Premade:
                QuestionData randomQ = premadeQuestions[Random.Range(0, premadeQuestions.Count)];
                question = randomQ.question;
                correctAnswer = randomQ.correctAnswer;
                wrongAnswers = randomQ.wrongAnswers;
                break;
            default:
                break;
        }

        QuestionData data = ScriptableObject.CreateInstance<QuestionData>();
        data.question = question;
        data.correctAnswer = correctAnswer;
        data.wrongAnswers = wrongAnswers;

        return data;
    }
}
