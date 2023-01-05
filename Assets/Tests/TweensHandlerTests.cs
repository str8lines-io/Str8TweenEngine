#region namespaces
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
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

#region Init
    [SetUp]
    public void SetUp()
    {
        UnityEngine.Debug.Log("executed");
        go = new GameObject();
        toVectorValue = new Vector2(2f, 2f);
        toFloatValue = 0.5f;
        easeType = Easing.EaseType.Linear;
        duration = 1f;
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(go);
    }
#endregion

#region Tests
    [Test]
    public void TweenAdded()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween(rect, toVectorValue, easeType, duration, false, "move");
        TweensHandler.Instance.Add(t);
        Assert.IsTrue(TweensHandler.Instance.tweens.Count == 1 && TweensHandler.Instance.tweens[t.id] == t);
    }
    
    [Test]
    public void TweenNullAdded()
    {
        TweensHandler.Instance.Add(null);
        Assert.AreEqual(TweensHandler.Instance.tweens.Count, 0);
    }

    [UnityTest]
    public IEnumerator TweenUpdated()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween(rect, toVectorValue, easeType, duration, true, "move");
        Vector3 initialPosition = rect.anchoredPosition3D;
        TweensHandler.Instance.Add(t);
        yield return new WaitForSeconds(duration + 0.1f);
        Assert.IsTrue(rect.anchoredPosition3D != initialPosition && rect.anchoredPosition3D == toVectorValue);
    }

    [UnityTest]
    public IEnumerator TweenRemainingAfterUpdate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween(rect, toVectorValue, easeType, duration, false, "move");
        TweensHandler.Instance.Add(t);
        yield return new WaitForSeconds(duration + 0.1f);
        Assert.AreEqual(TweensHandler.Instance.tweens[t.id], t);
    }

    [UnityTest]
    public IEnumerator TweenKilledAfterUpdate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween(rect, toVectorValue, easeType, duration, true, "move");
        TweensHandler.Instance.Add(t);
        yield return new WaitForSeconds(duration + 0.1f);
        Assert.Throws<KeyNotFoundException>(()=>{
            Tween result = TweensHandler.Instance.tweens[t.id];
        });
    }
#endregion
}