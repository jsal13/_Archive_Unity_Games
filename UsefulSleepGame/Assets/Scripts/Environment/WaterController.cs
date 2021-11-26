using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour, ITemperature
{
    [Range(-20, 20)]
    [SerializeField] private int temperature = -10;

    public int GetTemperature()
    {
        return temperature;
    }
}
