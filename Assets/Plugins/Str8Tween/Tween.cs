#region Author
/* 
    Str8lines Tween Engine for Unity
    Version: 1.0
    Author:	Geoffrey LESNE (Str8lines)
    Contact: contact@str8lines.io
*/
#endregion

namespace Str8lines.Tweening
{
    #region namespaces
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;
    #endregion
    
    /// <summary>Defines if values are reset on loop (Restart), if the tween is played forward then backward (Oscillate) or if tweening restarts from end values (WithOffset).</summary>
    public enum LoopStyle { Restart, Oscillate, WithOffset }
    
    /// <summary>Defines end values to use after the tween completion.</summary>
    /// <value><c>STATIC</c> will set end values to the initial toValue or toVector.</value>
    /// <value><c>DYNAMIC</c> works only for loops, this will set end values depending on the next value to reach.</value>
    /// <value><c>PROJECTED</c> is useful only for loops with offset, it will set end values to the values that would have been reached if the tween ended normaly.</value>
    public enum CompletionMode { STATIC, DYNAMIC, PROJECTED }
    
    /// <summary>Representation of a tween. A uuid is attributed to the tween on creation. Each instances can be manipulated with a variety of methods.</summary>
    public class Tween
    {
    #region Variables
        #region Public variables
        private string _id;
        /// <value>UUID given on creation.</value>
        public string id => _id;
        private GameObject _target;
        /// <value><see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see> on which the tween will be applied.</value>
        public GameObject target => _target;
        private EasingFunction _easingFunction;
        /// <value>Defines easing functions used internally and provides methods to calculate new values.</value>
        public EasingFunction easingFunction => _easingFunction;
        private float _duration;
        /// <value>Duration of a tween's loop (in seconds). If the tween is not a loop then it's the duration of the tween itself.</value>
        public float duration => _duration;
        private bool _isAlive;
        /// <value>While <c>true</c> the tween is referenced in <see cref="Str8Tween">Str8Tween</see> and can be controlled.</value>
        public bool isAlive => _isAlive;
        private bool _isRunning;
        /// <value>If <c>true</c> the tween is currently playing.</value>
        public bool isRunning => _isRunning;
        private bool _isFinished;
        /// <value>If <c>true</c> the tween finished playing.</value>
        public bool isFinished => _isFinished;
        private bool _killOnEnd;
        /// <value>If <c>true</c> the tween will be killed after playing.</value>
        public bool killOnEnd => _killOnEnd;
        private float _elapsedTotal;
        /// <value>Time elapsed in seconds (delay included).</value>
        public float elapsedTotal => _elapsedTotal;
        private float _elapsedSinceDelay;
        /// <value>Time elapsed in seconds (delay excluded).</value>
        public float elapsedSinceDelay => _elapsedSinceDelay;
        private int _loopsCount;
        /// <value>The number of loops to do.</value>
        public int loopsCount => _loopsCount;
        private LoopStyle _loopStyle;
        /// <value>The <see cref="LoopStyle">type of loop</see> to use.</value>
        public LoopStyle loopStyle => _loopStyle;
        private int _completedLoopsCount;
        /// <value>The number of loops completed since the tween started.</value>
        public int completedLoopsCount => _completedLoopsCount;
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
        private event TweenDelegate started
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
        /// <summary>Instantiate a new tween, initialize it and give it a UUID.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> on which changes will be applied.</param>
        /// <param name="toVector">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final position, scale or rotation.</param>
        /// <param name="easingFunction">The <see cref="EasingFunction">easing function</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the tween will be destroyed once completed. Default value is <c>true</c></param>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <returns>The tween instantiated.</returns>
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
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(RectTransform rectTransform, Vector3 toVector, EasingFunction easingFunction, float duration, bool killOnEnd = true, [CallerMemberName]string methodName = "")
        {
            if(rectTransform == null) throw new ArgumentNullException("rectTransform", "rectTransform can not be null");
            _rectTransform = rectTransform;
            _initCommon(rectTransform.gameObject, easingFunction, duration, killOnEnd);

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
        
        /// <summary>Instantiate a new tween, initialize it and give it a UUID.</summary>
        /// <param name="canvasRenderer">The <see href="https://docs.unity3d.com/ScriptReference/CanvasRenderer.html">CanvasRenderer</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="canvasRenderer"/>'s final alpha.</param>
        /// <param name="easingFunction">The <see cref="EasingFunction">easing function</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the tween will be destroyed once completed. Default value is <c>true</c></param>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <returns>The tween instantiated.</returns>
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
        ///         Tween t = new Tween("fade", canvasRenderer, 0f, EasingFunction.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(CanvasRenderer canvasRenderer, float toValue, EasingFunction easingFunction, float duration, bool killOnEnd = true, [CallerMemberName]string methodName = "")
        {
            if(canvasRenderer == null) throw new ArgumentNullException("canvasRenderer", "canvasRenderer can not be null");
            _canvasRenderer = canvasRenderer;
            _initCommon(canvasRenderer.gameObject, easingFunction, duration, killOnEnd);
            _initFloatTweening(methodName, toValue, _canvasRenderer.GetAlpha());
        }

        /// <summary>Instantiate a new tween, initialize it and give it a UUID.</summary>
        /// <param name="spriteRenderer">The <see href="https://docs.unity3d.com/ScriptReference/SpriteRenderer.html">SpriteRenderer</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="spriteRenderer"/>'s final alpha.</param>
        /// <param name="easingFunction">The <see cref="EasingFunction">easing function</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the tween will be destroyed once completed. Default value is <c>true</c></param>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <returns>The tween instantiated.</returns>
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
        ///         Tween t = new Tween("fade", spriteRenderer, 0f, EasingFunction.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(SpriteRenderer spriteRenderer, float toValue, EasingFunction easingFunction, float duration, bool killOnEnd = true, [CallerMemberName]string methodName = "")
        {
            if(spriteRenderer == null) throw new ArgumentNullException("spriteRenderer", "spriteRenderer can not be null");
            _spriteRenderer = spriteRenderer;
            _initCommon(spriteRenderer.gameObject, easingFunction, duration, killOnEnd);
            _initFloatTweening(methodName, toValue, _spriteRenderer.color.a);
        }

        /// <summary>Instantiate a new tween, initialize it and give it a UUID.</summary>
        /// <param name="graphic">The <see href="https://docs.unity3d.com/ScriptReference/Graphic.html">Graphic</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="graphic"/>'s final alpha.</param>
        /// <param name="easingFunction">The <see cref="EasingFunction">easing function</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the tween will be destroyed once completed. Default value is <c>true</c></param>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <returns>The tween instantiated.</returns>
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
        ///         Tween t = new Tween("fade", graphic, 0f, EasingFunction.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(Graphic graphic, float toValue, EasingFunction easingFunction, float duration, bool killOnEnd = true, [CallerMemberName]string methodName = "")
        {
            if(graphic == null) throw new ArgumentNullException("graphic", "graphic can not be null");
            _graphic = graphic;
            _initCommon(graphic.gameObject, easingFunction, duration, killOnEnd);
            _initFloatTweening(methodName, toValue, _graphic.color.a);
        }

        /// <summary>Add delay to the tween before it starts playing.</summary>
        /// <param name="delay">Time before the tween start (in seconds).</param>
        /// <returns>The tween delayed.</returns>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

        /// <returns>The delay before the tween starts (in seconds).</returns>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
        ///         Debug.Log(t.delay());
        ///     }
        /// }
        /// </code>
        /// </example>
        public float delay() { return _delay; }

        /// <summary>Makes the tween loop.</summary>
        /// <param name="loopsCount">(Optional) Number of loops to do. Default value is -1.</param>
        /// <param name="loopStyle">(Optional) <see cref="LoopStyle">Type of loop</see>. Default value is <see cref="LoopStyle.Restart">LoopStyle.Restart</see>.</param>
        /// <returns>The tween which loops.</returns>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
        ///         t.loop(5, LoopStyle.Oscillate);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween loop(int loopsCount = -1, LoopStyle loopStyle = LoopStyle.Restart)
        {
            if(loopsCount == 0 || loopsCount < -1){
                this.kill();
                throw new ArgumentException("loopsCount can not be equal to zero or inferior to minus one", "loopsCount");
            }
            _isLoop = true;
            _loopStyle = loopStyle;
            _loopsCount = loopsCount;
            _lifeTime = this.duration * this.loopsCount;
            return this;
        }

        /// <summary>Adds a callback to the tween's start event.</summary>
        /// <param name="callback">Callback function to trigger.</param>
        /// <returns>The tween which will rise the start event.</returns>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

        /// <summary>Adds a callback to the tween's loop event.</summary>
        /// <param name="callback">Callback function to trigger.</param>
        /// <returns>The tween which will rise the loop event.</returns>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
        ///         t.loop(5, LoopStyle.Oscillate).onLoop(_onLoop);
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

        /// <summary>Adds a callback to the tween's end event.</summary>
        /// <param name="callback">Callback function to trigger.</param>
        /// <returns>The tween which will rise the end event.</returns>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         Tween t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

            bool isTotalDurationOver = (_lifeTime > 0 && this.elapsedSinceDelay >= _lifeTime);
            bool revertIncrementation= false; //Used to tag the need to revert incrementation
            if(_isLoop) //Handle changes according to the loop style
            {
                switch(this.loopStyle)
                {
                    case LoopStyle.Restart :
                    case LoopStyle.WithOffset :
                        _loopTime += t; //These two loop styles are always played forward
                        if(_loopTime >= this.duration || isTotalDurationOver){
                            _loopTime = 0;
                            _completeLoop();
                        }
                        break;

                    case LoopStyle.Oscillate :
                        if(_isIncrementing){
                            _loopTime += t; //Plays the tween forward
                            if(_loopTime >= this.duration || isTotalDurationOver){
                                _loopTime = this.duration;
                                revertIncrementation = true;
                                _completeLoop();
                            }
                        }else{
                            _loopTime -= t; //Plays the tween backward
                            if(_loopTime <= 0f || isTotalDurationOver){
                                _loopTime = 0f;
                                revertIncrementation = true;
                                _completeLoop();
                            }
                        }
                        break;
                }
                time = _loopTime;
            }
            
            if(isTotalDurationOver) complete();
            else{
                _setCalculatedValue(time);
                //We must revert incrementation after the potential call of complete() since it checks _isIncrementingValue to set end values properly
                if(revertIncrementation) _isIncrementing = !_isIncrementing;
            }
        }

