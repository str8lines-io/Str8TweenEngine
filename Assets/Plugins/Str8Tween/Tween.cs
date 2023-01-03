#region Author
/* 
    Str8lines Tween Engine for Unity
    Version: 1.0
    Author:	Str8lines (Geoffrey LESNE)
    Contact: contact@str8lines.io
*/
#endregion

namespace Str8lines.Tweening
{
    #region namespaces
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    #endregion
    
    /// <summary>Representation of a <see cref="Tween">Tween</see>. A uuid is attributed to the tween on creation. Each instances can be manipulated with a variety of methods.</summary>
    [Serializable]
    public class Tween
    {
    #region Variables
        #region Public variables
        [SerializeField] private string _id;
        /// <value>UUID given on creation.</value>
        [SerializeField] public string id => _id;
        [SerializeField] private GameObject _target;
        /// <value><see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see> on which the tween will be applied.</value>
        [SerializeField] public GameObject target => _target;
        [SerializeField] private Easing.EaseType _easeType;
        /// <value>Defines easing functions used internally and provides methods to calculate new values.</value>
        [SerializeField] public Easing.EaseType easeType => _easeType;
        [SerializeField] private float _duration;
        /// <value>Duration of a tween loop (in seconds). If the tween is not a loop then it's the duration of the tween itself.</value>
        [SerializeField] public float duration => _duration;
        [SerializeField] private bool _isAlive;
        /// <value>While <c>true</c> the tween is referenced in <see cref="Str8Tween">Str8Tween</see> and can be controlled.</value>
        [SerializeField] public bool isAlive => _isAlive;
        [SerializeField] private bool _isRunning;
        /// <value>If <c>true</c> the tween is currently playing.</value>
        [SerializeField] public bool isRunning => _isRunning;
        [SerializeField] private bool _isFinished;
        /// <value>If <c>true</c> the tween finished playing.</value>
        [SerializeField] public bool isFinished => _isFinished;
        [SerializeField] private bool _killOnEnd;
        /// <value>If <c>true</c> the tween will be killed after playing.</value>
        [SerializeField] public bool killOnEnd => _killOnEnd;
        [SerializeField] private float _elapsedTotal;
        /// <value>Time elapsed in seconds (delay included).</value>
        [SerializeField] public float elapsedTotal => _elapsedTotal;
        [SerializeField] private float _elapsedSinceDelay;
        /// <value>Time elapsed in seconds (delay excluded).</value>
        [SerializeField] public float elapsedSinceDelay => _elapsedSinceDelay;
        [SerializeField] private int _loopsCount;
        /// <value>The number of loops to do.</value>
        [SerializeField] public int loopsCount => _loopsCount;
        /// <summary>Defines if values are reset on loop (Restart), if tween is played forward then backward (Oscillate) or if tweening restarts from end values (WithOffset).</summary>
        [SerializeField] public enum LoopType { Restart, Oscillate, WithOffset }
        [SerializeField] private LoopType _loopType;
        /// <value>The <see cref="LoopType">type of loop</see> to use.</value>
        [SerializeField] public LoopType loopType => _loopType;
        [SerializeField] private int _completedLoopsCount;
        /// <value>The number of loops completed since the Tween started.</value>
        [SerializeField] public int completedLoopsCount => _completedLoopsCount;
        #endregion

        #region Private variables
        private string _methodName;
        private float _lifeTime;
        private float _delay;
        private bool _isLoop;
        private float _loopTime;
        private bool _isDelayOver;
        private bool _isFirstUpdate;
        private bool _isIncrementing;
        private Vector3 _initialFromVector;
        private Vector3 _fromVector;
        private Vector3 _initialToVector;
        private Vector3 _toVector;
        private Vector3 _vectorChange;
        private float _initialFromValue;
        private float _fromValue;
        private float _initialToValue;
        private float _toValue;
        private float _valueChange;
        private RectTransform _rectTransform;
        private SpriteRenderer _spriteRenderer;
        private CanvasRenderer _canvasRenderer;
        private Graphic _graphic;
        #endregion

