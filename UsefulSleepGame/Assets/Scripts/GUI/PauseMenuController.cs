using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private InputAction pauseAction;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private int currentMenuIDX;
    [SerializeField] private List<string> menuOptions;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        menuOptions = new List<string>() {
            "resume", "save", "load", "quit"
        };
    }

    private void Start()
    {
        canvas.worldCamera = mainCamera;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += HandlePauseAction;
    }

    private void OnDisable()
    {
        pauseAction.Disable();
        pauseAction.performed -= HandlePauseAction;
    }

    private IEnumerator WaitForPlayerSelection()
    {
        // TODO: figure out new input system's "down and up" thing.
        yield return null;
    }


    private void HandlePauseAction(InputAction.CallbackContext obj)
    {
        if (GameManager.isPaused)
        {
            canvas.enabled = false;
            GameManager.isPaused = false;
            Time.timeScale = 1;
            StartCoroutine(WaitForPlayerSelection());
        }
        else
        {
            canvas.enabled = true;
            GameManager.isPaused = true;
            Time.timeScale = 0;
            StopAllCoroutines();
        }
    }
}
