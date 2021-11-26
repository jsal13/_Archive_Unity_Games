using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class MovementProperties
{
    public float movementSpeed = 80f;
}

[Serializable]
public class JumpProperties
{
    public float forceJump = 9500.0f;
    public float minVelocityJump = 1.0f;
    public float maxNumJumps = 2;
}

public class Movement : MonoBehaviour
{
    [SerializeField] private MovementProperties moveProperty;
    [SerializeField] private JumpProperties jumpProperty;
    [SerializeField] private bool isOnGround;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] private Collider2D playerCollider;
    [HideInInspector] private Animator animator;
    [HideInInspector] private SpriteRenderer sprite;
    [SerializeField] private ContactFilter2D groundFilter;

    [HideInInspector] private SpriteRenderer swordSprite;
    [HideInInspector] private GameObject swordObj;
    [HideInInspector] private Vector3 swordInitPos = new Vector3(5, -2, 0);

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
        swordSprite = swordObj.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isOnGround = (playerCollider.IsTouching(groundFilter));
        animator.SetBool("IsJumping", !isOnGround);

        Direction = moveAction.ReadValue<float>();
        Walk();
    }

    private void Walk()
    {
        rb.velocity = new Vector2(Direction * moveProperty.movementSpeed, rb.velocity.y);
        animator.SetBool("IsWalking", Direction != 0);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        // Reset jump count.
        if (isOnGround)
        {
            currentNumJump = 0;
        }

        float value = obj.ReadValue<float>();
        if (value == 1 && currentNumJump < jumpProperty.maxNumJumps) // pressed;
        {
            if (isOnGround || currentNumJump < jumpProperty.maxNumJumps)
            {
                currentNumJump += 1;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpProperty.forceJump));
            }
        }
        else if (value == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Min(rb.velocity.y, jumpProperty.minVelocityJump));
        }
    }
}