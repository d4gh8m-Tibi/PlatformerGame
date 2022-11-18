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
        Inits.InitPlayer(out Player player);

        Vector3 startPosition = player.transform.position;
        player.Move(0.1f);

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(player.transform.position.x > startPosition.x);

        startPosition = player.transform.position;
        player.Move(-0.1f);

        yield return new WaitForSeconds(2f);
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
