using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Str8lines.Tweening;

public class EndUserTests : MonoBehaviour
{
    public GameObject target;
    public string id;
    public float duration;
    public float delay;
    public bool isLoop;
    public Tween.LoopType loopType;
    public int loopsCount;
    
    /*
        * Tester tous les ease types
        * Tester les différents Tweens (move, scale, rotate, fade)
            * Move => up, right, down, left + combinations
            * Scale => up, down
            * Rotate => below 360 + Higher than 360, sens horaire et anti horaire
            * Fade => in and out

        - On va aussi bourrer le nb de trucs à check par test (delay, events)
        - Pour tester les reset/complete/kill etc il va falloir instantier des btns qui déclenchent ces commandes pour tous les Tweens/les Tweens d'une target/un Tween précis
    */

    void Start()
    {
        GameObject canvas = Resources.Load<GameObject>("TestCanvas");
        GameObject canvasGo = UnityEngine.Object.Instantiate(canvas);
        GameObject eventSystem = Resources.Load<GameObject>("EventSystem");
        GameObject eventSystemGo = UnityEngine.Object.Instantiate(eventSystem);

    #region Move
        #region In
        Transform movePanel = canvasGo.transform.GetChild(0);
        _move(movePanel, 1, Easing.EaseType.Linear, 225f);
        _move(movePanel, 2, Easing.EaseType.InSine, 225f);
        _move(movePanel, 3, Easing.EaseType.InQuad, 225f);
        _move(movePanel, 4, Easing.EaseType.InCubic, 225f);
        _move(movePanel, 5, Easing.EaseType.InQuart, 225f);
        _move(movePanel, 6, Easing.EaseType.InQuint, 225f);
        _move(movePanel, 7, Easing.EaseType.InExpo, 225f);
        _move(movePanel, 8, Easing.EaseType.InCirc, 225f);
        _move(movePanel, 9, Easing.EaseType.InBack, 225f);
        _move(movePanel, 10, Easing.EaseType.InElastic, 225f);
        _move(movePanel, 11, Easing.EaseType.InBounce, 225f);
        #endregion

        #region Out
        _move(movePanel, 12, Easing.EaseType.OutSine, 475f);
        _move(movePanel, 13, Easing.EaseType.OutQuad, 475f);
        _move(movePanel, 14, Easing.EaseType.OutCubic, 475f);
        _move(movePanel, 15, Easing.EaseType.OutQuart, 475f);
        _move(movePanel, 16, Easing.EaseType.OutQuint, 475f);
        _move(movePanel, 17, Easing.EaseType.OutExpo, 475f);
        _move(movePanel, 18, Easing.EaseType.OutCirc, 475f);
        _move(movePanel, 19, Easing.EaseType.OutBack, 475f);
        _move(movePanel, 20, Easing.EaseType.OutElastic, 475f);
        _move(movePanel, 21, Easing.EaseType.OutBounce, 475f);
        #endregion

        #region InOut
        _move(movePanel, 22, Easing.EaseType.InOutSine, 700f);
        _move(movePanel, 23, Easing.EaseType.InOutQuad, 700f);
        _move(movePanel, 24, Easing.EaseType.InOutCubic, 700f);
        _move(movePanel, 25, Easing.EaseType.InOutQuart, 700f);
        _move(movePanel, 26, Easing.EaseType.InOutQuint, 700f);
        _move(movePanel, 27, Easing.EaseType.InOutExpo, 700f);
        _move(movePanel, 28, Easing.EaseType.InOutCirc, 700f);
        _move(movePanel, 29, Easing.EaseType.InOutBack, 700f);
        _move(movePanel, 30, Easing.EaseType.InOutElastic, 700f);
        _move(movePanel, 31, Easing.EaseType.InOutBounce, 700f);
        #endregion
    #endregion

    #region Scale
        #region In
        Transform scalePanel = canvasGo.transform.GetChild(1);
        _scale(scalePanel, 1, Easing.EaseType.Linear);
        _scale(scalePanel, 2, Easing.EaseType.InSine);
        _scale(scalePanel, 3, Easing.EaseType.InQuad);
        _scale(scalePanel, 4, Easing.EaseType.InCubic);
        _scale(scalePanel, 5, Easing.EaseType.InQuart);
        _scale(scalePanel, 6, Easing.EaseType.InQuint);
        _scale(scalePanel, 7, Easing.EaseType.InExpo);
        _scale(scalePanel, 8, Easing.EaseType.InCirc);
        _scale(scalePanel, 9, Easing.EaseType.InBack);
        _scale(scalePanel, 10, Easing.EaseType.InElastic);
        _scale(scalePanel, 11, Easing.EaseType.InBounce);
        #endregion

        #region Out
        _scale(scalePanel, 12, Easing.EaseType.OutSine);
        _scale(scalePanel, 13, Easing.EaseType.OutQuad);
        _scale(scalePanel, 14, Easing.EaseType.OutCubic);
        _scale(scalePanel, 15, Easing.EaseType.OutQuart);
        _scale(scalePanel, 16, Easing.EaseType.OutQuint);
        _scale(scalePanel, 17, Easing.EaseType.OutExpo);
        _scale(scalePanel, 18, Easing.EaseType.OutCirc);
        _scale(scalePanel, 19, Easing.EaseType.OutBack);
        _scale(scalePanel, 20, Easing.EaseType.OutElastic);
        _scale(scalePanel, 21, Easing.EaseType.OutBounce);
        #endregion

        #region InOut
        _scale(scalePanel, 22, Easing.EaseType.InOutSine);
        _scale(scalePanel, 23, Easing.EaseType.InOutQuad);
        _scale(scalePanel, 24, Easing.EaseType.InOutCubic);
        _scale(scalePanel, 25, Easing.EaseType.InOutQuart);
        _scale(scalePanel, 26, Easing.EaseType.InOutQuint);
        _scale(scalePanel, 27, Easing.EaseType.InOutExpo);
        _scale(scalePanel, 28, Easing.EaseType.InOutCirc);
        _scale(scalePanel, 29, Easing.EaseType.InOutBack);
        _scale(scalePanel, 30, Easing.EaseType.InOutElastic);
        _scale(scalePanel, 31, Easing.EaseType.InOutBounce);
        #endregion
    #endregion
    
    #region Rotate
        #region In
        Transform rotatePanel = canvasGo.transform.GetChild(2);
        _rotate(rotatePanel, 1, Easing.EaseType.Linear, 60f);
        _rotate(rotatePanel, 2, Easing.EaseType.InSine, 60f);
        _rotate(rotatePanel, 3, Easing.EaseType.InQuad, 60f);
        _rotate(rotatePanel, 4, Easing.EaseType.InCubic, 60f);
        _rotate(rotatePanel, 5, Easing.EaseType.InQuart, 60f);
        _rotate(rotatePanel, 6, Easing.EaseType.InQuint, 60f);
        _rotate(rotatePanel, 7, Easing.EaseType.InExpo, 60f);
        _rotate(rotatePanel, 8, Easing.EaseType.InCirc, 60f);
        _rotate(rotatePanel, 9, Easing.EaseType.InBack, 60f);
        _rotate(rotatePanel, 10, Easing.EaseType.InElastic, 60f);
        _rotate(rotatePanel, 11, Easing.EaseType.InBounce, 60f);
        #endregion

        #region Out
        _rotate(rotatePanel, 12, Easing.EaseType.OutSine, 60f);
        _rotate(rotatePanel, 13, Easing.EaseType.OutQuad, 60f);
        _rotate(rotatePanel, 14, Easing.EaseType.OutCubic, 60f);
        _rotate(rotatePanel, 15, Easing.EaseType.OutQuart, 60f);
        _rotate(rotatePanel, 16, Easing.EaseType.OutQuint, 60f);
        _rotate(rotatePanel, 17, Easing.EaseType.OutExpo, 60f);
        _rotate(rotatePanel, 18, Easing.EaseType.OutCirc, 60f);
        _rotate(rotatePanel, 19, Easing.EaseType.OutBack, 60f);
        _rotate(rotatePanel, 20, Easing.EaseType.OutElastic, 60f);
        _rotate(rotatePanel, 21, Easing.EaseType.OutBounce, 60f);
        #endregion

        #region InOut
        _rotate(rotatePanel, 22, Easing.EaseType.InOutSine, 60f);
        _rotate(rotatePanel, 23, Easing.EaseType.InOutQuad, 60f);
        _rotate(rotatePanel, 24, Easing.EaseType.InOutCubic, 60f);
        _rotate(rotatePanel, 25, Easing.EaseType.InOutQuart, 60f);
        _rotate(rotatePanel, 26, Easing.EaseType.InOutQuint, 60f);
        _rotate(rotatePanel, 27, Easing.EaseType.InOutExpo, 60f);
        _rotate(rotatePanel, 28, Easing.EaseType.InOutCirc, 60f);
        _rotate(rotatePanel, 29, Easing.EaseType.InOutBack, 60f);
        _rotate(rotatePanel, 30, Easing.EaseType.InOutElastic, 60f);
        _rotate(rotatePanel, 31, Easing.EaseType.InOutBounce, 60f);
        #endregion
    #endregion

    #region FadeCanvasRenderer
        #region In
        Transform fadePanel = canvasGo.transform.GetChild(3);
        Transform fadeCanvasRendererPanel = fadePanel.GetChild(0);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeCanvasRenderer(fadeCanvasRendererPanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion
    
    #region FadeSpriteRenderer
        #region In
        Transform fadeSpriteRendererPanel = fadePanel.GetChild(1);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeSpriteRenderer(fadeSpriteRendererPanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion
    
    #region FadeRawImage
        #region In
        Transform fadeRawImagePanel = fadePanel.GetChild(2);
        _fadeRawImage(fadeRawImagePanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeRawImage(fadeRawImagePanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeRawImage(fadeRawImagePanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeRawImage(fadeRawImagePanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion

    #region FadeImage
        #region In
        Transform fadeImagePanel = fadePanel.GetChild(3);
        _fadeImage(fadeImagePanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeImage(fadeImagePanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeImage(fadeImagePanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeImage(fadeImagePanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeImage(fadeImagePanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeImage(fadeImagePanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeImage(fadeImagePanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeImage(fadeImagePanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeImage(fadeImagePanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeImage(fadeImagePanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeImage(fadeImagePanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeImage(fadeImagePanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeImage(fadeImagePanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeImage(fadeImagePanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeImage(fadeImagePanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeImage(fadeImagePanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeImage(fadeImagePanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeImage(fadeImagePanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeImage(fadeImagePanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeImage(fadeImagePanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeImage(fadeImagePanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeImage(fadeImagePanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeImage(fadeImagePanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeImage(fadeImagePanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeImage(fadeImagePanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeImage(fadeImagePanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeImage(fadeImagePanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeImage(fadeImagePanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeImage(fadeImagePanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeImage(fadeImagePanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeImage(fadeImagePanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion

    #region FadeText
        #region In
        Transform fadeTextPanel = fadePanel.GetChild(4);
        _fadeText(fadeTextPanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeText(fadeTextPanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeText(fadeTextPanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeText(fadeTextPanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeText(fadeTextPanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeText(fadeTextPanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeText(fadeTextPanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeText(fadeTextPanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeText(fadeTextPanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeText(fadeTextPanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeText(fadeTextPanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeText(fadeTextPanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeText(fadeTextPanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeText(fadeTextPanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeText(fadeTextPanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeText(fadeTextPanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeText(fadeTextPanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeText(fadeTextPanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeText(fadeTextPanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeText(fadeTextPanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeText(fadeTextPanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeText(fadeTextPanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeText(fadeTextPanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeText(fadeTextPanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeText(fadeTextPanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeText(fadeTextPanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeText(fadeTextPanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeText(fadeTextPanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeText(fadeTextPanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeText(fadeTextPanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeText(fadeTextPanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion

    #region FadeToggle
        #region In
        Transform fadeTogglePanel = fadePanel.GetChild(5);
        _fadeToggle(fadeTogglePanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeToggle(fadeTogglePanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeToggle(fadeTogglePanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeToggle(fadeTogglePanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeToggle(fadeTogglePanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeToggle(fadeTogglePanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeToggle(fadeTogglePanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeToggle(fadeTogglePanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeToggle(fadeTogglePanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeToggle(fadeTogglePanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeToggle(fadeTogglePanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeToggle(fadeTogglePanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeToggle(fadeTogglePanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeToggle(fadeTogglePanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeToggle(fadeTogglePanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeToggle(fadeTogglePanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeToggle(fadeTogglePanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeToggle(fadeTogglePanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeToggle(fadeTogglePanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeToggle(fadeTogglePanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeToggle(fadeTogglePanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeToggle(fadeTogglePanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeToggle(fadeTogglePanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeToggle(fadeTogglePanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeToggle(fadeTogglePanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeToggle(fadeTogglePanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeToggle(fadeTogglePanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeToggle(fadeTogglePanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeToggle(fadeTogglePanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeToggle(fadeTogglePanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeToggle(fadeTogglePanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion

    #region FadeSlider
        #region In
        Transform fadeSliderPanel = fadePanel.GetChild(6);
        _fadeSlider(fadeSliderPanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeSlider(fadeSliderPanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeSlider(fadeSliderPanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeSlider(fadeSliderPanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeSlider(fadeSliderPanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeSlider(fadeSliderPanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeSlider(fadeSliderPanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeSlider(fadeSliderPanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeSlider(fadeSliderPanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeSlider(fadeSliderPanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeSlider(fadeSliderPanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeSlider(fadeSliderPanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeSlider(fadeSliderPanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeSlider(fadeSliderPanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeSlider(fadeSliderPanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeSlider(fadeSliderPanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeSlider(fadeSliderPanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeSlider(fadeSliderPanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeSlider(fadeSliderPanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeSlider(fadeSliderPanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeSlider(fadeSliderPanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeSlider(fadeSliderPanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeSlider(fadeSliderPanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeSlider(fadeSliderPanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeSlider(fadeSliderPanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeSlider(fadeSliderPanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeSlider(fadeSliderPanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeSlider(fadeSliderPanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeSlider(fadeSliderPanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeSlider(fadeSliderPanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeSlider(fadeSliderPanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion

    #region FadeGraphic
        #region In
        Transform fadeGraphicPanel = fadePanel.GetChild(7);
        _fadeGraphic(fadeGraphicPanel, 0, Easing.EaseType.Linear, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 1, Easing.EaseType.InSine, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 2, Easing.EaseType.InQuad, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 3, Easing.EaseType.InCubic, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 4, Easing.EaseType.InQuart, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 5, Easing.EaseType.InQuint, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 6, Easing.EaseType.InExpo, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 7, Easing.EaseType.InCirc, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 8, Easing.EaseType.InBack, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 9, Easing.EaseType.InElastic, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 10, Easing.EaseType.InBounce, 0.2f);
        #endregion

        #region Out
        _fadeGraphic(fadeGraphicPanel, 11, Easing.EaseType.OutSine, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 12, Easing.EaseType.OutQuad, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 13, Easing.EaseType.OutCubic, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 14, Easing.EaseType.OutQuart, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 15, Easing.EaseType.OutQuint, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 16, Easing.EaseType.OutExpo, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 17, Easing.EaseType.OutCirc, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 18, Easing.EaseType.OutBack, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 19, Easing.EaseType.OutElastic, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 20, Easing.EaseType.OutBounce, 0.2f);
        #endregion

        #region InOut
        _fadeGraphic(fadeGraphicPanel, 21, Easing.EaseType.InOutSine, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 22, Easing.EaseType.InOutQuad, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 23, Easing.EaseType.InOutCubic, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 24, Easing.EaseType.InOutQuart, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 25, Easing.EaseType.InOutQuint, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 26, Easing.EaseType.InOutExpo, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 27, Easing.EaseType.InOutCirc, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 28, Easing.EaseType.InOutBack, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 29, Easing.EaseType.InOutElastic, 0.2f);
        _fadeGraphic(fadeGraphicPanel, 30, Easing.EaseType.InOutBounce, 0.2f);
        #endregion
    #endregion

    }

    void Update()
    {
        
    }

    private void _move(Transform movePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject moveGo = movePanel.GetChild(childIndex).gameObject;
        RectTransform rect = moveGo.GetComponent<RectTransform>();
        Text text = moveGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.move(rect, new Vector3(x, rect.anchoredPosition.y, 0f), easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _scale(Transform scalePanel, int childIndex, Easing.EaseType easeType){
        GameObject scaleGo = scalePanel.GetChild(childIndex).gameObject;
        RectTransform rect = scaleGo.GetComponent<RectTransform>();
        Text text = scaleGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.scale(rect, Vector3.one * 1.5f, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _rotate(Transform rotatePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject rotateGo = rotatePanel.GetChild(childIndex).gameObject;
        RectTransform rect = rotateGo.GetComponent<RectTransform>();
        Text text = rotateGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.rotate(rect, new Vector3(rect.eulerAngles.x, rect.eulerAngles.y, rect.eulerAngles.z + x), easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeCanvasRenderer(Transform fadeCanvasRendererPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeCanvasRendererGo = fadeCanvasRendererPanel.GetChild(childIndex).gameObject;
        CanvasRenderer canvasRenderer = fadeCanvasRendererGo.GetComponent<CanvasRenderer>();
        Text text = fadeCanvasRendererGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(canvasRenderer, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeSpriteRenderer(Transform fadeSpriteRendererPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeSpriteRendererGo = fadeSpriteRendererPanel.GetChild(childIndex).gameObject;
        SpriteRenderer spriteRenderer = fadeSpriteRendererGo.GetComponent<SpriteRenderer>();
        Text text = fadeSpriteRendererGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(spriteRenderer, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeRawImage(Transform fadeRawImagePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeRawImageGo = fadeRawImagePanel.GetChild(childIndex).gameObject;
        RawImage rawImage = fadeRawImageGo.GetComponent<RawImage>();
        Text text = fadeRawImageGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(rawImage, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeImage(Transform fadeImagePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeImageGo = fadeImagePanel.GetChild(childIndex).gameObject;
        Image image = fadeImageGo.GetComponent<Image>();
        Text text = fadeImageGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(image, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeText(Transform fadeTextPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeTextGo = fadeTextPanel.GetChild(childIndex).gameObject;
        Text txt = fadeTextGo.GetComponent<Text>();
        Text text = fadeTextGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(txt, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeToggle(Transform fadeTogglePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeToggleGo = fadeTogglePanel.GetChild(childIndex).gameObject;
        Toggle toggle = fadeToggleGo.GetComponent<Toggle>();
        Text text = fadeToggleGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(toggle, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeSlider(Transform fadeSliderPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeSliderGo = fadeSliderPanel.GetChild(childIndex).gameObject;
        Slider slider = fadeSliderGo.GetComponent<Slider>();
        Text text = fadeSliderGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(slider, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeGraphic(Transform fadeGraphicPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeGraphicGo = fadeGraphicPanel.GetChild(childIndex).gameObject;
        Graphic graphic = fadeGraphicGo.GetComponent<Image>();
        Text text = fadeGraphicGo.transform.GetChild(0).gameObject.GetComponent<Text>();
        Tween t = Str8Tween.fade(graphic, x, easeType, duration);
        t.delay(delay);
        text.text = easeType.ToString() + "\n" + t.id;
    }
}
