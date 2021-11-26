using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInfo : MonoBehaviour
{
    public class Resource {
        public string name;
        public Sprite sprite;

        public Resource(string name, Sprite sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }

    public static Dictionary<string, Resource> resources = new Dictionary<string, Resource> {
        { "Coin", new Resource(
            name: "Coin",
            sprite: Resources.Load<Sprite>("Images/Inventory/Coin")
        )},
       { "Wood", new Resource(
            name: "Wood",
            sprite: Resources.Load<Sprite>("Images/Inventory/Wood")
        )},
       { "Wool", new Resource(
            name: "Wool",
            sprite: Resources.Load<Sprite>("Images/Inventory/Wool")
        )},
        { "Double-Jump Boots", new Resource(
            name: "Double-Jump Boots",
            sprite: Resources.Load<Sprite>("Images/Crafts_and_Interactables/Jump_Boots")
        )},
        { "Parka", new Resource(
            name: "Parka",
            sprite: Resources.Load<Sprite>("Images/Crafts_and_Interactables/Parka")
        )},
        { "Backpack", new Resource(
            name: "Backpack",
            sprite: Resources.Load<Sprite>("Images/Crafts_and_Interactables/Backpack")
        )}
    };
}

