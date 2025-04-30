using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[ExecuteAlways]
public class GageBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField]
    private Image barImage;

    [SerializeField]
    private TextMeshProUGUI gageText;

    private Action<int> onGageChange;

    [Serializable]
    public class ColorValue
    {
        public float value;
        public Color color;
    }

    [SerializeField]
    private List<ColorValue> colorList;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void OnValueChange(float value)
    {
        //  SetColor();
    }

    public void SetMaxValue(int max)
    {
        slider.maxValue = max;
    }

    public void SetValue(int value)
    {
        slider.value = (float)value;
        gageText.text = ((int)slider.value).ToString();
    }

    public void SetText(string text)
    {
        gageText.text = text;
    }

    private void SetColor()
    {
        foreach (var colorValue in colorList)
        {
            if (slider.value >= colorValue.value)
            {
                barImage.color = colorValue.color;
                break;
            }
        }
    }

}
