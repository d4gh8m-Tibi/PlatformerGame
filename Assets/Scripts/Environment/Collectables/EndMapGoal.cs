using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMapGoal : Collectable
{
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            GameController gameController = GameController.instance;
            gameController.FinishGame();
        }        
    }

    protected override void SetCollider(BoxCollider2D boxCollider)
    {
        base.SetCollider(boxCollider);
    }
}
