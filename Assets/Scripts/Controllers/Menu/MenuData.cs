using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuData : MonoBehaviour
{
    public static MenuData instance;
    private PlayerData data;
    public bool FromLoad;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        FromLoad = false;
    }

    public void LoadPlayerData()
    {
        FromLoad = true;
        data = SaveController.LoadState();
    }

    public PlayerData GetPlayerData()
    {
        return data;
    }   
}
