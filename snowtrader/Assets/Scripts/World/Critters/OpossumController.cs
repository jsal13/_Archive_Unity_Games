using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool isCurrentlyWalkCycled = false;
    private float initPause;
    private float walkingDuration;
    private readonly float walkSpeed = 1.0f;
    private float direction;
    private Vector3 startingPos;
    private Vector3 finalPos;
    private float elapsedTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        initPause = 5 * Random.value;
        walkingDuration = Random.Range(1, 3);
        direction = 1;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!isCurrentlyWalkCycled) StartCoroutine(nameof(WalkCycle));
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
