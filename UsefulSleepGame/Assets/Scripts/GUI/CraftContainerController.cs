using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CraftContainerController : MonoBehaviour
{
    private bool isOnDelay = false;
    [SerializeField] private GameObject player;
    [SerializeField] private Image craftIcon;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Image costResource;
    [SerializeField] private CraftManager.CraftItem currentItem;

    private int _currentCraftIDX = 0;
    public int CurrentCraftIDX
    {
        get => _currentCraftIDX;
        set
        {
            if (value == -1)
            {
                _currentCraftIDX = CraftManager.playerCrafts.Count - 1;
            }
            else if (value == CraftManager.playerCrafts.Count)
            {
                _currentCraftIDX = 0;
            }
            else
            {
                _currentCraftIDX = value;
            }

            currentItem = CraftManager.craftDict[CraftManager.playerCrafts[_currentCraftIDX]];
            SetCurrentCraft();
        }

    }

    [SerializeField] private InputAction craftCycleAction;
    [SerializeField] private InputAction craftAction;
    private bool isOnCraftDelay;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        CurrentCraftIDX = 0;
    }

    private void OnEnable()
    {
        craftCycleAction.Enable();
        craftAction.Enable();
        craftCycleAction.performed += HandleCraftCycleAction;
        craftAction.performed += HandleCraftAction;

        PlayerManager.OnWoodChange += HandleResourceChange;
        PlayerManager.OnStoneChange += HandleResourceChange;
        PlayerManager.OnWoolChange += HandleResourceChange;
    }

    private void OnDisable()
    {
        craftCycleAction.Disable();
        craftAction.Disable();
        craftCycleAction.performed -= HandleCraftCycleAction;
        craftAction.performed -= HandleCraftAction;

        PlayerManager.OnWoodChange -= HandleResourceChange;
        PlayerManager.OnStoneChange -= HandleResourceChange;
        PlayerManager.OnWoolChange -= HandleResourceChange;
    }

    private void HandleResourceChange(int newValue)
    {
        // Reload the current craft to see if player can afford it.
        SetCurrentCraft();
    }

    private void HandleCraftAction(InputAction.CallbackContext obj)
    {
        if (!isOnCraftDelay && PlayerManager.isOnGround && PlayerManager.canCraft)
        {
            StartCoroutine(DelayCrafting());
        }
    }

    private IEnumerator DelayCrafting()
    {
        isOnCraftDelay = true;
        CraftItem();
        yield return new WaitForSeconds(0.25f);
        isOnCraftDelay = false;
    }


    private void HandleCraftCycleAction(InputAction.CallbackContext obj)
    {
        if (!isOnDelay)
        {
            StartCoroutine(DelayCycle());
        }
    }

    private IEnumerator DelayCycle()
    {
        isOnDelay = true;

        // TODO: Do the other way.
        CurrentCraftIDX += 1;
        yield return new WaitForSeconds(0.25f);

        isOnDelay = false;
    }

    private void SetCurrentCraft()
    {
        Color color = Color.white;
        if (!CanAffordCurrentCraft())
        {
            color = Color.red;
        }

        craftIcon.sprite = Resources.Load<Sprite>(currentItem.craftSpriteLoc);
        craftIcon.color = color;

        costResource.sprite = Resources.Load<Sprite>(currentItem.costResourceSpriteLoc);
        costResource.color = color;

        costText.text = currentItem.costValue.ToString();
        costText.color = color;
    }

    private bool CanAffordCurrentCraft()
    {

        // TODO: Refactor the PlayerManager.ITEM thing.
        switch (currentItem.costResource)
        {
            case "Wood":
                return PlayerManager.Wood >= currentItem.costValue;
            case "Stone":
                return PlayerManager.Stone >= currentItem.costValue;
            case "Wool":
                return PlayerManager.Wool >= currentItem.costValue;
            default:
                return false;
        }
    }

    private void CraftItem()
    {
        if (CanAffordCurrentCraft())
        {
            Vector3 offset = new Vector3(0, -4, 0);
            Instantiate(currentItem.craftPrefab, player.transform.position + offset, Quaternion.identity);

            // TODO: Refactor the PlayerManager.ITEM thing.
            switch (currentItem.costResource)
            {
                case "Wood":
                    PlayerManager.Wood -= currentItem.costValue;
                    break;
                case "Stone":
                    PlayerManager.Stone -= currentItem.costValue;
                    break;
                case "Wool":
                    PlayerManager.Wool -= currentItem.costValue;
                    break;
                default:
                    break;
            }
            SetCurrentCraft();
        }
    }
}
