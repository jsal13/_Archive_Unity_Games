using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Trade : MonoBehaviour
{
    [SerializeField] private bool isTrading = false;
    [SerializeField] private bool canScrollAgain = true;
    [SerializeField] private bool canTabAgain = true;
    private string npcType;

    public delegate void TradeMenuScroll(int value);
    public static event TradeMenuScroll OnTradeMenuScroll;

    public delegate void TradeMenuBuySellToggle();
    public static event TradeMenuBuySellToggle OnTradeMenuBuySellToggle;

    public delegate void TradeMenuSelectCurrent();
    public static event TradeMenuSelectCurrent OnTradeMenuSelectCurrent;

    public delegate void TradeMenuEnd();
    public static event TradeMenuEnd OnTradeMenuEnd;

    private IEnumerator Scroll(int val)
    {
        // We negate the val to make "up" correspond to up.
        OnTradeMenuScroll?.Invoke(-val);
        canScrollAgain = false;
        yield return new WaitForSeconds(0.2f);
        canScrollAgain = true;
    }

    private IEnumerator Tab(int _)
    {
        OnTradeMenuBuySellToggle?.Invoke();
        canTabAgain = false;
        yield return new WaitForSeconds(0.2f);
        canTabAgain = true;
    }

    private void Update()
    {
        if (isTrading)
        {
            Vector2 dpadDir = InputManager.gamepad.leftStick.ReadValue();

            if (Mathf.Abs(dpadDir.y) > 0.5 && canScrollAgain)
            {
                StartCoroutine(Scroll((int)Mathf.Sign(dpadDir.y)));
            }
            else if (npcType == "Trader" && Mathf.Abs(dpadDir.x) > 0.5 && canTabAgain)
            {
                StartCoroutine(Tab((int)Mathf.Sign(dpadDir.x)));
            }
            else if (InputManager.gamepad.buttonWest.wasPressedThisFrame)
            {
                OnTradeMenuSelectCurrent?.Invoke();
            }
            else if (InputManager.gamepad.buttonEast.wasPressedThisFrame)
            {
                OnTradeMenuEnd();
            }
        }
    }

    private void OnEnable()
    {
        Dialog.OnPlayerTrading += HandlePlayerTrading;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
    }

    private void OnDisable()
    {
        Dialog.OnPlayerTrading -= HandlePlayerTrading;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
    }

    private void HandlePlayerTrading(NPCController.NPC targetInfo)
    {
        npcType = targetInfo.type;
        StartCoroutine(PauseBeforeTrading());
    }

    IEnumerator PauseBeforeTrading()
    {
        // So that the player's input doesn't become a buy-item selection.
        yield return new WaitForSeconds(0.5f);
        isTrading = true;
    }

    private void HandleTradeMenuEnd()
    {
        isTrading = false;
    }
}
