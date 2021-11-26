using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class EnemyController : MonoBehaviour, IAttackable, IHostile, ITemperature, IKillable
{
    [InlineEditor(Expanded = true)]
    [SerializeField] protected EnemyInfo info;

    public abstract void Death();
    public abstract int GetTemperature();
    public abstract void IsHit();
}