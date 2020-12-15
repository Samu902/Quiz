using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorRun : Minigame
{
    public TMP_Text titleText;

    public TMP_Text scoreText;

    public List<Color> colors;
    public List<string> colorNames;
    private Color correctColor;
    private int correctInFieldCount;

    public float refreshRate;

    public List<Button> buttons;
    public List<Sprite> sprites;

    private int score;

    public override void StartMinigame(float duration)
    {
        base.StartMinigame(duration);

        score = 0;

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        correctColor = colors[Random.Range(0, colors.Count)];
        titleText.text = "Colpisci tutte le figure " + correctColor;

        while (TotalTime > 0)
        {
            foreach (Button b in buttons)
            {
                ColorBlock cb = ColorBlock.defaultColorBlock;
                cb.normalColor = colors[Random.Range(0, colors.Count)];
                cb.highlightedColor *= cb.normalColor;
                cb.pressedColor *= cb.normalColor;
                cb.selectedColor *= cb.normalColor;
                cb.disabledColor *= cb.normalColor;
                b.colors = cb;
                b.image.sprite = sprites[Random.Range(0, sprites.Count)];
            } 

            yield return new WaitForSeconds(refreshRate);
            TotalTime -= refreshRate;
        }
    }

    public void OnPressButton(Button b)
    {
        if (b.colors.normalColor == correctColor)
            score++;
        else
            score--;

        score = score < 0 ? 0 : score;
        scoreText.text = score.ToString();
    }
}
