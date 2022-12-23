using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Collectable
{
    protected string targetTag;
    protected Vector2 direction;
    protected float speed;

    public Projectile(string targetTag, Vector2 direction, float speed)
    {
        this.targetTag = targetTag;
        this.direction = direction;
        this.speed = speed;
    }

    public void Init(string targetTag, Vector2 direction, float speed)
    {
        this.targetTag = targetTag;
        this.direction = direction;
        this.speed = speed;
    }

    protected override void Start()
    {
        base.Start();
        WaitAndDestroy();
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    protected override void OnCollide(Collider2D coll)
    {
        OnCollect();
        ApplyOnTarget(coll);
    }

    protected abstract void ApplyOnTarget(Collider2D coll);

    protected void Move()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }

    private void WaitAndDestroy()
    {
        Destroy(gameObject, 1.5f);
    }
}
