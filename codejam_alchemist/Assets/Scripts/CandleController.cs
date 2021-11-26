using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CandleController : MonoBehaviour
{
    private Light2D lightComp;
    private float intensityInit;
    private float outerRadiusInit;
    private float startingTime;
    private float intensityOffset;
    private float outerRadiusOffset;

    private void Awake()
    {
        lightComp = GetComponent<Light2D>();
        intensityInit = lightComp.intensity;
        outerRadiusInit = lightComp.pointLightOuterRadius;
    }

    private void Start()
    {
        RandomizeLightParams();
    }

    private void Update()
    {
        FlickerLight();
    }

    private void FlickerLight()
    {
        lightComp.intensity = intensityInit + intensityOffset * Mathf.PingPong(Time.time, 1);
        lightComp.pointLightOuterRadius = outerRadiusInit + outerRadiusOffset * Mathf.PingPong(Time.time, 1);

        if (Time.time - startingTime > 2)
        {
            RandomizeLightParams();
        }
    }

    private void RandomizeLightParams()
    {
        startingTime = Time.time;
        intensityOffset = ((2 * Random.value) - 1) / 8;
        outerRadiusOffset = ((2 * Random.value) - 1) / 4;
    }
}
