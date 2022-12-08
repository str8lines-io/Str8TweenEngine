namespace Str8lines.Tweening
{
    #region namespaces
    using UnityEngine;
    #endregion

    /// <summary>Defines easing functions used internally and provides methods to calculate <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> and <c>float</c> values.</summary>
    public static class Easing
    {
        /// <summary>Used to specify the rate of change over time. See <see href="https://easings.net/">easings.net</see> to visualize each easing function.</summary>
        public enum EaseType
        {
            Linear, InSine, OutSine, InOutSine, InBack, OutBack, InOutBack, InCubic, OutCubic, InOutCubic, InQuad, OutQuad, InOutQuad, InQuart, OutQuart, InOutQuart, InQuint, OutQuint, InOutQuint, InExpo, OutExpo, InOutExpo, InCirc, OutCirc, InOutCirc, InElastic, OutElastic, InOutElastic, InBounce, OutBounce, InOutBounce
        }
        
        #region Private methods
        //t = current time value
        //b = begin value
        //c = change in value
        //d = duration
        private static float _linear(float t, float b, float c, float d){
            return c * t / d + b;
        }

        private static float _inSine(float t, float b, float c, float d){
            return c * (1 - Mathf.Cos(t / d * (Mathf.PI / 2))) + b;
        }

        private static float _outSine(float t, float b, float c, float d){
            return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
        }

        private static float _inOutSine(float t, float b, float c, float d){
            return c / 2 * (1 - Mathf.Cos(Mathf.PI * t / d)) + b;
        }

        private static float _inQuad(float t, float b, float c, float d){
            return c * (t /= d) * t + b;
        }

        private static float _outQuad(float t, float b, float c, float d){
            return -c * (t /= d) * (t - 2) + b;
        }

        private static float _inOutQuad(float t, float b, float c, float d){
            if ((t /= d / 2) < 1) return c / 2 * t * t + b;
            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        private static float _inCubic(float t, float b, float c, float d){
            return c * Mathf.Pow(t / d, 3) + b;
        }

        private static float _outCubic(float t, float b, float c, float d){
            return c * (Mathf.Pow(t / d - 1, 3) + 1) + b;
        }

        private static float _inOutCubic(float t, float b, float c, float d){
            if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(t, 3) + b;
            return c / 2 * (Mathf.Pow(t - 2, 3) + 2) + b;
        }

        private static float _inQuart(float t, float b, float c, float d){
            return c * Mathf.Pow(t / d, 4) + b;
        }

        private static float _outQuart(float t, float b, float c, float d){
            return -c * (Mathf.Pow(t / d - 1, 4) - 1) + b;
        }

        private static float _inOutQuart(float t, float b, float c, float d){
            if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(t, 4) + b;
            return -c / 2 * (Mathf.Pow(t - 2, 4) - 2) + b;
        }

        private static float _inQuint(float t, float b, float c, float d){
            return c * Mathf.Pow(t / d, 5) + b;
        }

        private static float _outQuint(float t, float b, float c, float d){
            return c * (Mathf.Pow(t / d - 1, 5) + 1) + b;
        }

        private static float _inOutQuint(float t, float b, float c, float d){
            if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(t, 5) + b;
            return c / 2 * (Mathf.Pow(t - 2, 5) + 2) + b;
        }

        private static float _inExpo(float t, float b, float c, float d){
            return c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
        }

        private static float _outExpo(float t, float b, float c, float d){
            return c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
        }

        private static float _inOutExpo(float t, float b, float c, float d){
            if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
        }

        private static float _inCirc(float t, float b, float c, float d){
            return c * (1 - Mathf.Sqrt(1 - (t /= d) * t)) + b;
        }

        private static float _outCirc(float t, float b, float c, float d){
            return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
        }

        private static float _inOutCirc(float t, float b, float c, float d){
            if ((t /= d / 2) < 1) return c / 2 * (1 - Mathf.Sqrt(1 - t * t)) + b;
            return c / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;

        }

        //a = amplitude
        //p = period
        //s = overshooting
        private static float _inElastic(float t, float b, float c, float d, float a = 1.0f, float p = 0.3f)
        {
            float s;
            if (t == 0) return b;
            if ((t /= d) == 1) return b + c;
            if (p == 0) p = d * 0.3f;
            if (a < Mathf.Abs(c))
            { 
                a = c;
                s = p / 4;
            }
            else s = p / (2 * Mathf.PI) * Mathf.Asin(c / a);
            return -(a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
        }

        private static float _outElastic(float t, float b, float c, float d, float a = 1.0f, float p = 0.3f)
        {
            float s;
            if (t == 0) return b;
            if ((t /= d) == 1) return b + c;
            if (p == 0) p = d * 0.3f;
            if (a < Mathf.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else s = p / (2 * Mathf.PI) * Mathf.Asin(c / a);
            return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b;
        }

        private static float _inOutElastic(float t, float b, float c, float d, float a = 1.0f, float p = 0.3f)
        {
            float s;
            if (t == 0) return b;
            if ((t /= d / 2) == 2) return b + c;
            if (p == 0) p = d * (0.3f * 1.5f);
            if (a < Mathf.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else s = p / (2 * Mathf.PI) * Mathf.Asin(c / a);
            if (t < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
            return a * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * 0.5f + c + b;
        }

        private static float _inBack(float t, float b, float c, float d, float s = 1.70158f){
            return c * (t /= d) * t * ((s + 1) * t - s) + b;
        }

        private static float _outBack(float t, float b, float c, float d, float s = 1.70158f){
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
        }

        private static float _inOutBack(float t, float b, float c, float d, float s = 1.70158f){
            if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }

        private static float _inBounce(float t, float b, float c, float d){
            return c - _outBounce(d - t, 0, c, d) + b;
        }

        private static float _outBounce(float t, float b, float c, float d){
            if ((t /= d) < (1 / 2.75f)) return c * (7.5625f * t * t) + b;
            else if (t < (2 / 2.75f)) return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f) + b;
            else if (t < (2.5f / 2.75f)) return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f) + b;
            else return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f) + b;
        }

        private static float _inOutBounce(float t, float b, float c, float d){
            if (t < d / 2) return _inBounce(t * 2, 0, c, d) * 0.5f + b;
            return _outBounce(t * 2 - d, 0, c, d) * 0.5f + c * 0.5f + b;
        }
        #endregion

        #region Public methods
        /// <summary>Calculates new <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> according to elapsed time.</summary>
        /// <param name="easeType">The <see cref="EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="t">Time elapsed (in seconds).</param>
        /// <param name="v">Initial <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> value.</param>
        /// <param name="c"><see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> value change.</param>
        /// <param name="d">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <returns>Resulting <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see>.</returns>
        /// <example>
        /// Calculates new Vector3 value.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     public Vector3 vector;
        ///
        ///     private void Start()
        ///     {
        ///         Vector3 vectorChange = new Vector3(0, 500, 0);
        ///         vector = Easing.ease(EaseType.Linear, 2f, vector, vectorChange, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static Vector3 ease(EaseType easeType, float t, Vector3 v, Vector3 c, float d)
        {
            Vector3 newVector;
            switch (easeType)
            {
                case EaseType.Linear:
                    newVector = new Vector3(_linear(t, v.x, c.x, d), _linear(t, v.y, c.y, d), _linear(t, v.z, c.z, d));
                    break;

                case EaseType.InSine:
                    newVector = new Vector3(_inSine(t, v.x, c.x, d), _inSine(t, v.y, c.y, d), _inSine(t, v.z, c.z, d));
                    break;

                case EaseType.OutSine:
                    newVector = new Vector3(_outSine(t, v.x, c.x, d), _outSine(t, v.y, c.y, d), _outSine(t, v.z, c.z, d));
                    break;

                case EaseType.InOutSine:
                    newVector = new Vector3(_inOutSine(t, v.x, c.x, d), _inOutSine(t, v.y, c.y, d), _inOutSine(t, v.z, c.z, d));
                    break;

                case EaseType.InQuad:
                    newVector = new Vector3(_inQuad(t, v.x, c.x, d), _inQuad(t, v.y, c.y, d), _inQuad(t, v.z, c.z, d));
                    break;

                case EaseType.OutQuad:
                    newVector = new Vector3(_outQuad(t, v.x, c.x, d), _outQuad(t, v.y, c.y, d), _outQuad(t, v.z, c.z, d));
                    break;

                case EaseType.InOutQuad:
                    newVector = new Vector3(_inOutQuad(t, v.x, c.x, d), _inOutQuad(t, v.y, c.y, d), _inOutQuad(t, v.z, c.z, d));
                    break;

                case EaseType.InQuart:
                    newVector = new Vector3(_inQuart(t, v.x, c.x, d), _inQuart(t, v.y, c.y, d), _inQuart(t, v.z, c.z, d));
                    break;

                case EaseType.OutQuart:
                    newVector = new Vector3(_outQuart(t, v.x, c.x, d), _outQuart(t, v.y, c.y, d), _outQuart(t, v.z, c.z, d));
                    break;

                case EaseType.InOutQuart:
                    newVector = new Vector3(_inOutQuart(t, v.x, c.x, d), _inOutQuart(t, v.y, c.y, d), _inOutQuart(t, v.z, c.z, d));
                    break;

                case EaseType.InQuint:
                    newVector = new Vector3(_inQuint(t, v.x, c.x, d), _inQuint(t, v.y, c.y, d), _inQuint(t, v.z, c.z, d));
                    break;

                case EaseType.OutQuint:
                    newVector = new Vector3(_outQuint(t, v.x, c.x, d), _outQuint(t, v.y, c.y, d), _outQuint(t, v.z, c.z, d));
                    break;

                case EaseType.InOutQuint:
                    newVector = new Vector3(_inOutQuint(t, v.x, c.x, d), _inOutQuint(t, v.y, c.y, d), _inOutQuint(t, v.z, c.z, d));
                    break;

                case EaseType.InExpo:
                    newVector = new Vector3(_inExpo(t, v.x, c.x, d), _inExpo(t, v.y, c.y, d), _inExpo(t, v.z, c.z, d));
                    break;

                case EaseType.OutExpo:
                    newVector = new Vector3(_outExpo(t, v.x, c.x, d), _outExpo(t, v.y, c.y, d), _outExpo(t, v.z, c.z, d));
                    break;

                case EaseType.InOutExpo:
                    newVector = new Vector3(_inOutExpo(t, v.x, c.x, d), _inOutExpo(t, v.y, c.y, d), _inOutExpo(t, v.z, c.z, d));
                    break;

                case EaseType.InCubic:
                    newVector = new Vector3(_inCubic(t, v.x, c.x, d), _inCubic(t, v.y, c.y, d), _inCubic(t, v.z, c.z, d));
                    break;

                case EaseType.OutCubic:
                    newVector = new Vector3(_outCubic(t, v.x, c.x, d), _outCubic(t, v.y, c.y, d), _outCubic(t, v.z, c.z, d));
                    break;

                case EaseType.InOutCubic:
                    newVector = new Vector3(_inOutCubic(t, v.x, c.x, d), _inOutCubic(t, v.y, c.y, d), _inOutCubic(t, v.z, c.z, d));
                    break;

                case EaseType.InCirc:
                    newVector = new Vector3(_inCirc(t, v.x, c.x, d), _inCirc(t, v.y, c.y, d), _inCirc(t, v.z, c.z, d));
                    break;

                case EaseType.OutCirc:
                    newVector = new Vector3(_outCirc(t, v.x, c.x, d), _outCirc(t, v.y, c.y, d), _outCirc(t, v.z, c.z, d));
                    break;

                case EaseType.InOutCirc:
                    newVector = new Vector3(_inOutCirc(t, v.x, c.x, d), _inOutCirc(t, v.y, c.y, d), _inOutCirc(t, v.z, c.z, d));
                    break;

                case EaseType.InBack:
                    newVector = new Vector3(_inBack(t, v.x, c.x, d), _inBack(t, v.y, c.y, d), _inBack(t, v.z, c.z, d));
                    break;

                case EaseType.OutBack:
                    newVector = new Vector3(_outBack(t, v.x, c.x, d), _outBack(t, v.y, c.y, d), _outBack(t, v.z, c.z, d));
                    break;

                case EaseType.InOutBack:
                    newVector = new Vector3(_inOutBack(t, v.x, c.x, d), _inOutBack(t, v.y, c.y, d), _inOutBack(t, v.z, c.z, d));
                    break;

                case EaseType.InElastic:
                    newVector = new Vector3(_inElastic(t, v.x, c.x, d), _inElastic(t, v.y, c.y, d), _inElastic(t, v.z, c.z, d));
                    break;

                case EaseType.OutElastic:
                    newVector = new Vector3(_outElastic(t, v.x, c.x, d), _outElastic(t, v.y, c.y, d), _outElastic(t, v.z, c.z, d));
                    break;

                case EaseType.InOutElastic:
                    newVector = new Vector3(_inOutElastic(t, v.x, c.x, d), _inOutElastic(t, v.y, c.y, d), _inOutElastic(t, v.z, c.z, d));
                    break;

                case EaseType.InBounce:
                    newVector = new Vector3(_inBounce(t, v.x, c.x, d), _inBounce(t, v.y, c.y, d), _inBounce(t, v.z, c.z, d));
                    break;

                case EaseType.OutBounce:
                    newVector = new Vector3(_outBounce(t, v.x, c.x, d), _outBounce(t, v.y, c.y, d), _outBounce(t, v.z, c.z, d));
                    break;

                case EaseType.InOutBounce:
                    newVector = new Vector3(_inOutBounce(t, v.x, c.x, d), _inOutBounce(t, v.y, c.y, d), _inOutBounce(t, v.z, c.z, d));
                    break;

                default:
                    newVector = new Vector3();
                    break;
            }
            return newVector;
        }

        /// <summary>Calculates new <c>float</c> value according to elapsed time.</summary>
        /// <param name="easeType">The <see cref="EaseType">ease type</see> represents the type of easing.</param>
        /// <param name="t">Time elapsed (in seconds).</param>
        /// <param name="f">Initial <c>float</c> value.</param>
        /// <param name="c"><c>float</c> value change.</param>
        /// <param name="d">Total <see cref="Tween">tween</see> duration (in seconds).</param>
        /// <returns>Resulting <c>float</c> value.</returns>
        /// <example>
        /// Calculates new float value.
        /// <code>
        /// using UnityEngine;
        /// using Str8lines.Tweening;
        /// 
        /// public class MyClass : MonoBehaviour
        /// {
        ///     private float val = 0f;
        ///
        ///     private void Start()
        ///     {
        ///         val = Easing.ease(EaseType.Linear, 2f, val, 10f, 3f);
        ///     }
        /// }
        /// </code>
        /// </example>
        public static float ease(EaseType easeType, float t, float f, float c, float d)
        {
            float newValue;
            switch (easeType)
            {
                case EaseType.Linear:
                    newValue = _linear(t, f, c, d);
                    break;

                case EaseType.InSine:
                    newValue = _inSine(t, f, c, d);
                    break;

                case EaseType.OutSine:
                    newValue =_outSine(t, f, c, d);
                    break;

                case EaseType.InOutSine:
                    newValue = _inOutSine(t, f, c, d);
                    break;

                case EaseType.InQuad:
                    newValue =_inQuad(t, f, c, d);
                    break;

                case EaseType.OutQuad:
                    newValue =_outQuad(t, f, c, d);
                    break;

                case EaseType.InOutQuad:
                    newValue = _inOutQuad(t, f, c, d);
                    break;

                case EaseType.InQuart:
                    newValue = _inQuart(t, f, c, d);
                    break;

                case EaseType.OutQuart:
                    newValue = _outQuart(t, f, c, d);
                    break;

                case EaseType.InOutQuart:
                    newValue = _inOutQuart(t, f, c, d);
                    break;

                case EaseType.InQuint:
                    newValue = _inQuint(t, f, c, d);
                    break;

                case EaseType.OutQuint:
                    newValue = _outQuint(t, f, c, d);
                    break;

                case EaseType.InOutQuint:
                    newValue = _inOutQuint(t, f, c, d);
                    break;

                case EaseType.InExpo:
                    newValue = _inExpo(t, f, c, d);
                    break;

                case EaseType.OutExpo:
                    newValue = _outExpo(t, f, c, d);
                    break;

                case EaseType.InOutExpo:
                    newValue = _inOutExpo(t, f, c, d);
                    break;

                case EaseType.InCubic:
                    newValue = _inCubic(t, f, c, d);
                    break;

                case EaseType.OutCubic:
                    newValue =_outCubic(t, f, c, d);
                    break;

                case EaseType.InOutCubic:
                    newValue = _inOutCubic(t, f, c, d);
                    break;

                case EaseType.InCirc:
                    newValue = _inCirc(t, f, c, d);
                    break;

                case EaseType.OutCirc:
                    newValue = _outCirc(t, f, c, d);
                    break;

                case EaseType.InOutCirc:
                    newValue = _inOutCirc(t, f, c, d);
                    break;

                case EaseType.InBack:
                    newValue = _inBack(t, f, c, d);
                    break;

                case EaseType.OutBack:
                    newValue = _outBack(t, f, c, d);
                    break;

                case EaseType.InOutBack:
                    newValue = _inOutBack(t, f, c, d);
                    break;

                case EaseType.InElastic:
                    newValue = _inElastic(t, f, c, d);
                    break;

                case EaseType.OutElastic:
                    newValue = _outElastic(t, f, c, d);
                    break;

                case EaseType.InOutElastic:
                    newValue = _inOutElastic(t, f, c, d);
                    break;

                case EaseType.InBounce:
                    newValue = _inBounce(t, f, c, d);
                    break;

                case EaseType.OutBounce:
                    newValue = _outBounce(t, f, c, d);
                    break;

                case EaseType.InOutBounce:
                    newValue = _inOutBounce(t, f, c, d);
                    break;

                default:
                    newValue= f;
                    break;
            }
            return newValue;
        }
        #endregion
    }
}