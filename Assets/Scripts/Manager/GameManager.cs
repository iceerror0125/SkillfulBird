using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private TextMeshProUGUI pointText;
    private int point = 0;
    public int Point => point;
    private bool isPlusPoint = true;
    public bool IsPlusPoint => isPlusPoint;
    public void SetPlusPoint(bool value) => isPlusPoint = value;

    private void Start()
    {
        pointText.text = "0";
    }
    public void PlusPoint()
    {
        point++;
        pointText.text = point.ToString();
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        // open "Game Over" dialog
    }
}
