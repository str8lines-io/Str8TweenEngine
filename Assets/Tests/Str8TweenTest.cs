using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Str8TweenTest
{
    /**
        - Tween methods : 
            * move
            * fade (8x)
            * scale
            * rotate

        - Engine methods :
            * getTween(id)
            * getTweens()
            * getTweens(target)
            * Update()
            * _init()
    */

    // [Test]
    // public void TweenRectTransformMoveDelayed()
    // {
    //     go.AddComponent<RectTransform>();
    //     RectTransform rect = go.GetComponent<RectTransform>();
    //     Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
    //     t.delay(1f);
    //     Vector3 initialValue = t.target.transform.position;
    //     Assert.That(
    //         t.target.transform.position, Is.EqualTo(initialValue).After(1)
    //     );
    // }
}
