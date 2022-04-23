using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cost
{
    [field: SerializeField] public int Gold { get; set; } = 0;
    [field: SerializeField] public int Health { get; set; } = 0;
    [field: SerializeField] public int Attack { get; set; } = 0;
    [field: SerializeField] public int Magic { get; set; } = 0;
    [field: SerializeField] public int Dexterity { get; set; } = 0;
}

public sealed class CostManager
{
    private static readonly Lazy<CostManager> lazy =
        new Lazy<CostManager>(() => new CostManager());
    public static CostManager Instance { get { return lazy.Value; } }
    private CostManager() { }
};
