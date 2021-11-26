using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class TradeManager
{
    private static readonly Lazy<TradeManager> lazy = new Lazy<TradeManager>(() => new TradeManager());
    public static TradeManager Instance { get { return lazy.Value; } }
    private TradeManager() { }

    public delegate void TradeDialogActive(List<TradeInventory.Item> inventory, bool value);
    public static TradeDialogActive OnTradeDialogActive;

    public static void ToggleTradeDialog(List<TradeInventory.Item> inventory, bool value)
    {
        PlayerManager.IsTrading = value;

        if (value)
        {
            GameObject gui = GameObject.Find("GUI");
            GameObject ts = Resources.Load<GameObject>("Prefabs/GUI/TradeDialog/TradeSystem");
            GameObject go = GameObject.Instantiate<GameObject>(ts, gui.transform);

            go.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        }
        else
        {
            GameObject go = GameObject.FindObjectOfType<TradeDialogController>().gameObject;
            GameObject.Destroy(go.gameObject);
        }

        OnTradeDialogActive?.Invoke(inventory, value);
    }
}
