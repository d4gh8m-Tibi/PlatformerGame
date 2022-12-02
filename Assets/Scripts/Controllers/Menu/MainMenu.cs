using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource mainMenuTheme;

    public void StartNewGame()
    {
        Time.timeScale = Constants.Game.GAMESPEED;
        SceneManager.LoadScene(Constants.Scenes.MAP1);
        mainMenuTheme.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
