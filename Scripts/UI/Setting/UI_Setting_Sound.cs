using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Setting_Sound : MonoBehaviour
{
    [SerializeField]
    private List<UI_SoundSlider> sliders;

    private void Load()
    {
        for (int i = 0; i < sliders.Count; i++)
        {
            sliders[i].LoadData();
        }
    }

    public void Init()
    {
        Load();
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < sliders.Count; i++)
        {
            sliders[i].SaveData();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


}
