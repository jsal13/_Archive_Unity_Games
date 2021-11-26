using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBarController : MonoBehaviour
{
    private Slider playerTemperatureSlider;
    private Image fillImage;

    private Color startingColor = new Color(0, 159, 255);
    private Color endingColor = new Color(236, 47, 75);

    private float startingH = 0;
    private float startingS = 0;
    private float startingV = 0;
    private float endingH = 0;
    private float endingS = 0;
    private float endingV = 0;
    private float interpH;
    private float interpS;
    private float interpV;

    private void Awake()
    {
        fillImage = transform.Find("Fill Area/Fill").GetComponent<Image>();
        playerTemperatureSlider = GetComponent<Slider>();
        Color.RGBToHSV(startingColor, out startingH, out startingS, out startingV);
        Color.RGBToHSV(endingColor, out endingH, out endingS, out endingV);
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
        playerTemperatureSlider.value = newTemp;
        ChangeFillColor(newTemp / 100f);
    }

    private void ChangeFillColor(float t)
    {
        interpH = t * startingH + (1 - t) * endingH;
        interpS = t * startingS + (1 - t) * endingS;
        interpV = t * startingV + (1 - t) * endingV;

        Color rgb = Color.HSVToRGB(interpH, interpS, interpV);
        rgb.r /= 255;
        rgb.g /= 255;
        rgb.b /= 255;

        fillImage.color = rgb;
    }

}