        /// <summary>Resets the tween values.</summary>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

        /// <summary>Plays the tween.</summary>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

        /// <summary>Pauses the tween.</summary>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

        /// <summary>Completes the tween.</summary>
        /// <param name="triggerOnEnd">(Optional) If <c>true</c>, triggers the tween's end event. Default value is <c>true</c></param>
        /// <param name="mode">(Optional) The <see cref="CompletionMode">completion mode</see> defines the end values to apply. Default value is <c>PROJECTED</c></param>
        /// <returns><c>void</c></returns>
        /// <remarks>Completing a tween sends the target to its final values.</remarks>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
        ///     }
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) t.complete();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void complete(bool triggerOnEnd = true, CompletionMode mode = CompletionMode.PROJECTED)
        {
            this.stop(triggerOnEnd);
            switch(mode){
                case CompletionMode.STATIC:
                    _setInitialValue(true);
                    break;

                case CompletionMode.DYNAMIC:
                    if(_isLoop && this.loopStyle == LoopStyle.WithOffset) _setCurrentToValue();
                    else _setInitialValue(_isIncrementing);
                    break;

                case CompletionMode.PROJECTED:
                    if(!_isLoop){
                        _setInitialValue(true);
                        return;
                    }

                    switch(this.loopStyle){
                        case LoopStyle.Restart:
                            _setInitialValue(true);
                            break;

                        case LoopStyle.Oscillate:
                            bool useToValues = (this.loopsCount < 0) ? _isIncrementing : !(this.loopsCount%2 == 0);
                            _setInitialValue(useToValues);
                            break;

                        case LoopStyle.WithOffset:
                            if(this.loopsCount < 0){
                                _setCurrentToValue();
                                return;
                            }
                            
                            //For positive a loops count we must calculate Tween's real end values
                            Vector3 projectedVector = new Vector3(_initialFromVector.x + (_vectorChange.x * this.loopsCount), _initialFromVector.y + (_vectorChange.y * this.loopsCount), _initialFromVector.z + (_vectorChange.z * this.loopsCount));
                            float projectedValue = _initialFromValue + (_valueChange * this.loopsCount);
                            if(projectedValue > 1) projectedValue = 1f;
                            if(projectedValue < 0) projectedValue = 0f;

                            switch(_methodName)
                            {
                                case "move":
                                    if(_rectTransform != null) _rectTransform.anchoredPosition3D = projectedVector;
                                    break;

                                case "scale":
                                    if(_rectTransform != null) _rectTransform.localScale = projectedVector;
                                    break;

                                case "rotate":
                                    if(_rectTransform != null) _rectTransform.localEulerAngles = projectedVector;
                                    break;

                                case "fade":
                                    if(_canvasRenderer != null) _canvasRenderer.SetAlpha(projectedValue);
                                    if(_spriteRenderer != null){
                                        Color newSpriteRendererColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, projectedValue);
                                        _spriteRenderer.color = newSpriteRendererColor;
                                    }
                                    if(_graphic != null){
                                        Color newGraphicColor = new Color(_graphic.color.r, _graphic.color.g, _graphic.color.b, projectedValue);
                                        _graphic.color = newGraphicColor;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        /// <summary>Stops the tween.</summary>
        /// <param name="triggerOnEnd">(Optional) If <c>true</c>, triggers the tween's end event. Default value is <c>true</c></param>
        /// <returns><c>void</c></returns>
        /// <remarks>Stopping a tween does not change the target's current values.</remarks>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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

        /// <summary>Kills the tween. This removes the tween's callbacks and sets <see cref="Tween.isAlive">isAlive</see> to <c>false</c>. <see cref="Str8Tween">Str8Tween</see> class will remove every reference to the tween.</summary>
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
        ///         Vector3 destination = new Vector3(0, 500, 0);
        ///         t = new Tween("move", rectTransform, destination, EasingFunction.Linear, 3f);
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
        private void _initCommon(GameObject target, EasingFunction easingFunction, float duration, bool killOnEnd)
        {
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            _id = Guid.NewGuid().ToString();
            _target = target;
            _easingFunction = easingFunction;
            _duration = duration;
            _isAlive = true;
            _isFinished = false;
            _isRunning = true;
            _loopsCount = 0;
            _completedLoopsCount = 0;
            _loopStyle = LoopStyle.Restart;
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
                    if(_rectTransform != null) _rectTransform.anchoredPosition3D = Easing.ease(this.easingFunction, playtime, _fromVector, _vectorChange, this.duration);
                    break;

                case "scale":
                    if(_rectTransform != null) _rectTransform.localScale = Easing.ease(this.easingFunction, playtime, _fromVector, _vectorChange, this.duration);
                    break;

                case "rotate":
                    if(_rectTransform != null) _rectTransform.localEulerAngles = Easing.ease(this.easingFunction, playtime, _fromVector, _vectorChange, this.duration);
                    break;

                case "fade":
                    if(_canvasRenderer != null) _canvasRenderer.SetAlpha(Easing.ease(this.easingFunction, playtime, _fromValue, _valueChange, this.duration));
                    if(_spriteRenderer != null){
                        Color newSpriteRendererColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Easing.ease(this.easingFunction, playtime, _fromValue, _valueChange, this.duration));
                        _spriteRenderer.color = newSpriteRendererColor;
                    }
                    if(_graphic != null){
                        Color newGraphicColor = new Color(_graphic.color.r, _graphic.color.g, _graphic.color.b, Easing.ease(this.easingFunction, playtime, _fromValue, _valueChange, this.duration));
                        _graphic.color = newGraphicColor;
                    }
                    break;
            }
        }

        //Handles values update for loops with offset, updates loops count and triggers onLoop event.
        private void _completeLoop()
        {
            if(this.loopStyle == LoopStyle.WithOffset)
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

        //Go to current "to values".
        private void _setCurrentToValue()
        {
            switch(_methodName)
            {
                case "move":
                    if(_rectTransform != null) _rectTransform.anchoredPosition3D = _toVector;
                    break;

                case "scale":
                    if(_rectTransform != null) _rectTransform.localScale = _toVector;
                    break;

                case "rotate":
                    if(_rectTransform != null) _rectTransform.localEulerAngles = _toVector;
                    break;

                case "fade":
                    float initialValue = _toValue;
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