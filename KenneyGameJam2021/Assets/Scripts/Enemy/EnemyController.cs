using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyInfo info;
    public string enemyType;
    public int hp;
    public int attack;
    public int gold;

    virtual protected void Start()
    {
        enemyType = info.enemyType;
        hp = info.hp;
        attack = info.attack;
        gold = info.gold;
    }
}
