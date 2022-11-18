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
        player.Move(0.6f);

        yield return new WaitForSeconds(4f);
        Debug.Log("A:" + player.transform.position.x + " | " + startPosition.x);
        //Assert.IsTrue(player.transform.position.x > startPosition.x);


        startPosition = player.transform.position;
        player.Move(-3f);

        yield return new WaitForSeconds(4f);
        Debug.Log("B:" + player.transform.position.x + " | " + startPosition.x);
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
