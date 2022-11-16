using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{

    [UnityTest]
    public IEnumerator Move()
    {
        new WaitForSeconds(2f);
        var gameObject = new GameObject();
        var rigidBody = gameObject.AddComponent<Rigidbody2D>();
        var player = gameObject.AddComponent<PlayerMovement>();
        player.SetRigidBody(rigidBody);
        var startPosition = player.transform.position;

        player.Move(0.1f);

        yield return new WaitForSeconds(5f);
        Assert.IsTrue(player.transform.position.x < startPosition.x);
    }
}
