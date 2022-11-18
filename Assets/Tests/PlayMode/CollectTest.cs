using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CollectTest
{

    [UnityTest]
    public IEnumerator Collect()
    {
        var gameObject = new GameObject();
        var gameObject2 = new GameObject();
        var inputController = gameObject.AddComponent<PlayerInput>();
        var rigidBody = gameObject.AddComponent<Rigidbody2D>();
        var player = gameObject.AddComponent<Player>();
        player.SetRigidBody(rigidBody);

        var star = gameObject2.AddComponent<Star>();
        var boxCollider2DStar = gameObject2.AddComponent<BoxCollider2D>();
        var boxCollider2DPlayer = gameObject.AddComponent<BoxCollider2D>();

        star.SetStarCollider(boxCollider2DStar);
        player.SetCollider(boxCollider2DPlayer);

        player.transform.position = new Vector3(0f, 0.3f);
        star.transform.position = new Vector3(0f, 0.0f);

        var startPosition = player.transform.position;

        Assert.AreEqual(player.StarCounter, 0);


        yield return new WaitForSeconds(2f);
        Assert.AreEqual(player.StarCounter, 1);

    }
}
