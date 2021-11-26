using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeBoxController : MonoBehaviour
{
    // TODO: Get rid of dynamic, unify TRADE and UPDATE manager.
    private TradeManager.Inventory inventory;
    private Animator animator;
    private Animator tabAnimator;
    private int inventoryLength;
    private GameObject rowPrefab;
    private GameObject buySellTabPrefab;
    private GameObject tradeBox;
    private List<GameObject> rowList;

    private string NPCType;

    private GameObject tradeBoxObj;
    private bool _isBuying;
    public bool IsBuying
    {
        get => _isBuying;
        set
        { 
            _isBuying = value;
            if (NPCType == "Trader")
            {
                tabAnimator.SetBool("IsBuying", value);
            }
        }
    }

    private TradeManager.Item currentItem;
    private Color sellModeSelectionColor = new Color(0.8588f, 0.255f, 0.38f);
    private Color buyModeSelectionColor = new Color(0.1569f, 0f, 0.7294f);
    private Color unselectedColor = new Color(1f, 1f, 1f, 0.05f);
    private int _currentIDX = 0;
    private GameObject buySellTabObj;

    private int CurrentIDX
    {
        get => _currentIDX;
        set
        {
            if (value == -1)
            {
                _currentIDX = inventoryLength - 1;
            }
            else if (value == inventoryLength)
            {
                _currentIDX = 0;
            }
            else
            {
                _currentIDX = value;
            }
        }
    }

    public delegate void GotUpgrade(string upgradeName);
    public static event GotUpgrade OnGotUpgrade;

    private void Awake()
    {
        tradeBox = Resources.Load<GameObject>("Prefabs/GUI/Trade/TradeBox");
        rowPrefab = Resources.Load<GameObject>("Prefabs/GUI/Trade/ItemRow");
        buySellTabPrefab = Resources.Load<GameObject>("Prefabs/GUI/Trade/BuySellTab");
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Dialog.OnPlayerTrading += HandlePlayerTrading;
        Trade.OnTradeMenuScroll += HandleTradeMenuScroll;
        Trade.OnTradeMenuBuySellToggle += HandleTradeMenuBuySellToggle;
        Trade.OnTradeMenuSelectCurrent += HandleTradeMenuSelectCurrent;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
    }

    private void OnDisable()
    {
        Dialog.OnPlayerTrading -= HandlePlayerTrading;
        Trade.OnTradeMenuScroll -= HandleTradeMenuScroll;
        Trade.OnTradeMenuBuySellToggle -= HandleTradeMenuBuySellToggle;
        Trade.OnTradeMenuSelectCurrent -= HandleTradeMenuSelectCurrent;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
    }

    private void HandlePlayerTrading(NPCController.NPC targetInfo)
    {
        NPCType = targetInfo.type;

        CurrentIDX = 0;
        if (NPCType == "Trader")
        {
            // Buy-Sell tab.
            buySellTabObj = Instantiate<GameObject>(buySellTabPrefab, transform);
            buySellTabObj.name = "BuySellTab";
            buySellTabObj.transform.localPosition = new Vector3(85, -35, 0);

            tabAnimator = transform.Find("BuySellTab/Image").GetComponent<Animator>();
        }

        tradeBoxObj = Instantiate<GameObject>(tradeBox, transform);
        tradeBoxObj.transform.localPosition = new Vector3(85, -100, 0);

        IsBuying = true;
        animator.SetBool("IsTrading", true);

        rowList = new List<GameObject>();
        inventory = TradeManager.GetTradeData(targetInfo.inventoryType);
        inventoryLength = inventory.Items.Count;

        PopulateTradeItemRows();
    }

    // TODO: Mesh the next two functions together.
    private void PopulateTradeItemRows()
    {
        foreach (TradeManager.Item invItem in inventory.Items)
        {
            GameObject row = Instantiate<GameObject>(rowPrefab, tradeBoxObj.transform);
            rowList.Add(row);
            row.transform.Find("Image").GetComponent<Image>().sprite = ResourceInfo.resources[invItem.Name].sprite;
            row.transform.Find("Description").GetComponent<TMP_Text>().text = invItem.Name;

            if (IsBuying)
            {
                row.transform.Find("Cost/Text").GetComponent<TMP_Text>().text = invItem.Buy.ToString();
                row.transform.Find("Cost/Image").GetComponent<Image>().sprite = ResourceInfo.resources[invItem.Material].sprite;
            }
            else
            {
                row.transform.Find("Cost/Text").GetComponent<TMP_Text>().text = invItem.Sell.ToString();
            }
        }
        UpdateTradeCosts();
        CursorCurrentItem();
    }

    private void UpdateTradeCosts()
    {
        for (int idx = 0; idx < inventoryLength; idx += 1)
        {
            var rowText = rowList[idx].transform.Find("Cost/Text").GetComponent<TMP_Text>();
            var rowItem = inventory.Items[idx];
            if (IsBuying)
            {

                rowText.text = rowItem.Buy.ToString();
                if (Inventory.GetItemValue(inventory.Items[idx].Material) < inventory.Items[idx].Buy)
                {
                    // If we can't afford it...
                    rowText.color = new Color(1, 0, 0, 1);
                } else
                {
                    rowText.color = new Color(1, 1, 1, 1);
                }
            }
            else
            {
                rowText.text = rowItem.Sell.ToString();
                if (Inventory.GetItemValue(inventory.Items[idx].Name) == 0)
                {
                    rowText.color = new Color(1, 0, 0, 1);
                } else
                {
                    rowText.color = new Color(1, 1, 1, 1);
                }
            }
        }
    }

    private void CursorCurrentItem()
    {
        var selectedColor = IsBuying ? buyModeSelectionColor : sellModeSelectionColor;

        for (int idx = 0; idx < rowList.Count; idx += 1) {
            var color = idx == CurrentIDX ? selectedColor : unselectedColor;
            rowList[idx].GetComponent<Image>().color = color;
        }
    }

    private void HandleTradeMenuScroll(int value)
    {
        CurrentIDX += value;  // This is +- 1.
        CursorCurrentItem();
    }

    private void HandleTradeMenuBuySellToggle()
    {
        if (NPCType == "Trader")
        {
            IsBuying = !IsBuying;
            UpdateTradeCosts();
            CursorCurrentItem();
        }
    }

    private void HandleTradeMenuSelectCurrent()
    {
        // TODO: Slim this bad boi down.
        currentItem = inventory.Items[CurrentIDX];
        var canAfford = Inventory.GetItemValue(currentItem.Material) >= currentItem.Buy;
        var ownsAtLeastOne = Inventory.GetItemValue(currentItem.Name) > 0;

        if (NPCType == "Trader")
        {
            if (IsBuying)
            {
                if (canAfford)
                {
                    Inventory.SetItemValue(currentItem.Name, 1);
                    Inventory.SetItemValue(currentItem.Material, -currentItem.Buy);
                }
                else
                {
                    Debug.Log("Put in something here where you can't buy it.");
                }
            }
            else
            {
                // If Selling...
                if (ownsAtLeastOne)
                {
                    Inventory.SetItemValue(currentItem.Name, -1);
                    Inventory.SetItemValue(currentItem.Material, currentItem.Sell);
                }
                else
                {
                    Debug.Log("Put in something here where you can't sell it.");
                }
            }
        }

        else if (NPCType == "Upgrade")
        {
            if (canAfford)
            {
                Inventory.SetItemValue(currentItem.Material, -currentItem.Buy);
                OnGotUpgrade?.Invoke(currentItem.Name);
            }
            else
            {
                Debug.Log("Put in something here where you can't buy it.");
            }   
        }
        UpdateTradeCosts();  // We now may have new resource values.
    }

    private void HandleTradeMenuEnd()
    {
        animator.SetBool("IsTrading", false);
        IsBuying = true;
        Destroy(tradeBoxObj);

        if (NPCType == "Trader")
        {
            Destroy(buySellTabObj);
        }

    }
}
