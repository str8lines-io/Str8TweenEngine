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
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    #endregion

    /// <summary>Str8Tween is a tween engine. This class is meant to instantiate <see cref="Tween">tweens</see> and manage their lifecycle. It provides additional methods to manipulate every <see cref="Tween">tweens</see> created.</summary>
    public class Str8Tween : MonoBehaviour
    {
        #region Variables
        private static GameObject _tweenerGameObject;
        private static Dictionary<string, Tween> _tweens = new Dictionary<string, Tween>();
        private List<string> _deadTweenIDs = new List<string>();
        #endregion

        #region Public methods
        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes the <see href="https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html">anchoredPosition3D</see> of the <paramref name="rectTransform"/>.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> to move.</param>
        /// <param name="toValue">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final position.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the rectTransform's anchoredPosition3D during 3 seconds in a linear motion.
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
        ///         Vector3 destination = new Vector3(0, 500);
        ///         Str8Tween.move(rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween move(RectTransform rectTransform, Vector3 toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rectTransform == null) throw new ArgumentNullException("rectTransform", "rectTransform can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if (_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, rectTransform, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes the <see href="https://docs.unity3d.com/ScriptReference/Transform-localScale.html">localScale</see> of the <paramref name="rectTransform"/>.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> to resize.</param>
        /// <param name="toValue">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final scale.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes rectTransform's localScale during 3 seconds with a linear easing.
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
        ///         Vector3 newScale = new Vector3(2, 2, 2);
        ///         Str8Tween.scale(rectTransform, newScale, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween scale(RectTransform rectTransform, Vector3 toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rectTransform == null) throw new ArgumentNullException("rectTransform", "rectTransform can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, rectTransform, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes the <see href="https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html">localEulerAngles</see> of the <paramref name="rectTransform"/>.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> that will rotate.</param>
        /// <param name="toValue">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final rotation.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <remarks>Due to the way Unity handles rotations, <paramref name="toValue"/> may differ from rectTransform's rotation in the inspector.
        /// To learn more about rotations in Unity, please check : <see href="https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html">Rotation and Orientation in Unity</see>.</remarks>
        /// <example>
        /// Changes the rectTransform's localEulerAngles during 3 seconds in a linear motion.
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
        ///         Vector3 newAngles = new Vector3(90, 180, 0);
        ///         Str8Tween.rotate(rectTransform, newAngles, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween rotate(RectTransform rectTransform, Vector3 toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rectTransform == null) throw new ArgumentNullException("rectTransform", "rectTransform can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, rectTransform, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes <paramref name="canvasRenderer"/>'s alpha to a given value.</summary>
        /// <param name="canvasRenderer">The <see href="https://docs.unity3d.com/ScriptReference/CanvasRenderer.html">CanvasRenderer</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="canvasRenderer"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the CanvasRenderer's alpha during 3 seconds with a linear easing.
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
        ///         Str8Tween.fade(canvasRenderer, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(CanvasRenderer canvasRenderer, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(canvasRenderer == null) throw new ArgumentNullException("canvasRenderer", "canvasRenderer can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, canvasRenderer, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes <paramref name="spriteRenderer"/>'s alpha to a given value.</summary>
        /// <param name="spriteRenderer">The <see href="https://docs.unity3d.com/ScriptReference/SpriteRenderer.html">SpriteRenderer</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="spriteRenderer"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the SpriteRenderer's alpha during 3 seconds with a linear easing.
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
        ///         Str8Tween.fade(spriteRenderer, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(SpriteRenderer spriteRenderer, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(spriteRenderer == null) throw new ArgumentNullException("spriteRenderer", "spriteRenderer can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, spriteRenderer, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes <paramref name="rawImage"/>'s alpha to a given value.</summary>
        /// <param name="rawImage">The <see href="https://docs.unity3d.com/ScriptReference/RawImage.html">RawImage</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="rawImage"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the RawImage's alpha during 3 seconds with a linear easing.
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
        ///         Str8Tween.fade(rawImage, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(RawImage rawImage, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rawImage == null) throw new ArgumentNullException("rawImage", "rawImage can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, rawImage, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes <paramref name="image"/>'s alpha to a given value.</summary>
        /// <param name="image">The <see href="https://docs.unity3d.com/ScriptReference/Image.html">Image</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="image"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the Image's alpha during 3 seconds with a linear easing.
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
        ///         Str8Tween.fade(image, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(Image image, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(image == null) throw new ArgumentNullException("image", "image can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, image, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes <paramref name="text"/>'s alpha to a given value.</summary>
        /// <param name="text">The <see href="https://docs.unity3d.com/ScriptReference/Text.html">Text</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="text"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the Text's alpha during 3 seconds with a linear easing.
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
        ///         Str8Tween.fade(text, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(Text text, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(text == null) throw new ArgumentNullException("text", "text can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, text, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiates a new <see cref="Tween">tween</see> which changes <paramref name="graphic"/>'s alpha to a given value.</summary>
        /// <param name="graphic">The <see href="https://docs.unity3d.com/ScriptReference/Graphic.html">Graphic</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="graphic"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed on end.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes the Graphic's alpha during 3 seconds with a linear easing.
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
        ///         Str8Tween.fade(graphic, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(Graphic graphic, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(graphic == null) throw new ArgumentNullException("graphic", "graphic can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, graphic, toValue, easeType, duration, killOnEnd);
            _tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Returns the <see cref="Tween">tween</see> associated to the given uuid.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to retrieve.</param>
        /// <returns>The <see cref="Tween">tween</see> retrieved or <c>null</c> if not found.</returns>
        /// <example>
        /// Gets a tween from its id :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         Tween t = Str8Tween.get("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween get(string id)
        {
            if(id != "" && _tweens.ContainsKey(id)) return _tweens[id];
            else return null;
        }

        /// <summary>Returns the <see cref="Tween">tweens</see> which are still alive.</summary>
        /// <returns>An <c>Array</c> of <see cref="Tween">tweens</see>.</returns>
        /// <example>
        /// Displays the ids of every existing tween in console :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         Tween[] tweens = Str8Tween.get();
        ///         if(tweens.Length > 0){
        ///             foreach(Tween t in tweens){
        ///                 UnityEngine.Debug.Log(t.id)
        ///             }
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween[] get()
        {
            List<Tween> t = new List<Tween>();
            foreach(Tween tween in _tweens.Values) t.Add(tween);
            return t.ToArray();
        }

        /// <summary>Returns the <see cref="Tween">tweens</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to retrieve.</param>
        /// <returns>An <c>Array</c> of <see cref="Tween">tweens</see>.</returns>
        /// <example>
        /// Displays target's tweens ids in console :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Start()
        ///     {
        ///         Tween[] tweens = Str8Tween.get(target);
        ///         if(tweens.Length > 0){
        ///             foreach(Tween t in tweens){
        ///                 UnityEngine.Debug.Log(t.id)
        ///             }
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween[] get(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            List<Tween> tweensFound = new List<Tween>();
            foreach(Tween tween in _tweens.Values){
                if(tween.target == target) tweensFound.Add(tween);
            }
            return tweensFound.ToArray();
        }

        /// <summary>Plays the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.play()"/>.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to play.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to play a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.play("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void play(string id)
        {
            get(id)?.play();
        }

        /// <summary>Plays every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.play()"/>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to play existing tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.play();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void play()
        {
            if(_tweens.Count > 0){
                foreach(Tween t in _tweens.Values) t.play();
            }
        }

        /// <summary>Plays every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.play()">play</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to play target's tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.play(target);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void play(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = get(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.play();
            }
        }

        /// <summary>Pauses the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.pause()"/>.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to pause.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to pause a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.pause("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void pause(string id)
        {
            get(id)?.pause();
        }

        /// <summary>Pauses every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.pause()"/>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to pause existing tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.pause();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void pause()
        {
            if(_tweens.Count > 0){
                foreach(Tween t in _tweens.Values) t.pause();
            }
        }

        /// <summary>Pauses every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.pause()">pause</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to pause target's tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.pause(target);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void pause(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = get(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.pause();
            }
        }

        /// <summary>Resets the <see cref="Tween">tween</see> associated to the given uuid.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to reset. See also <seealso cref="Tween.reset()"/></param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to reset a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.reset("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void reset(string id)
        {
            get(id)?.reset();
        }

        /// <summary>Reset every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.reset()"/>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to reset every tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.reset();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void reset()
        {
            if(_tweens.Count > 0){
                foreach(Tween t in _tweens.Values) t.reset();
            }
        }

        /// <summary>Reset every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.reset()">reset</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to reset target's tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.reset(target);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void reset(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = get(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.reset();
            }
        }

        /// <summary>Completes the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.complete(bool)"/>.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to complete.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to complete a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.complete("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void complete(string id, bool triggerOnEnd = true)
        {
            get(id)?.complete(triggerOnEnd);
        }

        /// <summary>Completes every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.complete(bool)"/>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to complete every tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.complete();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void complete(bool triggerOnEnd = true)
        {
            if(_tweens.Count > 0){
                foreach(Tween t in _tweens.Values) t.complete(triggerOnEnd);
            }
        }

        /// <summary>Completes every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.complete(bool)">complete</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to complete target's tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.complete(target);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void complete(GameObject target, bool triggerOnEnd = true)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = get(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.complete(triggerOnEnd);
            }
        }

        /// <summary>Stops the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.stop(bool)"/>.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to stop.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to stop a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.stop("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void stop(string id, bool triggerOnEnd = true)
        {
            get(id)?.stop(triggerOnEnd);
        }

        /// <summary>Stops every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.stop(bool)"/>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to stop every tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.stop();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void stop(bool triggerOnEnd = true)
        {
            if(_tweens.Count > 0){
                foreach(Tween t in _tweens.Values) t.stop(triggerOnEnd);
            }
        }

        /// <summary>Stops every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.stop(bool)">stop</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to stop target's tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.stop(target);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void stop(GameObject target, bool triggerOnEnd = true)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = get(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.stop(triggerOnEnd);
            }
        }

        /// <summary>Kills the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.kill()"/>.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to kill.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to kill a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.kill("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void kill(string id)
        {
            get(id)?.kill();
        }

        /// <summary>Kills every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.kill()"/>.</summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to kill every tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.kill();
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void kill()
        {
            if(_tweens.Count > 0){
                foreach(Tween t in _tweens.Values) t.kill();
            }
        }

        /// <summary>Kills every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.kill()">kill</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to kill target's tweens :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public GameObject target;
        ///
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space)) Str8Tween.kill(target);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void kill(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = get(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.kill();
            }
        }
        #endregion

        #region Private methods
        private static void _init()
        {
            //The Str8Tween Class must be attached to a GameObject so the Update method can be called on every frame
            _tweenerGameObject = new GameObject();
            _tweenerGameObject.name = "Str8Tween";
            _tweenerGameObject.AddComponent(typeof(Str8Tween));
            _tweenerGameObject.isStatic = true;
#if UNITY_EDITOR
            if(Application.isPlaying) DontDestroyOnLoad(_tweenerGameObject);
#else
            _tweenerGameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(_tweenerGameObject);
#endif
        }

        private void Update()
        {
            _deadTweenIDs.Clear(); //Remove dead tweens
            _deadTweenIDs.TrimExcess(); //Remove empty entries
            
            for(int i = 0; i < _tweens.Count; i++) //Working with foreach loops to iterate through a dictionnary while manipulating it triggers errors
            {
                KeyValuePair<string, Tween> entry = _tweens.ElementAt(i);
                if(entry.Value.target != null && entry.Value.isAlive) entry.Value.update(Time.deltaTime);
                else _deadTweenIDs.Add(entry.Key);
            }

            foreach(string id in _deadTweenIDs) _tweens.Remove(id);
            if(_tweens.Count == 0) Destroy(_tweenerGameObject);
        }
        #endregion
    }
}