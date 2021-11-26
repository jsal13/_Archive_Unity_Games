using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(SpriteRenderer))]
public class Glows : MonoBehaviour, IReaction
{
    Light2D lightComp;

    public void DoEffect()
    {
        lightComp = gameObject.AddComponent<Light2D>();
        lightComp.lightType = Light2D.LightType.Point;
        lightComp.intensity = 0.9f;
        lightComp.pointLightOuterRadius = 4;
        lightComp.color = Color.yellow;
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void ReverseEffect()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(lightComp);
    }
}