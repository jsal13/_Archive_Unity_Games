using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerActionType
{
    Rest,
    AttackClass,
    MagicClass,
    DexterityClass,
    Leave,
};

[Serializable]
public class PlayerAction
{
    public PlayerActionType type;
    public string actionName;
    public Cost cost;

    public PlayerAction(PlayerActionType type)
    {
        this.type = type;
        this.actionName = type.ToString();
    }

    public void SetRandomCost()
    {
        cost = new Cost();

        switch (type)
        {
            case PlayerActionType.Rest:
                cost.Health = 9;
                cost.Gold = -10;
                break;
            case PlayerActionType.Leave:
                break;
            case PlayerActionType.AttackClass:
                cost.Attack = UnityEngine.Random.Range(5, 10);
                cost.Gold = UnityEngine.Random.Range(9, 13);
                break;
            case PlayerActionType.MagicClass:
                cost.Magic = UnityEngine.Random.Range(3, 7);
                cost.Gold = UnityEngine.Random.Range(12, 20);
                break;
            case PlayerActionType.DexterityClass:
                cost.Dexterity = UnityEngine.Random.Range(4, 8);
                cost.Gold = UnityEngine.Random.Range(6, 16);
                break;
            default:
                break;
        }
    }

}

[Serializable]
public class PlayerActions
{
    public List<PlayerAction> actions = new List<PlayerAction>();

    public void GetRandomSubset(int size = -1)
    {
        // NOTE: the random subset will *always* include 0 and 1 (rest and leave).
        // NOTE: not specifying size will give a random size.

        // The maxLength is the total number of things to do, minus resting and leaving.
        int maxLength = Enum.GetNames(typeof(PlayerActionType)).Length - 2;

        // Set to a random size if -1, and clamp the value regardless.
        if (size == -1) size = UnityEngine.Random.Range(0, maxLength);
        size = Mathf.Clamp(size, 0, maxLength);

        // All Towns have Rest / Leave.
        actions.Add(new PlayerAction(PlayerActionType.Rest));
        actions.Add(new PlayerAction(PlayerActionType.Leave));

        // Possible Actions.        
        System.Random rand = new System.Random();
        List<int> indices = Enumerable.Range(0, maxLength).OrderBy(x => rand.Next()).Take(size).OrderBy(x => x).ToList();

        foreach (int idx in indices)
        {
            switch (idx)
            {
                case 0:
                    actions.Add(new PlayerAction(PlayerActionType.AttackClass));
                    break;
                case 1:
                    actions.Add(new PlayerAction(PlayerActionType.MagicClass));
                    break;
                case 2:
                    actions.Add(new PlayerAction(PlayerActionType.DexterityClass));
                    break;
                default:
                    break;
            }
        };
    }
}

public sealed class PlayerActionManager
{
    private static readonly Lazy<PlayerActionManager> lazy =
        new Lazy<PlayerActionManager>(() => new PlayerActionManager());
    public static PlayerActionManager Instance { get { return lazy.Value; } }
    private PlayerActionManager() { }
};
