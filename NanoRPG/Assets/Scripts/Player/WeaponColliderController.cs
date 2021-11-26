using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderController : MonoBehaviour
{
    public enum AttackType { Sword, Magic };
    [SerializeField] private AttackType attackType;
    [SerializeField] private BoxCollider2D col;
    private int power = 0;

    // public delegate void EnemyHit(GameObject enemy);
    // public static EnemyHit OnEnemyHit;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();

        switch (attackType)
        {
            case AttackType.Sword:
                power = PlayerManager.Instance.AttackPower;
                break;

            case AttackType.Magic:
                power = PlayerManager.Instance.MagicPower;
                break;

            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IAttackable>()?.decreaseHealth(power);
    }
}
