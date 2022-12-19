using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Str8lines.Tweening;
using UnityEngine.EventSystems;

public class TweenTest
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

        toVectorValue = new Vector3(2f, 2f, 2f);
        toFloatValue = 0.5f;
        easeType = Easing.EaseType.Linear;
        duration = 1f;
    }

    [TearDown]
    public void Teardown()
    {
        UnityEngine.Object.Destroy(go);
    }

#region constructors
    [Test]
    public void TweenRectTransformInvalidMethod()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, rect, toVectorValue, easeType, duration);
        });
    }

    #region move
    [Test]
    public void TweenRectTransformMoveDefaultKill()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRectTransformMoveKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenRectTransformMoveDontKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRectTransformMoveNullTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("move", null, toVectorValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenRectTransformMoveZeroDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("move", rect, toVectorValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenRectTransformMoveNegativeDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("move", rect, toVectorValue, easeType, -1f);
        });
    }
    #endregion

    #region scale
    [Test]
    public void TweenRectTransformScaleDefaultKill()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRectTransformScaleKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenRectTransformScaleDontKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenRectTransformScaleNullTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("scale", null, toVectorValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenRectTransformScaleZeroDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("scale", rect, toVectorValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenRectTransformScaleNegativeDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("scale", rect, toVectorValue, easeType, -1f);
        });
    }
    #endregion
    
    #region rotate
    [Test]
    public void TweenRectTransformRotateDefaultKill()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRectTransformRotateKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenRectTransformRotateDontKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRectTransformRotateNullTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("rotate", null, toVectorValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenRectTransformRotateZeroDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("rotate", rect, toVectorValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenRectTransformRotateNegativeDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("rotate", rect, toVectorValue, easeType, -1f);
        });
    }
    #endregion

    #region fade canvasRenderer
    [Test]
    public void TweenCanvasRendererFadeDefaultKill()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenCanvasRendererFadeKillOnEnd()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenCanvasRendererFadeDontKillOnEnd()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenCanvasRendererInvalidMethod()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, canvasRenderer, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenCanvasRendererFadeNullTarget()
    {
        CanvasRenderer canvasRenderer = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenCanvasRendererFadeZeroDuration()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenCanvasRendererFadeNegativeDuration()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, -1f);
        });
    }
    #endregion

    #region fade spriteRenderer
    [Test]
    public void TweenSpriteRendererFadeDefaultKill()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenSpriteRendererFadeKillOnEnd()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenSpriteRendererFadeDontKillOnEnd()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenSpriteRendererFadeNullTarget()
    {
        SpriteRenderer spriteRenderer = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenSpriteRendererInvalidMethod()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, spriteRenderer, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenSpriteRendererFadeZeroDuration()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenSpriteRendererFadeNegativeDuration()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, -1f);
        });
    }
    #endregion

    #region fade rawImage
    [Test]
    public void TweenRawImageFadeDefaultKill()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRawImageFadeKillOnEnd()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenRawImageFadeDontKillOnEnd()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenRawImageFadeNullTarget()
    {
        RawImage rawImage = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenRawImageInvalidMethod()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, rawImage, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenRawImageFadeZeroDuration()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", rawImage, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenRawImageFadeNegativeDuration()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", rawImage, toFloatValue, easeType, -1f);
        });
    }
    #endregion

    #region fade image
    [Test]
    public void TweenImageFadeDefaultKill()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenImageFadeKillOnEnd()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenImageFadeDontKillOnEnd()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenImageFadeNullTarget()
    {
        Image image = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenImageInvalidMethod()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, image, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenImageFadeZeroDuration()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", image, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenImageFadeNegativeDuration()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", image, toFloatValue, easeType, -1f);
        });
    }
    #endregion

    #region fade text
    [Test]
    public void TweenTextFadeDefaultKill()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenTextFadeKillOnEnd()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenTextFadeDontKillOnEnd()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenTextFadeNullTarget()
    {
        Text text = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenTextInvalidMethod()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, text, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenTextFadeZeroDuration()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", text, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenTextFadeNegativeDuration()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", text, toFloatValue, easeType, -1f);
        });
    }
    #endregion
    
    #region fade graphic
    [Test]
    public void TweenGraphicFadeDefaultKill()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenGraphicFadeKillOnEnd()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenGraphicFadeDontKillOnEnd()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == go) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenGraphicFadeNullTarget()
    {
        Graphic graphic = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenGraphicInvalidMethod()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, graphic, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenGraphicFadeZeroDuration()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", graphic, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenGraphicFadeNegativeDuration()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", graphic, toFloatValue, easeType, -1f);
        });
    }
    #endregion

#endregion

