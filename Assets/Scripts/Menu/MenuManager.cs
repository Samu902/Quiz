using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject quitPanel;
    [Space]
    public GameObject optionPanel;
    public GameObject shopPanel;

    private void Start()
    {
        mainPanel.SetActive(true);
        quitPanel.SetActive(false);
        optionPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            quitPanel.SetActive(!quitPanel.activeSelf);
    }

    public void SettingsButton()
    {
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void SettingsBackButton()
    {
        mainPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void BattleButton()
    {
        Debug.Log("In cerca di un avversario...");
    }

    public void RankingButton()
    {
        Debug.Log("Classifica...");
    }

    public void ShopButton()
    {
        mainPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void ShopBackButton()
    {
        mainPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    public void ConfirmButton()
    {
        Debug.Log("Hai quittato {non funziona nell'editor}");
        Application.Quit();
    }

    public void DenyButton()
    {
        quitPanel.SetActive(false);
    }
}
