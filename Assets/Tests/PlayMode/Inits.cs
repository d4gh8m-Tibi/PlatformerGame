using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public static class Inits
{
    public static void InitPlayer(out Player player)
    {
        GameObject playerGameObject = new GameObject();
        player = playerGameObject.AddComponent<Player>();
        PlayerInput inputController = playerGameObject.AddComponent<PlayerInput>();
        BoxCollider2D boxCollider2D = playerGameObject.AddComponent<BoxCollider2D>();
        Rigidbody2D rigidBody = playerGameObject.AddComponent<Rigidbody2D>();
        Animator animator = playerGameObject.AddComponent<Animator>();
        player.SetRigidBody(rigidBody);
        player.SetCollider(boxCollider2D);
    }

}
