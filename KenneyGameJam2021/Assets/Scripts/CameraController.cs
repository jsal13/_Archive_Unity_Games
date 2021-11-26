using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using System;

public class CameraController : MonoBehaviour
{
    public InputAction rotateActionClockwise;
    public InputAction rotateActionCounterClockwise;

    private bool canRotate = true;
    private bool _currentlyRotating = false;
    public bool CurrentlyRotating
    {
        get => _currentlyRotating;
        set
        {
            _currentlyRotating = value;
        }
    }

    public delegate void Rotating(bool value);
    public static Rotating OnRotating;

    private void OnEnable()
    {
        rotateActionClockwise.Enable();
        rotateActionCounterClockwise.Enable();
        VectorHelpers.OnRotating += HandleRotating;

        rotateActionCounterClockwise.performed += HandleRotateActionCounterClockwise;
        rotateActionClockwise.performed += HandleRotateActionClockwise;
        DialogController.OnDialogOpen += HandleOnDialogOpen;
        PlayerManager.OnPlayerDeath += HandlePlayerDeath;

    }

    private void OnDisable()
    {
        rotateActionClockwise.Disable();
        rotateActionCounterClockwise.Disable();
        VectorHelpers.OnRotating -= HandleRotating;

        rotateActionCounterClockwise.performed -= HandleRotateActionCounterClockwise;
        rotateActionClockwise.performed -= HandleRotateActionClockwise;
        DialogController.OnDialogOpen -= HandleOnDialogOpen;
        PlayerManager.OnPlayerDeath -= HandlePlayerDeath;

    }

    private void HandlePlayerDeath()
    {
        canRotate = false;
    }

    private void HandleOnDialogOpen(bool value)
    {
        canRotate = !value;
    }

    private void HandleRotateActionClockwise(InputAction.CallbackContext obj)
    {
        if (!CurrentlyRotating && canRotate)
        {
            StartCoroutine(VectorHelpers.Instance.RotateDegrees(transform, -90, 1.5f));
        }
    }

    private void HandleRotateActionCounterClockwise(InputAction.CallbackContext obj)
    {
        if (!CurrentlyRotating && canRotate)
        {
            StartCoroutine(VectorHelpers.Instance.RotateDegrees(transform, 90, 1.5f));
        }
    }

    private void HandleRotating(bool value, float _)
    {
        CurrentlyRotating = value;
    }

}