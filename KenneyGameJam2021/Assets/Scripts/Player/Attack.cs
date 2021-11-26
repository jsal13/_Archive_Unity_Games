using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip goldSound;
    public InputAction actionAttack;
    [SerializeField] private List<GameObject> enemyList;
    private bool isAttacking;
    [SerializeField] private float attackPauseDuration = 0.5f;

    private void Awake()
    {
        enemyList = new List<GameObject>();
    }

    private void OnEnable()
    {
        actionAttack.Enable();
        actionAttack.performed += HandleActionAttack;
    }

    private void OnDisable()
    {
        actionAttack.Disable();
        actionAttack.performed -= HandleActionAttack;
    }

    private void HandleActionAttack(InputAction.CallbackContext obj)
    {
        if (PlayerManager.Instance.CanAttack && !isAttacking && enemyList.Count > 0)
        {
            StartCoroutine(AttackEnemy(enemyList[0]));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // TODO: Maybe make "IAttackable"?
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // TODO: Maybe make "IAttackable"?
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            enemyList.Remove(other.gameObject);
        }
    }

    private IEnumerator AttackEnemy(GameObject enemy)
    {
        isAttacking = true;
        EnemyController enemyCont = enemy.GetComponent<EnemyController>();

        // Player attacks first.
        enemyCont.hp -= PlayerManager.Instance.AttackPower;
        // GameManager.audioSource.PlayOneShot(attackSound);
        StartCoroutine(CombatFunctions.Instance.FlashHurt(enemy));

        // Brief wait between attacks.
        yield return new WaitForSeconds(attackPauseDuration);

        if (enemyCont.hp > 0)
        {
            // Enemy attacks second.
            PlayerManager.Instance.Health -= enemyCont.attack;
            // GameManager.audioSource.PlayOneShot(attackSound);
            StartCoroutine(CombatFunctions.Instance.FlashHurt(gameObject));
        }

        yield return null;
        if (PlayerManager.Instance.Health <= 0)
        {
            StopAllCoroutines();
        }

        yield return null;
        if (enemyCont.hp <= 0)
        {
            // Enemy is dead.
            PlayerManager.Instance.Gold += enemyCont.gold;
            // GameManager.audioSource.PlayOneShot(goldSound);
            Destroy(enemy.gameObject);
        }
        yield return null;
        isAttacking = false;
    }
}
