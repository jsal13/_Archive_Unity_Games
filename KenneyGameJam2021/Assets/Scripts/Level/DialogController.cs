using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogController : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    public delegate void DialogOpen(bool value);
    public static DialogOpen OnDialogOpen;

    private void Awake()
    {
        dialogueRunner = GameObject.Find("Overlays/Dialogue System").GetComponent<DialogueRunner>();
    }

    public void InvokeDialogOpen(bool value)
    {
        OnDialogOpen?.Invoke(value);
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        dialogueRunner.StartDialogue("Death");
    }
}