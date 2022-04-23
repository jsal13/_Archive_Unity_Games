using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Statistics
// List + Getter/Setter of Statistic objects.
{
    public List<Statistic> statistics;

    public Statistics(List<Statistic> statistics)
    {
        this.statistics = statistics;
    }

    public Statistic GetStatistic(Stat statName) => this.statistics.FirstOrDefault(x => x.statName == statName);

    public void SetStatistic(Stat statName, int val)
    {
        this.statistics.FirstOrDefault(x => x.statName == statName).Value = val;
    }

    public void AddToStatistic(Stat statName, int val)
    {
        this.statistics.FirstOrDefault(x => x.statName == statName).Value += val;
    }
}

[Serializable]
public class Statistic
// An individual Player statistic (eg, Coin, Health, Strength, etc.)
{
    public Stat statName;
    public int maxValue = 100;

    // OnSkillChange takes Stat and new value.
    public static event Action<Stat, int> OnStatValueChange;

    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0, maxValue);
            OnStatValueChange?.Invoke(statName, _value);
        }
    }

    public Statistic(Stat statName, int value, int maxValue = 100)
    {
        this.statName = statName;
        this._value = value;
        this.Value = value;
        this.maxValue = maxValue;
    }
}

public enum Stat
{
    Coin,
    Health,
    Stress,
    Strength,
    Dexterity,
    Magic,
}