using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public Button pauseButton;
    public GameObject screenSetting, screenDefeat;
    public static GameManagerController instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        pauseButton.onClick.AddListener(() => pauseGame());
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        screenSetting.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        screenSetting.SetActive(false);
        screenDefeat.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        resumeGame();
    }

    public void HomeScene()
    {
        SceneManager.LoadScene("HomeScene", LoadSceneMode.Additive);
    }

    public void YouLose()
    {
        {
            Time.timeScale = 0;
            screenDefeat.SetActive(true);
        }    
    }    

    public void quitGame()
    {
        Application.Quit();
    }
}
