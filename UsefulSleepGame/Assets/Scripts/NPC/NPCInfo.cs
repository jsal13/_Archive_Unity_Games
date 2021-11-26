using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCInfo", menuName = "UsefulSleep/NPCInfo", order = 0)]
public class NPCInfo : ScriptableObject
{
    public string yarnNodeName;
    public TradeInventory inventory;
}
