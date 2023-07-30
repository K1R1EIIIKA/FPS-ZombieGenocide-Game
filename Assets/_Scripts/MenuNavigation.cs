using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        Level.Points = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Level.Points = 0;
        Level.IsGameOver = false;
        Level.IsGameStart = false;
        Level.IsActiveCooldown = false;
        CameraMovement.IsLocked = true;
        SceneManager.LoadScene("Game");
    }
}
