using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject minigamePanel;

    public Timer questionTimer;
    public Timer gameTimer;
    public QuestionUI questionUI;
    private bool timerFinished;
    private bool buttonPressed;

    public List<Minigame> minigames;

    private IEnumerator Start()
    {
        //Inizio sfida
        questionPanel.SetActive(true);
        minigamePanel.SetActive(false);

        //prima,seconda e terza domanda: 10s di cronometro o risposta
        List<QuestionType> alreadyDisplayed = new List<QuestionType>();
        for (int i = 0; i < 3; i++)
        {
            QuestionType currentType;
            do
                currentType = (QuestionType)Random.Range(1, System.Enum.GetNames(typeof(QuestionType)).Length);
            while (alreadyDisplayed.Contains(currentType));

            alreadyDisplayed.Add(currentType);

            //if (alreadyDisplayed.Count == System.Enum.GetNames(typeof(QuestionType)).Length - 1)        //test line
            //    alreadyDisplayed.Clear();

            questionTimer.StartTimer(10);
            questionUI.Visualize(currentType);

            while (true)
            {
                if(timerFinished || buttonPressed) 
                {
                    timerFinished = false;
                    buttonPressed = false;
                    break;
                }
                yield return null;
            }
            questionTimer.ToggleTimer();
            yield return new WaitForSeconds(1f);
            questionTimer.ResetTimer();
        }
        //minigioco: 30s di cronometro/vittoria
        questionPanel.SetActive(false);
        minigamePanel.SetActive(true);

        gameTimer.StartTimer(30);
        Minigame game = minigames[Random.Range(0, minigames.Count)];
        game.StartMinigame(30);
        while (true)
        {
            if (timerFinished || buttonPressed)
            {
                timerFinished = false;
                buttonPressed = false;
                break;
            }
            yield return null;
        }

        //fine sfida -> caricamento scena punteggio finale
        SceneManager.LoadScene("End");
    }

    public void OnAnswerButtonClick(Animator button)
    {
        buttonPressed = true;
        button.SetTrigger("Correct");
    }

    public void OnTimerFinished()
    {
        timerFinished = true;
    }
}
