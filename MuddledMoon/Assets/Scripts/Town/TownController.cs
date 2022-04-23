using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Town
{
    public string townName;
    public PlayerActions playerActions;

    public Town(string townName, PlayerActions playerActions, int numActions = -1)
    {
        this.townName = townName;
        this.playerActions = playerActions;
        this.playerActions.GetRandomSubset(numActions);  // Subset the player actions.
    }

    public void SetRandomActionPrices()
    {
        foreach (PlayerAction action in playerActions.actions)
        {
            action.SetRandomCost();
        }
    }
}

[Serializable]
public class Towns
{
    public List<Town> towns;

    public Towns(List<Town> towns)
    {
        this.towns = towns;
    }

    public void SetRandomActionPrices()
    {
        foreach (Town town in towns)
        {
            town.SetRandomActionPrices();
        }
    }
}

public class TownController : MonoBehaviour
{
}
