using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PlayerManager
{
    private static readonly Lazy<PlayerManager> lazy =
        new Lazy<PlayerManager>(() => new PlayerManager());
    public static PlayerManager Instance { get { return lazy.Value; } }
    private PlayerManager() { }
};