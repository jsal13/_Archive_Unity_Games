using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class LevelManager
{
    private static readonly Lazy<LevelManager> lazy = new Lazy<LevelManager>(() => new LevelManager());
    public static LevelManager Instance { get { return lazy.Value; } }
    private LevelManager() { }

    private int _currentLevel = 1;
    public int CurrentLevel
    {
        get => _currentLevel;
        set
        {
            _currentLevel = value;
            if (_currentLevel > 1)
            {
                OnLevelChange?.Invoke(_currentLevel);
            }
        }
    }

    public delegate void LevelChange(int newVal);
    public static LevelChange OnLevelChange;
}