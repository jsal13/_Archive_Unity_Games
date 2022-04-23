using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public sealed class GameManager
{
    private static readonly Lazy<GameManager> lazy = new Lazy<GameManager>(() => new GameManager());
    public static GameManager Instance { get { return lazy.Value; } }
    private GameManager() { }

    public static DialogueRunner dialogueRunner;

    // public static bool isPaused = false;

    // [Serializable]
    // public class Checkpoint
    // {
    //     public Vector3 position;
    //     public string sceneName;

    //     public Checkpoint(Vector3 position, string sceneName)
    //     {
    //         this.position = position;
    //         this.sceneName = sceneName;
    //     }
    // }

    // public static Checkpoint checkpoint = new Checkpoint(new Vector3(0, 0, 0), ""); // Initialize this to something.



}