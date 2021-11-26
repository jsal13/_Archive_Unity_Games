using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TradeDialogController : MonoBehaviour
{

    [SerializeField] private GameObject tradeContainer;
    [SerializeField] private GameObject buysellTabPrefab;
    [SerializeField] private Animator buysellAnimator;
    [SerializeField] private GameObject shopContainerPrefab;
    private GameObject shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private InputAction confirmAction;
    [SerializeField] private InputAction cancelAction;
    [SerializeField] private InputAction buysellAction;
    List<TradeInventory.Item> currentInventory;
    List<GameObject> currentShop;
    private bool canMoveCursor;

    private bool _isBuying = true;
    private bool isSwitchingBuySell;

    public bool IsBuying
    {
        get => _isBuying;
        set
        {
            _isBuying = value;
            buysellAnimator.SetBool("isBuying", _isBuying);
            currentShop = new List<GameObject>();
        }

    }
    private int _currentIDX = 0;
    private bool isBuySellCooldown;

    private int CurrentIDX
    {
        get => _currentIDX;
        set
        {
            _currentIDX = value;
            StartCoroutine(UpdateSelection());
        }
    }

    private void OnEnable()
    {
        TradeManager.OnTradeDialogActive += HandleTradeDialogActive;

        StartCoroutine(DelayForInput());
    }

    private IEnumerator DelayForInput()
    {
        // Delay this so that the button to talk doesn't confirm a trade.
        yield return new WaitForSeconds(1f);

        confirmAction.Enable();
        cancelAction.Enable();
        buysellAction.Enable();

        confirmAction.performed += HandleConfirmAction;
        cancelAction.performed += HandleCancelAction;
        buysellAction.performed += HandleBuySellAction;
    }

    private void OnDisable()
    {
        confirmAction.Disable();
        cancelAction.Disable();
        buysellAction.Disable();

        confirmAction.performed -= HandleConfirmAction;
        cancelAction.performed -= HandleCancelAction;
        buysellAction.performed -= HandleBuySellAction;
        TradeManager.OnTradeDialogActive -= HandleTradeDialogActive;
    }

    private void HandleBuySellAction(InputAction.CallbackContext obj)
    {
        if (!isSwitchingBuySell)
        {
            StartCoroutine(DelayForBuySellAction());
        }
    }

    private IEnumerator DelayForBuySellAction()
    {
        isSwitchingBuySell = true;
        // Delay this so that the button to talk doesn't confirm a trade.
        IsBuying = !IsBuying;
        PopulateShopItems();
        yield return new WaitForSeconds(0.25f);
        isSwitchingBuySell = false;
    }

    private void HandleCancelAction(InputAction.CallbackContext obj)
    {
        // Remove the dialog.
        TradeManager.ToggleTradeDialog(new List<TradeInventory.Item>(), false);
    }

    private void HandleConfirmAction(InputAction.CallbackContext obj)
    {
        if (!isBuySellCooldown)
        {
            StartCoroutine(BuySellCooldown());
        }
    }

    private IEnumerator BuySellCooldown()
    {
        isBuySellCooldown = true;
        TradeInventory.Item curItem = currentInventory[CurrentIDX];
        if (CanAfford(IsBuying ? "Coin" : curItem.itemName, IsBuying ? curItem.buy : 1))
        {
            if (IsBuying)
            {
                switch (curItem.itemName)
                {
                    case "Wood":
                        PlayerManager.Wood += 1;
                        break;
                    case "Stone":
                        PlayerManager.Stone += 1;
                        break;
                    case "Wool":
                        PlayerManager.Wool += 1;
                        break;
                    case "Coin":
                        PlayerManager.Coin += 1;
                        break;
                    default:
                        Debug.LogError($"No such resource {curItem.itemName}.");
                        break;
                }
                PlayerManager.Coin -= curItem.buy;
            }
            else if (!IsBuying)
            {
                switch (curItem.itemName)
                {
                    case "Wood":
                        PlayerManager.Wood -= 1;
                        break;
                    case "Stone":
                        PlayerManager.Stone -= 1;
                        break;
                    case "Wool":
                        PlayerManager.Wool -= 1;
                        break;
                    case "Coin":
                        PlayerManager.Coin -= 1;
                        break;
                    default:
                        Debug.LogError($"No such resource {curItem.itemName}.");
                        break;
                }
                PlayerManager.Coin += curItem.sell;
            }
        }
        UpdateSelection();
        yield return new WaitForSeconds(0.2f);
        isBuySellCooldown = false;
    }

    private void Start()
    {
        GameObject bstab = Instantiate(buysellTabPrefab, tradeContainer.transform);
        bstab.transform.SetSiblingIndex(0);
        buysellAnimator = bstab.GetComponent<Animator>();
        buysellAnimator.SetBool("isBuying", true);
    }

    private void Update()
    {
        if (canMoveCursor)
        {
            float upDownValue = GameManager.gamepad.leftStick.y.ReadValue();
            if (upDownValue > 0.5)
            {
                CurrentIDX = CurrentIDX == 0 ? currentShop.Count - 1 : CurrentIDX - 1;
            }
            else if (upDownValue < -0.5)
            {
                CurrentIDX = (CurrentIDX + 1) % currentShop.Count;
            }
        }
    }

    private void HandleTradeDialogActive(List<TradeInventory.Item> inventory, bool value)
    {
        tradeContainer.SetActive(value);
        if (value)
        {
            currentInventory = inventory;
            PopulateShopItems();

        }
        else
        {
            currentInventory = null;
        }
    }

    private void PopulateShopItems()
    {
        currentShop = new List<GameObject>();
        if (shopContainer != null)
        {
            // Destroy the old shop container.
            Destroy(shopContainer.gameObject);
        }
        shopContainer = Instantiate(shopContainerPrefab, tradeContainer.transform);

        foreach (TradeInventory.Item item in currentInventory)
        {
            GameObject itemObj = Instantiate(shopItemPrefab, shopContainer.transform);
            currentShop.Add(itemObj);

            // Set the appropriate values in the prefab.
            itemObj.transform.Find("Image").GetComponent<Image>().sprite = item.sprite;

            string valString = IsBuying ? item.buy.ToString() : item.sell.ToString();

            itemObj.transform.Find("Cost/Value").GetComponent<TMP_Text>().text = valString;
        }
        StartCoroutine(UpdateSelection());
    }

    private IEnumerator UpdateSelection()
    {
        for (int idx = 0; idx < currentShop.Count; idx += 1)
        {
            // Do you have enough coin to buy, or at least one to sell?
            if (!CanAfford(IsBuying ? "Coin" : currentInventory[idx].itemName, IsBuying ? currentInventory[idx].buy : 1))
            {
                currentShop[idx].transform.Find("Cost/Value").GetComponent<TMP_Text>().color = Color.red;
            }
            else
            {
                currentShop[idx].transform.Find("Cost/Value").GetComponent<TMP_Text>().color = Color.white;
            }

            if (idx == CurrentIDX)
            {
                currentShop[idx].GetComponent<Image>().color = new Color(0.253115f, 0.6792453f, 0.4449883f, 1);
            }
            else
            {
                currentShop[idx].GetComponent<Image>().color = Color.clear;
            }
        }
        canMoveCursor = false;
        yield return new WaitForSeconds(0.25f);
        canMoveCursor = true;
        yield return null;
    }

    private bool CanAfford(string resource, int value)
    {
        switch (resource)
        {
            case "Wood":
                return PlayerManager.Wood >= value;
            case "Stone":
                return PlayerManager.Stone >= value;
            case "Wool":
                return PlayerManager.Wool >= value;
            case "Coin":
                return PlayerManager.Coin >= value;
            default:
                Debug.LogError($"No such resource {resource}.");
                return false;
        }

    }

}
