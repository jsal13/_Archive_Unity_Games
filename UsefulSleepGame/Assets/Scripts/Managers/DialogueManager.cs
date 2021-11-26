using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Yarn.Unity;

public sealed class DialogueManager
{
    private static readonly Lazy<DialogueManager> lazy = new Lazy<DialogueManager>(() => new DialogueManager());
    public static DialogueManager Instance { get { return lazy.Value; } }
    private DialogueManager() { }

    public static Sprite GetCharacterPortrait(string characterName)
    {
        return Resources.Load<Sprite>($"Images/Portraits/{characterName}");
    }
}
