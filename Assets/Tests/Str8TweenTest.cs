#region namespaces
using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Str8lines.Tweening;
#endregion

public class Str8TweenTest
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
    public void Setup()
    {
        go = new GameObject();
        toVectorValue = new Vector2(2f, 2f);
        toFloatValue = 0.5f;
        easeType = Easing.EaseType.Linear;
        duration = 1f;
    }

    [TearDown]
    public void Teardown()
    {
        UnityEngine.Object.Destroy(go);
    }
#endregion

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
    public void MoveRectTransformDurationException()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.move(rect, toVectorValue, easeType, 0f);
        });
    }
    
    [Test]
    public void MoveRectTransformEngineInstantiation()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Str8Tween.move(rect, toVectorValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
    }

    [Test]
    public void MoveRectTransformResult()
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
    public void ScaleRectTransformDurationException()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.scale(rect, toVectorValue, easeType, 0f);
        });
    }
    
    [Test]
    public void ScaleRectTransformEngineInstantiation()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Str8Tween.scale(rect, toVectorValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
    }

    [Test]
    public void ScaleRectTransformResult()
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
    public void RotateRectTransformDurationException()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Assert.Throws<ArgumentException>(()=>{
            Str8Tween.rotate(rect, toVectorValue, easeType, 0f);
        });
    }
    
    [Test]
    public void RotateRectTransformEngineInstantiation()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Str8Tween.rotate(rect, toVectorValue, easeType, duration);
        GameObject engine = GameObject.Find("Str8Tween");
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
    }

    [Test]
    public void RotateRectTransformResult()
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
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
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
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
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
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
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
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
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
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
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
        Assert.AreNotEqual(engine.GetComponent<TweensHandler>(), null);
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
    #region get
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
    #endregion

    #region pause
    [Test]
    public void PauseTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isRunning = t.isRunning;
        Str8Tween.pause(t.id);
        Assert.AreNotEqual(isRunning, t.isRunning);
    }

    [Test]
    public void PauseTweenByEmptyId()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isRunning = t.isRunning;
        Str8Tween.pause(String.Empty);
        Assert.AreEqual(isRunning, t.isRunning);
    }

    [Test]
    public void PauseTweenByTargetNull()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Str8Tween.pause(go);
        });
    }

    [Test]
    public void PauseTweensByTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Running = t1.isRunning;
        bool isT2Running = t2.isRunning;
        Str8Tween.pause(go);
        Assert.IsTrue(isT1Running && !t1.isRunning && isT2Running && !t2.isRunning);
    }

    [Test]
    public void PauseAllTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Running = t1.isRunning;
        bool isT2Running = t2.isRunning;
        Str8Tween.pause();
        Assert.IsTrue(isT1Running && !t1.isRunning && isT2Running && !t2.isRunning);
    }
    #endregion

    #region play
    [Test]
    public void PlayTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Str8Tween.pause(t.id);
        bool isRunning = t.isRunning;
        Str8Tween.play(t.id);
        Assert.AreNotEqual(isRunning, t.isRunning);
    }

    [Test]
    public void PlayTweenByEmptyId()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Str8Tween.pause(t.id);
        bool isRunning = t.isRunning;
        Str8Tween.play(String.Empty);
        Assert.AreEqual(isRunning, t.isRunning);
    }

    [Test]
    public void PlayTweenByTargetNull()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Str8Tween.pause(go);
        });
    }

    [Test]
    public void PlayTweensByTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Str8Tween.pause(go);
        bool isT1Running = t1.isRunning;
        bool isT2Running = t2.isRunning;
        Str8Tween.play(go);
        Assert.IsTrue(!isT1Running && t1.isRunning && !isT2Running && t2.isRunning);
    }

    [Test]
    public void PlayAllTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Str8Tween.pause();
        bool isT1Running = t1.isRunning;
        bool isT2Running = t2.isRunning;
        Str8Tween.play();
        Assert.IsTrue(!isT1Running && t1.isRunning && !isT2Running && t2.isRunning);
    }
    #endregion

    #region reset
    [UnityTest]
    public IEnumerator ResetTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        yield return new WaitForSeconds(duration/2f);
        float elapsedSinceDelay = t.elapsedSinceDelay;
        Str8Tween.reset(t.id);
        Assert.IsTrue(elapsedSinceDelay != t.elapsedSinceDelay && t.elapsedSinceDelay == 0);
    }

    [UnityTest]
    public IEnumerator ResetTweenByEmptyId()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        float elapsedSinceDelay = t.elapsedSinceDelay;
        yield return new WaitForSeconds(duration/2f);
        Str8Tween.reset(String.Empty);
        Assert.IsTrue(elapsedSinceDelay != t.elapsedSinceDelay && t.elapsedSinceDelay != 0);
    }

    [Test]
    public void ResetTweenByTargetNull()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Str8Tween.pause(go);
        });
    }

    [UnityTest]
    public IEnumerator ResetTweensByTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        yield return new WaitForSeconds(duration/2f);
        float t1ElapsedSinceDelay = t1.elapsedSinceDelay;
        float t2ElapsedSinceDelay = t2.elapsedSinceDelay;
        Str8Tween.reset(go);
        Assert.IsTrue(t1ElapsedSinceDelay != t1.elapsedSinceDelay && t1.elapsedSinceDelay == 0 && t2ElapsedSinceDelay != t2.elapsedSinceDelay && t2.elapsedSinceDelay == 0);
    }

    [UnityTest]
    public IEnumerator ResetAllTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        yield return new WaitForSeconds(duration/2f);
        float t1ElapsedSinceDelay = t1.elapsedSinceDelay;
        float t2ElapsedSinceDelay = t2.elapsedSinceDelay;
        Str8Tween.reset();
        Assert.IsTrue(t1ElapsedSinceDelay != t1.elapsedSinceDelay && t1.elapsedSinceDelay == 0 && t2ElapsedSinceDelay != t2.elapsedSinceDelay && t2.elapsedSinceDelay == 0);
    }
    #endregion
    
    #region stop
    [Test]
    public void StopTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isFinished = t.isFinished;
        Str8Tween.stop(t.id);
        Assert.AreNotEqual(isFinished, t.isFinished);
    }

    [Test]
    public void StopTweenByEmptyId()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isFinished = t.isFinished;
        Str8Tween.stop(String.Empty);
        Assert.AreEqual(isFinished, t.isFinished);
    }

    [Test]
    public void StopTweenByTargetNull()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Str8Tween.pause(go);
        });
    }

    [Test]
    public void StopTweensByTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Finished = t1.isFinished;
        bool isT2Finished = t2.isFinished;
        Str8Tween.stop(go);
        Assert.IsTrue(!isT1Finished && t1.isFinished && !isT2Finished && t2.isFinished);
    }

    [Test]
    public void StopAllTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Finished = t1.isFinished;
        bool isT2Finished = t2.isFinished;
        Str8Tween.stop();
        Assert.IsTrue(!isT1Finished && t1.isFinished && !isT2Finished && t2.isFinished);
    }
    #endregion
    
    #region complete
    [Test]
    public void CompleteTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isFinished = t.isFinished;
        Str8Tween.complete(t.id);
        Assert.AreNotEqual(isFinished, t.isFinished);
    }

    [Test]
    public void CompleteTweenByEmptyId()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isFinished = t.isFinished;
        Str8Tween.complete(String.Empty);
        Assert.AreEqual(isFinished, t.isFinished);
    }

    [Test]
    public void CompleteTweenByTargetNull()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Str8Tween.pause(go);
        });
    }

    [Test]
    public void CompleteTweensByTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Finished = t1.isFinished;
        bool isT2Finished = t2.isFinished;
        Str8Tween.complete(go);
        Assert.IsTrue(!isT1Finished && t1.isFinished && !isT2Finished && t2.isFinished);
    }

    [Test]
    public void CompleteAllTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Finished = t1.isFinished;
        bool isT2Finished = t2.isFinished;
        Str8Tween.complete();
        Assert.IsTrue(!isT1Finished && t1.isFinished && !isT2Finished && t2.isFinished);
    }
    #endregion
    
    #region kill
    [Test]
    public void KillTweenById()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isAlive = t.isAlive;
        Str8Tween.kill(t.id);
        Assert.AreNotEqual(isAlive, t.isAlive);
    }

    [Test]
    public void KillTweenByEmptyId()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isAlive = t.isAlive;
        Str8Tween.kill(String.Empty);
        Assert.AreEqual(isAlive, t.isAlive);
    }

    [Test]
    public void KillTweenByTargetNull()
    {
        Assert.Throws<ArgumentNullException>(()=>{
            GameObject go = null;
            Str8Tween.pause(go);
        });
    }

    [Test]
    public void KillTweensByTarget()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Alive = t1.isAlive;
        bool isT2Alive = t2.isAlive;
        Str8Tween.kill(go);
        Assert.IsTrue(isT1Alive && !t1.isAlive && isT2Alive && !t2.isAlive);
    }

    [Test]
    public void KillAllTweens()
    {
        go.AddComponent<RectTransform>();
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t1 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        Tween t2 = Str8Tween.move(rect, toVectorValue, easeType, duration);
        bool isT1Alive = t1.isAlive;
        bool isT2Alive = t2.isAlive;
        Str8Tween.kill();
        Assert.IsTrue(isT1Alive && !t1.isAlive && isT2Alive && !t2.isAlive);
    }
    #endregion
#endregion
}