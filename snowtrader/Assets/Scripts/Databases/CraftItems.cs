using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItems : MonoBehaviour
{
    public class CraftItem
    {
        public string name;
        public int cost;
        public string material;
        public Sprite sprite;
        public GameObject objectPrefab;

        public CraftItem(string name, int cost, string material, Sprite sprite, GameObject objectPrefab)
        {
            this.name = name;
            this.cost = cost;
            this.material = material;
            this.sprite = sprite;
            this.objectPrefab = objectPrefab;
        }
    }

    public static List<CraftItem> craftItems = null;

    public static List<CraftItem> GetCraftItemData()
    {
        if (craftItems == null)
        {
            craftItems = new List<CraftItem>
            {
                new CraftItem(
                    name: "Bonfire",
                    cost: 4,
                    material: "Wood",
                    sprite: Resources.Load<Sprite>("Images/Crafts_and_Interactables/Icon_Bonfire"),
                    objectPrefab: Resources.Load<GameObject>("Prefabs/Crafts/Bonfire")
            ),

                new CraftItem(
                    name: "Leanto",
                    cost: 25,
                    material: "Wood",
                    sprite: Resources.Load<Sprite>("Images/Crafts_and_Interactables/Icon_Leanto"),
                    objectPrefab: Resources.Load<GameObject>("Prefabs/Crafts/Leanto")
            )
            };
        }

        return craftItems;
    }
}
