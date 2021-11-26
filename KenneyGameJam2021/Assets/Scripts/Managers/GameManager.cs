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

    public static AudioSource audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

    public static Dictionary<string, int> layerDict = new Dictionary<string, int>() {
        {"Obstruction",LayerMask.GetMask("Obstruction")},
        {"Enemy" ,LayerMask.GetMask("Enemy")},
        {"Character", LayerMask.GetMask("Character")},
    };

    public static Vector3 currentSouthernDir;
    private static Vector3 _currentEasternDir = new Vector3(1, 0, 0);
    public static Vector3 CurrentEasternDir
    {
        get => _currentEasternDir;
        set
        {
            _currentEasternDir = VectorHelpers.Instance.VectorToIntValues(value);
            currentSouthernDir = VectorHelpers.Instance.VectorToIntValues(Quaternion.Euler(0, 0, -90) * _currentEasternDir);
        }
    }
}