using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Level { get; private set; }
    public string[] Items { get; private set; }
    public string Spawn { get; private set; }
    public int[] Enemies { get; private set; }
    public bool PoweredUp { get; private set; }

    public PlayerData(Player player, GameController gameController, ItemManager itemManager)
    {
        Level = gameController.CurrentLevelIndex;
        Spawn = CheckPoint.GenerateIdString(gameController.LastCheckPointIdValue);
        Items = new string[itemManager.Count];

        for (int i = 0; i < Items.Length; ++i)
        {
            Items[i] = itemManager.ElementAt(i);
        }

        PoweredUp = player.IsPoweredUp;
    }
}
