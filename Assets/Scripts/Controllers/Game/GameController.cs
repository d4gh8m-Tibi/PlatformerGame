using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    private PlayerInput playerInput;
    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();
    }

    public bool IsRunning => Time.timeScale > Constants.Game.GAMESPEEDSTOPPED;

    public void PauseGame()
    {
        Time.timeScale = Constants.Game.GAMESPEEDSTOPPED;
    }

    public void ResumeGame()
    {
        Time.timeScale = Constants.Game.GAMESPEED;
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

    public void FinishGame()
    {
        SceneManager.LoadScene(Constants.Scenes.MENU);
    }
}
