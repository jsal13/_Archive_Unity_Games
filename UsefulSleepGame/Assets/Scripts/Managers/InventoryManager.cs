using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class InventoryManager
{
    private static readonly Lazy<InventoryManager> lazy = new Lazy<InventoryManager>(() => new InventoryManager());
    public static InventoryManager Instance { get { return lazy.Value; } }
    private InventoryManager() { }

    public static void AddToPlayerInventory(string item)
    {
        Debug.Log($"Added {item} to inventory!");
    }
}
