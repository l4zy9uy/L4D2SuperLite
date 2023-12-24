using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        // Load lại scene hiện tại (GameOverScene)
        SceneManager.LoadScene("MainScene");
    }
    public void MainMenu()
    {
        // Load scene MainMenu
        SceneManager.LoadScene("MenuScene");
    }
    public void Exit()
    {
        // Thoát ứng dụng
        Application.Quit();
    }
}
