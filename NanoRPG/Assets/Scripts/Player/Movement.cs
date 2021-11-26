using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using System;

public class Movement : MonoBehaviour
{
    private int walkDistance = 16;  // 16 pixels.
    [SerializeField] float speed = 3f;
    private bool isWalking = false;
    private bool isTeleporting = false;
    Vector2 direction;
    private bool isObstructed;
    private int obstructionBitMask;

    private void Awake()
    {
        obstructionBitMask = GameManager.layerDict["Obstruction"] | GameManager.layerDict["Enemy"] | GameManager.layerDict["Character"];
        isWalking = false;
    }

    private void OnEnable()
    {
        TeleportController.OnTeleportingPlayer += HandleTeleportingPlayer;
    }

    private void OnDisable()
    {
        TeleportController.OnTeleportingPlayer -= HandleTeleportingPlayer;
    }

    private void HandleTeleportingPlayer(bool value)
    {
        isTeleporting = value;
    }

    private void Update()
    {
        if (!isWalking && PlayerManager.Instance.CanMove)
        {

            direction = GameManager.gamepad.leftStick.ReadValue();
            if (Mathf.Abs(direction.x) > 0.5 || Mathf.Abs(direction.y) > 0.5)
            {
                StartCoroutine(Move(direction));
            }
        }
    }
    private IEnumerator Move(Vector2 direction)
    {
        isWalking = true;
        float t = 0;
        Vector2 oldPos = transform.position;
        Vector2 newPos;

        // X has priority; make the base vector in the dir.
        Vector2 newDir = Mathf.Abs(direction.x) > 0.5 ? new Vector2(Mathf.Sign(direction.x) * walkDistance, 0) : new Vector2(0, Mathf.Sign(direction.y) * walkDistance);

        newPos = oldPos + newDir;

        if (!IsPlayerObstructed(newDir))
        {
            while (Vector2.Distance(newPos, transform.position) != 0)
            {
                if (isTeleporting)
                {
                    isWalking = false;
                    yield break;
                }
                t += Time.deltaTime;
                transform.position = Vector2.Lerp(oldPos, newPos, Mathf.Clamp(t * speed, 0, 1));
                yield return null;
            }
        }

        isWalking = false;
        yield return null;
    }

    private bool IsPlayerObstructed(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, walkDistance, obstructionBitMask);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
