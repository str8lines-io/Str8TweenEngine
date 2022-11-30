#region Author
/* 
    Str8lines Tween Engine for Unity
    Version: 1.0
    Author:	Geoffrey LESNE
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
        private static Dictionary<string, Tween> tweens = new Dictionary<string, Tween>();
        private List<string> _deadTweenIDs = new List<string>();
        #endregion

        #region Public methods
        /// <summary>Instantiate a new <see cref="Tween">tween</see> which moves the <paramref name="rectTransform"/> to a given position.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> to move.</param>
        /// <param name="toValue">A <see href="https://docs.unity3d.com/ScriptReference/Vector2.html">Vector2</see> that represents <paramref name="rectTransform"/>'s final position.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Moves the rectTransform during 3 seconds to the given position in a linear motion.
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
        ///         Str8Tween.move(rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween move(RectTransform rectTransform, Vector2 toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(rectTransform == null) throw new ArgumentNullException("rectTransform", "rectTransform can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if (_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, rectTransform, toValue, easeType, duration, killOnEnd);
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes the scale of the <paramref name="rectTransform"/>.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> to resize.</param>
        /// <param name="toValue">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final scale.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Changes rectTransform's scale during 3 seconds with a linear easing.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes the euler angles of the <paramref name="rectTransform"/>.</summary>
        /// <param name="rectTransform">The <see href="https://docs.unity3d.com/ScriptReference/RectTransform.html">RectTransform</see> that will rotate.</param>
        /// <param name="toValue">A <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> that represents <paramref name="rectTransform"/>'s final euler angles.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <remarks>Due to the way Unity handles rotations, <paramref name="toValue"/> may differ from rectTransform's euler angles in the inspector.
        /// To learn more about rotations in Unity, please check : <see href="https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html">Rotation and Orientation in Unity</see>.</remarks>
        /// <example>
        /// Rotates the rectTransform during 3 seconds to the given angles in a linear motion.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="canvasRenderer"/>'s alpha to a given value.</summary>
        /// <param name="canvasRenderer">The <see href="https://docs.unity3d.com/ScriptReference/CanvasRenderer.html">CanvasRenderer</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="canvasRenderer"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the CanvasRenderer during 3 seconds to the given alpha with a linear easing.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="spriteRenderer"/>'s alpha to a given value.</summary>
        /// <param name="spriteRenderer">The <see href="https://docs.unity3d.com/ScriptReference/SpriteRenderer.html">SpriteRenderer</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="spriteRenderer"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the SpriteRenderer during 3 seconds to the given alpha with a linear easing.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="rawImage"/>'s alpha to a given value.</summary>
        /// <param name="rawImage">The <see href="https://docs.unity3d.com/ScriptReference/RawImage.html">RawImage</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="rawImage"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the RawImage during 3 seconds to the given alpha with a linear easing.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="image"/>'s alpha to a given value.</summary>
        /// <param name="image">The <see href="https://docs.unity3d.com/ScriptReference/Image.html">Image</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="image"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the Image during 3 seconds to the given alpha with a linear easing.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="text"/>'s alpha to a given value.</summary>
        /// <param name="text">The <see href="https://docs.unity3d.com/ScriptReference/Text.html">Text</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="text"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the Text during 3 seconds to the given alpha with a linear easing.
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
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="toggle"/>'s alpha to a given value.</summary>
        /// <param name="toggle">The <see href="https://docs.unity3d.com/ScriptReference/Toggle.html">Toggle</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="toggle"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the Toggle during 3 seconds to the given alpha with a linear easing.
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
        ///         Str8Tween.fade(toggle, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(Toggle toggle, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(toggle == null) throw new ArgumentNullException("toggle", "toggle can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, toggle, toValue, easeType, duration, killOnEnd);
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="slider"/>'s alpha to a given value.</summary>
        /// <param name="slider">The <see href="https://docs.unity3d.com/ScriptReference/Slider.html">Slider</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="slider"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the Slider during 3 seconds to the given alpha with a linear easing.
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
        ///         Str8Tween.fade(slider, 0f, Easing.EaseType.Linear, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween fade(Slider slider, float toValue, Easing.EaseType easeType, float duration, bool killOnEnd = true)
        {
            if(slider == null) throw new ArgumentNullException("slider", "slider can not be null");
            if(duration <= 0f) throw new ArgumentException("duration must be positive and superior to zero", "duration");
            if(toValue < 0f || toValue > 1f) throw new ArgumentException("toValue must be between 0 and 1", "toValue");
            if(_tweenerGameObject == null) _init();
            string method = new StackTrace().GetFrame(0).GetMethod().Name;
            Tween t = new Tween(method, slider, toValue, easeType, duration, killOnEnd);
            tweens.Add(t.id, t);
            return t;
        }

        /// <summary>Instantiate a new <see cref="Tween">tween</see> which changes <paramref name="graphic"/>'s alpha to a given value.</summary>
        /// <param name="graphic">The <see href="https://docs.unity3d.com/ScriptReference/Graphic.html">Graphic</see> that will fade.</param>
        /// <param name="toValue">A <c>float</c> that represents <paramref name="graphic"/>'s final alpha value.</param>
        /// <param name="easeType">The <see cref="Easing.EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="duration">Total tween duration (in seconds).</param>
        /// <param name="killOnEnd">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will be destroyed once completed.</param>
        /// <returns>The <see cref="Tween">tween</see> created.</returns>
        /// <example>
        /// Fades the Graphic during 3 seconds to the given alpha with a linear easing.
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
            tweens.Add(t.id, t);
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
        ///         Tween t = Str8Tween.getTween("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween getTween(string id)
        {
            if(id != "" && tweens.ContainsKey(id)) return tweens[id];
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
        ///         Tween[] tweens = Str8Tween.getTweens();
        ///         if(tweens.Length > 0){
        ///             foreach(Tween t in tweens){
        ///                 UnityEngine.Debug.Log(t.id)
        ///             }
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween[] getTweens()
        {
            List<Tween> t = new List<Tween>();
            foreach(Tween tween in tweens.Values) t.Add(tween);
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
        ///         Tween[] tweens = Str8Tween.getTweens(target);
        ///         if(tweens.Length > 0){
        ///             foreach(Tween t in tweens){
        ///                 UnityEngine.Debug.Log(t.id)
        ///             }
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Tween[] getTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            List<Tween> tweensFound = new List<Tween>();
            foreach(Tween tween in tweens.Values){
                if(tween.target == target) tweensFound.Add(tween);
            }
            return tweensFound.ToArray();
        }

        /// <summary>Play the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.play()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.playTween("7aac6207-107f-4ddc-8761-122f669d3e3b"); 
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void playTween(string id)
        {
            getTween(id)?.play();
        }

        /// <summary>Plays every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.play()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.playTweens(); 
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void playTweens()
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.play();
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.playTweens(target); 
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void playTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.play();
            }
        }

        /// <summary>Pause the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.pause()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.pauseTween("7aac6207-107f-4ddc-8761-122f669d3e3b"); 
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void pauseTween(string id)
        {
            getTween(id)?.pause();
        }

        /// <summary>Pauses every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.pause()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.pauseTweens(); 
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void pauseTweens()
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.pause();
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.pauseTweens(target);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void pauseTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.pause();
            }
        }

        /// <summary>Reset the <see cref="Tween">tween</see> associated to the given uuid.</summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to reset.</param>
        /// <param name="playOnReset">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will play automatically after values <see cref="Tween.reset(bool)">reset</see>. Default value is <c>false</c></param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.resetTween("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void resetTween(string id, bool playOnReset = false)
        {
            getTween(id)?.reset(playOnReset);
        }

        /// <summary>Reset every <see cref="Tween">tween</see> alive.</summary>
        /// <param name="playOnReset">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will play automatically after values <see cref="Tween.reset(bool)">reset</see>. Default value is <c>false</c></param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.resetTweens();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void resetTweens(bool playOnReset = false)
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.reset(playOnReset);
            }
        }

        /// <summary>Reset every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.reset(bool)">reset</see>. Default value is <c>false</c></param>
        /// <param name="playOnReset">(Optional) If <c>true</c>, the <see cref="Tween">tween</see> will play automatically after values <see cref="Tween.reset(bool)">reset</see>.</param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.resetTweens(target);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void resetTweens(GameObject target, bool playOnReset = false)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.reset(playOnReset);
            }
        }

        /// <summary>Complete the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.complete()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.completeTween("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void completeTween(string id)
        {
            getTween(id)?.complete();
        }

        /// <summary>Complete every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.complete()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.completeTweens();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void completeTweens()
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.complete();
            }
        }

        /// <summary>Complete every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.complete()">complete</see>. Default value is <c>true</c></param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.completeTweens(target);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void completeTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.complete();
            }
        }

        /// <summary>Cancel the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.cancel()"/></summary>
        /// <param name="id">The <see cref="Tween.id">id</see> of the <see cref="Tween">tween</see> to cancel.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to cancel a tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.cancelTween("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void cancelTween(string id)
        {
            getTween(id)?.cancel();
        }

        /// <summary>Cancel every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.cancel()"/></summary>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to cancel every tween :
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private void Update()
        ///     {
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.cancelTweens();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void cancelTweens()
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.cancel();
            }
        }

        /// <summary>Cancel every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.cancel()">cancel</see>.</param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// Press space to cancel target's tweens :
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.cancelTweens(target);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void cancelTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.cancel();
            }
        }

        /// <summary>Stop the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.stop()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.stopTween("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void stopTween(string id)
        {
            getTween(id)?.stop();
        }

        /// <summary>Stop every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.stop()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.stopTweens();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void stopTweens()
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.stop();
            }
        }

        /// <summary>Stop every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
        /// <param name="target">The <see cref="Tween.target">target</see> of the <see cref="Tween">tweens</see> to <see cref="Tween.stop()">stop</see>.</param>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.stopTweens(target);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void stopTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.stop();
            }
        }

        /// <summary>Kill the <see cref="Tween">tween</see> associated to the given uuid. See also <seealso cref="Tween.kill()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.killTween("7aac6207-107f-4ddc-8761-122f669d3e3b");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void kill(string id)
        {
            getTween(id)?.kill();
        }

        /// <summary>Kill every <see cref="Tween">tween</see> alive. See also <seealso cref="Tween.kill()"/></summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.killTweens();
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void killTweens()
        {
            if(tweens.Count > 0){
                foreach(Tween t in tweens.Values) t.kill();
            }
        }

        /// <summary>Kill every <see cref="Tween">tween</see> associated to the given <see href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</see>.</summary>
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
        ///         if (Input.GetKeyDown(KeyCode.Space))
        ///         {
        ///             Str8Tween.killTweens(target);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void killTweens(GameObject target)
        {
            if(target == null) throw new ArgumentNullException("target", "target can not be null");
            Tween[] tweensFound = getTweens(target);
            if(tweensFound.Length > 0){
                foreach(Tween t in tweensFound) t.kill();
            }
        }
        #endregion

        #region Private methods
        private static void _init()
        {
            //The Str8Tween Class must be attached to a GameObject so the Update method can be triggered on every frame
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
            
            for(int i = 0; i < tweens.Count; i++) //Working with foreach loops to iterate through a dictionnary while manipulating it triggers errors
            {
                KeyValuePair<string, Tween> entry = tweens.ElementAt(i);
                if(entry.Value.target != null && entry.Value.isAlive) entry.Value.update(Time.deltaTime);
                else _deadTweenIDs.Add(entry.Key);
            }

            foreach(string id in _deadTweenIDs) tweens.Remove(id);
            if(tweens.Count == 0) Destroy(_tweenerGameObject);
        }
        #endregion
    }
}