using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject pauseMenuUI;
    
    private PlayerInput playerInput;
    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();
    }

    public bool IsRunning => Time.timeScale > Constants.Game.GAMESPEEDSTOPPED;

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = Constants.Game.GAMESPEEDSTOPPED;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
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