        #region Events
        /// <summary>Delegate for start and complete events.</summary>
        public delegate void TweenDelegate();
        private TweenDelegate _callbackOnStart = () => {};
        public event TweenDelegate started
        {
            add { _callbackOnStart += value; }
            remove { _callbackOnStart -= value; }
        }
        private TweenDelegate _callbackOnEnd = () => {};
        private event TweenDelegate ended
        {
            add { _callbackOnEnd += value; }
            remove { _callbackOnEnd -= value; }
        }

        /// <summary>Delegate for loop event.</summary>
        /// <param name="loopsCount">The number of loops completed.</param>
        public delegate void TweenLoopDelegate(int loopsCount);
        private TweenLoopDelegate _callbackOnLoop = (int i) => {};
        private event TweenLoopDelegate looped
        {
            add { _callbackOnLoop += value; }
            remove { _callbackOnLoop -= value; }
        }
        
        #endregion
    #endregion

    #region Constructors
        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> on which changes will be applied.</param>
        /// <param name="toVector">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final position, scale or rotation.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes target's position :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, RectTransform rectTransform, Vector3 toVector, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rectTransform == null) throw new ArgumentNullException("rectTransform", "rectTransform can not be null");
            _rectTransform = rectTransform;
            _initCommon(rectTransform.gameObject, easeType, duration, killOnEnd);

