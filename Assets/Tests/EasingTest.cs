using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Str8lines.Tweening;

public class EasingTest
{
    float elapsed = 0f;
    float initial = 1f;
    float change = 1f;
    float duration = 1f;

    // A Test behaves as an ordinary method
    [Test]
    public void EasingLinearNoChangeAtStart()
    {
        // Use the Assert class to test conditions
        float resultStart = Easing.ease(Easing.EaseType.Linear, elapsed, initial, change, duration);
        Assert.AreEqual(resultStart, initial);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void EasingLinearResultAtEnd()
    {
        // Use the Assert class to test conditions
        float resultEnd = Easing.ease(Easing.EaseType.Linear, duration, initial, change, duration);
        Assert.AreEqual(resultEnd, (initial + change));
    }

    // A Test behaves as an ordinary method
    [Test]
    public void EasingLinearChangeAtEnd()
    {
        // Use the Assert class to test conditions
        float resultEnd = Easing.ease(Easing.EaseType.Linear, duration, initial, change, duration);
        Assert.AreNotEqual(resultEnd, initial);
    }
}
