using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : Collidable
{
    protected bool collected;
    [SerializeField]
    private AudioClip collectedSound;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true;
        if(collectedSound != null)
        gameController.PlaySoundEffect(collectedSound);
    }

    public void SetStarCollider(BoxCollider2D boxCollider)
    {
        base.SetCollider(boxCollider);
    }
}
