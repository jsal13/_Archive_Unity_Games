using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class UtilsForYarn : MonoBehaviour
{

    public float GetRandomValue()
    {
        return Random.value;
    }

    void Start()
    {
        GetComponent<DialogueRunner>().AddFunction("random", () => { return GetRandomValue(); });
    }
}