#region delay
    // Delay affect every tween the same way
    // We don't need to test every combination of tween.
    [Test]
    public void TweenRectTransformMoveDelayedOneSecond()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        float initialDelay = t.delay();
        t.delay(1f);
        Assert.IsTrue(initialDelay != t.delay() && t.delay() >= 0f);
    }

    [Test]
    public void TweenRectTransformMoveDelayedZeroSecond()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.delay(0f);
        Assert.IsTrue(t.delay() == 0f);
    }

    [Test]
    public void TweenRectTransformMoveDelayedNegativeOneSecond()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.delay(-1f);
        Assert.IsTrue(t.delay() == 0f);
    }
    #endregion

#region loops
    // Loops affect every tween the same way
    // We don't need to test every combination of tween.
    [Test]
    public void TweenRectTransformMoveDefaultLoops()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop();
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Restart);
    }

    [Test]
    public void TweenRectTransformMoveLoopsBelowMinusOneLoop()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(-2);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Restart);
    }

    [Test]
    public void TweenRectTransformMoveLoopsZeroLoop()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(0);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Restart);
    }

    [Test]
    public void TweenRectTransformMoveLoopsMultipleLoops()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(10000);
        Assert.IsTrue(t.loopsCount() == 10000 && t.loopType() == Tween.LoopType.Restart);
    }

    [Test]
    public void TweenRectTransformMoveLoopsBelowMinusOneAndRestart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(-2, Tween.LoopType.Restart);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Restart);
    }

    [Test]
    public void TweenRectTransformMoveLoopsZeroAndRestart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(0, Tween.LoopType.Restart);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Restart);
    }

    [Test]
    public void TweenRectTransformMoveLoopsMultipleAndRestart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(10000, Tween.LoopType.Restart);
        Assert.IsTrue(t.loopsCount() == 10000 && t.loopType() == Tween.LoopType.Restart);
    }
    
    [Test]
    public void TweenRectTransformMoveLoopsBelowMinusOneAndOscillate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(-2, Tween.LoopType.Oscillate);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Oscillate);
    }

    [Test]
    public void TweenRectTransformMoveLoopsZeroAndOscillate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(0, Tween.LoopType.Oscillate);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.Oscillate);
    }

    [Test]
    public void TweenRectTransformMoveLoopsMultipleAndOscillate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(10000, Tween.LoopType.Oscillate);
        Assert.IsTrue(t.loopsCount() == 10000 && t.loopType() == Tween.LoopType.Oscillate);
    }

    [Test]
    public void TweenRectTransformMoveLoopsBelowMinusOneAndWithOffset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(-2, Tween.LoopType.WithOffset);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.WithOffset);
    }

    [Test]
    public void TweenRectTransformMoveLoopsZeroAndWithOffset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(0, Tween.LoopType.WithOffset);
        Assert.IsTrue(t.loopsCount() == -1 && t.loopType() == Tween.LoopType.WithOffset);
    }

    [Test]
    public void TweenRectTransformMoveLoopsMultipleAndWithOffset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.loop(10000, Tween.LoopType.WithOffset);
        Assert.IsTrue(t.loopsCount() == 10000 && t.loopType() == Tween.LoopType.WithOffset);
    }
    #endregion

