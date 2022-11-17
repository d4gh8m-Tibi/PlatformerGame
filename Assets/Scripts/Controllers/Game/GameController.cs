using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    private PlayerInput playerInput;
    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();
    }

    public bool IsRunning => Time.timeScale > 0;

    public void PauseGame()
    {
        Time.timeScale = Constants.GAME.GAMESPEEDSTOPPED;
    }

    public void ResumeGame()
    {
        Time.timeScale = Constants.GAME.GAMESPEED;
    }

    public void SetGameSpeedForMenu()
    {
        if (IsRunning)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
}
