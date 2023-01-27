#region namespaces
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Str8lines.Tweening;
#endregion

/*
    This class provides navigation functions for the UI,
    methods to control tweens lifecycles,
    updates UI for monitoring purposes,
    allows tweens parameters updates through UI,
    and instantiates Tweens
*/
public class EndUserTests : MonoBehaviour 
{
    #region Variables
    private const string ENABLED_SCENE = "UITests";
    private readonly string[] SCENES = new string[]{"UITests", "ObjectsTests"};
    public GameObject target;
    private bool _controlAll;
    private string _id;
    private float _duration;
    private float _delay;
    private bool _killOnEnd;
    private bool _isLoop;
    private Tween.LoopType _loopType;
    private int _loopsCount;
    private Tween.CompletionMode _completionMode;
    private float _moveX;
    private float _moveY;
    private float _moveZ;
    private float _scaleX;
    private float _scaleY;
    private float _scaleZ;
    private float _rotateX;
    private float _rotateY;
    private float _rotateZ;
    private float _alpha;

    public Dropdown sceneDropdown;
    public Dropdown effectDropdown;
    public Dropdown easeCategoryDropdown;
    public Dropdown targetDropdown;
    public Transform body;
    public Transform logsPanel;
    public Transform logsBtn;
    public Transform monitor;
    public Toggle controlAll;
    public Toggle killOnEnd;
    public Toggle isLoop;
    public InputField duration;
    public InputField delay;
    public InputField loopsCount;
    public Dropdown completionMode;
    public Dropdown loopType;
    public InputField moveX;
    public InputField moveY;
    public InputField moveZ;
    public InputField scaleX;
    public InputField scaleY;
    public InputField scaleZ;
    public InputField rotateX;
    public InputField rotateY;
    public InputField rotateZ;
    public InputField id;
    public InputField alpha;
    private GameObject _activeScenePanel;
    private Transform _activeEffectPanel;
    private Transform _activeTargetPanel;
    private Transform _activeTestPanel;
    private List<string> _easeCategories = new List<string>{"In", "Out", "InOut"};
    private Dictionary<GameObject, Vector3> _initialVectors3;
    private Dictionary<GameObject, Vector4> _initialVectors4;
    #endregion

    //Initializes UI and public variables
    void Start()
    {
        //Init Scenes dropdown
        Scene s = SceneManager.GetActiveScene();
        for(int i = 0; i < SCENES.Length; i++){
            sceneDropdown.options.Add(new Dropdown.OptionData(SCENES[i])); //Add option to dropdown

            bool isActiveScenePanel = (SCENES[i] == s.name);
            GameObject g = GameObject.Find(SCENES[i]);
            if(isActiveScenePanel) _activeScenePanel = g; //Defines active scene panel
            g.SetActive(isActiveScenePanel); //Activate panel related to the active scene and deactivate other scene related panels
        }
        sceneDropdown.value = s.buildIndex;

        if(s.name != ENABLED_SCENE) return;
        
        //Init other dropdowns
        effectDropdown.value = 0;
        for(int i = 0; i < _activeScenePanel.transform.childCount; i++){
            Transform t = _activeScenePanel.transform.GetChild(i);
            effectDropdown.options.Add(new Dropdown.OptionData(t.gameObject.name));
            bool isActiveEffectPanel = (i == effectDropdown.value);
            if(isActiveEffectPanel) _activeEffectPanel = t; //Defines active effect panel
            t.gameObject.SetActive(isActiveEffectPanel); //Activate panel related to the active effect and deactivate other effect related panels
            _initPanelChildren(t);
        }
        for(int i = 0; i < _easeCategories.Count; i++) easeCategoryDropdown.options.Add(new Dropdown.OptionData(_easeCategories[i]));
        targetDropdown.value = 0;
        easeCategoryDropdown.value = 0;

        //Init panels
        logsPanel.gameObject.SetActive(false);
        logsBtn.GetChild(0).GetComponent<Text>().text = ">";
        targetDropdown.transform.parent.gameObject.SetActive(false);
        _activeTestPanel = (_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)) ? _activeEffectPanel.GetChild(easeCategoryDropdown.value) : _activeTargetPanel.GetChild(easeCategoryDropdown.value);

        //Init starting values storage
        _initialVectors3 = new Dictionary<GameObject, Vector3>();
        _initialVectors4 = new Dictionary<GameObject, Vector4>();

