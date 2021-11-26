using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : EnemyController
{
    private int obstructionBitMask;
    private bool isWalking;
    private int walkDistance = 8;
    private float speed = 2;

    protected void Awake()
    {
        obstructionBitMask = GameManager.layerDict["Obstruction"] | GameManager.layerDict["Enemy"];
        isWalking = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        if (!isWalking)
        {
            StartCoroutine(Move((2 * Random.Range(0, 2) - 1) * GameManager.CurrentEasternDir));

        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        isWalking = true;
        float t = 0;
        Vector2 oldPos = transform.position;
        Vector2 newPos;

        newPos = oldPos + direction * walkDistance;

        if (!IsObstructed(direction))
        {
            while (Vector2.Distance(newPos, transform.position) != 0)
            {
                t += Time.deltaTime;
                transform.position = Vector2.Lerp(oldPos, newPos, Mathf.Clamp(t * speed, 0, 1));
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);
        isWalking = false;

    }

    private bool IsObstructed(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, walkDistance, obstructionBitMask);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

}