            switch(methodName)
            {
                case "move":
                    _initialFromVector = _rectTransform.anchoredPosition3D;
                    break;

                case "scale":
                    _initialFromVector = _rectTransform.localScale;
                    break;

                case "rotate":
                    _initialFromVector = _rectTransform.localEulerAngles;
                    break;

                default: throw new ArgumentException("methodName is not allowed", "methodName");
            }
            _methodName = methodName;
            _initialToVector = toVector;
            _toVector = _initialToVector;
            _fromVector = _initialFromVector;
            _vectorChange = _toVector - _fromVector;
        }
        
        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="canvasRenderer">The <see href="https://docs.unity3d.com/ScriptReference/CanvasRenderer.html">CanvasRenderer</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="canvasRenderer"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes CanvasRenderer's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public CanvasRenderer canvasRenderer;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", canvasRenderer, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, CanvasRenderer canvasRenderer, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(canvasRenderer == null) throw new ArgumentNullException("canvasRenderer", "canvasRenderer can not be null");
            _canvasRenderer = canvasRenderer;
            _initCommon(canvasRenderer.gameObject, easeType, duration, killOnEnd);
            _initFloatTweening(methodName, toValue, _canvasRenderer.GetAlpha());
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="spriteRenderer">The <see href="https://docs.unity3d.com/ScriptReference/SpriteRenderer.html">SpriteRenderer</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="spriteRenderer"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes SpriteRenderer's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public SpriteRenderer spriteRenderer;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", spriteRenderer, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, SpriteRenderer spriteRenderer, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(spriteRenderer == null) throw new ArgumentNullException("spriteRenderer", "spriteRenderer can not be null");
            _spriteRenderer = spriteRenderer;
            _initCommon(spriteRenderer.gameObject, easeType, duration, killOnEnd);
            _initFloatTweening(methodName, toValue, _spriteRenderer.color.a);
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="graphic">The <see href="https://docs.unity3d.com/ScriptReference/Graphic.html">Graphic</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="graphic"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes graphic's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public Graphic graphic;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", graphic, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, Graphic graphic, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(graphic == null) throw new ArgumentNullException("graphic", "graphic can not be null");
            _graphic = graphic;
            _initCommon(graphic.gameObject, easeType, duration, killOnEnd);
            _initFloatTweening(methodName, toValue, _graphic.color.a);
        }

        /// <summary>Add delay to the <see cref="Tween">tween</see> before it starts playing.</summary>
        /// <param name="delay">Time before the tween start (in seconds).</param>
        /// <returns>The <see cref="Tween">tween</see> delayed.</returns>
        /// <example>
        /// Add 2 seconds of delay before the tween starts.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         t.delay(2f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween delay(float delay)
        {
            if(delay < 0f) delay = 0f;
            _delay = delay;
            return this;
        }

        /// <returns>The delay before the <see cref="Tween">tween</see> starts (in seconds).</returns>
        /// <example>
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         Debug.Log(t.delay());
        ///     }
        /// }
        /// </code>
        /// </example>
        public float delay() { return _delay; }

        /// <summary>Makes the <see cref="Tween">tween</see> loop.</summary>
        /// <param name="loopsCount">(Optional) Number of loops to do. Default value is -1.</param>
        /// <param name="loopType">(Optional) <see cref="LoopType">Type of loop</see>. Default value is <see cref="LoopType.Restart">LoopType.Restart</see>.</param>
        /// <returns>The <see cref="Tween">tween</see> which loops.</returns>
        /// <remarks>Default <paramref name="loopsCount"/>'s value is -1 which is equivalent to infinite loops.</remarks>
        /// <example>
        /// Realizes five oscillating loops of a move tween.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         t.loop(5, LoopType.Oscillate);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween loop(int loopsCount = -1, LoopType loopType = LoopType.Restart)
        {
            if(loopsCount == 0 || loopsCount < -1){
                this.kill();
                throw new ArgumentException("loopsCount can not be equal to zero or inferior to minus one", "loopsCount");
            }
            _isLoop = true;
            _loopType = loopType;
            _loopsCount = loopsCount;
            _lifeTime = this.duration * this.loopsCount;
            return this;
        }

        /// <summary>Registers to <see cref="Tween">tween</see>'s start event.</summary>
        /// <param name="onStart">Callback function to trigger.</param>
        /// <returns>The <see cref="Tween">tween</see> which will rise the start event.</returns>
        /// <example>
        /// Displays a message in console when the move tween starts.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         t.onStart(_onStart);
        ///     }
        ///
        ///     private void _onStart()
        ///     {
        ///         UnityEngine.Debug.Log("Start");
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween onStart(TweenDelegate callback)
        {
            if(callback != null) _callbackOnStart += callback;
            return this;
        }

        /// <summary>Registers to <see cref="Tween">tween</see>'s loop event.</summary>
        /// <param name="onLoop">Callback function to trigger.</param>
        /// <returns>The <see cref="Tween">tween</see> which will rise the loop event.</returns>
        /// <example>
        /// Displays the number of loops accomplished in console when the move tween loops.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         t.loop(5, LoopType.Oscillate).onLoop(_onLoop);
        ///     }
        ///
        ///     private void _onLoop(int loopsCount)
        ///     {
        ///         UnityEngine.Debug.Log("Loop count : " + loopsCount);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween onLoop(TweenLoopDelegate callback)
        {
            if(callback != null) _callbackOnLoop += callback;
            return this;
        }

        /// <summary>Registers to <see cref="Tween">tween</see>'s end event.</summary>
        /// <param name="onEnd">Callback function to trigger.</param>
        /// <returns>The <see cref="Tween">tween</see> which will rise the end event.</returns>
        /// <example>
        /// Displays a message in console when the move tween ends.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        /// 
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         t.onEnd(_onEnd);
        ///     }
        ///
        ///     private void _onEnd()
        ///     {
        ///         UnityEngine.Debug.Log("End");
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween onEnd(TweenDelegate callback)
        {
            if(callback != null) _callbackOnEnd += callback;
            return this;
        }
    #endregion

    #region Public methods
        /// <summary>Determines new values from the time <paramref name="t"/> elapsed and applies it to the tween's <see cref="Tween.target">target</see>. Also triggers callbacks when required.</summary>
        /// <param name="t">The time elapsed (in seconds).</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Updates a tween at new frame.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         t.update(Time.deltaTime)
        ///     }
        /// }
        /// </code>
        /// </example>
        public void update(float t)
        {
            if(!this.isAlive || this.target == null || this.isFinished || !this.isRunning) return;

            if(_isFirstUpdate) _isFirstUpdate = false; //At first update the time elapsed is 0
            else _elapsedTotal += t;

            if(this.elapsedTotal < _delay) return; //Delay is not over, the tween can not start
            
            float time = 0f; //Time used for calculations
            if(!_isDelayOver){
                //The first frame when the tween plays is the starting frame
                _isDelayOver = true;
                _callbackOnStart?.Invoke();
                t = this.elapsedTotal - _delay; // Fixing time for the first animation frame
            }
            
            _elapsedSinceDelay += t;
            time = this.elapsedSinceDelay;

            if(_isLoop)
            {
                switch(this.loopType)
                {
                    case LoopType.Restart :
                    case LoopType.WithOffset :
                        _loopTime += t; //These two loop types are always played forward
                        if(_loopTime >= this.duration || this.elapsedSinceDelay >= _lifeTime){
                            _loopTime = 0;
                            _completeLoop();
                        }
                        break;

                    case LoopType.Oscillate :
                        if(_isIncrementing){
                            _loopTime += t; //Plays the tween forward
                            if(_loopTime >= this.duration || this.elapsedSinceDelay >= _lifeTime){
                                _loopTime = this.duration;
                                _isIncrementing = !_isIncrementing;
                                _completeLoop();
                            }
                        }else{
                            _loopTime -= t; //Plays the tween backward
                            if(_loopTime <= 0f || this.elapsedSinceDelay >= _lifeTime){
                                _loopTime = 0f;
                                _isIncrementing = !_isIncrementing;
                                _completeLoop();
                            }
                        }
                        break;
                }
                time = _loopTime;
            }
            
            if(_lifeTime > 0 && this.elapsedSinceDelay >= _lifeTime) complete();
            else _setCalculatedValue(time);
        }

        /// <summary>Resets the <see cref="Tween">tween</see> values.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to reset.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.reset();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void reset()
        {
            _isDelayOver = false;
            _isFirstUpdate = true;
            _isIncrementing = true;
            _loopTime = 0f;
            _completedLoopsCount = 0;
            _isFinished = false;
            _elapsedTotal = 0f;
            _elapsedSinceDelay = 0f;
            _fromValue = _initialFromValue;
            _toValue = _initialToValue;
            _fromVector = _initialFromVector;
            _toVector = _initialToVector;
            _setInitialValue();
        }

        /// <summary>Plays the <see cref="Tween">tween</see>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to play.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.play();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void play()
        {
            if(this.isFinished) this.reset();
            _isRunning = true;
        }

        /// <summary>Pauses the <see cref="Tween">tween</see>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to pause.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.pause();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void pause()
        {
            _isRunning = false;
        }

        /// <summary>Completes the <see cref="Tween">tween</see>.</summary>
        /// <param name="triggerOnEnd">(Optional) If <c>true</c>, triggers <see cref="Tween">tween</see>'s end event. Default value is <c>true</c></param>
        /// <returns><c>void</c></returns>
        /// <remarks>Completing a <see cref="Tween">tween</see> sends the target to its final values.</remarks>
        /// <example>
        /// Press space to complete.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.complete();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void complete(bool triggerOnEnd = true)
        {
            this.stop(triggerOnEnd);
            _setInitialValue(true);
        }

        /// <summary>Stops the <see cref="Tween">tween</see>.</summary>
        /// <param name="triggerOnEnd">(Optional) If <c>true</c>, triggers <see cref="Tween">tween</see>'s end event. Default value is <c>true</c></param>
        /// <returns><c>void</c></returns>
        /// <remarks>Stopping a <see cref="Tween">tween</see> does not change the target's current values.</remarks>
        /// <example>
        /// Press space to stop.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.stop();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void stop(bool triggerOnEnd = true)
        {
            if(this.isFinished) return;
            _isFinished = true;
            _isRunning = false;
            if(triggerOnEnd) _callbackOnEnd?.Invoke();
            if(this.killOnEnd == true) kill();
        }

        /// <summary>Kills the <see cref="Tween">tween</see>. This unregisters the <see cref="Tween">tween</see>'s events and sets <see cref="this.isAlive">isAlive</see> to <c>false</c>. <see cref="Str8Tween">Str8Tween</see> class will remove every reference to the <see cref="Tween">tween</see>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to kill.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RectTransform rectTransform;
        ///     private Tween t;
        ///
        ///     private void Start()
        ///     {
        ///         Vector2 destination = new Vector2(0, 500);
        ///         t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.kill();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void kill()
        {
            _isAlive = false;
            this.started -= _callbackOnStart;
            this.looped -= _callbackOnLoop;
            this.ended -= _callbackOnEnd;
        }
    #endregion

    #region Private methods
        //Handles common values initialization and common checks.
        private void _initCommon(GameObject target, Easing.EaseType easeType, float duration, bool killOnEnd)
        {
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            _id = Guid.NewGuid().ToString();
            _target = target;
            _easeType = easeType;
            _duration = duration;
            _isAlive = true;
            _isFinished = false;
            _isRunning = true;
            _loopsCount = 0;
            _completedLoopsCount = 0;
            _loopType = LoopType.Restart;
            _elapsedTotal = 0f;
            _elapsedSinceDelay = 0f;
            _killOnEnd = killOnEnd;
            
            _lifeTime = this.duration;
            _delay = 0f;
            _isLoop = false;
            _loopTime = 0f;
            _isDelayOver = false;
            _isFirstUpdate = true;
            _isIncrementing = true;
        }

        //Handles specific values initialization and checks for float values tweening.
        private void _initFloatTweening(string methodName, float toValue, float fromValue){
            if(methodName != "fade") throw new ArgumentException("methodName is not allowed", "methodName");
            _methodName = methodName;
            _initialToValue = toValue;
            _toValue = toValue;
            _initialFromValue = fromValue;
            _fromValue = _initialFromValue;
            _valueChange = _toValue - _fromValue;
        }

        //Updates values according to the method which called in the Str8Tween class for tween's instantiation and the time elapsed passed in parameters.
        private void _setCalculatedValue(float playtime)
        {
            switch(_methodName)
            {
                case "move":
                    if(_rectTransform != null) _rectTransform.anchoredPosition3D = Easing.ease(this.easeType, playtime, _fromVector, _vectorChange, this.duration);
                    break;

                case "scale":
                    if(_rectTransform != null) _rectTransform.localScale = Easing.ease(this.easeType, playtime, _fromVector, _vectorChange, this.duration);
                    break;

                case "rotate":
                    if(_rectTransform != null) _rectTransform.localEulerAngles = Easing.ease(this.easeType, playtime, _fromVector, _vectorChange, this.duration);
                    break;

                case "fade":
                    if(_canvasRenderer != null) _canvasRenderer.SetAlpha(Easing.ease(this.easeType, playtime, _fromValue, _valueChange, this.duration));
                    if(_spriteRenderer != null){
                        Color newSpriteRendererColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Easing.ease(this.easeType, playtime, _fromValue, _valueChange, this.duration));
                        _spriteRenderer.color = newSpriteRendererColor;
                    }
                    if(_graphic != null){
                        Color newGraphicColor = new Color(_graphic.color.r, _graphic.color.g, _graphic.color.b, Easing.ease(this.easeType, playtime, _fromValue, _valueChange, this.duration));
                        _graphic.color = newGraphicColor;
                    }
                    break;
            }
        }

        //Handles values update for loops with offset, updates loops count and triggers onLoop event.
        private void _completeLoop()
        {
            if(this.loopType == LoopType.WithOffset)
            {
                _fromVector = _toVector;
                _toVector += _vectorChange;
                _fromValue = _toValue;
                _toValue += _valueChange;

                if(_toValue > 1){
                    _toValue = 1; //Clamps alpha max value
                    _valueChange = _toValue - _fromValue;
                }
                if(_toValue < 0) _toValue = 0; //Clamps alpha min value
            }
            _completedLoopsCount++;
            _callbackOnLoop?.Invoke(this.completedLoopsCount);
        }

        //Go to initial "to values" or to initial "from values".
        private void _setInitialValue(bool useToValues = false)
        {
            switch(_methodName)
            {
                case "move":
                    if(_rectTransform != null) _rectTransform.anchoredPosition3D = useToValues ? _initialToVector : _initialFromVector;
                    break;

                case "scale":
                    if(_rectTransform != null) _rectTransform.localScale = useToValues ? _initialToVector : _initialFromVector;
                    break;

                case "rotate":
                    if(_rectTransform != null) _rectTransform.localEulerAngles = useToValues ? _initialToVector : _initialFromVector;
                    break;

                case "fade":
                    float initialValue = useToValues ? _initialToValue : _initialFromValue;
                    if(_canvasRenderer != null) _canvasRenderer.SetAlpha(initialValue);
                    if(_spriteRenderer != null){
                        Color newSpriteRendererColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, initialValue);
                        _spriteRenderer.color = newSpriteRendererColor;
                    }
                    if(_graphic != null){
                        Color newGraphicColor = new Color(_graphic.color.r, _graphic.color.g, _graphic.color.b, initialValue);
                        _graphic.color = newGraphicColor;
                    }
                    break;
            }
        }
    #endregion
    }
}