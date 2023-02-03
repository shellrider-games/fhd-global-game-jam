using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class testCollectible
{
    [UnityTest]
    public IEnumerator testCollectibleCanExist() {
        GameObject collectible = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Collectible"));
        Assert.NotNull(collectible);
        yield return null;
    }

}

