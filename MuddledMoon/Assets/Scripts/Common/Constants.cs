using System;
using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    // Clamp Values.
    public const int maxStatValue = 100;

    // Set Init Player Inventory Values.
    public const int initGold = 100;

    // Stat Init Values.
    public const int initHealth = 100;
    public const int initAttack = 1;
    public const int initMagic = 2;
    public const int initDexterity = 3;

    // Dates
    public const int seasonLength = 30;
    public static readonly List<string> seasons = new List<string>() {
        "Spring", "Summer", "Autumn", "Winter"
    };
    public static readonly int totalDays = seasonLength * seasons.Count;

}