using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class GameManager
{
    private static readonly Lazy<GameManager> lazy = new Lazy<GameManager>(() => new GameManager());
    public static GameManager Instance { get { return lazy.Value; } }
    private GameManager() { }

    public static Gamepad gamepad = Gamepad.current;
    public static Dictionary<string, int> layerDict = new Dictionary<string, int>() {
        {"Obstruction",LayerMask.GetMask("Obstruction")},
        {"Enemy" ,LayerMask.GetMask("Enemy")},
        {"Character", LayerMask.GetMask("Character")},
    };
}
