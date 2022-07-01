using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Str8lines.Tweening;

public class EndUserTests : MonoBehaviour
{
    public RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        Tween t = Str8Tween.move(rect, Vector2.right, Easing.EaseType.Linear, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
