using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Collectable
{
    [SerializeField] private Transform spawnPoint;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            spriteRenderer.enabled = false;
            base.OnCollect();
            
        }
        base.OnCollide(coll);
    }

    protected override void OnCollect()
    {
        Player.instance.IsPoweredUp = true;
        Debug.Log("player is powered up!");
        base.OnCollect();
    }

    protected override void SetCollider(BoxCollider2D boxCollider)
    {
        base.SetCollider(boxCollider);
    }

    public override string GetId()
    {
        return nameof(PowerUp) + "_" + id.ToString();
    }

    public void ResetPowerUp()
    {
        transform.position = spawnPoint.position;
        spriteRenderer.enabled = true;
        collected = false;
    }

    public override void OnLoadInit()
    {
        ResetPowerUp();
    }
}
