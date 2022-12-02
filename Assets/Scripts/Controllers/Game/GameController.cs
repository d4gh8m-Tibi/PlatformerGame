using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource bgThemeSource;
    
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
        PlayStopBGTheme();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = Constants.Game.GAMESPEED;
        PlayStopBGTheme();
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

    public void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect == null) return;
        this.effectSource.clip = soundEffect;
        effectSource.Play();
    }

    private void PlayStopBGTheme()
    {
        if(bgThemeSource.isPlaying)
        {
            bgThemeSource.Pause();
        }
        else
        {
            bgThemeSource.UnPause();
        }
    }
}
