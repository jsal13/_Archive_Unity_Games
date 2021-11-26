using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "BonfireInfo", menuName = "UsefulSleep/BonfireInfo", order = 0)]
public class BonfireInfo : ScriptableObject
{
    [Range(-20, 20)]
    public int temperature;
    [Range(1, 20)]
    public float duration;
    [Range(1, 20)]
    public float emberDuration;
}
