using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestScript
{
    

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestResourcesLoad()
    {
        Sprite[] SpritesData = Resources.LoadAll<Sprite>("Resources/sprites/qbert/block_yellow_1");

        Assert.IsNotNull(SpritesData);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
