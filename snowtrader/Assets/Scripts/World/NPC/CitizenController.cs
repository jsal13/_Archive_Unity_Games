using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool isCurrentlyWalkCycled = false;
    private float initPause;
    private float walkingDuration;
    private readonly float walkSpeed = 0.5f;
    private float direction;
    private Vector3 startingPos;
    private Vector3 finalPos;
    private float elapsedTime;
    private bool canMove;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canMove = true;
    }

    private void Start()
    {
        initPause = 5 * Random.value;
        walkingDuration = 1f;
        direction = 1;
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        Dialog.OnPlayerSpeaking += HandlePlayerSpeaking;
        Dialog.OnPlayerTrading += HandlePlayerTrading;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
    }
    
    private void OnDisable()
    {
        Dialog.OnPlayerSpeaking -= HandlePlayerSpeaking;
        Dialog.OnPlayerTrading -= HandlePlayerTrading;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
    }

    private void HandleTradeMenuEnd()
    {
        canMove = true;
    }

    private void HandlePlayerTrading(NPCController.NPC _)
    {
        canMove = false;
    }

    private void HandlePlayerSpeaking(bool value)
    {
        canMove = !value;
    }

    private void Move()
    {
        if (!isCurrentlyWalkCycled && canMove)
        {
            StartCoroutine(nameof(WalkCycle));
        }
    }

    IEnumerator WalkCycle()
    {
        isCurrentlyWalkCycled = true;

        yield return new WaitForSeconds(initPause);

        startingPos = transform.position;
        finalPos = transform.position += direction * (new Vector3(1, 0) * walkSpeed * walkingDuration);
        elapsedTime = 0;

        // Change directions
        direction = -direction;
        spriteRenderer.flipX = direction == 1;

        while (elapsedTime < walkingDuration)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / walkingDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isCurrentlyWalkCycled = false;
    }
}
