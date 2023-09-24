using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        // Initialize Stats.
        Stat.OnStatChange?.Invoke("health", StatsManager.health.Value);

        Item.OnItemChange?.Invoke("food", ItemManager.food.Value);
        Item.OnItemChange?.Invoke("water", ItemManager.water.Value);
        Item.OnItemChange?.Invoke("salt", ItemManager.salt.Value);
        Item.OnItemChange?.Invoke("wood", ItemManager.wood.Value);

    }
}