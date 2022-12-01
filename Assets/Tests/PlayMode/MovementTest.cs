using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{

    [UnityTest]
    public IEnumerator MoveRight()
    {
        Inits.InitPlayer(out Player player);

        Vector3 startPosition = player.transform.position;
        player.Move(3f);

        yield return new WaitForSeconds(4f);
        Debug.Log("Player position:" + player.transform.position.x + " start position " + startPosition.x);
        Assert.IsTrue(player.transform.position.x > startPosition.x);
    }

    [UnityTest]
    public IEnumerator MoveLeft()
    {
        Inits.InitPlayer(out Player player);
        Vector3 startPosition = player.transform.position;
        player.Move(-3f);

        yield return new WaitForSeconds(4f);
        Debug.Log("Player position:" + player.transform.position.x + " start position " + startPosition.x);
        Assert.IsTrue(player.transform.position.x < startPosition.x);
    }

    [UnityTest]
    public IEnumerator Jump()
    {
        Inits.InitPlayer(out Player player);

        player.Jump();

        yield return new WaitForSeconds(0.1f);
        Assert.IsFalse(player.IsGrounded);
    }
}
