using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private int level;
    private int checkPoint;
    private float[] position;

    public PlayerData(Player player, GameController gameController)
    {
        level = gameController.CurrentLevelIndex;
    }
}
