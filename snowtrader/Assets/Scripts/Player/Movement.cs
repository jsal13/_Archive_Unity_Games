using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("General Player Structures")]
    private PlayerManager player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D playerCollider;
    private Animator animator;

    [Header("Horizontal Movement")]
    private float _direction = 0;
    public float Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            if (value != 0)
            {
                // Flip the sprite the appropriate way.
                spriteRenderer.flipX = _direction == -1;
            }
        }
    }

    private readonly float walkSpeed = 5.0f;
    private readonly float rayCastEpsilon = 0.05f;

    [Header("Vertical Movement")]
    private float forceJump = 750f;
    private readonly float minVelocityJump = 1.0f;
    private readonly float maxVelocityFall = 14.0f;
    private int countJump = 0;
    private int _maxCountJump;
    private int MaxCountJump
    {
        get => _maxCountJump;
        set
        {
            _maxCountJump = value;
            PersistenceManager.Instance.maxCountJump = _maxCountJump;
        }
    }
    private bool _isSlashingSword;
    private bool IsSlashingSword
    {
        get => _isSlashingSword;
        set
        {
            _isSlashingSword = value;
            animator.SetBool("isSlashingSword", _isSlashingSword);
        }
    }
    private GameObject currentSlashable;

    public float valy;
    public float valx;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        MaxCountJump = PersistenceManager.Instance.maxCountJump;
    }

    private void Update()
    {
        if (player.CanMove)
        {
            Walk();
            Jump();
            if (PlayerSlashedSword())
            {
                StartCoroutine(SlashSword());
            }
        }

        if (PlayerSlashedSword() && currentSlashable != null)
        {
            // Destroy slashable.
            currentSlashable.GetComponent<SlashableController>().PlaySlashAnimationAndDestroy();
        }

        //> Animation Control
        animator.SetBool("isWalking", Direction != 0);
        animator.SetBool("isJumping", !player.IsOnGround);
    }

    private void FixedUpdate()
    {
        LimitFallVelocity();
    }

    private void LimitFallVelocity()
    {
        // Failsafe for falling; limit max fall velcity.
        if (rb.velocity.y < -Mathf.Abs(maxVelocityFall))
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                Mathf.Clamp(rb.velocity.y, -Mathf.Abs(maxVelocityFall), Mathf.Infinity)
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("slashable"))
        {
            currentSlashable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("slashable"))
        {
            currentSlashable = null;
        }
    }

    private void Walk()
    {
        if (player.CanWalk)
        {
            if (!IsObstructed()) { 
                rb.velocity = new Vector2(Direction * walkSpeed, rb.velocity.y); 
            }
        }
        else { rb.velocity = new Vector2(0, Mathf.Min(0, rb.velocity.y)); }
    }

    private void OnMove(InputValue value)
    {
        Direction = value.Get<float>();
    }

    // > SLASH SWORD
    private bool PlayerSlashedSword() => InputManager.gamepad.buttonWest.wasPressedThisFrame;

    IEnumerator SlashSword()
    {
        IsSlashingSword = true;
        yield return new WaitForSeconds(0.05f);
        IsSlashingSword = false;
        
    }

    // > JUMP
    private bool PlayerPressedJump() => InputManager.gamepad.buttonSouth.wasPressedThisFrame;
    private bool PlayerReleasedJump() => InputManager.gamepad.buttonSouth.wasReleasedThisFrame;

    private void Jump() {

        if (PlayerPressedJump() && player.CanMove) // Normal Jump (at full speed)
        {
            if (player.IsOnGround || countJump < MaxCountJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, forceJump));
            }

            if (player.IsOnGround) countJump = 1;
            else if (countJump < MaxCountJump) { countJump += 1; }
        }

        if (PlayerReleasedJump()) // Shortened jump (small taps)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Min(rb.velocity.y, minVelocityJump));
        }
    }

    private bool IsObstructed()
    {
        List<int> locs = new List<int> { -1, 1 }; // top and bottom checking.

        foreach (int loc in locs)
        {
            Vector3 rcOrigin = transform.position + new Vector3(0, loc * ((playerCollider.size.y / 2) - rayCastEpsilon), 0);
            Vector2 rcDir = Mathf.Sign(Direction) * Vector2.right;
            float rcDist = (playerCollider.size.x + 4 * rayCastEpsilon) / 2;

            RaycastHit2D hit = Physics2D.Raycast(rcOrigin, rcDir, rcDist, PersistenceManager.GetPlayerToGroundObstructionLayer());
            if (hit.collider != null) { return true; }
        }
        return false;
    }

    private void OnEnable()
    {
        Upgrades.OnGotJumpBoots += HandleGotJumpBoots;
    }

    private void OnDisable()
    {
        Upgrades.OnGotJumpBoots -= HandleGotJumpBoots;
    }

    private void HandleGotJumpBoots()
    {
        MaxCountJump += 1;
    }
}
