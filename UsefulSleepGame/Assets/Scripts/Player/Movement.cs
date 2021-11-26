using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Movement : MonoBehaviour
{
    public float movementSpeed = 80f;

    private float forceJump = 9500.0f;
    private float minVelocityJump = 1.0f;
    private float maxNumJumps = 2;

    [HideInInspector] public Rigidbody2D rb;
    private Collider2D playerCollider;
    private Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] private ContactFilter2D groundFilter;
    [SerializeField] private ContactFilter2D waterFilter;

    // HINDERANCES
    // Drag of 1 is normal.
    [SerializeField] private Vector2 currentDrag = Vector2.one;
    [SerializeField] private float waterHorizDrag = 0.35f;
    [SerializeField] private float waterVertDrag = 0.995f;

    // ATTACK
    [HideInInspector] private SpriteRenderer swordSprite;
    [HideInInspector] private GameObject swordObj;
    [HideInInspector] private Vector3 swordInitPos = new Vector3(5, -2, 0);
    [HideInInspector] private GameObject attackPoint;

    // KNOCKBACK
    [SerializeField] private float knockbackVelY = 32;
    [SerializeField] private float knockbackVelXForce = 12;
    [SerializeField] private float knockbackDuration = 0.35f;

    private float _direction = 0;
    public float Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            if (value != 0)
            {
                sprite.flipX = _direction == -1;
                swordSprite.flipX = _direction == -1;
                swordObj.transform.localPosition = new Vector3(_direction * swordInitPos.x, swordInitPos.y, swordInitPos.z);
                attackPoint.transform.localScale = new Vector3(_direction == -1 ? -1 : 1, 1, 1);
            }
        }
    }

    [SerializeField] private int currentNumJump = 0;

    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction jumpAction;


    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Awake()
    {
        jumpAction.performed += OnJump;

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        swordObj = transform.Find("SwordSlash").gameObject;
        attackPoint = transform.Find("AttackPoint").gameObject;
        swordSprite = swordObj.GetComponent<SpriteRenderer>();
    }

    // TODO: Changed this to fixed update.  Is there a diff?
    private void FixedUpdate()
    {
        PlayerManager.isOnGround = (playerCollider.IsTouching(groundFilter) || PlayerManager.isOnPlatform);
        PlayerManager.isInWater = (playerCollider.IsTouching(waterFilter));
        PlayerManager.canJump = PlayerManager.isOnGround || PlayerManager.isInWater || PlayerManager.isOnPlatform;

        CalculateHinderances();

        animator.SetBool("IsJumping", !PlayerManager.isOnGround);

        if (PlayerManager.canMove && !PlayerManager.IsHit)
        {
            Direction = moveAction.ReadValue<float>();
            Walk();
        }
        else if (!PlayerManager.IsHit)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void CalculateHinderances()
    {
        if (PlayerManager.isInWater)
        {
            currentDrag = new Vector2(waterHorizDrag, waterVertDrag);
        }
        else
        {
            currentDrag = Vector2.one;
        }
    }

    private void Walk()
    {
        rb.velocity = new Vector2(Direction * movementSpeed * currentDrag.x, rb.velocity.y * currentDrag.y);
        animator.SetBool("IsWalking", Direction != 0);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        // Reset jump count.
        if (PlayerManager.isOnGround)
        {
            currentNumJump = 0;
        }

        float value = obj.ReadValue<float>();
        if (PlayerManager.canMove && value == 1 && currentNumJump < maxNumJumps) // pressed;
        {
            if (PlayerManager.isOnGround || currentNumJump < maxNumJumps)
            {
                currentNumJump += 1;
                rb.velocity = new Vector2(rb.velocity.x * currentDrag.x, 0);
                rb.AddForce(new Vector2(0, forceJump * currentDrag.y));
            }
        }
        else if (value == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * currentDrag.x, Mathf.Min(rb.velocity.y * currentDrag.y, minVelocityJump));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Stop movement, get thrown backward.
            Vector3 hitVel = knockbackVelXForce * (this.transform.position - other.transform.position).normalized;
            hitVel.x *= knockbackVelXForce;
            hitVel.y *= knockbackVelY;
            // TODO: Should we have negative y vel?
            hitVel = new Vector3(Mathf.Clamp(hitVel.x, -130, 130), Mathf.Clamp(hitVel.y, -130, 130));

            PlayerManager.Temperature += other.gameObject.GetComponent<EnemyController>().GetTemperature();

            if (!PlayerManager.IsDead)
            {
                StartCoroutine(KnockbackPlayer(hitVel));
            }
        }
    }

    private IEnumerator KnockbackPlayer(Vector3 hitVel)
    {
        PlayerManager.IsHit = true;
        StartCoroutine(Combat.FlashRed(sprite, 0.4f));
        rb.AddForce(hitVel, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        PlayerManager.IsHit = false;
        yield return null;
    }
}