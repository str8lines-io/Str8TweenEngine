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
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    #endregion

    /// <summary></summary>
    public class TweensHandler : MonoBehaviour
    {
        #region Variables
        private static TweensHandler instance;
        /// <value></value>
        public static TweensHandler Instance
        {
            get { return instance ?? (instance = new GameObject("Str8Tween").AddComponent<TweensHandler>()); } 
            private set{ instance = value;} 
        }
        private Dictionary<string, Tween> _tweens = new Dictionary<string, Tween>();
        /// <value></value>
        public Dictionary<string, Tween> tweens => _tweens;
        private List<string> _deadTweenIDs = new List<string>();
        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
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
        }

        /// <summary></summary>
        /// <param name="t"></param>
        /// <returns><c>void</c></returns>
        /// <example>
        /// 
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
        ///         Tween t = new Tween("move", rectTransform, destination, Easing.EaseType.Linear, 3f);
        ///         TweensHandler.AddTween(t);
        ///     }
        /// }
        /// </code>
        /// </example>
        public void AddTween(Tween t){ if(t != null) _tweens.Add(t.id, t); }
    }
}