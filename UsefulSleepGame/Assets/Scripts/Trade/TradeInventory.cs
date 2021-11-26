using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "TradeInventory", menuName = "UsefulSleep/TradeInventory", order = 0)]
public class TradeInventory : ScriptableObject
{
    [Serializable]

    public class Item
    {
        [BoxGroup("Shop Item")]
        [HorizontalGroup("Shop Item/Horiz", 64)]
        [VerticalGroup("Shop Item/Horiz/Col1", -1)]
        [PreviewField(64), HideLabel]
        public Sprite sprite;
        [VerticalGroup("Shop Item/Horiz/Col2")]
        public string itemName;
        [VerticalGroup("Shop Item/Horiz/Col2")]
        [Range(1, 20)]
        public int buy;
        [VerticalGroup("Shop Item/Horiz/Col2")]
        [Range(1, 20)]
        public int sell;
    }

    public List<Item> inventory;
}

