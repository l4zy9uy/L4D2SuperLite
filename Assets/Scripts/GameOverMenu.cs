using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI pointTxt;

    private void Start()
    {
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Mouse.current);
    }
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
