using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource bgThemeSource;
    
    private PlayerInput playerInput;

    private Player player;
    private MapGeneration map;

    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();
    }

    private void Init()
    {
        lastCheckPointId = 0;
    }

    public bool IsRunning => Time.timeScale > Constants.Game.GAMESPEEDSTOPPED;

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = Constants.Game.GAMESPEEDSTOPPED;
        PlayStopBackGroundTheme();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = Constants.Game.GAMESPEED;
        PlayStopBackGroundTheme();
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

    public void SetMapInstance()
    {
        map = MapGeneration.instance;
    }

    public void SetPlayerInstace()
    {
        player = Player.instance;
    }

    public int CurrentLevelIndex => SceneManager.GetActiveScene().buildIndex;
    private int lastCheckPointId;
    public int LastCheckPointIdValue => lastCheckPointId;

    public void FinishGame()
    {
        SceneManager.LoadScene(Constants.Scenes.MENU);
    }

    public void SpawnPlayer()
    {
        SceneManager.LoadScene(CurrentLevelIndex);
        player.SetPosition(GetCheckPoint());
    }

    public void KillPlayer()
    {
        player.SetPosition(GetCheckPoint());
    }

    private Vector3 GetCheckPoint()
    {
        return map.GetCheckPointPositionById(CheckPoint.GenerateIdString(lastCheckPointId));
    }

    public void SetLastCheckPointId(int id)
    {
        lastCheckPointId = id;
    }

    public void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect == null) return;
        this.effectSource.clip = soundEffect;
        effectSource.Play();
    }

    private void PlayStopBackGroundTheme()
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
