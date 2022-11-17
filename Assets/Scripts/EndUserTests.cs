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
}
