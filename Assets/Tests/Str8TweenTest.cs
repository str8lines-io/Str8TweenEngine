using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Str8lines.Tweening;
using UnityEngine.EventSystems;

public class Str8TweenTest
{
    GameObject go;
    Vector3 toVectorValue;
    float toFloatValue;
    Easing.EaseType easeType;
    float duration;
    
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        _setInitialValues();
    }

    [TearDown]
    public void Teardown()
    {
        UnityEngine.Object.Destroy(go);
    }

    [UnitySetUp]
    public void SetUp()
    {
        go = new GameObject();
        _setInitialValues();
    }

    [UnityTearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(go);
    }

    private void _setInitialValues()
    {
        toVectorValue = new Vector2(2f, 2f);
        toFloatValue = 0.5f;
        easeType = Easing.EaseType.Linear;
        duration = 1f;
    }

#region Tween methods
    #region move
    [Test]
    public void MoveRectTransformNullException()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            Str8Tween.move(null, toVectorValue, easeType, duration);
        });
    }

    [Test]
    public void MoveDurationException()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.move(rect, toVectorValue, easeType, 0f);
        });
    }
    
    [Test]
    public void MoveEngineInstantiation()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Str8Tween.move(rect, toVectorValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void MoveResult()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.AreNotEqual(Str8Tween.move(rect, toVectorValue, easeType, duration), null);
    }
    #endregion
    
    #region scale
    [Test]
    public void ScaleRectTransformNullException()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            Str8Tween.scale(null, toVectorValue, easeType, duration);
        });
    }

    [Test]
    public void ScaleDurationException()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.scale(rect, toVectorValue, easeType, 0f);
        });
    }
    
    [Test]
    public void ScaleEngineInstantiation()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Str8Tween.scale(rect, toVectorValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void ScaleResult()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.AreNotEqual(Str8Tween.scale(rect, toVectorValue, easeType, duration), null);
    }
    #endregion
    
    #region rotate
    [Test]
    public void RotateRectTransformNullException()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            Str8Tween.rotate(null, toVectorValue, easeType, duration);
        });
    }

    [Test]
    public void RotateDurationException()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.rotate(rect, toVectorValue, easeType, 0f);
        });
    }
    
    [Test]
    public void RotateEngineInstantiation()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Str8Tween.rotate(rect, toVectorValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void RotateResult()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.AreNotEqual(Str8Tween.rotate(rect, toVectorValue, easeType, duration), null);
    }
    #endregion
    
    #region fade canvasRenderer
    [Test]
    public void FadeCanvasRendererDurationException()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(canvasRenderer, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void FadeCanvasRendererNegativeToValueException()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(canvasRenderer, -0.1f, easeType, duration);
        });
    }

    [Test]
    public void FadeCanvasRendererToValueHigherThanOneException()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(canvasRenderer, 1.1f, easeType, duration);
        });
    }
    
    [Test]
    public void FadeCanvasRendererEngineInstantiation()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Str8Tween.fade(canvasRenderer, toFloatValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void FadeCanvasRendererResult()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.AreNotEqual(Str8Tween.fade(canvasRenderer, toFloatValue, easeType, duration), null);
    }
    #endregion
    
    #region fade spriteRenderer
    [Test]
    public void FadeSpriteRendererDurationException()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(spriteRenderer, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void FadeSpriteRendererNegativeToValueException()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(spriteRenderer, -0.1f, easeType, duration);
        });
    }

    [Test]
    public void FadeSpriteRendererToValueHigherThanOneException()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(spriteRenderer, 1.1f, easeType, duration);
        });
    }
    
    [Test]
    public void FadeSpriteRendererEngineInstantiation()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Str8Tween.fade(spriteRenderer, toFloatValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void FadeSpriteRendererResult()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.AreNotEqual(Str8Tween.fade(spriteRenderer, toFloatValue, easeType, duration), null);
    }
    #endregion
    
    #region fade rawImage
    [Test]
    public void FadeRawImageDurationException()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(rawImage, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void FadeRawImageNegativeToValueException()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(rawImage, -0.1f, easeType, duration);
        });
    }

    [Test]
    public void FadeRawImageToValueHigherThanOneException()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(rawImage, 1.1f, easeType, duration);
        });
    }
    
    [Test]
    public void FadeRawImageEngineInstantiation()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Str8Tween.fade(rawImage, toFloatValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void FadeRawImageResult()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.AreNotEqual(Str8Tween.fade(rawImage, toFloatValue, easeType, duration), null);
    }
    #endregion
    
    #region fade image
    [Test]
    public void FadeImageDurationException()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(image, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void FadeImageNegativeToValueException()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(image, -0.1f, easeType, duration);
        });
    }

    [Test]
    public void FadeImageToValueHigherThanOneException()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(image, 1.1f, easeType, duration);
        });
    }
    
    [Test]
    public void FadeImageEngineInstantiation()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Str8Tween.fade(image, toFloatValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void FadeImageResult()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.AreNotEqual(Str8Tween.fade(image, toFloatValue, easeType, duration), null);
    }
    #endregion
    
    #region fade text
    [Test]
    public void FadeTextDurationException()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(text, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void FadeTextNegativeToValueException()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(text, -0.1f, easeType, duration);
        });
    }

    [Test]
    public void FadeTextToValueHigherThanOneException()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(text, 1.1f, easeType, duration);
        });
    }
    
    [Test]
    public void FadeTextEngineInstantiation()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Str8Tween.fade(text, toFloatValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void FadeTextResult()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.AreNotEqual(Str8Tween.fade(text, toFloatValue, easeType, duration), null);
    }
    #endregion

    #region fade graphic
    [Test]
    public void FadeGraphicDurationException()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(graphic, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void FadeGraphicNegativeToValueException()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(graphic, -0.1f, easeType, duration);
        });
    }

    [Test]
    public void FadeGraphicToValueHigherThanOneException()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.fade(graphic, 1.1f, easeType, duration);
        });
    }
    
    [Test]
    public void FadeGraphicEngineInstantiation()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Str8Tween.fade(graphic, toFloatValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine, null);
    }

    [Test]
    public void FadeGraphicResult()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.AreNotEqual(Str8Tween.fade(graphic, toFloatValue, easeType, duration), null);
    }
    #endregion
#endregion

#region Engine methods
    [Test]
    public void GetTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween result = Str8Tween.get(t.id);
        Assert.IsTrue(result != null && result == t);
    }
    
    [Test]
    public void GetTweenByIdNull()
    {
        Assert.AreEqual(Str8Tween.get("randomID"), null);
    }
    
    [Test]
    public void GetTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween[] result = Str8Tween.get();
        Assert.IsTrue(result.Length > 0 && Array.Exists(result, element => element.id == t.id));
    }
    
    [Test]
    public void GetTweensNullTarget()
    {      
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Tween[] result = Str8Tween.get(go);
        });
    }
    
    [Test]
    public void GetTweensTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween[] result = Str8Tween.get(go);
        Assert.IsTrue(result.Length > 0 && Array.Exists(result, element => element.id == t.id));
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
#endregion
}