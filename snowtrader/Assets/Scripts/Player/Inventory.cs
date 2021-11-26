using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static int _coin;
    public static int coinCap;
    public static int Coin
    {
        get => _coin;
        set
        {
            _coin = Mathf.Clamp(value, 0, coinCap);
            PersistenceManager.Instance.resourceDict["Coin"].Owns = _coin;
            OnPlayerResourceChange?.Invoke("Coin", _coin);
        }
    }

    private static int _wood;
    public static int woodCap;
    public static int Wood
    {
        get => _wood;
        set
        {
            _wood = Mathf.Clamp(value, 0, woodCap);
            PersistenceManager.Instance.resourceDict["Wood"].Owns = _wood;
            OnPlayerResourceChange?.Invoke("Wood", _wood);
        }
    }

    private static int _wool;
    public static int woolCap;
    public static int Wool
    {
        get => _wool;
        set
        {
            _wool = Mathf.Clamp(value, 0, woolCap);
            PersistenceManager.Instance.resourceDict["Wool"].Owns = _wool;
            OnPlayerResourceChange?.Invoke("Wool", _wool);
        }
    }

    public static int GetItemValue(string name)
    {
        return name switch
        {
            "Wood" => Wood,
            "Coin" => Coin,
            "Wool" => Wool,
            _ => 0,
        };
    }

    public static int SetItemValue(string name, int value)
    {
        return name switch
        {
            "Wood" => Wood += value,
            "Coin" => Coin += value,
            "Wool" => Wool += value,
            _ => 0,
        };
    }


    public static int GetItemCap(string name)
    {
        return name switch
        {
            "Wood" => woodCap,
            "Coin" => coinCap,
            "Wool" => woolCap,
            _ => 0,
        };
    }

    public delegate void PlayerResourceChange(string resource, int value);
    public static event PlayerResourceChange OnPlayerResourceChange;

    private void Awake()
    {
        woodCap = PersistenceManager.Instance.resourceDict["Wood"].Cap;
        Wood = PersistenceManager.Instance.resourceDict["Wood"].Owns;

        woolCap = PersistenceManager.Instance.resourceDict["Wool"].Cap; ;
        Wool = PersistenceManager.Instance.resourceDict["Wool"].Owns;

        coinCap = PersistenceManager.Instance.resourceDict["Coin"].Cap; ;
        Coin = PersistenceManager.Instance.resourceDict["Coin"].Owns; ;
    }

    private void Start()
    {
        OnPlayerResourceChange?.Invoke("Coin", Coin);
        OnPlayerResourceChange?.Invoke("Wood", Wood);
        OnPlayerResourceChange?.Invoke("Wool", Wool);
    }

    private void OnEnable()
    {
        HarvestRersource.OnChoppingForest += HandleChoppingForest;
    }

    private void OnDisable()
    {
        HarvestRersource.OnChoppingForest -= HandleChoppingForest;
    }

    private void HandleChoppingForest(bool value, int quantity)
    {
        Wood += quantity;
    }
}
