using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    [SerializeField] protected int id;
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    protected GameController gameController;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        gameController = GameController.instance;
    }

    protected virtual void Update()
    {
        if(hits == null) hits = new Collider2D[10]; ;
        boxCollider.OverlapCollider(filter, hits);
        for(int i = 0; i < hits.Length; ++i)
        {
            if(hits[i] == null)
            {
                continue;
            }
            OnCollide(hits[i]);
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll) { }
    protected virtual void SetCollider(BoxCollider2D boxCollider) 
    {
        this.boxCollider = boxCollider;
    }

    public abstract void OnLoadInit();
    public abstract string GetId();
}
