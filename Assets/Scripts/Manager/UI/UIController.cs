using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private GameObject endGamePanel;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(EventType.ResetPoint, ResetPoint);
        Observer.Instance.Subscribe(EventType.PlusPoint, PlusPoint);
        Observer.Instance.Subscribe(EventType.ShowGameOverPanel, ShowGameOverDialog);
    }
    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(EventType.ResetPoint, ResetPoint);
        Observer.Instance.UnSubscribe(EventType.PlusPoint, PlusPoint);
        Observer.Instance.UnSubscribe(EventType.ShowGameOverPanel, ShowGameOverDialog);
    }

    public void ResetPoint(Message msg)
    {
        pointText.text = "0";
    }

    public void PlusPoint(Message msg)
    {
        if (msg.param == null)
        {
            Debug.LogError("Missing point (score) param to plus");
            return;
        }

        int point = (int)msg.param;
        pointText.text = point.ToString();
    }
    public void ShowGameOverDialog(Message msg)
    {
        endGamePanel.SetActive(true);
    }
    public void RestartGame()
    {
        endGamePanel.SetActive(false);
        GameManager.Instance.Restart();
        Observer.Instance.Announce(new Message(EventType.ResetPoint));
    }
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    public void BackToMainMenu()
    {
        GameManager.Instance.GoBackToMainMenu();
    }
}
