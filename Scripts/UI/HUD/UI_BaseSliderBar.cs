using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UI_BaseSliderBar : MonoBehaviour
{
    [Header("Bar Option")]
    [SerializeField]
    protected bool changeWidthWithMaxValue = false;
    [SerializeField]
    protected float widthMultiplier = 2f;

    protected Slider slider;
    protected RectTransform rectTransform;

    public virtual void Init()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    protected void UpdateSliderBar(float value)
    {
        slider.value = value;
    }

    protected void UpdateSliderBarMax(float value)
    {
        slider.maxValue = value;

        if (changeWidthWithMaxValue)
        {
            float originWidth = rectTransform.sizeDelta.x;
            float newWidth = value * widthMultiplier;

            rectTransform.anchoredPosition += new Vector2((newWidth - originWidth) * 0.5f, 0);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        }
    }

    protected void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
