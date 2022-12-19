using System;
using UnityEngine;
using UnityEngine.UI;
using Str8lines.Tweening;

public class EndUserTests : MonoBehaviour 
{    
    public bool controlAll;
    public GameObject target;
    public string id;
    public float duration;
    public float delay;
    public bool killOnEnd;
    public bool playOnReset;
    public bool isLoop;
    public Tween.LoopType loopType;
    public int loopsCount;
    private GameObject activePanel;
    private Transform panelsContainer;
    private Transform header;
    private Transform monitor;
    private int panelIndex;
    private int fadeSetIndex;

    void Start()
    {
        panelIndex = 0;
        fadeSetIndex = 0;
        header = this.gameObject.transform.GetChild(0);
        monitor = header.GetChild(2);
        panelsContainer = this.gameObject.transform.GetChild(1);
        for(int i = 0; i < panelsContainer.childCount - 1; i++) panelsContainer.GetChild(i).gameObject.SetActive(false);
        _updatePanel();
    }

    private void Update() {
        Tween[] tweens = Str8Tween.get();
        int running = 0;
        int completed = 0;
        int paused = 0;
        foreach(Tween t in tweens){
            if(!t.isAlive) continue;
            if(t.isCompleted) completed++;
            else{
                if(t.isRunning) running++;
                else paused++;
            }
        }
        monitor.GetChild(0).GetComponent<Text>().text = "Total = " + tweens.Length;
        monitor.GetChild(1).GetComponent<Text>().text = "Running = " + running;
        monitor.GetChild(2).GetComponent<Text>().text = "Completed = " + completed;
        monitor.GetChild(3).GetComponent<Text>().text = "Paused = " + paused;
    }

    #region public
    public void Next(){
        panelsContainer.GetChild(panelIndex).gameObject.SetActive(false);
        if(panelIndex < 3) panelIndex++;
        _updatePanel();
    }

    public void Previous(){
        panelsContainer.GetChild(panelIndex).gameObject.SetActive(false);
        if(panelIndex > 0) panelIndex--;
        _updatePanel();
    }

    public void ChangeFadeTarget(Dropdown change){
        Transform fadePanel = panelsContainer.GetChild(3);
        for(int i = 0; i < fadePanel.childCount; i++) fadePanel.GetChild(i).gameObject.SetActive(false);
        fadeSetIndex = change.value;
        activePanel = fadePanel.GetChild(fadeSetIndex).gameObject;
        activePanel.SetActive(true);
    }

