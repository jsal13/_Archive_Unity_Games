using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Sparkles : MonoBehaviour, IReaction
{
    GameObject particleObj;
    GameObject sparkles;

    private void Awake()
    {
        particleObj = Resources.Load<GameObject>("Prefabs/Sparkles");
    }

    public void DoEffect()
    {
        sparkles = Instantiate(particleObj, transform, false);
    }

    public void ReverseEffect()
    {
        Destroy(sparkles);
    }
}