using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StatsManager
{
    private static readonly Lazy<StatsManager> lazy =
        new Lazy<StatsManager>(() => new StatsManager());
    public static StatsManager Instance { get { return lazy.Value; } }
    private StatsManager() { }

    public static Stat health = new Stat("health", Constants.initHealth);
}

public class Stat
{
    public string statName;
    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0, Constants.maxStatValue);
            OnStatChange?.Invoke(statName, _value);
        }
    }
    public static Action<string, int> OnStatChange;  // name, val.

    public Stat(string statName, int initValue)
    {
        this.statName = statName;
        this.Value = initValue;
    }
}