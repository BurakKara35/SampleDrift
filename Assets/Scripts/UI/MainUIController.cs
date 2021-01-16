using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public GameObject startUI;
    public GameObject finishUI;
    public GameObject victoryUI;

    public Button playButton;
    public Button restartButton;

    private void Awake()
    {
        OpenStartUI();
        CloseFinishUI();
        CloseVictoryUI();
    }

    public void OpenFinishUI()
    {
        finishUI.SetActive(true);
    }

    public void CloseFinishUI()
    {
        finishUI.SetActive(false);
    }

    public void OpenStartUI()
    {
        startUI.SetActive(true);
    }

    public void CloseStartUI()
    {
        startUI.SetActive(false);
    }

    public void OpenVictoryUI()
    {
        victoryUI.SetActive(true);
    }

    public void CloseVictoryUI()
    {
        victoryUI.SetActive(false);
    }
}