#region update
    // Update method needs to be tested for every type of tween.
    // We have to check if values are properly updated in the _setCalculatedValue() method.
    #region move
    [Test]
    public void TweenRectTransformMoveUpdateFirstFrame()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        t.update(0f);
        Assert.IsTrue(initialPosition == rect.anchoredPosition3D && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformMoveUpdateWithTimeEqualZero()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialPosition == rect.anchoredPosition3D && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformMoveUpdateWithTimeBelowDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialPosition != rect.anchoredPosition3D && toVectorValue != rect.anchoredPosition3D && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformMoveUpdateWithTimeEqualDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toVectorValue == rect.anchoredPosition3D && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformMoveUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toVectorValue == rect.anchoredPosition3D && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformMoveLoopsRestart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        Vector3 firstLoopEndValues = rect.anchoredPosition3D;
        t.update(duration * 0.000001f);
        Vector3 secondLoopStartValues = rect.anchoredPosition3D;
        t.update(duration * 0.999999f);
        Vector3 secondLoopEndValues = rect.anchoredPosition3D;
        t.update(duration * 0.000001f);
        Vector3 thirdLoopStartValues = rect.anchoredPosition3D;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopStartValues == initialPosition && secondLoopEndValues == toVectorValue && thirdLoopStartValues == initialPosition);
    }

    [Test]
    public void TweenRectTransformMoveLoopsWithOffset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        Vector3 firstLoopEndValues = rect.anchoredPosition3D;
        t.update(duration);
        Vector3 secondLoopEndValues = rect.anchoredPosition3D;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopEndValues != initialPosition && secondLoopEndValues != firstLoopEndValues);
    }

    [Test]
    public void TweenRectTransformMoveLoopsOscillate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        Vector3 firstLoopEndValues = rect.anchoredPosition3D;
        t.update(duration);
        Vector3 secondLoopEndValues = rect.anchoredPosition3D;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopEndValues == initialPosition);
    }
    #endregion

    #region scale
    [Test]
    public void TweenRectTransformScaleUpdateFirstFrame()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialScale = rect.localScale;
        t.update(0f);
        Assert.IsTrue(initialScale == rect.localScale && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformScaleUpdateWithTimeEqualZero()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialScale = rect.localScale;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialScale == rect.localScale && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformScaleUpdateWithTimeBelowDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialScale = rect.localScale;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialScale != rect.localScale && toVectorValue != rect.localScale && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformScaleUpdateWithTimeEqualDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toVectorValue == rect.localScale && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformScaleUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toVectorValue == rect.localScale && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformScaleLoopsRestart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.localScale;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        Vector3 firstLoopEndValues = rect.localScale;
        t.update(duration * 0.000001f);
        Vector3 secondLoopStartValues = rect.localScale;
        t.update(duration * 0.999999f);
        Vector3 secondLoopEndValues = rect.localScale;
        t.update(duration * 0.000001f);
        Vector3 thirdLoopStartValues = rect.localScale;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopStartValues == initialPosition && secondLoopEndValues == toVectorValue && thirdLoopStartValues == initialPosition);
    }

    [Test]
    public void TweenRectTransformScaleLoopsWithOffset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.localScale;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        Vector3 firstLoopEndValues = rect.localScale;
        t.update(duration);
        Vector3 secondLoopEndValues = rect.localScale;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopEndValues != initialPosition && secondLoopEndValues != firstLoopEndValues);
    }

    [Test]
    public void TweenRectTransformScaleLoopsOscillate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.localScale;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        Vector3 firstLoopEndValues = rect.localScale;
        t.update(duration);
        Vector3 secondLoopEndValues = rect.localScale;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopEndValues == initialPosition);
    }
    #endregion

    #region rotate
    [Test]
    public void TweenRectTransformRotateUpdateFirstFrame()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialRotation = rect.localEulerAngles;
        t.update(0f);
        Assert.IsTrue(initialRotation == rect.localEulerAngles && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformRotateUpdateWithTimeEqualZero()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialRotation = rect.localEulerAngles;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialRotation == rect.localEulerAngles && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformRotateUpdateWithTimeBelowDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialRotation = rect.localEulerAngles;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialRotation != rect.localEulerAngles && toVectorValue != rect.localEulerAngles && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRectTransformRotateUpdateWithTimeEqualDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toVectorValue == rect.localEulerAngles && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformRotateUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toVectorValue == rect.localEulerAngles && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformRotateLoopsRestart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.localEulerAngles;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        Vector3 firstLoopEndValues = rect.localEulerAngles;
        t.update(duration * 0.000001f);
        Vector3 secondLoopStartValues = rect.localEulerAngles;
        t.update(duration * 0.999999f);
        Vector3 secondLoopEndValues = rect.localEulerAngles;
        t.update(duration * 0.000001f);
        Vector3 thirdLoopStartValues = rect.localEulerAngles;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopStartValues == initialPosition && secondLoopEndValues == toVectorValue && thirdLoopStartValues == initialPosition);
    }

    [Test]
    public void TweenRectTransformRotateLoopsWithOffset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.localEulerAngles;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        Vector3 firstLoopEndValues = rect.localEulerAngles;
        t.update(duration);
        Vector3 secondLoopEndValues = rect.localEulerAngles;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopEndValues != initialPosition && secondLoopEndValues != firstLoopEndValues);
    }

    [Test]
    public void TweenRectTransformRotateLoopsOscillate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.localEulerAngles;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        Vector3 firstLoopEndValues = rect.localEulerAngles;
        t.update(duration);
        Vector3 secondLoopEndValues = rect.localEulerAngles;
        Assert.IsTrue(firstLoopEndValues == toVectorValue && secondLoopEndValues == initialPosition);
    }
    #endregion

    #region fade canvasRenderer
    [Test]
    public void TweenCanvasRendererFadeUpdateFirstFrame()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.update(0f);
        Assert.IsTrue(initialAlpha == canvasRenderer.GetAlpha() && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenCanvasRendererFadeUpdateWithTimeEqualZero()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == canvasRenderer.GetAlpha() && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenCanvasRendererFadeUpdateWithTimeBelowDuration()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != canvasRenderer.GetAlpha() && toFloatValue != canvasRenderer.GetAlpha() && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenCanvasRendererFadeUpdateWithTimeEqualDuration()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == canvasRenderer.GetAlpha() && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenCanvasRendererFadeUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == canvasRenderer.GetAlpha() && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenCanvasRendererFadeLoopsRestart()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        float firstLoopEndAlpha = canvasRenderer.GetAlpha();
        t.update(duration * 0.000001f);
        float secondLoopStartAlpha = canvasRenderer.GetAlpha();
        t.update(duration * 0.999999f);
        float secondLoopEndAlpha = canvasRenderer.GetAlpha();
        t.update(duration * 0.000001f);
        float thirdLoopStartAlpha = canvasRenderer.GetAlpha();
        Assert.IsTrue(Math.Round(firstLoopEndAlpha, 5) == toFloatValue && secondLoopStartAlpha == initialAlpha && Math.Round(secondLoopEndAlpha, 5) == toFloatValue && thirdLoopStartAlpha == initialAlpha);
    }

    [Test]
    public void TweenCanvasRendererFadeLoopsWithOffset()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = canvasRenderer.GetAlpha();
        t.update(duration);
        float secondLoopEndAlpha = canvasRenderer.GetAlpha();
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha != initialAlpha && secondLoopEndAlpha != firstLoopEndAlpha);
    }

    [Test]
    public void TweenCanvasRendererFadeLoopsOscillate()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = canvasRenderer.GetAlpha();
        t.update(duration);
        float secondLoopEndAlpha = canvasRenderer.GetAlpha();
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha == initialAlpha);
    }
    #endregion

    #region fade spriteRenderer
    [Test]
    public void TweenSpriteRendererFadeUpdateFirstFrame()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.update(0f);
        Assert.IsTrue(initialAlpha == spriteRenderer.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenSpriteRendererFadeUpdateWithTimeEqualZero()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == spriteRenderer.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenSpriteRendererFadeUpdateWithTimeBelowDuration()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != spriteRenderer.color.a && toFloatValue != spriteRenderer.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenSpriteRendererFadeUpdateWithTimeEqualDuration()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == spriteRenderer.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenSpriteRendererFadeUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == spriteRenderer.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenSpriteRendererFadeLoopsRestart()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        float firstLoopEndAlpha = spriteRenderer.color.a;
        t.update(duration * 0.000001f);
        float secondLoopStartAlpha = spriteRenderer.color.a;
        t.update(duration * 0.999999f);
        float secondLoopEndAlpha = spriteRenderer.color.a;
        t.update(duration * 0.000001f);
        float thirdLoopStartAlpha = spriteRenderer.color.a;
        Assert.IsTrue(Math.Round(firstLoopEndAlpha, 5) == toFloatValue && secondLoopStartAlpha == initialAlpha && Math.Round(secondLoopEndAlpha, 5) == toFloatValue && thirdLoopStartAlpha == initialAlpha);
    }

    [Test]
    public void TweenSpriteRendererFadeLoopsWithOffset()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = spriteRenderer.color.a;
        t.update(duration);
        float secondLoopEndAlpha = spriteRenderer.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha != initialAlpha && secondLoopEndAlpha != firstLoopEndAlpha);
    }

    [Test]
    public void TweenSpriteRendererFadeLoopsOscillate()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = spriteRenderer.color.a;
        t.update(duration);
        float secondLoopEndAlpha = spriteRenderer.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha == initialAlpha);
    }
    #endregion

    #region fade rawImage
    [Test]
    public void TweenRawImageFadeUpdateFirstFrame()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.update(0f);
        Assert.IsTrue(initialAlpha == rawImage.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRawImageFadeUpdateWithTimeEqualZero()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == rawImage.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRawImageFadeUpdateWithTimeBelowDuration()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != rawImage.color.a && toFloatValue != rawImage.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenRawImageFadeUpdateWithTimeEqualDuration()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == rawImage.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRawImageFadeUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == rawImage.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenRawImageFadeLoopsRestart()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        float firstLoopEndAlpha = rawImage.color.a;
        t.update(duration * 0.000001f);
        float secondLoopStartAlpha = rawImage.color.a;
        t.update(duration * 0.999999f);
        float secondLoopEndAlpha = rawImage.color.a;
        t.update(duration * 0.000001f);
        float thirdLoopStartAlpha = rawImage.color.a;
        Assert.IsTrue(Math.Round(firstLoopEndAlpha, 5) == toFloatValue && secondLoopStartAlpha == initialAlpha && Math.Round(secondLoopEndAlpha, 5) == toFloatValue && thirdLoopStartAlpha == initialAlpha);
    }

    [Test]
    public void TweenRawImageFadeLoopsWithOffset()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = rawImage.color.a;
        t.update(duration);
        float secondLoopEndAlpha = rawImage.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha != initialAlpha && secondLoopEndAlpha != firstLoopEndAlpha);
    }

    [Test]
    public void TweenRawImageFadeLoopsOscillate()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = rawImage.color.a;
        t.update(duration);
        float secondLoopEndAlpha = rawImage.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha == initialAlpha);
    }
    #endregion

    #region fade image
    [Test]
    public void TweenImageFadeUpdateFirstFrame()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.update(0f);
        Assert.IsTrue(initialAlpha == image.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenImageFadeUpdateWithTimeEqualZero()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == image.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenImageFadeUpdateWithTimeBelowDuration()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != image.color.a && toFloatValue != image.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenImageFadeUpdateWithTimeEqualDuration()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == image.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenImageFadeUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == image.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenImageFadeLoopsRestart()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        float firstLoopEndAlpha = image.color.a;
        t.update(duration * 0.000001f);
        float secondLoopStartAlpha = image.color.a;
        t.update(duration * 0.999999f);
        float secondLoopEndAlpha = image.color.a;
        t.update(duration * 0.000001f);
        float thirdLoopStartAlpha = image.color.a;
        Assert.IsTrue(Math.Round(firstLoopEndAlpha, 5) == toFloatValue && secondLoopStartAlpha == initialAlpha && Math.Round(secondLoopEndAlpha, 5) == toFloatValue && thirdLoopStartAlpha == initialAlpha);
    }

    [Test]
    public void TweenImageFadeLoopsWithOffset()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = image.color.a;
        t.update(duration);
        float secondLoopEndAlpha = image.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha != initialAlpha && secondLoopEndAlpha != firstLoopEndAlpha);
    }

    [Test]
    public void TweenImageFadeLoopsOscillate()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = image.color.a;
        t.update(duration);
        float secondLoopEndAlpha = image.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha == initialAlpha);
    }
    #endregion

    #region fade text
    [Test]
    public void TweenTextFadeUpdateFirstFrame()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.update(0f);
        Assert.IsTrue(initialAlpha == text.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenTextFadeUpdateWithTimeEqualZero()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == text.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenTextFadeUpdateWithTimeBelowDuration()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != text.color.a && toFloatValue != text.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenTextFadeUpdateWithTimeEqualDuration()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == text.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenTextFadeUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == text.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenTextFadeLoopsRestart()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        float firstLoopEndAlpha = text.color.a;
        t.update(duration * 0.000001f);
        float secondLoopStartAlpha = text.color.a;
        t.update(duration * 0.999999f);
        float secondLoopEndAlpha = text.color.a;
        t.update(duration * 0.000001f);
        float thirdLoopStartAlpha = text.color.a;
        Assert.IsTrue(Math.Round(firstLoopEndAlpha, 5) == toFloatValue && secondLoopStartAlpha == initialAlpha && Math.Round(secondLoopEndAlpha, 5) == toFloatValue && thirdLoopStartAlpha == initialAlpha);
    }

    [Test]
    public void TweenTextFadeLoopsWithOffset()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = text.color.a;
        t.update(duration);
        float secondLoopEndAlpha = text.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha != initialAlpha && secondLoopEndAlpha != firstLoopEndAlpha);
    }

    [Test]
    public void TweenTextFadeLoopsOscillate()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = text.color.a;
        t.update(duration);
        float secondLoopEndAlpha = text.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha == initialAlpha);
    }
    #endregion
    
    #region fade graphic
    [Test]
    public void TweenGraphicFadeUpdateFirstFrame()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.update(0f);
        Assert.IsTrue(initialAlpha == graphic.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenGraphicFadeUpdateWithTimeEqualZero()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == graphic.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenGraphicFadeUpdateWithTimeBelowDuration()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != graphic.color.a && toFloatValue != graphic.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenGraphicFadeUpdateWithTimeEqualDuration()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == graphic.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenGraphicFadeUpdateWithTimeHigherThanDuration()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == graphic.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenGraphicFadeLoopsRestart()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.loop(3, Tween.LoopType.Restart).update(0f); //First frame is not taken in count
        t.update(duration * 0.999999f);
        float firstLoopEndAlpha = graphic.color.a;
        t.update(duration * 0.000001f);
        float secondLoopStartAlpha = graphic.color.a;
        t.update(duration * 0.999999f);
        float secondLoopEndAlpha = graphic.color.a;
        t.update(duration * 0.000001f);
        float thirdLoopStartAlpha = graphic.color.a;
        Assert.IsTrue(Math.Round(firstLoopEndAlpha, 5) == toFloatValue && secondLoopStartAlpha == initialAlpha && Math.Round(secondLoopEndAlpha, 5) == toFloatValue && thirdLoopStartAlpha == initialAlpha);
    }

    [Test]
    public void TweenGraphicFadeLoopsWithOffset()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.loop(3, Tween.LoopType.WithOffset).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = graphic.color.a;
        t.update(duration);
        float secondLoopEndAlpha = graphic.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha != initialAlpha && secondLoopEndAlpha != firstLoopEndAlpha);
    }

    [Test]
    public void TweenGraphicFadeLoopsOscillate()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.loop(3, Tween.LoopType.Oscillate).update(0f); //First frame is not taken in count
        t.update(duration);
        float firstLoopEndAlpha = graphic.color.a;
        t.update(duration);
        float secondLoopEndAlpha = graphic.color.a;
        Assert.IsTrue(firstLoopEndAlpha == toFloatValue && secondLoopEndAlpha == initialAlpha);
    }
    #endregion
