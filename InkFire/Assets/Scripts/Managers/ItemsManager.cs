using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ItemManager
{
    private static readonly Lazy<ItemManager> lazy =
        new Lazy<ItemManager>(() => new ItemManager());
    public static ItemManager Instance { get { return lazy.Value; } }
    private ItemManager() { }

    public static Item food = new Item("food", Constants.initFood);
    public static Item water = new Item("water", Constants.initWater);
    public static Item salt = new Item("salt", Constants.initSalt);
    public static Item wood = new Item("wood", Constants.initWood);

    public static List<Item> itemList = new List<Item>(){ food, water, salt, wood
};
    public static Backpack backpack = new Backpack(itemList);

}

public class Backpack
{
    public List<Item> items;

    public Backpack(List<Item> items)
    {
        this.items = items;
    }
}

public class Item
{
    public string itemName;
    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, 0, Constants.maxItemValue);
            OnItemChange?.Invoke(itemName, _value);
        }
    }
    public static Action<string, int> OnItemChange;  // name, val.

    public Item(string itemName, int initValue)
    {
        this.itemName = itemName;
        this.Value = initValue;
    }
}