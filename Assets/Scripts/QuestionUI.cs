using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TMP_Text questionText;
    public List<TMP_Text> answerTexts;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Visualize(QuestionType.Any);
    }
}
