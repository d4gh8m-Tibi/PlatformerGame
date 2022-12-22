using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MapTrigger
{
    [SerializeField] private int id;

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

    public int GetId()
    {
        return id;
    }
}