    public void StartTweens(){
        int easeTypesCount = Enum.GetValues(typeof(Easing.EaseType)).Length;
        switch(panelIndex){
            case 0:
            #region Move
                Transform movePanel = panelsContainer.GetChild(0);
                float xValue = 225f;
                for(int i = 0; i < easeTypesCount; i++){
                    GameObject go = movePanel.GetChild(i).gameObject;
                    if(i == 0) _move(go, (Easing.EaseType)i, xValue); //Linear
                    else if(i <= 10) _move(go, (Easing.EaseType)(3*i - 2), xValue); //In
                    else if(i > 10 && i <= 20) _move(go, (Easing.EaseType)(3*(i - 10) - 1), 475f); //Out
                    else _move(go, (Easing.EaseType)(3*(i - 20)), 700f); //InOut
                }
            #endregion
                break;

            case 1:
            #region Scale
                Transform scalePanel = panelsContainer.GetChild(1);
                for(int i = 0; i < easeTypesCount; i++){
                    GameObject go = scalePanel.GetChild(i).gameObject;
                    if(i == 0) _scale(go, (Easing.EaseType)i); //Linear
                    else if(i <= 10) _scale(go, (Easing.EaseType)(3*i - 2)); //In
                    else if(i > 10 && i <= 20) _scale(go, (Easing.EaseType)(3*(i - 10) - 1)); //Out
                    else _scale(go, (Easing.EaseType)(3*(i - 20))); //InOut
                }
            #endregion
                break;

            case 2:
            #region Rotate
                Transform rotatePanel = panelsContainer.GetChild(2);
                for(int i = 0; i < easeTypesCount; i++){
                    GameObject go = rotatePanel.GetChild(i).gameObject;
                    if(i == 0) _rotate(go, (Easing.EaseType)i, 60f); //Linear
                    else if(i <= 10) _rotate(go, (Easing.EaseType)(3*i - 2), 60f); //In
                    else if(i > 10 && i <= 20) _rotate(go, (Easing.EaseType)(3*(i - 10) - 1), 60f); //Out
                    else _rotate(go, (Easing.EaseType)(3*(i - 20)), 60f); //InOut
                }
            #endregion
                break;
                
            case 3:
                Transform fadePanel = panelsContainer.GetChild(3);
                switch(fadeSetIndex){
                    case 0:
                    #region FadeCanvasRenderer
                        Transform fadeCanvasRendererPanel = fadePanel.GetChild(0);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                    #endregion
                        break;

                    case 1:
                    #region FadeSpriteRenderer
                        Transform fadeSpriteRendererPanel = fadePanel.GetChild(1);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                    #endregion
                        break;

                    case 2:
                    #region FadeImage
                        Transform fadeImagePanel = fadePanel.GetChild(2);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeImage(fadeImagePanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeImage(fadeImagePanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeImage(fadeImagePanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeImage(fadeImagePanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                    #endregion
                        break;
                        
                    case 3:
                    #region FadeRawImage
                        Transform fadeRawImagePanel = fadePanel.GetChild(3);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                    #endregion
                        break;
                        
                    case 4:
                    #region FadeText
                        Transform fadeTextPanel = fadePanel.GetChild(4);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeText(fadeTextPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeText(fadeTextPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeText(fadeTextPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeText(fadeTextPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                    #endregion
                        break;
                        
                    case 5:
                    #region FadeGraphic
                        Transform fadeGraphicPanel = fadePanel.GetChild(5);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                    #endregion
                        break;
                }

                break;
        }
    }

    public void PlayTweens(){
        if(controlAll) Str8Tween.play();
        else{
            if(target != null) Str8Tween.play(target);
            else if(id != String.Empty) Str8Tween.play(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.play(activePanel.transform.GetChild(i).gameObject);
        }
    }

    public void PauseTweens(){
        if(controlAll) Str8Tween.pause();
        else{
            if(target != null) Str8Tween.pause(target);
            else if(id != String.Empty) Str8Tween.pause(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.pause(activePanel.transform.GetChild(i).gameObject);
        }
    }

    public void StopTweens(){
        if(controlAll) Str8Tween.stop();
        else{
            if(target != null) Str8Tween.stop(target);
            else if(id != String.Empty) Str8Tween.stop(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.stop(activePanel.transform.GetChild(i).gameObject);
        }
    }

    public void CompleteTweens(){
        if(controlAll) Str8Tween.complete();
        else{
            if(target != null) Str8Tween.complete(target);
            else if(id != String.Empty) Str8Tween.complete(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.complete(activePanel.transform.GetChild(i).gameObject);
        }
    }

    public void ResetTweens(){
        if(controlAll) Str8Tween.reset();
        else{
            if(target != null) Str8Tween.reset(target);
            else if(id != String.Empty) Str8Tween.reset(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.reset(activePanel.transform.GetChild(i).gameObject);
        }
    }

    public void CancelTweens(){
        if(controlAll) Str8Tween.cancel();
        else{
            if(target != null) Str8Tween.cancel(target);
            else if(id != String.Empty) Str8Tween.cancel(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.cancel(activePanel.transform.GetChild(i).gameObject);
        }
    }

    public void KillTweens(){
        if(controlAll) Str8Tween.kill();
        else{
            if(target != null) Str8Tween.kill(target);
            else if(id != String.Empty) Str8Tween.kill(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.kill(activePanel.transform.GetChild(i).gameObject);
        }
    }
    #endregion

    #region private
    private void _move(GameObject go, Easing.EaseType easeType, float x){
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.move(rect, new Vector3(x, rect.anchoredPosition.y, 0f), easeType, duration, killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _scale(GameObject go, Easing.EaseType easeType){
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.scale(rect, Vector3.one * 1.5f, easeType, duration, killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _rotate(GameObject go, Easing.EaseType easeType, float x){
        RectTransform rect = go.GetComponent<RectTransform>();
        Tween t = Str8Tween.rotate(rect, new Vector3(rect.eulerAngles.x, rect.eulerAngles.y, rect.eulerAngles.z + x), easeType, duration, killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _fadeCanvasRenderer(Transform fadeCanvasRendererPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeCanvasRendererGo = fadeCanvasRendererPanel.GetChild(childIndex).gameObject;
        CanvasRenderer canvasRenderer = fadeCanvasRendererGo.GetComponent<CanvasRenderer>();
        Tween t = Str8Tween.fade(canvasRenderer, x, easeType, duration, killOnEnd);
        _handleTweenUpdates(fadeCanvasRendererGo, t, easeType);
    }

    private void _fadeSpriteRenderer(Transform fadeSpriteRendererPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeSpriteRendererGo = fadeSpriteRendererPanel.GetChild(childIndex).gameObject;
        SpriteRenderer spriteRenderer = fadeSpriteRendererGo.GetComponent<SpriteRenderer>();
        Tween t = Str8Tween.fade(spriteRenderer, x, easeType, duration, killOnEnd);
        _handleTweenUpdates(fadeSpriteRendererGo, t, easeType);
    }

    private void _fadeRawImage(Transform fadeRawImagePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeRawImageGo = fadeRawImagePanel.GetChild(childIndex).gameObject;
        RawImage rawImage = fadeRawImageGo.GetComponent<RawImage>();
        Tween t = Str8Tween.fade(rawImage, x, easeType, duration, killOnEnd);
        _handleTweenUpdates(fadeRawImageGo, t, easeType);
    }

    private void _fadeImage(Transform fadeImagePanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeImageGo = fadeImagePanel.GetChild(childIndex).gameObject;
        Image image = fadeImageGo.GetComponent<Image>();
        Tween t = Str8Tween.fade(image, x, easeType, duration, killOnEnd);
        _handleTweenUpdates(fadeImageGo, t, easeType);
    }

    private void _fadeText(Transform fadeTextPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeTextGo = fadeTextPanel.GetChild(childIndex).gameObject;
        Text txt = fadeTextGo.GetComponent<Text>();
        Tween t = Str8Tween.fade(txt, x, easeType, duration, killOnEnd);
        t.delay(delay);
        if(isLoop) t.loop(loopsCount, loopType);
        string logRoot = easeType.ToString() + " (" + t.id + ") ";
        t.onStart(() => Debug.Log(logRoot + "Started")).onComplete(()=> Debug.Log(logRoot + "Completed"));
        if(isLoop) t.onLoop((loopsCount) => Debug.Log(logRoot + "Loops = " + loopsCount));
        txt.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeGraphic(Transform fadeGraphicPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeGraphicGo = fadeGraphicPanel.GetChild(childIndex).gameObject;
        Graphic graphic = fadeGraphicGo.GetComponent<Image>();
        Tween t = Str8Tween.fade(graphic, x, easeType, duration, killOnEnd);
        _handleTweenUpdates(fadeGraphicGo, t, easeType);
    }

    private void _updatePanel(){
        activePanel = panelsContainer.GetChild(panelIndex).gameObject;
        activePanel.SetActive(true);
        header.GetChild(0).GetComponent<Text>().text = activePanel.name.Replace("Panel", String.Empty);
        if(panelIndex == 3) header.GetChild(1).gameObject.SetActive(true);
        else header.GetChild(1).gameObject.SetActive(false);
    }

    private void _handleTweenUpdates(GameObject go, Tween t, Easing.EaseType easeType){
        t.delay(delay);
        if(isLoop) t.loop(loopsCount, loopType);
        string logRoot = easeType.ToString() + " (" + t.id + ") ";
        t.onStart(() => Debug.Log(logRoot + "Started")).onComplete(()=> Debug.Log(logRoot + "Completed"));
        if(isLoop) t.onLoop((loopsCount) => Debug.Log(logRoot + "Loops = " + loopsCount));
        Text text = go.transform.GetChild(0).gameObject.GetComponent<Text>();
        text.text = easeType.ToString() + "\n" + t.id;
    }
    #endregion
}