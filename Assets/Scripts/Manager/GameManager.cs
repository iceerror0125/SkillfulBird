using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 1;
    }
    private void Start()
    {
        point = 0;

        Observer.Instance.Announce(new Message(EventType.ResetPoint));
    }
    public void PlusPoint()
    {
        point++;
        Observer.Instance.Announce(new Message(EventType.PlusPoint, point));
    }

    public void EndGame()
    {
        if (isImortalTesting)
            return;

        Time.timeScale = 0;
        Observer.Instance.Announce(new Message(EventType.ShowGameOverPanel));
    }

    public void Restart()
    {
        point = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  
}
