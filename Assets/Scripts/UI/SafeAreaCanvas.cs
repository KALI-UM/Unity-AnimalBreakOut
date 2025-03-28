using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UIElements;


public enum AnchorPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottonCenter,
    BottomRight,
    BottomStretch,

    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,

    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,

    StretchAll,
    None
}

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class SafeAreaCanvas : MonoBehaviour
{
    [HideInInspector]
    public RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeAreaCanvasAnchor();
    }

    public void ApplySafeAreaCanvasAnchor()
    {
        var minAnchor = Screen.safeArea.position;
        var maxAnchor = Screen.safeArea.position + Screen.safeArea.size;

        minAnchor.x /= Screen.currentResolution.width;
        minAnchor.y /= Screen.currentResolution.height;

        maxAnchor.x /= Screen.currentResolution.width;
        maxAnchor.y /= Screen.currentResolution.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }

}
