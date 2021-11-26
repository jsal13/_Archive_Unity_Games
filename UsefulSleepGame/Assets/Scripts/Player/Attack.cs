using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private float slashRate = 10f;
    private float slashSpeed = 0.005f;

    private SpriteRenderer swordSprite;
    private GameObject swordObj;
    private Animator swordAnimator;
    private AttackPointController attackPointController;
    [SerializeField] private List<Collider2D> attackRangeObjects;

    [SerializeField] private InputAction attackAction;

    private bool isSlashing;

    private void OnEnable()
    {
        attackAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
    }

    private void Awake()
    {
        attackAction.performed += OnAttack;

        swordSprite = transform.Find("SwordSlash").GetComponent<SpriteRenderer>();
        swordAnimator = transform.Find("SwordSlash").GetComponent<Animator>();
        attackPointController = transform.Find("AttackPoint").gameObject.GetComponent<AttackPointController>();
        // swordSprite.color = new Color(1, 1, 1, 0);  // Make sword invisible.
    }

    private void OnAttack(InputAction.CallbackContext _)
    {
        if (!isSlashing && PlayerManager.canMove && PlayerManager.canAttack)
        {
            StartCoroutine(AnimateSlash());
            StartCoroutine(ProcessAttack());
        }
    }

    IEnumerator ProcessAttack()
    {
        yield return new WaitForSeconds(slashSpeed);
        // Attack enemies, etc.
        foreach (GameObject other in attackPointController.objsInRange)
        {
            if (other.GetComponent<IAttackable>() != null)
            {
                other.GetComponent<IAttackable>().IsHit();
            }

            if (other.GetComponent<ISlashable>() != null)
            {
                other.GetComponent<ISlashable>().SlashedAction();
            }
        }
        yield return new WaitForSeconds(slashRate);
    }

    private IEnumerator AnimateSlash()
    {
        isSlashing = true;
        swordAnimator.SetTrigger("Slash");
        yield return new WaitForSeconds(0.1f);
        isSlashing = false;
        yield return null;
    }


    // IEnumerator AnimateSlash()
    // {
    //     isSlashing = true;
    //     float t = 0;
    //     while (t < slashSpeed)
    //     {
    //         t += Time.deltaTime;
    //         swordSprite.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), t / slashSpeed);
    //         yield return null;
    //     }

    //     t = 0;
    //     while (t * slashRate < 1)
    //     {
    //         t += Time.deltaTime;
    //         swordSprite.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), t * slashRate);
    //         yield return null;
    //     }

    //     isSlashing = false;
    // }
}
