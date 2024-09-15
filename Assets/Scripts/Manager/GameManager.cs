using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    private UIController uiController;

    private int point = 0;
    public int Score => point;
    private bool isPlusPoint = true;
    public bool IsPlusPoint => isPlusPoint;
    public void SetPlusPoint(bool value) => isPlusPoint = value;

    public bool isTesting;
    public bool isImortalTesting;

    private void Start()
    {
        point = 0;
    }
    public void PlusPoint()
    {
        point++;
        Observer.Instance.Announce(new Message(EventType.PlusPoint, point)); // refactor this later
    }

    public void EndGame()
    {
        if (isImortalTesting)
            return;

        Observer.Instance.Announce(new Message(EventType.ShowGameOverPanel));
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(GameConstants.GAME_PLAY_SCENE);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        point = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(GameConstants.MAIN_MENU_SCENE);
    }
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
