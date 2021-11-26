using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "ScriptableObjects/EnemyInfo", order = 1)]
public class EnemyInfo : ScriptableObject
{
    public string enemyName;
    public int health;
    public int attack;
    public int defense;
    public float movementSpeed;
    public float movementPause;
    public int goldValue;
}
