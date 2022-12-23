using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapTrigger : Collectable
{    
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {            
            OnTrigger();
            base.OnCollide(coll);
        }
    }

    protected override void SetCollider(BoxCollider2D boxCollider)
    {
        base.SetCollider(boxCollider);
    }

    protected abstract void OnTrigger();
 
}