#endregion

#region events
    // Events are triggered regardless of the type of tween.
    // We don't need to test every combination of tween.
    [Test]
    public void TweenRectTransformMoveStart()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        bool triggered = false;
        t.onStart(()=>{ triggered = true; }).update(0f);
        Assert.IsTrue(triggered);
    }

    [Test]
    public void TweenRectTransformMoveLoopSingle()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        int triggered = 0;
        t.loop(1).onLoop((loopsCompletedCount)=>{ triggered = loopsCompletedCount; }).update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(triggered == 1);
    }

    [Test]
    public void TweenRectTransformMoveLoopEven()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        int triggered = 0;
        t.loop(2).onLoop((loopsCompletedCount)=>{ triggered = loopsCompletedCount; }).update(0f); //First frame is not taken in count
        t.update(duration);
        t.update(duration);
        Assert.IsTrue(triggered == 2);
    }

    [Test]
    public void TweenRectTransformMoveLoopOdd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        int triggered = 0;
        t.loop(3).onLoop((loopsCompletedCount)=>{ triggered = loopsCompletedCount; }).update(0f); //First frame is not taken in count
        t.update(duration);
        t.update(duration);
        t.update(duration);
        Assert.IsTrue(triggered == 3);
    }

    [Test]
    public void TweenRectTransformMoveCompleteOnUpdate()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        bool triggered = false;
        t.onComplete(()=>{ triggered = true; }).update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(triggered);
    }
