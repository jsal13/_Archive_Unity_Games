using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// TODO: Go from forest right into NPC; you'll have canCraft set to "True".
public class CraftBoxController : MonoBehaviour
{
    public bool canChangeCraftingItem = true;
    public bool canCraft;
    private bool _isVisible;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            craftBox.SetActive(_isVisible);
            SetSelectedCraftingItem(CurrentCraftingItemIDX);
        }
    }


    private int _currentCraftingItemIDX;
    public int CurrentCraftingItemIDX
    {
        get => _currentCraftingItemIDX;
        set
        {
            if (value == CraftItems.GetCraftItemData().Count) _currentCraftingItemIDX = 0;
            else if (value == -1) _currentCraftingItemIDX = CraftItems.GetCraftItemData().Count - 1;
            else { _currentCraftingItemIDX = value; }
            SetSelectedCraftingItem(_currentCraftingItemIDX);
        }
    }

    private GameObject player;
    private GameObject craftBox;
    private GameObject craftIcon;
    private Image craftIconSprite;
    private TMP_Text craftCostText;
    public Image craftCostMaterial;
    private bool canAffordCurrentItem;

    public delegate void ChangedCurrentCraftItem(string itemName);
    public static event ChangedCurrentCraftItem OnChangedCurrentCraftItem;

    private void Awake()
    {
        player = GameObject.Find("Player");
        craftBox = transform.Find("CraftBox").gameObject;
        craftIcon = craftBox.transform.Find("Icon").gameObject;
        craftIconSprite = craftIcon.GetComponent<Image>();
        craftCostText = craftIcon.transform.Find("Cost").GetComponent<TMP_Text>();
        craftCostMaterial = craftIcon.transform.Find("Material").GetComponent<Image>();
        CurrentCraftingItemIDX = 0;
        IsVisible = false;
        canCraft = true;
    }

    private void OnEnable()
    {
        Inventory.OnPlayerResourceChange += HandlePlayerResourceChange;
        Dialog.OnPlayerSpeaking += HandlePlayerSpeaking;
        Dialog.OnPlayerTrading += HandlePlayerTrading;
        Dialog.OnPlayerNearSpeaker += HandlePlayerNearSpeaker;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
        PlayerManager.OnPlayerOpenedCrafting += HandlePlayerOpenedCrafting;
        HarvestRersource.OnPlayerNearInteractable += HandlePlayerNearInteractable;
    }

    private void OnDisable()
    {
        Inventory.OnPlayerResourceChange -= HandlePlayerResourceChange;
        Dialog.OnPlayerSpeaking -= HandlePlayerSpeaking;
        Dialog.OnPlayerTrading -= HandlePlayerTrading;
        Dialog.OnPlayerNearSpeaker -= HandlePlayerNearSpeaker;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
        PlayerManager.OnPlayerOpenedCrafting -= HandlePlayerOpenedCrafting;
        HarvestRersource.OnPlayerNearInteractable -= HandlePlayerNearInteractable;
    }

    private void HandlePlayerNearInteractable(bool value)
    {
        canCraft = !value;
    }

    private void HandlePlayerNearSpeaker(bool value)
    {
        canCraft = !value;
    }

    private void HandlePlayerOpenedCrafting(bool value)
    {
        canChangeCraftingItem = value;
        IsVisible = value;
    }

    private void HandlePlayerTrading(NPCController.NPC targetInfo)
    {
        canChangeCraftingItem = false;
        IsVisible = false;
    }

    private void HandleTradeMenuEnd()
    {
        canChangeCraftingItem = true;
    }

    private void HandlePlayerSpeaking(bool value)
    {
        canChangeCraftingItem = !value;
        if (value)
        {
            IsVisible = false;
        }
    }

    void Update()
    {
        // Only update the sprite if there's an update to the IDX.
        transform.position = player.transform.position + new Vector3(0, 1f, 0);

        if (canChangeCraftingItem && IsVisible)
        {
            if (InputManager.gamepad.leftShoulder.wasPressedThisFrame) CurrentCraftingItemIDX -= 1;
            else if (InputManager.gamepad.leftShoulder.wasPressedThisFrame) CurrentCraftingItemIDX += 1;
            else if (InputManager.gamepad.buttonWest.wasPressedThisFrame) {
                if (canAffordCurrentItem && canCraft)
                {
                    var currentCraft = CraftItems.GetCraftItemData()[CurrentCraftingItemIDX];
                    var currentCraftMaterial = currentCraft.material;
                    var currentCraftCost = currentCraft.cost;
                    _ = Instantiate(currentCraft.objectPrefab, player.transform.position, Quaternion.identity);
                    Inventory.SetItemValue(currentCraftMaterial, -currentCraftCost);

                    IsVisible = false;
                }
            }
        }
    }

    public void SetSelectedCraftingItem(int idx)
    {
        var craftData = CraftItems.GetCraftItemData()[idx];
        OnChangedCurrentCraftItem?.Invoke(craftData.name);
        craftIconSprite.sprite = craftData.sprite;
        craftCostMaterial.sprite = ResourceInfo.resources[craftData.material].sprite;
        craftCostText.text = craftData.cost.ToString();

        if (Inventory.GetItemValue(craftData.material) < craftData.cost)
        {
            canAffordCurrentItem = false;
            craftCostText.color = Color.red;
            craftIconSprite.color = Color.red;
        }
        else
        {
            canAffordCurrentItem = true;
            craftCostText.color = Color.white;
            craftIconSprite.color = Color.white;
        }

    }

    public void HandlePlayerResourceChange(string name, int value)
    {
        // Checks to see if we can afford things now.
        SetSelectedCraftingItem(CurrentCraftingItemIDX);
    }

}
