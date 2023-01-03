#region namespaces
using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Str8lines.Tweening;
#endregion

public class TweensHandlerTests
{
    #region Variables
    GameObject go;
    Vector3 toVectorValue;
    float toFloatValue;
    Easing.EaseType easeType;
    float duration;
    #endregion

#region Setups and Teardowns
    [UnitySetUp]
    public void SetUp()
    {
        go = new GameObject();
        toVectorValue = new Vector2(2f, 2f);
        toFloatValue = 0.5f;
        easeType = Easing.EaseType.Linear;
        duration = 1f;
    }

    [UnityTearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(go);
    }
#endregion

#region Tests
    [UnityTest]
    public IEnumerator TweenAdded()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration, false);
        yield return new WaitForSeconds(duration + 0.1f);
        Tween result = Str8Tween.get(t.id);
        Assert.AreEqual(result, t);
    }
    
    [UnityTest]
    public IEnumerator TweenNullAdded()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration, false);
        yield return new WaitForSeconds(duration + 0.1f);
        Tween result = Str8Tween.get(t.id);
        Assert.AreEqual(result, t);
    }

    [UnityTest]
    public IEnumerator TweenUpdated()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        yield return new WaitForSeconds(duration + 0.1f);
        Assert.IsTrue(rect.anchoredPosition3D != initialPosition && rect.anchoredPosition3D == toVectorValue);
    }

    [UnityTest]
    public IEnumerator TweenRemainingAfterUpdate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration, false);
        yield return new WaitForSeconds(duration + 0.1f);
        Tween result = Str8Tween.get(t.id);
        Assert.AreEqual(result, t);
    }

    [UnityTest]
    public IEnumerator TweenKilledAfterUpdate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration, false);
        yield return new WaitForSeconds(duration + 0.1f);
        Tween result = Str8Tween.get(t.id);
        Assert.AreEqual(result, t);
    }
#endregion
}