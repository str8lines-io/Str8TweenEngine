namespace Str8lines.Tweening
{
    #region namespaces
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    #endregion
    
    /// <summary>Representation of a <see cref="Tween">Tween</see>. A uuid is attributed to the tween on creation. Each instances can be manipulated with a variety of methods.</summary>
    public class Tween
    {
    #region Variables
        #region Public variables
        /// <value>UUID given on creation.</value>
        public string id { get;private set; }
        /// <value><see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see> on which the tween will be applied.</value>
        public GameObject target { get;private set; }
        /// <value>Defines easing functions used internally and provides methods to calculate <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> and <c>float</c> values.</value>
        public Easing.EaseType easeType { get;private set; }
        /// <value>Duration of a tween loop (in seconds). If the tween is not a loop then it's the duration of the tween itself.</value>
        public float duration { get;private set; }
        /// <value>While <c>true</c> the tween is referenced in <see cref="Str8Tween">Str8Tween</see>.</value>
        public bool isAlive { get;private set; }
        /// <value>If <c>true</c> the tween is currently playing.</value>
        public bool isRunning { get;private set; }
        /// <value>If <c>true</c> the tween finished playing.</value>
        public bool isCompleted { get;private set; }
        #endregion

        #region Private variables
        private string[] _authorizedMethods = {"move", "fade", "scale", "rotate"};
        private bool _killOnEnd;
        private bool _isDelayOver;
        private string _methodName;
        private float _elapsed;
        private float _lifeTime;
        private float _playTime;
        private bool _isFirstUpdate;
        private float _delay;
        private Vector3 _initialFromVector;
        private Vector3 _fromVector;
        private Vector3 _initialToVector;
        private Vector3 _toVector;
        private Vector3 _vectorChange;
        private SpriteRenderer _spriteRenderer;
        private CanvasRenderer _canvasRenderer;
        private RawImage _rawImage;
        private Image _image;
        private Toggle _toggle;
        private Slider _slider;
        private Graphic _graphic;
        private Text _text;
        private float _initialFromSpriteRendererAlpha;
        private float _fromSpriteRendererAlpha;
        private float _spriteRendererAlphaChange;
        private float _initialFromCanvasRendererAlpha;
        private float _fromCanvasRendererAlpha;
        private float _canvasRendererAlphaChange;
        private float _initialFromRawImageAlpha;
        private float _fromRawImageAlpha;
        private float _rawImageAlphaChange;
        private float _initialFromImageAlpha;
        private float _fromImageAlpha;
        private float _imageAlphaChange;
        private float _initialFromToggleAlpha;
        private float _fromToggleAlpha;
        private float _toggleAlphaChange;
        private float _initialFromSliderAlpha;
        private float _fromSliderAlpha;
        private float _sliderAlphaChange;
        private float _initialFromTextAlpha;
        private float _fromTextAlpha;
        private float _textAlphaChange;
        private float _initialFromGraphicAlpha;
        private float _fromGraphicAlpha;
        private float _graphicAlphaChange;
        private float _initialToValue;
        private float _toValue;
        #endregion

        #region Loop specific
        /// <summary>Defines if values are reset on loop (Restart), if tween is played forward then backward (Oscillate) or if tweening restarts from end values (WithOffset).</summary>
        public enum LoopType { Restart, Oscillate, WithOffset }
        private bool _isLoop;
        private bool _isIncrementing;
        private int _loopsCount;
        private int _passedLoopsCount;
        private float _loopTime;
        private LoopType _loopType;
        #endregion

        #region Events
        /// <summary>Delegate for start and complete events.</summary>
        public delegate void TweenDelegate();
        private event TweenDelegate _start;
        private TweenDelegate _callbackOnStart;
        private event TweenDelegate _complete;
        private TweenDelegate _callbackOnComplete;

        /// <summary>Delegate for loop event.</summary>
        /// <param name="loopsCount">The number of loops completed.</param>
        public delegate void TweenLoopDelegate(int loopsCount);
        private event TweenLoopDelegate _loop;
        private TweenLoopDelegate _callbackOnLoop;
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
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            this.id = Guid.NewGuid().ToString();
            this.target = rectTransform.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToVector = toVector;
            _toVector = toVector;

            switch(_methodName)
            {
                case "move":
                    _initialFromVector = rectTransform.anchoredPosition3D;
                    break;

                case "scale":
                    _initialFromVector = rectTransform.localScale;
                    break;

                case "rotate":
                    _initialFromVector = rectTransform.localEulerAngles;
                    break;
            }
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
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            
            this.id = Guid.NewGuid().ToString();
            this.target = canvasRenderer.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _canvasRenderer = canvasRenderer;
            _initialFromCanvasRendererAlpha = _canvasRenderer.GetAlpha();
            _fromCanvasRendererAlpha = _initialFromCanvasRendererAlpha;
            _canvasRendererAlphaChange = _toValue - _fromCanvasRendererAlpha;
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
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            this.id = Guid.NewGuid().ToString();
            this.target = spriteRenderer.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _spriteRenderer = spriteRenderer;
            _initialFromSpriteRendererAlpha = _spriteRenderer.color.a;
            _fromSpriteRendererAlpha = _initialFromSpriteRendererAlpha;
            _spriteRendererAlphaChange = _toValue - _fromSpriteRendererAlpha;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="rawImage">The <see href="https://docs.unity3d.com/ScriptReference/RawImage.html">RawImage</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="rawImage"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes RawImage's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public RawImage rawImage;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", rawImage, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, RawImage rawImage, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rawImage == null) throw new ArgumentNullException("rawImage", "rawImage can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            this.id = Guid.NewGuid().ToString();
            this.target = rawImage.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _rawImage = rawImage;
            _initialFromRawImageAlpha = _rawImage.color.a;
            _fromRawImageAlpha = _initialFromRawImageAlpha;
            _rawImageAlphaChange = _toValue - _fromRawImageAlpha;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="image">The <see href="https://docs.unity3d.com/ScriptReference/Image.html">Image</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="image"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes Image's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public Image image;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", image, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, Image image, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(image == null) throw new ArgumentNullException("image", "image can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            this.id = Guid.NewGuid().ToString();
            this.target = image.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _image = image;
            _initialFromImageAlpha = _image.color.a;
            _fromImageAlpha = _initialFromImageAlpha;
            _imageAlphaChange = _toValue - _fromImageAlpha;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="text">The <see href="https://docs.unity3d.com/ScriptReference/Text.html">Text</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="text"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes Text's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public Text text;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", text, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, Text text, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(text == null) throw new ArgumentNullException("text", "text can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");

            this.id = Guid.NewGuid().ToString();
            this.target = text.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _text = text;
            _initialFromTextAlpha = _text.color.a;
            _fromTextAlpha = _initialFromTextAlpha;
            _textAlphaChange = _toValue - _fromTextAlpha;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="toggle">The <see href="https://docs.unity3d.com/ScriptReference/Toggle.html">Toggle</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="toggle"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes Toggle's alpha :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public Toggle toggle;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", toggle, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, Toggle toggle, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(toggle == null) throw new ArgumentNullException("toggle", "toggle can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toggle.graphic == null && toggle.targetGraphic == null) throw new Exception("Toggle has no graphic to fade");

            this.id = Guid.NewGuid().ToString();
            this.target = toggle.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _toggle = toggle;
            if(_toggle.targetGraphic == null) _initialFromToggleAlpha = _toggle.targetGraphic.color.a;
            if(_toggle.graphic == null) _initialFromToggleAlpha = _toggle.graphic.color.a;
            _fromToggleAlpha = _initialFromToggleAlpha;
            _toggleAlphaChange = _toValue - _fromToggleAlpha;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see>, initialize it and give it a UUID.</summary>
        /// <param name="methodName">Name of the method which called this constructor.</param>
        /// <param name="slider">The <see href="https://docs.unity3d.com/ScriptReference/Slider.html">Slider</see> on which changes will be applied.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="slider"/>'s final alpha.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed. Default value is <c>true</c></param>
        /// <returns>The <see cref="Tween">Tween</see> instantiated.</returns>
        /// <example>
        /// Create new tween that changes slider's transparency :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public Slider slider;
        /// 
        ///     private void Start()
        ///     {
        ///         Tween t = new Tween("fade", slider, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween(string methodName, Slider slider, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(slider == null) throw new ArgumentNullException("slider", "slider can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(slider.image == null && slider.fillRect == null && slider.handleRect == null) throw new Exception("Slider has no graphic to fade");

            this.id = Guid.NewGuid().ToString();
            this.target = slider.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _slider = slider;
            if(_slider.fillRect != null && _slider.fillRect.gameObject.TryGetComponent(out Image fill)) _initialFromSliderAlpha = fill.color.a;
            if(_slider.handleRect != null && _slider.handleRect.gameObject.TryGetComponent(out Image handle)) _initialFromSliderAlpha = handle.color.a;
            if(_slider.image != null) _initialFromSliderAlpha = _slider.image.color.a;
            _fromSliderAlpha = _initialFromSliderAlpha;
            _sliderAlphaChange = _toValue - _fromSliderAlpha;
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
        /// Create new tween that changes graphic's transparency :
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
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            
            this.id = Guid.NewGuid().ToString();
            this.target = graphic.gameObject;
            this.easeType = easeType;
            this.duration = duration;
            
            _init(methodName, killOnEnd);
            _initialToValue = toValue;
            _toValue = toValue;

            _graphic = graphic;
            _initialFromGraphicAlpha = _graphic.color.a;
            _fromGraphicAlpha = _initialFromGraphicAlpha;
            _graphicAlphaChange = _toValue - _fromGraphicAlpha;
        }

        /// <summary>Add delay to the <see cref="Tween">tween</see> before play.</summary>
        /// <param name="delay">Time before tween start (in seconds).</param>
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
            _isLoop = true;
            _loopType = loopType;
            _loopsCount = loopsCount;
            if(_loopsCount <= 0) _loopsCount = -1; //Fix loops count value
            _lifeTime = this.duration * _loopsCount;
            return this;
        }

        /// <returns>The number of loops to do.</returns>
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
        ///         t.loop(5, LoopType.Oscillate);
        ///         Debug.Log(t.loopsCount());
        ///     }
        /// }
        /// </code>
        /// </example>
        public int loopsCount() { return _loopsCount; }

        /// <returns>The <see cref="LoopType">type of loop</see> to do.</returns>
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
        ///         t.loop(5, LoopType.Oscillate);
        ///         Debug.Log(t.loopType());
        ///     }
        /// }
        /// </code>
        /// </example>
        public LoopType loopType() { return _loopType; }

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
        public Tween onStart(TweenDelegate onStart)
        {
            if(onStart != null){
                _callbackOnStart = onStart;
                _start += _callbackOnStart;
            }
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
        public Tween onLoop(TweenLoopDelegate onLoop)
        {
            if(onLoop != null){
                _callbackOnLoop = onLoop;
                _loop += _callbackOnLoop;
            }
            return this;
        }

        /// <summary>Registers to <see cref="Tween">tween</see>'s complete event.</summary>
        /// <param name="onComplete">Callback function to trigger.</param>
        /// <returns>The <see cref="Tween">tween</see> which will rise the complete event.</returns>
        /// <example>
        /// Displays a message in console when the move tween completes.
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
        ///         t.onComplete(_onComplete);
        ///     }
        ///
        ///     private void _onComplete()
        ///     {
        ///         UnityEngine.Debug.Log("End");
        ///     }
        /// }
        /// </code>
        /// </example>
        public Tween onComplete(TweenDelegate onComplete)
        {
            if(onComplete != null){
                _callbackOnComplete = onComplete;
                _complete += _callbackOnComplete;
            }
            return this;
        }
    #endregion

    #region Public methods
        /// <summary>Determines new values from the time <paramref name="t"/> elapsed since the last frame and applies it to the <see cref="Tween.target"/>. Also triggers callbacks when required.</summary>
        /// <param name="t">The time elapsed since the last frame (in seconds).</param>
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
        ///         t.update(Time.DeltaTime)
        ///     }
        /// }
        /// </code>
        /// </example>
        public void update(float t)
        {
            if(this.isAlive && this.target != null)
            {
                if(!this.isCompleted)
                {
                    if(this.isRunning)
                    {
                        if(_isFirstUpdate) _isFirstUpdate = false; //At first update the time elapsed is 0
                        else _elapsed += t;

                        if(_elapsed >= _delay) //Delay is over, the tween can start
                        {
                            float time = 0f; //Time used for calculations
                            if(!_isDelayOver){
                                //The first frame when the tween plays is the starting frame
                                _isDelayOver = true;
                                _start?.Invoke();
                                t = _elapsed - _delay; // Fixing time for the first animation frame
                            }
                            
                            _playTime += t;
                            time = _playTime;

                            if(_isLoop)
                            {
                                switch(_loopType)
                                {
                                    case LoopType.Restart :
                                    case LoopType.WithOffset :
                                        _loopTime += t; //These two loop types are always played forward
                                        if(_loopTime >= this.duration){
                                            _loopTime = 0;
                                            _completeLoop();
                                        }
                                        break;

                                    case LoopType.Oscillate :
                                        if(_isIncrementing){
                                            _loopTime += t; //Plays the tween forward
                                            if(_loopTime >= this.duration){
                                                _loopTime = this.duration;
                                                _isIncrementing = !_isIncrementing;
                                                _completeLoop();
                                            }
                                        }else{
                                            _loopTime -= t; //Plays the tween backward
                                            if(_loopTime <= 0f){
                                                _loopTime = 0f;
                                                _isIncrementing = !_isIncrementing;
                                                _completeLoop();
                                            }
                                        }
                                        break;
                                }
                                time = _loopTime;
                            }
                            
                            if(_lifeTime > 0 && _playTime >= _lifeTime) complete();
                            else _setCalculatedValue(time);
                        }
                    }
                }
            }
        }

        /// <summary>Resets the <see cref="Tween">tween</see> values.</summary>
        /// <param name="playOnReset">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will play automatically. Default value is <c>false</c>.</param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.reset();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void reset(bool playOnReset = false)
        {
            _passedLoopsCount = 0;
            _isIncrementing = true;
            _isDelayOver = false;
            this.isCompleted = false;
            this.isRunning = playOnReset;
            _isFirstUpdate = true;
            _elapsed = 0f;
            _playTime = 0f;
            _loopTime = 0f;
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.play();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void play()
        {
            this.isRunning = true;
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.pause();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void pause()
        {
            this.isRunning = false;
        }

        /// <summary>Completes the <see cref="Tween">tween</see>.</summary>
        /// <param name="callbackOnComplete">(Optional) If <c>true</c>, triggers method call on <see cref="Tween">tween</see>'s end. Default value is <c>true</c></param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.complete();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void complete()
        {
            this.isCompleted = true;
            this.isRunning = false;
            _setInitialValue(true);
            _complete?.Invoke();
            if(_killOnEnd == true) kill();
        }

        /// <summary>Cancels the <see cref="Tween">tween</see>.</summary>
        /// <param name="callbackOnComplete">(Optional) If <c>true</c>, triggers method call on <see cref="Tween">tween</see>'s end. Default value is <c>true</c></param>
        /// <returns><c>void</c></returns>
        /// <remarks>Canceling a <see cref="Tween">tween</see> sends the target to its initial values.</remarks>
        /// <example>
        /// Press space to cancel.
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.cancel();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void cancel()
        {
            this.isCompleted = true;
            this.isRunning = false;
            _setInitialValue();
            if(_killOnEnd == true) kill();
        }

        /// <summary>Stops the <see cref="Tween">tween</see>.</summary>
        /// <param name="callbackOnComplete">(Optional) If <c>true</c>, triggers method call on <see cref="Tween">tween</see>'s end. Default value is <c>true</c></param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.stop();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void stop()
        {
            this.isCompleted = true;
            this.isRunning = false;
            if(_killOnEnd == true) kill();
        }

        /// <summary>Kills the <see cref="Tween">tween</see>. This unregisters the <see cref="Tween">tween</see>'s events and sets this.isAlive to <c>false</c>. <see cref="Str8Tween">Str8Tween</see> class will remove every reference to the <see cref="Tween">tween</see>.</summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             t.kill();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public void kill()
        {
            this.isAlive = false;
            _start -= _callbackOnStart;
            _loop -= _callbackOnLoop;
            _complete -= _callbackOnComplete;
        }
    #endregion

    #region Private methods
        private void _init(string methodName, bool killOnEnd)
        {
            if(!Array.Exists(_authorizedMethods, name => name == methodName)) throw new ArgumentException("methodName is not allowed", "methodName");
            this.isAlive = true;
            this.isCompleted = false;
            this.isRunning = true;
            _methodName = methodName;
            _delay = 0f;
            _isLoop = false;
            _loopsCount = 0;
            _passedLoopsCount = 0;
            _loopType = LoopType.Restart;
            _isIncrementing = true;
            _isDelayOver = false;
            _isFirstUpdate = true;
            _elapsed = 0f;
            _loopTime = 0f;
            _lifeTime = this.duration;
            _playTime = 0f;
            _killOnEnd = killOnEnd;
        }

        private void _setCalculatedValue(float playtime)
        {
            RectTransform rectTransform = null;
            if(this.target.TryGetComponent(out RectTransform rt)) rectTransform = rt;
            switch(_methodName)
            {
                case "move":
                    Vector3 newPosition = Easing.ease(this.easeType, playtime, _fromVector, _vectorChange, this.duration);
                    if(rectTransform != null) rectTransform.anchoredPosition3D = new Vector3(newPosition.x, newPosition.y, newPosition.z);
                    break;

                case "scale":
                    Vector3 newScale = Easing.ease(this.easeType, playtime, _fromVector, _vectorChange, this.duration);
                    if(rectTransform != null) rectTransform.localScale = new Vector3(newScale.x, newScale.y, newScale.z);
                    break;

                case "rotate":
                    Vector3 newEulerAngles = Easing.ease(this.easeType, playtime, _fromVector, _vectorChange, this.duration);
                    if(rectTransform != null) rectTransform.localEulerAngles = new Vector3(newEulerAngles.x, newEulerAngles.y, newEulerAngles.z);
                    break;

                case "fade":
                    if(_canvasRenderer != null && this.target.TryGetComponent(out CanvasRenderer canvasRenderer)) canvasRenderer.SetAlpha(Easing.ease(this.easeType, playtime, _fromCanvasRendererAlpha, _canvasRendererAlphaChange, this.duration));
                    if(_spriteRenderer != null && this.target.TryGetComponent(out SpriteRenderer spriteRenderer)){
                        Color newSpriteRendererColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Easing.ease(this.easeType, playtime, _fromSpriteRendererAlpha, _spriteRendererAlphaChange, this.duration));
                        spriteRenderer.color = newSpriteRendererColor;
                    }
                    if(_rawImage != null && this.target.TryGetComponent(out RawImage rawImage)){
                        Color newRawImageColor = new Color(_rawImage.color.r, _rawImage.color.g, _rawImage.color.b, Easing.ease(this.easeType, playtime, _fromRawImageAlpha, _rawImageAlphaChange, this.duration));
                        rawImage.color = newRawImageColor;
                    }
                    if(_image != null && this.target.TryGetComponent(out Image image)){
                        Color newImageColor = new Color(_image.color.r, _image.color.g, _image.color.b, Easing.ease(this.easeType, playtime, _fromImageAlpha, _imageAlphaChange, this.duration));
                        image.color = newImageColor;
                    }
                    if(_toggle != null && this.target.TryGetComponent(out Toggle toggle)){
                        float newAlpha = Easing.ease(this.easeType, playtime, _fromToggleAlpha, _toggleAlphaChange, this.duration);
                        if(toggle.graphic != null) toggle.graphic.color = new Color(_toggle.graphic.color.r, _toggle.graphic.color.g, _toggle.graphic.color.b, newAlpha);
                        if(toggle.targetGraphic != null) toggle.targetGraphic.color = new Color(_toggle.targetGraphic.color.r, _toggle.targetGraphic.color.g, _toggle.targetGraphic.color.b, newAlpha);
                    }
                    if(_slider != null && this.target.TryGetComponent(out Slider slider)){
                        float newAlpha = Easing.ease(this.easeType, playtime, _fromSliderAlpha, _sliderAlphaChange, this.duration);
                        if(slider.image != null) slider.image.color = new Color(_slider.image.color.r, _slider.image.color.g, _slider.image.color.b, newAlpha);
                        if(slider.fillRect.gameObject.TryGetComponent(out Image fill)){ fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, newAlpha); }
                        if(slider.handleRect.gameObject.TryGetComponent(out Image handle)){ handle.color = new Color(handle.color.r, handle.color.g, handle.color.b, newAlpha); }
                    }
                    if(_text != null && this.target.TryGetComponent(out Text text)){
                        Color newTextColor = new Color(_text.color.r, _text.color.g, _text.color.b, Easing.ease(this.easeType, playtime, _fromTextAlpha, _textAlphaChange, this.duration));
                        text.color = newTextColor;
                    }
                    if(_graphic != null && this.target.TryGetComponent(out Graphic graphic)){
                        Color newGraphicColor = new Color(_graphic.color.r, _graphic.color.g, _graphic.color.b, Easing.ease(this.easeType, playtime, _fromGraphicAlpha, _graphicAlphaChange, this.duration));
                        graphic.color = newGraphicColor;
                    }
                    break;
            }
        }

        private void _completeLoop()
        {
            if(_loopType == LoopType.WithOffset)
            {
                _fromVector = _toVector;
                _toVector += _vectorChange;

                if(_canvasRenderer != null){
                    _fromCanvasRendererAlpha = _toValue;
                    _toValue += _canvasRendererAlphaChange;
                }

                if(_spriteRenderer != null){
                    _fromSpriteRendererAlpha = _toValue;
                    _toValue += _spriteRendererAlphaChange;
                }

                if(_rawImage != null){
                    _fromRawImageAlpha = _toValue;
                    _toValue += _rawImageAlphaChange;
                }

                if(_image != null){
                    _fromImageAlpha = _toValue;
                    _toValue += _imageAlphaChange;
                }

                if(_toggle != null){
                    _fromToggleAlpha = _toValue;
                    _toValue += _toggleAlphaChange;
                }

                if(_slider != null){
                    _fromSliderAlpha = _toValue;
                    _toValue += _sliderAlphaChange;
                }

                if(_text != null){
                    _fromTextAlpha = _toValue;
                    _toValue += _textAlphaChange;
                }

                if(_graphic != null){
                    _fromGraphicAlpha = _toValue;
                    _toValue += _graphicAlphaChange;
                }

                if(_toValue > 1)
                {
                    _toValue = 1; //Clamps alpha max value
                    if(_canvasRenderer != null) _canvasRendererAlphaChange = _toValue - _fromCanvasRendererAlpha;
                    if(_spriteRenderer != null) _spriteRendererAlphaChange = _toValue - _fromSpriteRendererAlpha;
                    if(_rawImage != null) _rawImageAlphaChange = _toValue - _fromRawImageAlpha;
                    if(_image != null) _imageAlphaChange = _toValue - _fromImageAlpha;
                    if(_toggle != null) _toggleAlphaChange = _toValue - _fromToggleAlpha;
                    if(_slider != null) _sliderAlphaChange = _toValue - _fromSliderAlpha;
                    if(_text != null) _textAlphaChange = _toValue - _fromTextAlpha;
                    if(_graphic != null) _graphicAlphaChange = _toValue - _fromGraphicAlpha;
                }
                if(_toValue < 0) _toValue = 0; //Clamps alpha min value
            }
            _passedLoopsCount++;
            _loop?.Invoke(_passedLoopsCount);
        }

        //Go to initial "to values" or to initial "from values"
        private void _setInitialValue(bool useToValues = false){
            RectTransform rectTransform = null;
            if(this.target.TryGetComponent(out RectTransform rt)) rectTransform = rt;
            switch(_methodName)
            {
                case "move":
                    if(rectTransform != null) rectTransform.anchoredPosition3D = useToValues ? _initialToVector : _initialFromVector;
                    break;

                case "scale":
                    if(rectTransform != null) rectTransform.localScale = useToValues ? _initialToVector : _initialFromVector;
                    break;

                case "rotate":
                    if(rectTransform != null) rectTransform.localEulerAngles = useToValues ? _initialToVector : _initialFromVector;
                    break;

                case "fade":
                    float initialValue;
                    if(_canvasRenderer != null && this.target.TryGetComponent(out CanvasRenderer canvasRenderer)){
                        initialValue = useToValues ? _initialToValue : _initialFromCanvasRendererAlpha;
                        canvasRenderer.SetAlpha(initialValue);
                    }
                    if(_spriteRenderer != null && this.target.TryGetComponent(out SpriteRenderer spriteRenderer)){
                        initialValue = useToValues ? _initialToValue : _initialFromSpriteRendererAlpha;
                        Color newSpriteRendererColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, initialValue);
                        spriteRenderer.color = newSpriteRendererColor;
                    }
                    if(_rawImage != null && this.target.TryGetComponent(out RawImage rawImage)){
                        initialValue = useToValues ? _initialToValue : _initialFromRawImageAlpha;
                        Color newRawImageColor = new Color(_rawImage.color.r, _rawImage.color.g, _rawImage.color.b, initialValue);
                        rawImage.color = newRawImageColor;
                    }
                    if(_image != null && this.target.TryGetComponent(out Image image)){
                        initialValue = useToValues ? _initialToValue : _initialFromImageAlpha;
                        Color newImageColor = new Color(_image.color.r, _image.color.g, _image.color.b, initialValue);
                        image.color = newImageColor;
                    }
                    if(_toggle != null && this.target.TryGetComponent(out Toggle toggle)){
                        initialValue = useToValues ? _initialToValue : _initialFromToggleAlpha;
                        if(toggle.graphic != null) toggle.graphic.color = new Color(_toggle.graphic.color.r, _toggle.graphic.color.g, _toggle.graphic.color.b, initialValue);
                        if(toggle.targetGraphic != null) toggle.targetGraphic.color = new Color(_toggle.targetGraphic.color.r, _toggle.targetGraphic.color.g, _toggle.targetGraphic.color.b, initialValue);
                    }
                    if(_slider != null && this.target.TryGetComponent(out Slider slider)){
                        initialValue = useToValues ? _initialToValue : _initialFromSliderAlpha;
                        if(slider.image != null) slider.image.color = new Color(_slider.image.color.r, _slider.image.color.g, _slider.image.color.b, initialValue);
                        if(slider.fillRect != null && slider.fillRect.gameObject.TryGetComponent(out Image fill)){ fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, initialValue); }
                        if(slider.handleRect != null && slider.handleRect.gameObject.TryGetComponent(out Image handle)){ handle.color = new Color(handle.color.r, handle.color.g, handle.color.b, initialValue); }
                    }
                    if(_text != null && this.target.TryGetComponent(out Text text)){
                        initialValue = useToValues ? _initialToValue : _initialFromTextAlpha;
                        Color newTextColor = new Color(_text.color.r, _text.color.g, _text.color.b, initialValue);
                        text.color = newTextColor;
                    }
                    if(_graphic != null && this.target.TryGetComponent(out Graphic graphic)){
                        initialValue = useToValues ? _initialToValue : _initialFromGraphicAlpha;
                        Color newGraphicColor = new Color(_graphic.color.r, _graphic.color.g, _graphic.color.b, initialValue);
                        graphic.color = newGraphicColor;
                    }
                    break;
            }
        }
    #endregion
    }
}