using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MapTrigger
{
    protected override void OnTrigger()
    {
        GameController.instance.KillPlayer();
    }

    public override string GetId() 
    {
        return nameof(DeathZone) + "_" + id.ToString(); 
    }
}
