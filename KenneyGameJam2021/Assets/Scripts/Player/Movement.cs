using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField] private AudioClip footStepSound;



    private int walkDistance = 8;  // 16 pixels.
    [SerializeField] float speed = 3f;
    private bool isWalking = false;

    Vector2 direction;
    private bool isObstructed;
    private int obstructionBitMask;

    private void Awake()
    {
        obstructionBitMask = GameManager.layerDict["Obstruction"] | GameManager.layerDict["Enemy"];
        isWalking = false;
    }

    private void OnEnable()
    {
        VectorHelpers.OnRotating += HandleRotating;
        LevelManager.OnLevelChange += HandleOnLevelChange;
        LevelController.OnLevelChangeComplete += HandleLevelChangeComplete;
        DialogController.OnDialogOpen += HandleDialogOpen;
    }

    private void OnDisable()
    {
        VectorHelpers.OnRotating -= HandleRotating;
        LevelManager.OnLevelChange -= HandleOnLevelChange;
        LevelController.OnLevelChangeComplete -= HandleLevelChangeComplete;
        DialogController.OnDialogOpen -= HandleDialogOpen;
    }

    private void HandleDialogOpen(bool value)
    {
        PlayerManager.Instance.CanMove = !value;
    }

    private void HandleLevelChangeComplete()
    {
        PlayerManager.Instance.CanMove = true;
    }

    private void HandleOnLevelChange(int newVal)
    {
        PlayerManager.Instance.CanMove = false;
    }

    private void Update()
    {

        if (PlayerManager.Instance.CanMove && !isWalking && CanMoveDown())
        {
            StartCoroutine(Move(GameManager.currentSouthernDir));
        }

        if (!isWalking && PlayerManager.Instance.CanMove)
        {

            direction = GameManager.gamepad.leftStick.ReadValue();
            if (Mathf.Abs(direction.x) > 0.5)
            {
                StartCoroutine(Move(direction.x * GameManager.CurrentEasternDir));
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
            // GameManager.audioSource.PlayOneShot(footStepSound);
            while (Vector2.Distance(newPos, transform.position) != 0)
            {
                t += Time.deltaTime;
                transform.position = Vector2.Lerp(oldPos, newPos, Mathf.Clamp(t * speed, 0, 1));
                yield return null;
            }
        }

        isWalking = false;
        yield return null;
    }

    private bool CanMoveDown()
    {
        return !IsPlayerObstructed(GameManager.currentSouthernDir);
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

    private void HandleRotating(bool value, float degrees)
    {
        if (value)
        {
            // Rotating room...
            PlayerManager.Instance.CanMove = false;
            GameManager.CurrentEasternDir = Quaternion.Euler(0, 0, degrees) * GameManager.CurrentEasternDir;

        }
        else
        {
            // Finished rotating room...
            PlayerManager.Instance.CanMove = true;
        }
    }
}
