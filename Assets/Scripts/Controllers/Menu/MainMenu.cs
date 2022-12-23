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

    public void ContinueGame()
    {
        MenuData.instance.LoadPlayerData();
        Time.timeScale = Constants.Game.GAMESPEED;
        SceneManager.LoadScene(MenuData.instance.GetPlayerData().Level);
        mainMenuTheme.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
