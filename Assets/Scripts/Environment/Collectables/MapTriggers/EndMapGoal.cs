using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMapGoal : MapTrigger
{
    protected override void OnTrigger()
    {
        GameController.instance.FinishGame();
    }
    public override void OnLoadInit()
    {
        throw new System.NotImplementedException();
    }

    public override string GetId()
    {
        throw new System.NotImplementedException();
    }
}
