using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// TODO: Maybe Study/Work/Rest action for the name?  Something.

[CreateAssetMenu(menuName = "TurnActions", order = 1)]
[InlineEditor]
public class TurnAction : SerializedScriptableObject
{
    public string actionName;
    public ActionType actionType;
    public Dictionary<Stat, int> actionStatChange;
    public int actionCoinCost;
}