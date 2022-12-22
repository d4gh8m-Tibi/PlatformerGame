using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MapTrigger
{
    protected override void OnTrigger()
    {
        GameController.instance.KillPlayer();
    }
}
