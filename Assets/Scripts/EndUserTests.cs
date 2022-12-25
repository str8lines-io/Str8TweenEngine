using System;
using UnityEngine;
using UnityEngine.UI;
using Str8lines.Tweening;

/*
    This class provides navigation functions for the UI,
    methods to control tweens lifecycles,
    updates UI for monitoring purposes,
    allows tweens parameters updates through UI,
    and instantiates Tweens
*/
public class EndUserTests : MonoBehaviour 
{
    #region variables
    public bool controlAll;
    public GameObject target;
    public string id;
    public float duration;
    public float delay;
    public bool killOnEnd;
    public bool isLoop;
    public Tween.LoopType loopType;
    public int loopsCount;
    private GameObject activePanel;
    private Transform panelsContainer;
    private Transform header;
    private Transform monitor;
    private int panelIndex;
    private int fadeSetIndex;
    #endregion

    //Initializes UI and public variables
    void Start()
    {
        panelIndex = 0;
        fadeSetIndex = 0;
        header = this.gameObject.transform.GetChild(0);
        monitor = header.GetChild(2);
        panelsContainer = this.gameObject.transform.GetChild(1);
        for(int i = 0; i < panelsContainer.childCount - 1; i++) panelsContainer.GetChild(i).gameObject.SetActive(false);
        _updatePanel();

        controlAll = false;
        killOnEnd = false;
        isLoop = false;
        duration = 0;
        delay = 0;
        loopsCount = 0;
        loopType = Tween.LoopType.Restart;
        id = String.Empty;
    }

    #region monitoring
    //Display tweens count according to tweens state in real time
    private void Update() {
        Tween[] tweens = Str8Tween.get();
        int running = 0;
        int finished = 0;
        int paused = 0;
        foreach(Tween t in tweens){
            if(!t.isAlive) continue;
            if(t.isFinished) finished++;
            else{
                if(t.isRunning) running++;
                else paused++;
            }
        }
        monitor.GetChild(0).GetComponent<Text>().text = "Total = " + tweens.Length;
        monitor.GetChild(1).GetComponent<Text>().text = "Running = " + running;
        monitor.GetChild(2).GetComponent<Text>().text = "Finished = " + finished;
        monitor.GetChild(3).GetComponent<Text>().text = "Paused = " + paused;
    }
    
    //Logs when events are triggered with associated tween's id in console
    private void _logEvents(Tween t){
        string logRoot = t.easeType.ToString() + " (" + t.id + ") ";
        t.onStart(() => Debug.Log(logRoot + "Started")).onEnd(()=> Debug.Log(logRoot + "Ended"));
        if(isLoop) t.onLoop((loopsCount) => Debug.Log(logRoot + "Loops = " + loopsCount));
    }
    #endregion

    /*
        Contains the functions used to display the differents panels in the scene
        and the methods called by clicking on buttons for tweens lifecycle tests
    */
    #region navigation and control
    //Shows next panel
    public void Next(){
        panelsContainer.GetChild(panelIndex).gameObject.SetActive(false);
        if(panelIndex < 3) panelIndex++;
        _updatePanel();
    }

    //Shows previous panel
    public void Previous(){
        panelsContainer.GetChild(panelIndex).gameObject.SetActive(false);
        if(panelIndex > 0) panelIndex--;
        _updatePanel();
    }

    //Display approriate panel according to the target to fade
    public void ChangeFadeTarget(Dropdown change){
        Transform fadePanel = panelsContainer.GetChild(3);
        for(int i = 0; i < fadePanel.childCount; i++) fadePanel.GetChild(i).gameObject.SetActive(false);
        fadeSetIndex = change.value;
        activePanel = fadePanel.GetChild(fadeSetIndex).gameObject;
        activePanel.SetActive(true);
    }

