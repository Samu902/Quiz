using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TMP_Text questionText;
    public Image questionImage;
    public List<TMP_Text> answerTexts;

    //[Header("Debug variable"), Tooltip("Change it to try different types of question")]
    //public QuestionType generatedQuestionType;

    //public Timer timer;

    public void Visualize(QuestionType type)
    {
        QuestionData q = QuestionPicker.Instance.Generate(type);
        questionText.text = q.question;

        bool[] usedSlots = new bool[] { false, false, false, false };

        for (int i = 0, qIndex = 0; i < answerTexts.Count; i++)
        {
            do
                qIndex = Random.Range(0, answerTexts.Count);
            while (usedSlots[qIndex]);
            usedSlots[qIndex] = true;
            answerTexts[i].text = q[qIndex];
        }

        if(q.questionSprite != null)    //type da Any è diventato ByImage
        {
            questionText.text = "";
            questionImage.color = Color.white;
            questionImage.sprite = q.questionSprite;
        }
        else
        {
            questionImage.color = new Color(0, 0, 0, 0);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //        Visualize(generatedQuestionType);

    //    if (Input.GetKeyDown(KeyCode.T))
    //        timer.StartTimer(Random.Range(2f, 10f));
    //}
}
