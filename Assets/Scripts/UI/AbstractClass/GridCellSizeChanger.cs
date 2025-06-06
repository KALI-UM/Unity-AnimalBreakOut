using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public abstract class GridCellSizeChanger<T> : MonoBehaviour
    where T : RectTransformDimensionsChangedInvoker<T>
{
    private GridLayoutGroup gridLayoutGroup;

    protected virtual void Start()
    {
        TryGetComponent(out gridLayoutGroup);
        
        SubscribeRectTransformDimensionsChanged(OnDimensionsChanged);
    }

    protected virtual void OnDestroy()
    {
        UnsubscribeRectTransformDimensionsChanged(OnDimensionsChanged);
    }

    protected virtual void OnDimensionsChanged(RectTransform rectTransform)
    {
        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.cellSize.x, rectTransform.rect.height);
    }
    
    protected void SubscribeRectTransformDimensionsChanged(Action<RectTransform> action)
    {
        RectTransformDimensionsChangedInvoker<T>.OnRectTransformDimensionsChanged += action;
    }

    protected void UnsubscribeRectTransformDimensionsChanged(Action<RectTransform> action)
    {
        RectTransformDimensionsChangedInvoker<T>.OnRectTransformDimensionsChanged -= action;
    }
}