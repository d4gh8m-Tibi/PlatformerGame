using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MapTrigger
{   
    protected override void OnTrigger()
    {
        if (collected) return;

        GameController.instance.SetLastCheckPointId(id);
        Debug.Log("checkpoint reached: " + id);
    }

    public Vector3 Position
    {
        get { return gameObject.transform.position; }
    }

    public override string GetId()
    {
        return GenerateIdString(id);
    }

    public static string GenerateIdString(int id)
    {
        return nameof(CheckPoint) + "_" + id.ToString();
    }

    public override void OnLoadInit()
    {
        collected = true;
    }
}
