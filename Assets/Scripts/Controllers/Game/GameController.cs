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
    private ItemManager itemManager;

    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();
    }

    private bool IsReady => player != null && map != null && itemManager != null;
    private bool initiated = false;

    private void Init()
    {
        if (initiated) return;
        if (MenuData.instance == null)
        {
            initiated = true;
            return;
        };
        if (!MenuData.instance.FromLoad) return;       
        if (!IsReady) return;

        PlayerData data = MenuData.instance.GetPlayerData();

        SpawnPlayer(data);
        LoadMapItems(data);
        initiated = true;
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
        Init();
    }

    public void SetPlayerInstace()
    {
        player = Player.instance;
        Init();
    }
    public void SetItemMangerInstace()
    {
        itemManager = ItemManager.instance;
        Init();
    }


    public int CurrentLevelIndex => SceneManager.GetActiveScene().buildIndex;
    private int lastCheckPointId;
    public int LastCheckPointIdValue => lastCheckPointId;

    public void FinishGame()
    {
        SceneManager.LoadScene(Constants.Scenes.MENU);
    }

    private void SpawnPlayer(PlayerData data)
    {
        player.SetPosition(GetCheckPoint(data.Spawn));
        player.StarCounter = data.Items.Length;//shall be fixed
    }

    private void LoadMapItems(PlayerData data)
    {
        itemManager.LoadItemsOnLoad();
    }


    public void KillPlayer()
    {
        player.SetPosition(GetCheckPoint());
    }

    private Vector3 GetCheckPoint(string checkPointId = null)
    {
        if(checkPointId == null)
        {
            return map.GetCheckPointPositionById(CheckPoint.GenerateIdString(lastCheckPointId));
        }
        else
        {
            return map.GetCheckPointPositionById(checkPointId);
        }
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
