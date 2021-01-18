using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CarUIController carUIController;
    MainUIController mainUIController;

    [SerializeField] private string playerName;
    [SerializeField] private Sprite flag;

    public string[] aiNames;
    public Sprite[] aiFlags;

    private void Awake()
    {
        carUIController = GameObject.FindGameObjectWithTag("CarUI").GetComponent<CarUIController>();
        mainUIController = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUIController>();

        StopGameFirst();
    }

    private void Start()
    {
        carUIController.PlayerName = playerName;
        carUIController.Flag = flag;

        carUIController.SetPlayerInformations();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        mainUIController.CloseStartUI();
    }

    public void StopGameFirst()
    {
        Time.timeScale = 0;
    }

    public void StopGameLosing()
    {
        Time.timeScale = 0;
        mainUIController.OpenFinishUI();
        carUIController.GiveBadEmojiToPlayer();
    }

    public void StopGameVictory()
    {
        mainUIController.OpenFinishUI();
        mainUIController.OpenVictoryUI();

        CarController carController = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        carController.animator.SetTrigger("finish");
        carController.isAlive = false;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