    //Instantiates panel's tweens
    public void StartTweens(){
        int easeTypesCount = Enum.GetValues(typeof(Easing.EaseType)).Length;
        switch(panelIndex){
            case 0:
                Transform movePanel = panelsContainer.GetChild(panelIndex);
                float xValue = 225f;
                for(int i = 0; i < easeTypesCount; i++){
                    GameObject go = movePanel.GetChild(i).gameObject;
                    if(i == 0) _move(go, (Easing.EaseType)i, xValue); //Linear
                    else if(i <= 10) _move(go, (Easing.EaseType)(3*i - 2), xValue); //In
                    else if(i > 10 && i <= 20) _move(go, (Easing.EaseType)(3*(i - 10) - 1), 475f); //Out
                    else _move(go, (Easing.EaseType)(3*(i - 20)), 700f); //InOut
                }
                break;

            case 1:
                Transform scalePanel = panelsContainer.GetChild(panelIndex);
                for(int i = 0; i < easeTypesCount; i++){
                    GameObject go = scalePanel.GetChild(i).gameObject;
                    if(i == 0) _scale(go, (Easing.EaseType)i); //Linear
                    else if(i <= 10) _scale(go, (Easing.EaseType)(3*i - 2)); //In
                    else if(i > 10 && i <= 20) _scale(go, (Easing.EaseType)(3*(i - 10) - 1)); //Out
                    else _scale(go, (Easing.EaseType)(3*(i - 20))); //InOut
                }
                break;

            case 2:
                Transform rotatePanel = panelsContainer.GetChild(panelIndex);
                for(int i = 0; i < easeTypesCount; i++){
                    GameObject go = rotatePanel.GetChild(i).gameObject;
                    if(i == 0) _rotate(go, (Easing.EaseType)i, 60f); //Linear
                    else if(i <= 10) _rotate(go, (Easing.EaseType)(3*i - 2), 60f); //In
                    else if(i > 10 && i <= 20) _rotate(go, (Easing.EaseType)(3*(i - 10) - 1), 60f); //Out
                    else _rotate(go, (Easing.EaseType)(3*(i - 20)), 60f); //InOut
                }
                break;
                
            case 3:
                Transform fadePanel = panelsContainer.GetChild(panelIndex);
                switch(fadeSetIndex){
                    case 0:
                        Transform fadeCanvasRendererPanel = fadePanel.GetChild(fadeSetIndex);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeCanvasRenderer(fadeCanvasRendererPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                        break;

                    case 1:
                        Transform fadeSpriteRendererPanel = fadePanel.GetChild(fadeSetIndex);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeSpriteRenderer(fadeSpriteRendererPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                        break;

                    case 2:
                        Transform fadeImagePanel = fadePanel.GetChild(fadeSetIndex);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeImage(fadeImagePanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeImage(fadeImagePanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeImage(fadeImagePanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeImage(fadeImagePanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                        break;
                        
                    case 3:
                        Transform fadeRawImagePanel = fadePanel.GetChild(fadeSetIndex);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeRawImage(fadeRawImagePanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                        break;
                        
                    case 4:
                        Transform fadeTextPanel = fadePanel.GetChild(fadeSetIndex);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeText(fadeTextPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeText(fadeTextPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeText(fadeTextPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeText(fadeTextPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                        break;
                        
                    case 5:
                        Transform fadeGraphicPanel = fadePanel.GetChild(fadeSetIndex);
                        for(int i = 0; i < easeTypesCount; i++){
                            if(i == 0) _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)i, 0.2f); //Linear
                            else if(i <= 10) _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)(3*i - 2), 0.2f); //In
                            else if(i > 10 && i <= 20) _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)(3*(i - 10) - 1), 0.2f); //Out
                            else _fadeGraphic(fadeGraphicPanel, i, (Easing.EaseType)(3*(i - 20)), 0.2f); //InOut
                        }
                        break;
                }
                break;
        }
    }

    //Calls tween's play() method
    public void PlayTweens(){
        if(controlAll) Str8Tween.play();
        else{
            if(target != null) Str8Tween.play(target);
            else if(id != String.Empty) Str8Tween.play(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.play(activePanel.transform.GetChild(i).gameObject);
        }
    }

    //Calls tween's pause() method
    public void PauseTweens(){
        if(controlAll) Str8Tween.pause();
        else{
            if(target != null) Str8Tween.pause(target);
            else if(id != String.Empty) Str8Tween.pause(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.pause(activePanel.transform.GetChild(i).gameObject);
        }
    }

    //Calls tween's stop() method
    public void StopTweens(){
        if(controlAll) Str8Tween.stop();
        else{
            if(target != null) Str8Tween.stop(target);
            else if(id != String.Empty) Str8Tween.stop(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.stop(activePanel.transform.GetChild(i).gameObject);
        }
    }

    //Calls tween's complete() method
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
    
    //Calls tween's kill() method
    public void KillTweens(){
        if(controlAll) Str8Tween.kill();
        else{
            if(target != null) Str8Tween.kill(target);
            else if(id != String.Empty) Str8Tween.kill(id);
            else for(int i = 0; i < activePanel.transform.childCount; i++) Str8Tween.kill(activePanel.transform.GetChild(i).gameObject);
        }
    }
    
    //Updates the UI according to the active panel
    private void _updatePanel(){
        activePanel = panelsContainer.GetChild(panelIndex).gameObject;
        activePanel.SetActive(true);
        header.GetChild(0).GetComponent<Text>().text = activePanel.name.Replace("Panel", String.Empty);
        if(panelIndex == 3){
            header.GetChild(1).gameObject.SetActive(true);
            Transform fadePanel = panelsContainer.GetChild(panelIndex);
            for(int i = 0; i < fadePanel.childCount; i++) fadePanel.GetChild(i).gameObject.SetActive(false);
            activePanel = fadePanel.GetChild(fadeSetIndex).gameObject;
            activePanel.SetActive(true);
        }else header.GetChild(1).gameObject.SetActive(false);
    }   
    #endregion

    // Functions which updates public variables of the current script
    #region parameters
    public void UpdateControlAll(Toggle change){ controlAll = change.isOn; }
    public void UpdateKillOnEnd(Toggle change){ killOnEnd = change.isOn; }
    public void UpdateIsLoop(Toggle change){ isLoop = change.isOn; }
    public void UpdateID(InputField change){ id = change.text; }
    public void UpdateDuration(InputField change){ 
        try{
            duration = Int32.Parse(change.text);
        }catch{
            duration = 0;
        }
    }
    public void UpdateDelay(InputField change){ 
        try{
            delay = Int32.Parse(change.text);
        }catch{
            delay = 0;
        }
    }
    public void UpdateLoopsCount(InputField change){
        try{
            loopsCount = Int32.Parse(change.text);
        }catch{
            loopsCount = 0;
        }
    }
    public void UpdateLoopType(Dropdown change){ loopType = (Tween.LoopType)change.value; }
    #endregion

    // Creates the tweens to checks
    #region instantiations
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
        _logEvents(t);
        txt.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeGraphic(Transform fadeGraphicPanel, int childIndex, Easing.EaseType easeType, float x){
        GameObject fadeGraphicGo = fadeGraphicPanel.GetChild(childIndex).gameObject;
        Graphic graphic = fadeGraphicGo.GetComponent<Image>();
        Tween t = Str8Tween.fade(graphic, x, easeType, duration, killOnEnd);
        _handleTweenUpdates(fadeGraphicGo, t, easeType);
    }

    //Updates delay and loop parameters of a tween and displays the ID of the tween created
    private void _handleTweenUpdates(GameObject go, Tween t, Easing.EaseType easeType){
        t.delay(delay);
        if(isLoop) t.loop(loopsCount, loopType);
        _logEvents(t);
        Text text = go.transform.GetChild(0).gameObject.GetComponent<Text>();
        text.text = easeType.ToString() + "\n" + t.id;
    } 
    #endregion
}