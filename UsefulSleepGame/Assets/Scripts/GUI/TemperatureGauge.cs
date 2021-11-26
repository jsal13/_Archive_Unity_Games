using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureGauge : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void OnEnable()
    {
        PlayerManager.OnTemperatureChange += HandleTemperatureChange;
    }

    private void OnDisable()
    {
        PlayerManager.OnTemperatureChange -= HandleTemperatureChange;
    }

    private void HandleTemperatureChange(int newValue)
    {
        slider.value = newValue;
    }
}