        //Init Tweens controls
        controlAll.isOn = _controlAll = false;
        killOnEnd.isOn = _killOnEnd = true;
        isLoop.isOn = _isLoop = false;
        duration.text = (_duration = 3).ToString();
        delay.text = (_delay = 0).ToString();
        loopsCount.text = (_loopsCount = -1).ToString();
        completionMode.value = (int)(_completionMode = Tween.CompletionMode.STATIC);
        loopType.value = (int)(_loopType = Tween.LoopType.Restart);
        id.text = (_id = String.Empty);
        moveX.text = (_moveX = 225f).ToString();
        moveY.text = (_moveY = 0).ToString();
        moveZ.text = (_moveZ = 0).ToString();
        scaleX.text = (_scaleX = 1.5f).ToString();
        scaleY.text = (_scaleY = 1.5f).ToString();
        scaleZ.text = (_scaleZ = 0).ToString();
        rotateX.text = (_rotateX = 0).ToString();
        rotateY.text = (_rotateY = 0).ToString();
        rotateZ.text = (_rotateZ = 60f).ToString();
        alpha.text = (_alpha = 0.2f).ToString();
    }

    //Browse transform children, activate the first and deactivate others
    //Update target dropdown if the child do not correspond to an ease category
    //Then handle its children activation
    private void _initPanelChildren(Transform t){
        for(int i = 0; i < t.childCount; i++){
            GameObject g = t.GetChild(i).gameObject;
            bool isActivePanel = (i == 0);
            g.SetActive(isActivePanel);
            if(!_easeCategories.Contains(g.name)){
                if(isActivePanel) _activeTargetPanel = g.transform;
                targetDropdown.options.Add(new Dropdown.OptionData(g.name));
                _initPanelChildren(g.transform);
            }
        }
    }

    #region Monitoring
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

    //Destroys every logs added
    public void clearLogs(){
        Transform logContainer = logsPanel.GetChild(0).GetChild(0);
        for(int i = 1; i < logContainer.childCount; i++) Destroy(logContainer.GetChild(i).gameObject);
    }
    
    //Logs when events are triggered with associated tween's id in console
    private void _logEvents(Tween t){
        string logRoot = t.easeType.ToString() + " (" + t.id + ") ";
        t.onStart(() => _logAdd(logRoot + "Started")).onEnd(()=> _logAdd(logRoot + "Ended"));
        if(_isLoop) t.onLoop((loopsCount) => _logAdd(logRoot + "Loops = " + loopsCount));
    }

    //Adds log text in scrollview and console
    private void _logAdd(string log){
        Transform logContainer = logsPanel.GetChild(0).GetChild(0);
        Transform template = logContainer.GetChild(0);
        GameObject clone = Instantiate(template.gameObject, logContainer);
        clone.transform.GetComponent<Text>().text = log;
        clone.SetActive(true);
        Debug.Log(log);
    }

    public void toggleLogs(){
        if(logsPanel.gameObject.activeSelf){
            logsBtn.GetChild(0).GetComponent<Text>().text = ">";
            logsPanel.gameObject.SetActive(false);
        }else{
            logsBtn.GetChild(0).GetComponent<Text>().text = "<";
            logsPanel.gameObject.SetActive(true);
        }
    }
    #endregion

    /*
        Contains the functions used to display the differents panels in the scene
        and the methods called by clicking on buttons for tweens lifecycle tests
    */
    #region Navigation and control
    //Load selected scene
    public void LoadScene(){
        SceneManager.LoadScene(SCENES[sceneDropdown.value]);
    }
    
    //Close app
    public void Quit(){
        Application.Quit();
    }
    
    //Display appropriate panel according to selected effect
    public void ChangeEffect(Dropdown change){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        //Update effect panel
        _activeEffectPanel = _activeScenePanel.transform.GetChild(change.value);
        for(int i = 0; i < _activeScenePanel.transform.childCount; i++) _activeScenePanel.transform.GetChild(i).gameObject.SetActive((change.value == i));

        //Update ease dropdown
        for(int i = 0; i < _activeEffectPanel.childCount; i++){
            if(_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)) _activeEffectPanel.GetChild(i).gameObject.SetActive(easeCategoryDropdown.value == i);
            else{
                for(int j = 0; j < _activeTargetPanel.childCount; j++) _activeTargetPanel.GetChild(j).gameObject.SetActive((easeCategoryDropdown.value == j));
            }
        }
        
        //Handle target dropdown activation
        targetDropdown.transform.parent.gameObject.SetActive(!_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name));

        //Update active test panel
        _activeTestPanel = (_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)) ? _activeEffectPanel.GetChild(easeCategoryDropdown.value) : _activeTargetPanel.GetChild(easeCategoryDropdown.value);
    }

    //Display appropriate panel according to selected ease category
    public void ChangeEaseCategory(Dropdown change){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;
        
        Transform t = null;
        if(_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)) t = _activeEffectPanel;
        else t = _activeTargetPanel;
        for(int i = 0; i < t.childCount; i++) t.GetChild(i).gameObject.SetActive((change.value == i));

        //Update active test panel
        _activeTestPanel = (_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)) ? _activeEffectPanel.GetChild(easeCategoryDropdown.value) : _activeTargetPanel.GetChild(easeCategoryDropdown.value);
    }

    //Display appropriate panel according to selected target
    public void ChangeTarget(Dropdown change){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)){
            change.value = _activeTargetPanel.GetSiblingIndex();
            return;
        }
        _activeTargetPanel = _activeEffectPanel.GetChild(change.value);
        for(int i = 0; i < _activeEffectPanel.childCount; i++) _activeEffectPanel.GetChild(i).gameObject.SetActive((change.value == i));
        for(int i = 0; i < _activeTargetPanel.childCount; i++) _activeTargetPanel.GetChild(i).gameObject.SetActive((easeCategoryDropdown.value == i));

        //Update active test panel
        _activeTestPanel = (_easeCategories.Contains(_activeEffectPanel.GetChild(0).gameObject.name)) ? _activeEffectPanel.GetChild(easeCategoryDropdown.value) : _activeTargetPanel.GetChild(easeCategoryDropdown.value);
    }

    //Instantiates panel's tweens
    public void StartTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;
        
        int easeTypesCount = Enum.GetValues(typeof(Easing.EaseType)).Length;
        switch(effectDropdown.value){
            case 0:
                for(int i = 0; i < _activeTestPanel.childCount; i++){
                    GameObject go = _activeTestPanel.GetChild(i).gameObject;
                    switch(easeCategoryDropdown.value){
                        case 0:
                            if(i==0) _move(go, (Easing.EaseType)i);
                            else _move(go, (Easing.EaseType)(3*i - 2));
                            break;
                          
                        case 1:
                            _move(go, (Easing.EaseType)(3*(i+1) - 1));
                            break;
                            
                        case 2:
                            _move(go, (Easing.EaseType)(3*(i+1)));
                            break;
                    }
                }
                break;

            case 1:
                for(int i = 0; i < _activeTestPanel.childCount; i++){
                    GameObject go = _activeTestPanel.GetChild(i).gameObject;
                    switch(easeCategoryDropdown.value){
                        case 0:
                            if(i==0) _scale(go, (Easing.EaseType)i);
                            else _scale(go, (Easing.EaseType)(3*i - 2));
                            break;
                          
                        case 1:
                            _scale(go, (Easing.EaseType)(3*(i+1) - 1));
                            break;
                            
                        case 2:
                            _scale(go, (Easing.EaseType)(3*(i+1)));
                            break;
                    }
                }
                break;

            case 2:
                for(int i = 0; i < _activeTestPanel.childCount; i++){
                    GameObject go = _activeTestPanel.GetChild(i).gameObject;
                    switch(easeCategoryDropdown.value){
                        case 0:
                            if(i==0) _rotate(go, (Easing.EaseType)i);
                            else _rotate(go, (Easing.EaseType)(3*i - 2));
                            break;
                          
                        case 1:
                            _rotate(go, (Easing.EaseType)(3*(i+1) - 1));
                            break;
                            
                        case 2:
                            _rotate(go, (Easing.EaseType)(3*(i+1)));
                            break;
                    }
                }
                break;
                
            case 3:
                switch(targetDropdown.value){
                    case 0:
                        for(int i = 0; i < _activeTestPanel.childCount; i++){
                            GameObject go = _activeTestPanel.GetChild(i).gameObject;
                            switch(easeCategoryDropdown.value){
                                case 0:
                                    if(i==0) _fadeCanvasRenderer(go, (Easing.EaseType)i);
                                    else _fadeCanvasRenderer(go, (Easing.EaseType)(3*i - 2));
                                    break;
                                
                                case 1:
                                    _fadeCanvasRenderer(go, (Easing.EaseType)(3*(i+1) - 1));
                                    break;
                                    
                                case 2:
                                    _fadeCanvasRenderer(go, (Easing.EaseType)(3*(i+1)));
                                    break;
                            }
                        }
                        break;

                    case 1:
                        for(int i = 0; i < _activeTestPanel.childCount; i++){
                            GameObject go = _activeTestPanel.GetChild(i).gameObject;
                            switch(easeCategoryDropdown.value){
                                case 0:
                                    if(i==0) _fadeSpriteRenderer(go, (Easing.EaseType)i);
                                    else _fadeSpriteRenderer(go, (Easing.EaseType)(3*i - 2));
                                    break;
                                
                                case 1:
                                    _fadeSpriteRenderer(go, (Easing.EaseType)(3*(i+1) - 1));
                                    break;
                                    
                                case 2:
                                    _fadeSpriteRenderer(go, (Easing.EaseType)(3*(i+1)));
                                    break;
                            }
                        }
                        break;

                    case 2:
                        for(int i = 0; i < _activeTestPanel.childCount; i++){
                            GameObject go = _activeTestPanel.GetChild(i).gameObject;
                            switch(easeCategoryDropdown.value){
                                case 0:
                                    if(i==0) _fadeImage(go, (Easing.EaseType)i);
                                    else _fadeImage(go, (Easing.EaseType)(3*i - 2));
                                    break;
                                
                                case 1:
                                    _fadeImage(go, (Easing.EaseType)(3*(i+1) - 1));
                                    break;
                                    
                                case 2:
                                    _fadeImage(go, (Easing.EaseType)(3*(i+1)));
                                    break;
                            }
                        }
                        break;
                        
                    case 3:
                        for(int i = 0; i < _activeTestPanel.childCount; i++){
                            GameObject go = _activeTestPanel.GetChild(i).gameObject;
                            switch(easeCategoryDropdown.value){
                                case 0:
                                    if(i==0) _fadeRawImage(go, (Easing.EaseType)i);
                                    else _fadeRawImage(go, (Easing.EaseType)(3*i - 2));
                                    break;
                                
                                case 1:
                                    _fadeRawImage(go, (Easing.EaseType)(3*(i+1) - 1));
                                    break;
                                    
                                case 2:
                                    _fadeRawImage(go, (Easing.EaseType)(3*(i+1)));
                                    break;
                            }
                        }
                        break;
                        
                    case 4:
                        for(int i = 0; i < _activeTestPanel.childCount; i++){
                            GameObject go = _activeTestPanel.GetChild(i).gameObject;
                            switch(easeCategoryDropdown.value){
                                case 0:
                                    if(i==0) _fadeText(go, (Easing.EaseType)i);
                                    else _fadeText(go, (Easing.EaseType)(3*i - 2));
                                    break;
                                
                                case 1:
                                    _fadeText(go, (Easing.EaseType)(3*(i+1) - 1));
                                    break;
                                    
                                case 2:
                                    _fadeText(go, (Easing.EaseType)(3*(i+1)));
                                    break;
                            }
                        }
                        break;
                        
                    case 5:
                        for(int i = 0; i < _activeTestPanel.childCount; i++){
                            GameObject go = _activeTestPanel.GetChild(i).gameObject;
                            switch(easeCategoryDropdown.value){
                                case 0:
                                    if(i==0) _fadeGraphic(go, (Easing.EaseType)i);
                                    else _fadeGraphic(go, (Easing.EaseType)(3*i - 2));
                                    break;
                                
                                case 1:
                                    _fadeGraphic(go, (Easing.EaseType)(3*(i+1) - 1));
                                    break;
                                    
                                case 2:
                                    _fadeGraphic(go, (Easing.EaseType)(3*(i+1)));
                                    break;
                            }
                        }
                        break;
                }
                break;
        }
    }

    //Calls tween's play() method
    public void PlayTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_controlAll) Str8Tween.play();
        else{
            if(target != null) Str8Tween.play(target);
            else if(_id != String.Empty) Str8Tween.play(_id);
            else for(int i = 0; i < _activeTestPanel.childCount; i++) Str8Tween.play(_activeTestPanel.GetChild(i).gameObject);
        }
    }

    //Calls tween's pause() method
    public void PauseTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_controlAll) Str8Tween.pause();
        else{
            if(target != null) Str8Tween.pause(target);
            else if(_id != String.Empty) Str8Tween.pause(_id);
            else for(int i = 0; i < _activeTestPanel.childCount; i++) Str8Tween.pause(_activeTestPanel.GetChild(i).gameObject);
        }
    }

    //Calls tween's stop() method
    public void StopTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_controlAll) Str8Tween.stop();
        else{
            if(target != null) Str8Tween.stop(target);
            else if(_id != String.Empty) Str8Tween.stop(_id);
            else for(int i = 0; i < _activeTestPanel.childCount; i++) Str8Tween.stop(_activeTestPanel.GetChild(i).gameObject);
        }
    }

    //Calls tween's complete() method
    public void CompleteTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_controlAll) Str8Tween.complete(true, _completionMode);
        else{
            if(target != null) Str8Tween.complete(target, _completionMode);
            else if(_id != String.Empty) Str8Tween.complete(_id, true, _completionMode);
            else for(int i = 0; i < _activeTestPanel.childCount; i++) Str8Tween.complete(_activeTestPanel.GetChild(i).gameObject, true, _completionMode);
        }
    }

    public void ResetTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_controlAll) Str8Tween.reset();
        else{
            if(target != null) Str8Tween.reset(target);
            else if(_id != String.Empty) Str8Tween.reset(_id);
            else for(int i = 0; i < _activeTestPanel.childCount; i++) Str8Tween.reset(_activeTestPanel.GetChild(i).gameObject);
        }
    }
    
    //Calls tween's kill() method
    public void KillTweens(){
        if(SceneManager.GetActiveScene().name != ENABLED_SCENE) return;

        if(_controlAll) Str8Tween.kill();
        else{
            if(target != null) Str8Tween.kill(target);
            else if(_id != String.Empty) Str8Tween.kill(_id);
            else for(int i = 0; i < _activeTestPanel.childCount; i++) Str8Tween.kill(_activeTestPanel.GetChild(i).gameObject);
        }
    }
    #endregion

    // Functions which updates public variables of the current script
    #region Parameters
    public void UpdateControlAll(Toggle change){ _controlAll = change.isOn; }
    public void UpdateKillOnEnd(Toggle change){ _killOnEnd = change.isOn; }
    public void UpdateIsLoop(Toggle change){ _isLoop = change.isOn; }
    public void UpdateID(InputField change){ _id = change.text; }
    public void UpdateDuration(InputField change){ 
        try{
            _duration = Int32.Parse(change.text);
        }catch{
            _duration = 0;
        }
    }
    public void UpdateDelay(InputField change){ 
        try{
            _delay = Int32.Parse(change.text);
        }catch{
            _delay = 0;
        }
    }
    public void UpdateLoopsCount(InputField change){
        try{
            _loopsCount = Int32.Parse(change.text);
        }catch{
            _loopsCount = 0;
        }
    }
    public void UpdateLoopType(Dropdown change){ _loopType = (Tween.LoopType)change.value; }
    public void UpdateCompletionMode(Dropdown change){ _completionMode = (Tween.CompletionMode)change.value; }
    public void UpdateMoveX(InputField change){ 
        try{
            _moveX = float.Parse(change.text);
        }catch{
            _moveX = 0;
        }
    }
    public void UpdateMoveY(InputField change){ 
        try{
            _moveY = float.Parse(change.text);
        }catch{
            _moveY = 0;
        }
    }
    public void UpdateMoveZ(InputField change){ 
        try{
            _moveZ = float.Parse(change.text);
        }catch{
            _moveZ = 0;
        }
    }
    public void UpdateScaleX(InputField change){ 
        try{
            _scaleX = float.Parse(change.text);
        }catch{
            _scaleX = 0;
        }
    }
    public void UpdateScaleY(InputField change){ 
        try{
            _scaleY = float.Parse(change.text);
        }catch{
            _scaleY = 0;
        }
    }
    public void UpdateScaleZ(InputField change){ 
        try{
            _scaleZ = float.Parse(change.text);
        }catch{
            _scaleZ = 0;
        }
    }
    public void UpdateRotateX(InputField change){ 
        try{
            _rotateX = float.Parse(change.text);
        }catch{
            _rotateX = 0;
        }
    }
    public void UpdateRotateY(InputField change){ 
        try{
            _rotateY = float.Parse(change.text);
        }catch{
            _rotateY = 0;
        }
    }
    public void UpdateRotateZ(InputField change){ 
        try{
            _rotateZ = float.Parse(change.text);
        }catch{
            _rotateZ = 0;
        }
    }
    public void UpdateAlpha(InputField change){ 
        try{
            _alpha = float.Parse(change.text);
        }catch{
            _alpha = 0;
        }
    }
    #endregion

    // Save or resets initial values and creates the tweens to check
    #region Instantiations
    private void _move(GameObject go, Easing.EaseType easeType){
        RectTransform rect = go.GetComponent<RectTransform>();
        if(!_initialVectors3.ContainsKey(go)) _initialVectors3.Add(go, go.GetComponent<RectTransform>().anchoredPosition3D);
        else go.GetComponent<RectTransform>().anchoredPosition3D = _initialVectors3[go];

        Tween t = Str8Tween.move(rect, new Vector3(rect.anchoredPosition3D.x + _moveX, rect.anchoredPosition3D.y + _moveY, rect.anchoredPosition3D.z + _moveZ), easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _scale(GameObject go, Easing.EaseType easeType){
        RectTransform rect = go.GetComponent<RectTransform>();
        if(!_initialVectors3.ContainsKey(go)) _initialVectors3.Add(go, go.GetComponent<RectTransform>().localScale);
        else go.GetComponent<RectTransform>().localScale = _initialVectors3[go];

        Tween t = Str8Tween.scale(rect, new Vector3(rect.localScale.x * _scaleX, rect.localScale.y * _scaleY, rect.localScale.z * _scaleZ), easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _rotate(GameObject go, Easing.EaseType easeType){
        RectTransform rect = go.GetComponent<RectTransform>();
        if(!_initialVectors3.ContainsKey(go)) _initialVectors3.Add(go, go.GetComponent<RectTransform>().localEulerAngles);
        else go.GetComponent<RectTransform>().localEulerAngles = _initialVectors3[go];

        Tween t = Str8Tween.rotate(rect, new Vector3(rect.localEulerAngles.x + _rotateX, rect.localEulerAngles.y + _rotateY, rect.localEulerAngles.z + _rotateZ), easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _fadeCanvasRenderer(GameObject go, Easing.EaseType easeType){
        CanvasRenderer canvasRenderer = go.GetComponent<CanvasRenderer>();
        if(!_initialVectors4.ContainsKey(go)) _initialVectors4.Add(go, canvasRenderer.GetColor());
        else canvasRenderer.SetColor(_initialVectors4[go]);

        Tween t = Str8Tween.fade(canvasRenderer, _alpha, easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _fadeSpriteRenderer(GameObject go, Easing.EaseType easeType){
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        if(!_initialVectors4.ContainsKey(go)) _initialVectors4.Add(go, spriteRenderer.color);
        else spriteRenderer.color = _initialVectors4[go];

        Tween t = Str8Tween.fade(spriteRenderer, _alpha, easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _fadeRawImage(GameObject go, Easing.EaseType easeType){
        RawImage rawImage = go.GetComponent<RawImage>();
        if(!_initialVectors4.ContainsKey(go)) _initialVectors4.Add(go, rawImage.color);
        else rawImage.color = _initialVectors4[go];

        Tween t = Str8Tween.fade(rawImage, _alpha, easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _fadeImage(GameObject go, Easing.EaseType easeType){
        Image image = go.GetComponent<Image>();
        if(!_initialVectors4.ContainsKey(go)) _initialVectors4.Add(go, image.color);
        else image.color = _initialVectors4[go];
        
        Tween t = Str8Tween.fade(image, _alpha, easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    private void _fadeText(GameObject go, Easing.EaseType easeType){
        Text txt = go.GetComponent<Text>();
        if(!_initialVectors4.ContainsKey(go)) _initialVectors4.Add(go, txt.color);
        else txt.color = _initialVectors4[go];
        
        Tween t = Str8Tween.fade(txt, _alpha, easeType, _duration, _killOnEnd);
        t.delay(_delay);
        if(_isLoop) t.loop(_loopsCount, _loopType);
        _logEvents(t);
        txt.text = easeType.ToString() + "\n" + t.id;
    }

    private void _fadeGraphic(GameObject go, Easing.EaseType easeType){
        Graphic graphic = go.GetComponent<Image>();
        if(!_initialVectors4.ContainsKey(go)) _initialVectors4.Add(go, graphic.color);
        else graphic.color = _initialVectors4[go];
        
        Tween t = Str8Tween.fade(graphic, _alpha, easeType, _duration, _killOnEnd);
        _handleTweenUpdates(go, t, easeType);
    }

    //Updates delay and loop parameters of a tween and displays the ID of the tween created
    private void _handleTweenUpdates(GameObject go, Tween t, Easing.EaseType easeType){
        t.delay(_delay);
        if(_isLoop) t.loop(_loopsCount, _loopType);
        _logEvents(t);
        Text text = go.transform.GetChild(0).gameObject.GetComponent<Text>();
        text.text = easeType.ToString() + "\n" + t.id;
    } 
    #endregion
}