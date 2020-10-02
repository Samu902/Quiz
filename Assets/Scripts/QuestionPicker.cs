using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPicker : MonoBehaviour
{
    public static QuestionPicker Instance { get; private set; }

    private Dictionary<MathOp, string> mathOpToString;

    public List<QuestionData> premadeQuestions;

    public List<TextAsset> listFiles;
    private Dictionary<ListType, List<string>> itemLists;

    private void Awake()
    {
        Instance = this;

        mathOpToString = new Dictionary<MathOp, string>() { { MathOp.Sum, "+" }, { MathOp.Sub, "-" }, { MathOp.Mul, "×" }, { MathOp.Div, "÷" } };

        itemLists = new Dictionary<ListType, List<string>>();
        for (int i = 0; i < listFiles.Count; i++)
        {
            itemLists[(ListType)i] = new List<string>(listFiles[i].text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries));
            foreach (var item in itemLists[(ListType)i])
            {
                Debug.Log(item);
            }
        }
    }

    public QuestionData Generate(QuestionType type)
    {
        string question = "";
        string correctAnswer = "";
        string[] wrongAnswers = new string[3];

        if (type == QuestionType.Any)
            type = (QuestionType)Random.Range(1, System.Enum.GetNames(typeof(QuestionType)).Length);

        switch (type)
        {
            case QuestionType.Numeric:
                int firstOperand, secondOperand, result;
                MathOp operation = (MathOp)Random.Range(0, 4);

                switch (operation)
                {
                    case MathOp.Sum:
                        firstOperand = Random.Range(0, 51);
                        secondOperand = Random.Range(0, 51);
                        result = firstOperand + secondOperand;
                        break;
                    case MathOp.Sub:
                        firstOperand = Random.Range(0, 51);
                        secondOperand = Random.Range(0, 51);
                        result = firstOperand - secondOperand;
                        break;
                    case MathOp.Mul:
                        firstOperand = Random.Range(0, 21);
                        secondOperand = Random.Range(0, 21);
                        result = firstOperand * secondOperand;
                        break;
                    case MathOp.Div:
                        firstOperand = Random.Range(0, 21);
                        do
                            secondOperand = Random.Range(1, 21);
                        while (firstOperand % secondOperand != 0);
                        result = firstOperand / secondOperand;
                        break;
                    default:
                        firstOperand = secondOperand = result = 0;
                        break;
                }

                question = string.Format("Quanto fa {0} {1} {2}?", firstOperand, mathOpToString[operation], secondOperand);
                correctAnswer = result.ToString();
                for (int i = 0; i < 3; i++)
                    wrongAnswers[i] = (result - (i + 1) * Random.Range(1, 8)).ToString();

                break;
            case QuestionType.Premade:
                QuestionData randomQ = premadeQuestions[Random.Range(0, premadeQuestions.Count)];
                question = randomQ.question;
                correctAnswer = randomQ.correctAnswer;
                wrongAnswers = randomQ.wrongAnswers;

                break;
            case QuestionType.ByList:
                ListType category = (ListType)Random.Range(0, itemLists.Count);
                int randomIndex = Random.Range(0, itemLists[category].Count);
                question = string.Format("Quali di questi è un {0}?", itemLists[category][randomIndex]);
                correctAnswer = itemLists[category][randomIndex];

                //do
                //    qIndex = Random.Range(0, answerTexts.Count);
                //while (usedSlots[qIndex]);
                //sono qui
                break;
            default:
                Debug.LogError("This question type doesn't exist");
                break;
        }

        QuestionData data = ScriptableObject.CreateInstance<QuestionData>();
        data.question = question;
        data.correctAnswer = correctAnswer;
        data.wrongAnswers = wrongAnswers;

        return data;
    }
}
