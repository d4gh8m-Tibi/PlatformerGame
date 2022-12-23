using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : Enemy
{        
    public override void OnLoadInit()
    {
        Destroy(gameObject);
    }

    protected override void Move()
    {
        if (!canMove) return;
        distanceFromPoint = Vector2.Distance(transform.position, nextPoint.transform.position);
        Vector2 direction = nextPoint.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(this.transform.position, nextPoint.transform.position, speed * Time.deltaTime);

        if(distanceFromPoint < 0.5f)
        {
            NextCirclePoint();
        }

    }

    protected override void OnBulletCollide()
    {
        Destroy(gameObject);
    }

    protected override void OnPlayerCollide()
    {
        GameController.instance.KillPlayer();
    }

    public override string GetId()
    {
        return nameof(Walker) + "_" + id.ToString();
    }
}
