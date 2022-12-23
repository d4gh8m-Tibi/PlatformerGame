using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Enemy : Collidable
{
    protected Rigidbody2D rigidBody;
    [SerializeField] protected float speed;
    [SerializeField] protected List<CirclePoint> circlePoints = new List<CirclePoint>();
    protected float distanceFromPoint;
    protected CirclePoint nextPoint;
    protected int lastPointOrder;
    protected bool canMove = false;

    protected override void Start()
    {
        base.Start();
        SetRigidBody(GetComponent<Rigidbody2D>());
        nextPoint = circlePoints[0];
        lastPointOrder = circlePoints.Max(i => i.OrderNumberValue);
        CheckCirclePoints();
    }
    protected override void Update()
    {
        Move();
        base.Update();
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            OnPlayerCollide();
        }
        else if (coll.tag == "Bullet")
        {
            OnBulletCollide();
        }
    }

    public void SetRigidBody(Rigidbody2D rigidbody)
    {
        rigidBody = rigidbody;
    }

    protected void NextCirclePoint()
    {
        if(nextPoint.OrderNumberValue == lastPointOrder)
        {
            nextPoint = circlePoints.First();
        }
        else
        {
            nextPoint = circlePoints.Where(i => i.OrderNumberValue > nextPoint.OrderNumberValue).OrderBy(i => i.OrderNumberValue).First();
        }
    }

    private void CheckCirclePoints()
    {
        var orderedList = circlePoints.OrderBy(i => i.OrderNumberValue);
        var actualPoint = orderedList.First();
        for(int i = 1; i < orderedList.Count(); ++i)
        {
            if(circlePoints[i].OrderNumberValue <= actualPoint.OrderNumberValue)
            {
                Debug.Log(GetId() + " Enemy NPC's circle points are not all unique!");
                return;
            }
        }
        canMove = true;
    }

    protected abstract void OnPlayerCollide();
    public abstract void OnBulletCollide();
    protected abstract void Move();
}
