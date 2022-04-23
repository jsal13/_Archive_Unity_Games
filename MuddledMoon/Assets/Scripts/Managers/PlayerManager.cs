using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerManager
{
    private static readonly Lazy<PlayerManager> lazy =
        new Lazy<PlayerManager>(() => new PlayerManager());
    public static PlayerManager Instance { get { return lazy.Value; } }
    private PlayerManager() { }

    public static Stat health = new Stat("health", Constants.initHealth);
    public static Stat attack = new Stat("attack", Constants.initAttack);
    public static Stat magic = new Stat("magic", Constants.initMagic);
    public static Stat dexterity = new Stat("dexterity", Constants.initDexterity);
    public static Stat gold = new Stat("gold", Constants.initGold);
}
