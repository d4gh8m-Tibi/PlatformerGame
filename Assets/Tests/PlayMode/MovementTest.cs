using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{

    /*[UnityTest]
    public IEnumerator Move()
    {
        var gameObject = new GameObject();
        var inputController = gameObject.AddComponent<PlayerInput>();
        var rigidBody = gameObject.AddComponent<Rigidbody2D>();
        var player = gameObject.AddComponent<Player>();
        player.SetRigidBody(rigidBody);

        var startPosition = player.transform.position;
        player.Move(0.1f);

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(player.transform.position.x > startPosition.x);

        startPosition = player.transform.position;
        player.Move(-0.1f);

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(player.transform.position.x < startPosition.x);
    }*/

    [UnityTest]
    public IEnumerator MovementWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator Jump()
    {
        var gameObject = new GameObject();
        var boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        var inputController = gameObject.AddComponent<PlayerInput>();
        var rigidBody = gameObject.AddComponent<Rigidbody2D>();        
        var player = gameObject.AddComponent<Player>();
        player.SetRigidBody(rigidBody);
        player.SetCollider(boxCollider2D);

        player.Jump();

        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(player.IsGrounded);
    }
}
