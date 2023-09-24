using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager
{
    private static readonly Lazy<GameManager> lazy =
        new Lazy<GameManager>(() => new GameManager());
    public static GameManager Instance { get { return lazy.Value; } }
    private GameManager() { }

}