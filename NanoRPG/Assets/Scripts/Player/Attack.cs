using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private enum AttackType { Sword, Magic }

    private GameObject swordObj;
    private SpriteRenderer swordSpriteRenderer;
    private BoxCollider2D swordCollider;
    [SerializeField] private int swordOffset = 8;
    [SerializeField] private float swordSpeed = 48f;

    private GameObject magicObj;
    private SpriteRenderer magicSpriteRenderer;
    private BoxCollider2D magicCollider;
    private int magicOffset = 32;
    [SerializeField] private float magicSpeed = 36f;

    private bool isAttacking;

    private Color transparent = new Color(1, 1, 1, 0);
    private Color opaque = new Color(1, 1, 1, 1);
    private bool isTeleporting;

    private void Awake()
    {
        swordObj = transform.Find("Sword").gameObject;
        swordSpriteRenderer = swordObj.GetComponent<SpriteRenderer>();
        swordCollider = swordObj.GetComponent<BoxCollider2D>();

        magicObj = transform.Find("Magic").gameObject;
        magicSpriteRenderer = magicObj.GetComponent<SpriteRenderer>();
        magicCollider = magicObj.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        isAttacking = false;

        // Turn off colliders for weapons.
        swordCollider.enabled = false;
        magicCollider.enabled = false;
    }

    private void OnEnable()
    {
        Dialog.OnNextToNPC += HandleNextToNPC;
        TeleportController.OnTeleportingPlayer += HandleTeleportingPlayer;
    }

    private void OnDisable()
    {
        Dialog.OnNextToNPC -= HandleNextToNPC;
        TeleportController.OnTeleportingPlayer -= HandleTeleportingPlayer;
    }

    private void HandleTeleportingPlayer(bool value)
    {
        isTeleporting = value;
    }

    private void HandleNextToNPC(bool value)
    {
        PlayerManager.Instance.CanAttack = !value;
    }

    void Update()
    {
        if (GameManager.gamepad.buttonWest.wasPressedThisFrame && !isAttacking && PlayerManager.Instance.CanAttack)
        {
            StartCoroutine(PerformAttack(AttackType.Sword));
        }
        else if (GameManager.gamepad.buttonNorth.wasPressedThisFrame && !isAttacking && PlayerManager.Instance.CanAttack)
        {
            if (PlayerManager.Instance.Mana > 0)
            {
                StartCoroutine(PerformAttack(AttackType.Magic));
                PlayerManager.Instance.Mana -= 1;
            }
        }
    }

    private IEnumerator PerformAttack(AttackType attackType)
    {
        isAttacking = true;

        yield return null;

        GameObject attackObj = null;
        SpriteRenderer spriteRendererObj = null;
        int attackOffset = 0;
        BoxCollider2D attackCollider = null;
        float speed = 1;

        switch (attackType)
        {
            case AttackType.Sword:
                attackObj = swordObj;
                spriteRendererObj = swordSpriteRenderer;
                attackOffset = swordOffset;
                attackCollider = swordCollider;
                speed = swordSpeed;
                break;

            case AttackType.Magic:
                attackObj = magicObj;
                spriteRendererObj = magicSpriteRenderer;
                attackOffset = magicOffset;
                attackCollider = magicCollider;
                speed = magicSpeed;
                break;

            default:
                break;
        }

        spriteRendererObj.color = opaque;
        attackCollider.enabled = true;

        float t = 0;
        int discreteT = 0;
        while (t < 13)
        {

            if (isTeleporting)
            {
                isAttacking = false;
                spriteRendererObj.color = transparent;
                yield break;
            }

            t += Time.deltaTime * speed;
            if (t > discreteT)
            {
                discreteT = Mathf.CeilToInt(t);


                Quaternion rotOrbit = Quaternion.Euler(0, 0, (30 * discreteT) % 360);

                Vector3 orbit = rotOrbit * (new Vector3(1, 1, 0)).normalized * attackOffset;

                attackObj.transform.position = transform.position + orbit;
                attackObj.transform.localRotation = rotOrbit;
            }
            yield return null;
        }

        spriteRendererObj.color = transparent;
        attackCollider.enabled = false;
        isAttacking = false;
    }

}