#endregion

#region lifecycle
    #region pause
    [Test]
    public void TweenRectTransformMovePause()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        bool isRunning = t.isRunning;
        t.pause();
        Assert.IsTrue(isRunning && !t.isRunning);
    }

    [Test]
    public void TweenRectTransformMovePlay()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        t.pause();
        bool isRunning = t.isRunning;
        t.play();
        Assert.IsTrue(!isRunning && t.isRunning);
    }
    #endregion

    #region stop
    [Test]
    public void TweenRectTransformMoveStopDefault()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.stop();
        Assert.IsTrue(isAlive && isRunning && !isCompleted && !t.isRunning && t.isCompleted && !t.isAlive);
    }

    [Test]
    public void TweenRectTransformMoveStopDontKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, false);
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.stop();
        Assert.IsTrue(isAlive && isRunning && !isCompleted && !t.isRunning && t.isCompleted && t.isAlive);
    }

    [Test]
    public void TweenRectTransformMoveStopKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, true);
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.stop();
        Assert.IsTrue(isAlive && isRunning && !isCompleted && !t.isRunning && t.isCompleted && !t.isAlive);
    }
    #endregion

    #region kill
    [Test]
    public void TweenRectTransformMoveKill()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        bool isAlive = t.isAlive;
        t.kill();
        Assert.IsTrue(isAlive && !t.isAlive);
    }
    #endregion
    
    #region complete
    [Test]
    public void TweenRectTransformMoveComplete()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.complete();
        Assert.IsTrue(isAlive && !t.isAlive && !isCompleted && t.isCompleted && isRunning && !t.isRunning && rect.anchoredPosition3D == toVectorValue);
    }
    
    [Test]
    public void TweenRectTransformMoveCompleteDontKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, false);
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.complete();
        Assert.IsTrue(isAlive && t.isAlive && !isCompleted && t.isCompleted && isRunning && !t.isRunning && rect.anchoredPosition3D == toVectorValue);
    }
    
    [Test]
    public void TweenRectTransformMoveCompleteKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, true);
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.complete();
        Assert.IsTrue(isAlive && !t.isAlive && !isCompleted && t.isCompleted && isRunning && !t.isRunning && rect.anchoredPosition3D == toVectorValue);
    }
    
    [Test]
    public void TweenRectTransformMoveCompleteEvent()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, true);
        bool triggered = false;
        t.onComplete(()=>{ triggered = true; }).complete();
        Assert.IsTrue(triggered);
    }
    
    [Test]
    public void TweenRectTransformScaleComplete()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        t.complete();
        Assert.AreEqual(rect.localScale, toVectorValue);
    }
    
    [Test]
    public void TweenRectTransformRotateComplete()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        t.complete();
        Assert.AreEqual(rect.localEulerAngles, toVectorValue);
    }
    
    [Test]
    public void TweenCanvasRendererFadeComplete()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        t.complete();
        Assert.AreEqual(canvasRenderer.GetAlpha(), toFloatValue);
    }
    
    [Test]
    public void TweenSpriteRendererFadeComplete()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        t.complete();
        Assert.AreEqual(spriteRenderer.color.a, toFloatValue);
    }
    
    [Test]
    public void TweenRawImageFadeComplete()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        t.complete();
        Assert.AreEqual(rawImage.color.a, toFloatValue);
    }
    
    [Test]
    public void TweenImageFadeComplete()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        t.complete();
        Assert.AreEqual(image.color.a, toFloatValue);
    }
    
    [Test]
    public void TweenTextFadeComplete()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        t.complete();
        Assert.AreEqual(text.color.a, toFloatValue);
    }
    
    [Test]
    public void TweenGraphicFadeComplete()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        t.complete();
        Assert.AreEqual(graphic.color.a, toFloatValue);
    }
    #endregion
    
    #region cancel
    [Test]
    public void TweenRectTransformMoveCancel()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.cancel();
        Assert.IsTrue(isAlive && !t.isAlive && !isCompleted && t.isCompleted && isRunning && !t.isRunning && rect.anchoredPosition3D == initialPosition);
    }
    
    [Test]
    public void TweenRectTransformMoveCancelDontKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, false);
        Vector3 initialPosition = rect.anchoredPosition3D;
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.cancel();
        Assert.IsTrue(isAlive && t.isAlive && !isCompleted && t.isCompleted && isRunning && !t.isRunning && rect.anchoredPosition3D == initialPosition);
    }
    
    [Test]
    public void TweenRectTransformMoveCancelKillOnEnd()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, true);
        Vector3 initialPosition = rect.anchoredPosition3D;
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.cancel();
        Assert.IsTrue(isAlive && !t.isAlive && !isCompleted && t.isCompleted && isRunning && !t.isRunning && rect.anchoredPosition3D == initialPosition);
    }
    
    [Test]
    public void TweenRectTransformMoveCancelEvent()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration, true);
        bool triggered = false;
        t.onComplete(()=>{ triggered = true; }).cancel();
        Assert.IsTrue(!triggered);
    }

    [Test]
    public void TweenRectTransformScaleCancel()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialScale = rect.localScale;
        t.cancel();
        Assert.AreEqual(rect.localScale, initialScale);
    }

    [Test]
    public void TweenRectTransformRotateCancel()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialRotation = rect.localEulerAngles;
        t.cancel();
        Assert.AreEqual(rect.localEulerAngles, initialRotation);
    }
    
    [Test]
    public void TweenCanvasRendererFadeCancel()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.cancel();
        Assert.AreEqual(canvasRenderer.GetAlpha(), initialAlpha);
    }

    [Test]
    public void TweenSpriteRendererFadeCancel()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.cancel();
        Assert.AreEqual(spriteRenderer.color.a, initialAlpha);
    }

    [Test]
    public void TweenRawImageFadeCancel()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.cancel();
        Assert.AreEqual(rawImage.color.a, initialAlpha);
    }

    [Test]
    public void TweenImageFadeCancel()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.cancel();
        Assert.AreEqual(image.color.a, initialAlpha);
    }

    [Test]
    public void TweenTextFadeCancel()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.cancel();
        Assert.AreEqual(text.color.a, initialAlpha);
    }

    [Test]
    public void TweenGraphicFadeCancel()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.cancel();
        Assert.AreEqual(graphic.color.a, initialAlpha);
    }
    #endregion

    #region reset
    [Test]
    public void TweenRectTransformMoveReset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        Vector3 initialPosition = rect.anchoredPosition3D;
        bool isAlive = t.isAlive;
        bool isCompleted = t.isCompleted;
        bool isRunning = t.isRunning;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        Vector3 positionBeforeReset = rect.anchoredPosition3D;
        t.reset();
        Assert.IsTrue(isAlive && t.isAlive && !isCompleted && !t.isCompleted && isRunning && !t.isRunning 
            && positionBeforeReset != initialPosition && rect.anchoredPosition3D == initialPosition && rect.anchoredPosition3D != positionBeforeReset);
    }
    
    [Test]
    public void TweenRectTransformMoveResetLoopCountWhenRunning()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        int triggered = 0;
        t.loop(3).onLoop((loopsCompletedCount)=>{ triggered = loopsCompletedCount; }).update(0f); //First frame is not taken in count
        t.update(duration);
        t.update(duration);
        int loopsBeforeReset = triggered;
        t.reset();
        triggered = 0;
        t.update(0f); //First frame update is also reset 
        t.update(duration);
        Assert.IsTrue(loopsBeforeReset == 2 && triggered == 0);
    }
    
    [Test]
    public void TweenRectTransformMoveResetLoopCountWhenNotRunning()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("move", rect, toVectorValue, easeType, duration);
        int triggered = 0;
        t.loop(3).onLoop((loopsCompletedCount)=>{ triggered = loopsCompletedCount; }).update(0f); //First frame is not taken in count
        t.update(duration);
        t.update(duration);
        int loopsBeforeReset = triggered;
        t.reset();
        triggered = 0;
        t.update(0f); //First frame update is also reset 
        t.update(duration);
        Assert.IsTrue(loopsBeforeReset == 2 && triggered == 1);
    }
    
    [Test]
    public void TweenRectTransformScaleReset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("scale", rect, toVectorValue, easeType, duration);
        Vector3 initialScale = rect.localScale;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        Vector3 scaleBeforeReset = rect.localScale;
        t.reset();
        Assert.IsTrue(scaleBeforeReset != initialScale && rect.localScale == initialScale && rect.localScale != scaleBeforeReset);
    }

    [Test]
    public void TweenRectTransformRotateReset()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = new Tween("rotate", rect, toVectorValue, easeType, duration);
        Vector3 initialRotation = rect.localEulerAngles;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        Vector3 rotationBeforeReset = rect.localEulerAngles;
        t.reset();
        Assert.IsTrue(rotationBeforeReset != initialRotation && rect.localEulerAngles == initialRotation && rect.localEulerAngles != rotationBeforeReset);
    }
    
    [Test]
    public void TweenCanvasRendererFadeReset()
    {
        go.AddComponent<CanvasRenderer>();
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        Tween t = new Tween("fade", canvasRenderer, toFloatValue, easeType, duration);
        float initialAlpha = canvasRenderer.GetAlpha();
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        float alphaBeforeReset = canvasRenderer.GetAlpha();
        t.reset();
        Assert.IsTrue(alphaBeforeReset != initialAlpha && canvasRenderer.GetAlpha() == initialAlpha && canvasRenderer.GetAlpha() != alphaBeforeReset);
    }
    
    [Test]
    public void TweenSpriteRendererFadeReset()
    {
        go.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        Tween t = new Tween("fade", spriteRenderer, toFloatValue, easeType, duration);
        float initialAlpha = spriteRenderer.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        float alphaBeforeReset = spriteRenderer.color.a;
        t.reset();
        Assert.IsTrue(alphaBeforeReset != initialAlpha && spriteRenderer.color.a == initialAlpha && spriteRenderer.color.a != alphaBeforeReset);
    }
    
    [Test]
    public void TweenRawImageFadeReset()
    {
        go.AddComponent<RawImage>();
        RawImage rawImage = go.GetComponent<RawImage>();
        Tween t = new Tween("fade", rawImage, toFloatValue, easeType, duration);
        float initialAlpha = rawImage.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        float alphaBeforeReset = rawImage.color.a;
        t.reset();
        Assert.IsTrue(alphaBeforeReset != initialAlpha && rawImage.color.a == initialAlpha && rawImage.color.a != alphaBeforeReset);
    }
    
    [Test]
    public void TweenImageFadeReset()
    {
        go.AddComponent<Image>();
        Image image = go.GetComponent<Image>();
        Tween t = new Tween("fade", image, toFloatValue, easeType, duration);
        float initialAlpha = image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        float alphaBeforeReset = image.color.a;
        t.reset();
        Assert.IsTrue(alphaBeforeReset != initialAlpha && image.color.a == initialAlpha && image.color.a != alphaBeforeReset);
    }
    
    [Test]
    public void TweenTextFadeReset()
    {
        go.AddComponent<Text>();
        Text text = go.GetComponent<Text>();
        Tween t = new Tween("fade", text, toFloatValue, easeType, duration);
        float initialAlpha = text.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        float alphaBeforeReset = text.color.a;
        t.reset();
        Assert.IsTrue(alphaBeforeReset != initialAlpha && text.color.a == initialAlpha && text.color.a != alphaBeforeReset);
    }
    
    [Test]
    public void TweenGraphicFadeReset()
    {
        go.AddComponent<Image>();
        Graphic graphic = go.GetComponent<Graphic>();
        Tween t = new Tween("fade", graphic, toFloatValue, easeType, duration);
        float initialAlpha = graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2);
        float alphaBeforeReset = graphic.color.a;
        t.reset();
        Assert.IsTrue(alphaBeforeReset != initialAlpha && graphic.color.a == initialAlpha && graphic.color.a != alphaBeforeReset);
    }
    #endregion
#endregion
}