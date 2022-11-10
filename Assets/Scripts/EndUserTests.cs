using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Str8lines.Tweening;

public class EndUserTests : MonoBehaviour
{
    public GameObject target;
    public string id;
    public float duration;
    public float delay;
    public Easing.EaseType easeType;
    public bool isLoop;
    public Tween.LoopType loopType;
    public int loopsCount;
    public bool isFrom;
    
    /*
        * Tester les animations sans loop
        * Tester les loops
        * Tester tous les ease types
        * Tester les différents Tweens (move, scale, rotate, fade)
            * Move => up, right, down, left + combinations
            * Scale => up, down
            * Rotate => below 360 + Higher than 360, sens horaire et anti horaire
            * Fade => in and out

        - On va aussi bourrer le nb de trucs à check par test (delay, events)
        - Pour tester les reset/complete/kill etc il va falloir instantier des btns qui déclenchent ces commandes pour tous les Tweens/les Tweens d'une target/un Tween précis
    */

    void Start()
    {
        GameObject canvas = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(VerticalLayoutGroup));
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.GetComponent<VerticalLayoutGroup>().spacing = 5;
        
        GameObject itemToMove = new GameObject("itemToMove", typeof(RectTransform), typeof(Image));
        itemToMove.transform.SetParent(canvas.transform);
        GameObject itemToScale = new GameObject("itemToScale", typeof(RectTransform), typeof(Image));
        itemToScale.transform.SetParent(canvas.transform);
        GameObject itemToRotate = new GameObject("itemToRotate", typeof(RectTransform), typeof(Image));
        itemToRotate.transform.SetParent(canvas.transform);
        GameObject canvasRendererToFade = new GameObject("canvasRendererToFade", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        canvasRendererToFade.transform.SetParent(canvas.transform);
        GameObject spriteRendererToFade = new GameObject("spriteRendererToFade", typeof(RectTransform), typeof(SpriteRenderer), typeof(Image));
        spriteRendererToFade.transform.SetParent(canvas.transform);
        GameObject imageToFade = new GameObject("imageToFade", typeof(RectTransform), typeof(Image));
        imageToFade.transform.SetParent(canvas.transform);
        GameObject rawImageToFade = new GameObject("rawImageToFade", typeof(RectTransform), typeof(RawImage));
        rawImageToFade.transform.SetParent(canvas.transform);
        GameObject textToFade = new GameObject("textToFade", typeof(RectTransform), typeof(Text));
        textToFade.transform.SetParent(canvas.transform);
        GameObject graphicToFade = new GameObject("graphicToFade", typeof(RectTransform), typeof(Image));
        graphicToFade.transform.SetParent(canvas.transform);

        //Create Toggle
        GameObject toggleToFade = new GameObject("toggleToFade", typeof(RectTransform), typeof(Toggle));
        GameObject toggleBg = new GameObject("Background", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        GameObject checkmark = new GameObject("Checkmark", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        GameObject toggleLabel = new GameObject("Label", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        checkmark.transform.SetParent(toggleBg.transform);
        toggleBg.transform.SetParent(toggleToFade.transform);
        toggleLabel.transform.SetParent(toggleToFade.transform);
        toggleToFade.transform.SetParent(canvas.transform);

        Toggle toggle = toggleToFade.GetComponent<Toggle>();
        toggle.targetGraphic = toggleBg.GetComponent<Image>();
        toggle.isOn = true;

        RectTransform toggleToFadeRectTransform = toggleToFade.GetComponent<RectTransform>();
        toggleToFadeRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        toggleToFadeRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        toggleToFadeRectTransform.sizeDelta = new Vector2(160, 20);

        RectTransform toggleBgRectTransform = toggleBg.GetComponent<RectTransform>();
        toggleBgRectTransform.anchorMin = Vector2.up;
        toggleBgRectTransform.anchorMax = Vector2.up;
        toggleBgRectTransform.anchoredPosition = new Vector2(10, -10);
        toggleBgRectTransform.sizeDelta = new Vector2(20, 20);

        RectTransform toggleLabelRectTransform = toggleLabel.GetComponent<RectTransform>();
        toggleLabelRectTransform.anchorMin = Vector2.zero;
        toggleLabelRectTransform.anchorMax = Vector2.one;
        toggleLabelRectTransform.offsetMin = new Vector2(23, 1);
        toggleLabelRectTransform.offsetMax = new Vector2(-5, -2);

        RectTransform checkmarkRectTransform = checkmark.GetComponent<RectTransform>();
        checkmarkRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        checkmarkRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        checkmarkRectTransform.sizeDelta = new Vector2(20, 20);

        //Create Slider
        GameObject sliderToFade = new GameObject("sliderToFade", typeof(RectTransform), typeof(Slider));
        GameObject sliderBg = new GameObject("Background", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        GameObject fillArea = new GameObject("FillArea", typeof(RectTransform));
        GameObject fill = new GameObject("Fill", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        GameObject handleSlideArea = new GameObject("HandleSlideArea", typeof(RectTransform));
        GameObject handle = new GameObject("Handle", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        sliderBg.transform.SetParent(sliderToFade.transform);
        fill.transform.SetParent(fillArea.transform);
        fillArea.transform.SetParent(sliderToFade.transform);
        handle.transform.SetParent(handleSlideArea.transform);
        handleSlideArea.transform.SetParent(sliderToFade.transform);
        sliderToFade.transform.SetParent(canvas.transform);
        
        Slider slider = sliderToFade.GetComponent<Slider>();
        slider.targetGraphic = handle.GetComponent<Image>();
        slider.fillRect = fill.GetComponent<RectTransform>();
        slider.handleRect = handle.GetComponent<RectTransform>();

        RectTransform sliderToFadeRectTransform = sliderToFade.GetComponent<RectTransform>();
        sliderToFadeRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        sliderToFadeRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        sliderToFadeRectTransform.sizeDelta = new Vector2(160, 20);

        RectTransform sliderBgRectTransform = sliderBg.GetComponent<RectTransform>();
        sliderBgRectTransform.anchorMin = new Vector2(0f, 0.25f);
        sliderBgRectTransform.anchorMax = new Vector2(1f, 0.75f);
        sliderBgRectTransform.offsetMin = Vector2.zero;
        sliderBgRectTransform.offsetMax = Vector2.zero;

        RectTransform fillAreaRectTransform = fillArea.GetComponent<RectTransform>();
        fillAreaRectTransform.anchorMin = new Vector2(0f, 0.25f);
        fillAreaRectTransform.anchorMax = new Vector2(1f, 0.75f);
        fillAreaRectTransform.offsetMin = new Vector2(5f, 0f);
        fillAreaRectTransform.offsetMax = new Vector2(-15f, 0f);

        RectTransform fillRectTransform = fill.GetComponent<RectTransform>();
        fillRectTransform.offsetMin = Vector2.zero;
        fillRectTransform.offsetMax = new Vector2(-10f, 0f);

        RectTransform handleSlideAreaRectTransform = handleSlideArea.GetComponent<RectTransform>();
        handleSlideAreaRectTransform.anchorMin = Vector2.zero;
        handleSlideAreaRectTransform.anchorMax = Vector2.one;
        handleSlideAreaRectTransform.offsetMin = new Vector2(10f, 0f);
        handleSlideAreaRectTransform.offsetMax = new Vector2(-10f, 0f);

        RectTransform handleRectTransform = handle.GetComponent<RectTransform>();
        handleRectTransform.offsetMin = Vector2.zero;
        handleRectTransform.offsetMax = new Vector2(20f, 0f);

        GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
    }

    void Update()
    {
        
    }
}
