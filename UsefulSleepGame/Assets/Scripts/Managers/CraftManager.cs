using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CraftManager
{
    private static readonly Lazy<CraftManager> lazy = new Lazy<CraftManager>(() => new CraftManager());
    public static CraftManager Instance { get { return lazy.Value; } }
    private CraftManager() { }

    [Serializable]
    public class CraftItem
    {
        public string craftName;
        public GameObject craftPrefab;
        public string craftSpriteLoc;
        public string costResource;
        public string costResourceSpriteLoc;
        public int costValue;

        public CraftItem(string craftName, GameObject craftPrefab, string craftSpriteLoc, string costResource, string costResourceSpriteLoc, int costValue)
        {
            this.craftName = craftName;
            this.craftPrefab = craftPrefab;
            this.craftSpriteLoc = craftSpriteLoc;
            this.costResource = costResource;
            this.costResourceSpriteLoc = costResourceSpriteLoc;
            this.costValue = costValue;
        }
    }

    // SPRITES.
    // Resource Sprites.
    public static string woodSpriteLoc = "Images/MaterialResources/Wood";
    public static string stoneSpriteLoc = "Images/MaterialResources/Stone";
    public static string woolSpriteLoc = "Images/MaterialResources/Wool";

    // Crafting Sprites.
    public static string bonfireSpriteLoc = "Images/Items/Bonfire";
    public static string stoneBonfireSpriteLoc = "Images/Items/StoneBonfire";

    // PREFABS.
    public static GameObject bonfirePrefab = Resources.Load<GameObject>("Prefabs/Craft/Bonfire_Wood");
    public static GameObject stoneBonfirePrefab = Resources.Load<GameObject>("Prefabs/Craft/Bonfire_Stone");

    // CRAFT ITEMS.
    public static CraftItem craftBonfire = new CraftItem("Bonfire", bonfirePrefab, bonfireSpriteLoc, "Wood", woodSpriteLoc, 4);
    public static CraftItem craftStoneBonfire = new CraftItem("StoneBonfire", stoneBonfirePrefab, stoneBonfireSpriteLoc, "Stone", stoneSpriteLoc, 4);

    public static Dictionary<string, CraftItem> craftDict = new Dictionary<string, CraftItem>()
    {
        {"Bonfire", craftBonfire },
        {"StoneBonfire", craftStoneBonfire}
    };

    // CURRENT PLAYER CRAFTS.
    public static List<string> playerCrafts = new List<string>() { "Bonfire", "StoneBonfire" };

}