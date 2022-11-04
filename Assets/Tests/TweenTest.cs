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
    /**
        - Tween methods :
            * _completeLoop()
            * onStart()
            * onLoop()
            * onComplete()
            
            * pause()
            * play()
            * stop()
            * kill()
            * reset() => test values of isCompleted and isRunning + check reset of vector and float
            * complete() => test values of isCompleted and isRunning + check final values of vector and float + invoke event + killonend
            * cancel() => test values of isCompleted and isRunning + check reset of vector and float + killonend
            * Kept alive with killOnEnd false
    */

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

    #region fade toggle
    [Test]
    public void TweenToggleFadeDefaultKill()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == target.gameObject) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenToggleFadeKillOnEnd()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == target.gameObject) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenToggleFadeDontKillOnEnd()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == target.gameObject) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenToggleFadeNullTarget()
    {
        Toggle toggle = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenToggleInvalidMethod()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();
        
        // Test
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, toggle, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenToggleFadeZeroDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();
        
        // Test
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", toggle, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenToggleFadeNegativeDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();
        
        // Test
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", toggle, toFloatValue, easeType, -1f);
        });
    }
    #endregion

    #region fade slider
    [Test]
    public void TweenSliderFadeDefaultKill()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration);
        Assert.IsTrue((t.id.Length == 36) && (t.target == target.gameObject) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenSliderFadeKillOnEnd()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration, true);
        Assert.IsTrue((t.id.Length == 36) && (t.target == target.gameObject) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }

    [Test]
    public void TweenSliderFadeDontKillOnEnd()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration, false);
        Assert.IsTrue((t.id.Length == 36) && (t.target == target.gameObject) && (t.easeType == easeType) && (t.duration == duration) && t.isAlive, "Properties not properly set");
    }
    
    [Test]
    public void TweenSliderFadeNullTarget()
    {
        Slider slider = null;
        Assert.Throws<ArgumentNullException>(()=>{
            Tween t = new Tween("fade", slider, toFloatValue, easeType, duration);
        });
    }

    [Test]
    public void TweenSliderInvalidMethod()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();
        
        // Test
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween(String.Empty, slider, toFloatValue, easeType, duration);
        });
    }
    
    [Test]
    public void TweenSliderFadeZeroDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();
        
        // Test
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", slider, toFloatValue, easeType, 0f);
        });
    }

    [Test]
    public void TweenSliderFadeNegativeDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();
        
        // Test
        Assert.Throws<ArgumentException>(()=>{
            Tween t = new Tween("fade", slider, toFloatValue, easeType, -1f);
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
    #endregion

#region update
    #region move
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
    #endregion

    #region scale
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
    #endregion

    #region rotate
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
    #endregion

    #region fade canvasRenderer
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
    #endregion

    #region fade spriteRenderer
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
    #endregion

    #region fade rawImage
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
    #endregion

    #region fade image
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
    #endregion

    #region fade text
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
    #endregion

    #region fade toggle
    [Test]
    public void TweenToggleFadeUpdateWithTimeEqualZero()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration);
        float initialAlpha = toggle.graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == toggle.graphic.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenToggleFadeUpdateWithTimeBelowDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration);
        float initialAlpha = toggle.graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != toggle.graphic.color.a && toFloatValue != toggle.graphic.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenToggleFadeUpdateWithTimeEqualDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration);
        float initialAlpha = toggle.graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == toggle.graphic.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenToggleFadeUpdateWithTimeHigherThanDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(0);
        Toggle toggle = target.GetComponent<Toggle>();

        // Test
        Tween t = new Tween("fade", toggle, toFloatValue, easeType, duration);
        float initialAlpha = toggle.graphic.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == toggle.graphic.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }
    #endregion

    #region fade slider
    [Test]
    public void TweenSliderFadeUpdateWithTimeEqualZero()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration);
        float initialAlpha = slider.image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(0f);
        Assert.IsTrue(initialAlpha == slider.image.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenSliderFadeUpdateWithTimeBelowDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration);
        float initialAlpha = slider.image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration/2f);
        Assert.IsTrue(initialAlpha != slider.image.color.a && toFloatValue != slider.image.color.a && t.isAlive && !t.isCompleted && t.isRunning);
    }

    [Test]
    public void TweenSliderFadeUpdateWithTimeEqualDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration);
        float initialAlpha = slider.image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration);
        Assert.IsTrue(toFloatValue == slider.image.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }

    [Test]
    public void TweenSliderFadeUpdateWithTimeHigherThanDuration()
    {
        //Prepare
        GameObject canvas = Resources.Load<GameObject>("Canvas");
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        Transform target = canvas.transform.GetChild(1);
        Slider slider = target.GetComponent<Slider>();

        // Test
        Tween t = new Tween("fade", slider, toFloatValue, easeType, duration);
        float initialAlpha = slider.image.color.a;
        t.update(0f); //First frame is not taken in count
        t.update(duration + 0.5f);
        Assert.IsTrue(toFloatValue == slider.image.color.a && !t.isAlive && t.isCompleted && !t.isRunning);
    }
    #endregion

    #region fade graphic
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
    #endregion
#endregion

#region events
    // Créer un tween pour chaque type de Tween (move, scale, rotate et tous les fade)    
    // Ajouter les events de start, loop, end
    // Assert que les events sont bien triggered
#endregion

#region lifecycle

#endregion
}