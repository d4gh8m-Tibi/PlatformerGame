using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Collectable
{
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            var player = coll.GetComponent<Player>();
            player.IncrementStars();
            ItemManager.instance.Add(GetId());
            base.OnCollect();
            Destroy(gameObject);
        }
        base.OnCollide(coll);        
    }

    protected override void SetCollider(BoxCollider2D boxCollider)
    {
        base.SetCollider(boxCollider);
    }

    public override string GetId()
    {
        return nameof(Star) + "_" + id.ToString();
    }

    public override void OnLoadInit()
    {
        Destroy(gameObject);
    }
}
