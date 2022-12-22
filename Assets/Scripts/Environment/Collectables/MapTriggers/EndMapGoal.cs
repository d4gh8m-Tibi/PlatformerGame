using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMapGoal : MapTrigger
{
    protected override void OnTrigger()
    {
        GameController.instance.FinishGame();
    }
}
