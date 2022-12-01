using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        Time.timeScale = Constants.Game.GAMESPEED;
        SceneManager.LoadScene(Constants.Scenes.MAP1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
