using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CollectTest
{

    [UnityTest]
    public IEnumerator CollectStar()
    {
        var gameObjectStar = new GameObject();
        var star = gameObjectStar.AddComponent<Star>();
        var boxCollider2DStar = gameObjectStar.AddComponent<BoxCollider2D>();
        star.SetStarCollider(boxCollider2DStar);

        Inits.InitPlayer(out Player player);       

        player.transform.position = new Vector3(0f, 0.3f);
        star.transform.position = new Vector3(0f, 0.0f);

        Assert.AreEqual(player.StarCounter, 0);

        yield return new WaitForSeconds(2f);
        Assert.AreEqual(player.StarCounter, 1);
    }

    [UnityTest]
    public IEnumerator FinishGame()
    {
        var gameObjectSEndMapGoal = new GameObject();
        var gameController = gameObjectSEndMapGoal.AddComponent<GameController>();
        var endMapGoal = gameObjectSEndMapGoal.AddComponent<EndMapGoal>();
        var boxCollider2DEndMapGoal = gameObjectSEndMapGoal.AddComponent<BoxCollider2D>();
        endMapGoal.SetStarCollider(boxCollider2DEndMapGoal);

        Inits.InitPlayer(out Player player);

        player.transform.position = new Vector3(0f, 0.3f);
        endMapGoal.transform.position = new Vector3(0f, 0.0f);

        Assert.IsFalse(SceneManager.GetActiveScene().buildIndex == Constants.Scenes.MENU);
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(SceneManager.GetActiveScene().buildIndex == Constants.Scenes.MENU);
    }
}
