using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class testCollectible
{
    private GameObject _gameObject;
    private Collectible _collectible;
    private CollectibleState _collectibleState;
    [SetUp]
    public void SetUp()
    {
        _gameObject = new GameObject();
        _collectible = _gameObject.AddComponent(typeof(Collectible)) as Collectible;
        _collectibleState = _gameObject.AddComponent(typeof(CollectibleState)) as CollectibleState;
    }
    
    [UnityTest]
    public IEnumerator testCollectibleCanExist() {
        Assert.NotNull(_collectible);
        yield return null;
    }

    [UnityTest]
    public IEnumerator testCollectibleIncreasesCollectedStateWhenCollected()
    {
        Assert.That(CollectibleState.Instance.collectablesCollected, Is.EqualTo(0));
        _collectible.Collect();
        Assert.That(CollectibleState.Instance.collectablesCollected, Is.EqualTo(1));
        yield return null;
    }
}

