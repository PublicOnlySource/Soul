using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SoundSlider : MonoBehaviour
{
    [SerializeField]
    private Enums.SoundType soundType;
    [SerializeField]
    private Slider slider;

    public Enums.SoundType SoundType { get => soundType; }
    public float Value { get => slider.value; }

    private void Awake()
    {
        slider.onValueChanged.AddListener(UpdateSlider);
    }

    public void UpdateSlider(float value)
    {
        SoundManager.Instance.SetSound(soundType, value); 
    }

    public void LoadData()
    {
        slider.value = PlayerPrefs.GetFloat(soundType.ToString(), 1f);
        UpdateSlider(slider.value);
    }

    public void SaveData()
    {
       PlayerPrefs.SetFloat(soundType.ToString(), slider.value);
    }
}
