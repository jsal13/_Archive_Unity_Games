using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PointLight : MonoBehaviour
{
    private Light2D playerLight;
    private float initialIntensity;
    private float initialRadius;

    private void Awake()
    {
        playerLight = GameObject.Find(HierarchyAddrs.playerPointLight).GetComponent<Light2D>();
        initialIntensity = playerLight.intensity;
        initialRadius = playerLight.pointLightOuterRadius;
    }

    private void OnEnable()
    {
        Temperature.OnPlayerTemperatureChange += HandlePlayerTemperatureChange;
    }

    private void OnDisable()
    {
        Temperature.OnPlayerTemperatureChange -= HandlePlayerTemperatureChange;
    }

    private void HandlePlayerTemperatureChange(float newTemp)
    {
        if (newTemp >= 50)
        {
            playerLight.intensity = initialIntensity;
            playerLight.pointLightOuterRadius = initialRadius;
        }
        else if (newTemp < 50 && newTemp > 5)
        {
            var tempPercent = 1 - (newTemp / 50f);
            playerLight.intensity = Mathf.Lerp(initialIntensity, 0.1f, tempPercent);
            playerLight.pointLightOuterRadius = Mathf.Lerp(initialRadius, 1f, tempPercent);
        }
        else if (newTemp <= 5)
        {
            var tempPercent = 1f - 0.1f * (newTemp / 5f);
            playerLight.intensity = Mathf.Lerp(0.1f, 0.0f, tempPercent);
            playerLight.pointLightOuterRadius = Mathf.Lerp(1f, 0f, tempPercent);
        }
    }
}
