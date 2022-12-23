using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public Bullet(string targetTag, Vector2 direction, float speed) : base(targetTag, direction, speed)
    {
    }

    public override string GetId()
    {
        return nameof(Bullet) + "_" + id.ToString();
    }    

    protected override void ApplyOnTarget(Collider2D coll)
    {
        if(coll.CompareTag("Player") && coll.CompareTag(targetTag))
        {
            PlayerEffect(coll);
        } 
        else if(coll.CompareTag("Enemy") && coll.CompareTag(targetTag))
        {
            EnemyEffect(coll);
        }        
    }

    protected void PlayerEffect(Collider2D coll)
    {
        GameController.instance.KillPlayer();
    }

    protected void EnemyEffect(Collider2D coll)
    {
        Enemy enemy = coll.gameObject.GetComponent<Enemy>();
        enemy.OnBulletCollide();
        Destroy(gameObject);
    }

    public override void OnLoadInit()
    {
        throw new System.NotImplementedException();
    }

    
}
