using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private bool _jumpBoots;
    public bool JumpBoots
    {
        get => _jumpBoots;
        set
        {
            _jumpBoots = value;
            PersistenceManager.Instance.jumpBoots = true;
            OnGotJumpBoots?.Invoke();
        }
    }

    public delegate void GotJumpBoots();
    public static event GotJumpBoots OnGotJumpBoots;

    private void OnEnable()
    {
        TradeBoxController.OnGotUpgrade += HandleGotUpgrade;
    }

    private void OnDisable()
    {
        TradeBoxController.OnGotUpgrade -= HandleGotUpgrade;
    }

    private void HandleGotUpgrade(string upgradeName)
    {
        if (upgradeName == "Double-Jump Boots")
        {
            JumpBoots = true;
        }
    }
}
