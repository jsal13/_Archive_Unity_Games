using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ShowOdinSerializedPropertiesInInspector]
[CreateAssetMenu(fileName = "EnemyInfo", menuName = "UsefulSleep/EnemyInfo", order = 0)]
public class EnemyInfo : ScriptableObject
{
    public int health;
    public int temperature;
    public float knockbackVelY = 32;
    public float knockbackVelXForce = 12;
    public float knockbackDuration = 0.35f;
}