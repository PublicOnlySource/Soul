using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitString : MonoBehaviour
{
    [SerializeField]
    private int stringIndex;

    private TMP_Text text;

    private void Start()
    {
        if (text == null)
        {
            text = gameObject.GetComponent<TMP_Text>();
        }

        text.text = DataManager.Instance.GetString(stringIndex);
    }
}
