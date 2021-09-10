using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetText : MonoBehaviour
{
    [SerializeField] TMP_Text textField;
    [SerializeField] Slider slider;

    void Update()
    {
        textField.text = Mathf.RoundToInt(slider.value).ToString();
    }
}
