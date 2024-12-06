using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextUpdater : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI textPro;

    void Start()
    {
        // get componenets
        slider = GetComponent<Slider>();
        textPro = GetComponentInChildren<TextMeshProUGUI>();

        UpdateText(slider.value);

        slider.onValueChanged.AddListener(UpdateText);
    }
    
    void UpdateText(float value)
    {
        textPro.text = transform.name.Replace("Slider", "") + ": " + value.ToString("0.0");
    }

    void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(UpdateText);
        }
    }
}