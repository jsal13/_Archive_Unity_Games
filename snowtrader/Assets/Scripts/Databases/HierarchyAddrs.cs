using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyAddrs : MonoBehaviour
{
    // Player
    public const string playerPointLight = "Player/PointLight";

    // GUI
    public const string GUI = "GUI";
    public const string GUICanvas = GUI + "/Canvas";

    public const string SelectedCraftItemContainer = GUICanvas + "/CraftBox";
    public const string SelectedCraftItem = SelectedCraftItemContainer + "/Image";
    public const string SelectedCraftItemCost = SelectedCraftItemContainer + "/Cost";

    public const string GUIInventory = GUICanvas + "/Inventory/ResourceContainer";
    public const string GUIInventoryResource = "Prefabs/GUI/InventoryBar/Resource";

    // Chartacter Dialog
    public const string dialogBox = GUICanvas + "/DialogBox";
    public const string dialogBoxCharacterName = dialogBox + "/MainBorder/CharacterName";
    public const string dialogBoxCharacterImage = dialogBox + "/MainBorder/CharacterBorder/CharacterPortrait";
    public const string dialogBoxDialog = dialogBox + "/MainBorder/Dialog";

    // Misc
    //public const string ControllerButtonTooltip =  "Player/ControllerButtonTooltip";
}
