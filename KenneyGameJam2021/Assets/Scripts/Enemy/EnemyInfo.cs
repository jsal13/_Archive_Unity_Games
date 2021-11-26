using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "KenneyJam2021/EnemyInfo", order = 0)]
public class EnemyInfo : ScriptableObject
{
    public string enemyType;
    public int hp;
    public int attack;
    public int gold;
}
