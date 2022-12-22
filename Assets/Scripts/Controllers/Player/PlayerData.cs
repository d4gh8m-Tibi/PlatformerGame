using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int level;
    private string[] items;
    private int[] enemies;

    public PlayerData(Player player, GameController gameController, ItemManager itemManager)
    {
        level = gameController.CurrentLevelIndex;
        items = new string[itemManager.Count];
        items[0] = CheckPoint.GenerateIdString(gameController.LastCheckPointIdValue);

        for (int i = 0; i < items.Length; ++i)
        {
            items[i] = itemManager.ElementAt(i);
        }
    }
}
