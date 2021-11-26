using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public static class TradeManager
{
    private static string json;

    public class Item
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public int Buy { get; set; }
        public int Sell { get; set; }
    }

    public class Inventory
    {
        public string Type { get; set; }
        public List<Item> Items { get; set; }
    }

    public static List<Inventory> tradeInventory;

    public static void LoadData()
    {
        json = Resources.Load<TextAsset>("Data/Inventories").text;
        tradeInventory = JsonConvert.DeserializeObject<List<Inventory>>(json);
    }

    public static Inventory GetTradeData(string inventoryType)
    {
        if (tradeInventory == null) LoadData();
        foreach (Inventory inventory in tradeInventory)
        {
            if (inventory.Type == inventoryType)
            {
                return inventory;
            }
        }
        Debug.LogError($"No trade type of type {inventoryType} exists!");
        return null;
    }
}
