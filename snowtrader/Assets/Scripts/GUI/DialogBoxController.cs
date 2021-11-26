using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Dialog.OnPlayerSpeaking += HandlePlayerSpeaking;
    }

    private void OnDisable()
    {
        Dialog.OnPlayerSpeaking -= HandlePlayerSpeaking;
    }

    private void HandlePlayerSpeaking(bool value)
    {
        animator.SetBool("isSpeaking", value);
    }
}
