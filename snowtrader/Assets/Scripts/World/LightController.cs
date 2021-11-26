using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private CircleCollider2D circCollider;
    private Light2D lightComp;

    private void Awake()
    {
        // Set the warmth radius to the initial outer radius of the light.
        circCollider = GetComponent<CircleCollider2D>();
        lightComp = GetComponent<Light2D>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetNewRadius), 0, 0.5f);
    }

    private void GetNewRadius()
    {
        circCollider.radius = lightComp.pointLightOuterRadius;
    }
}
