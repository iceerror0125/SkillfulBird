using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public void StartGame()
    {
        // load loading ui here
        GameManager.Instance.StartGame();
    }
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
