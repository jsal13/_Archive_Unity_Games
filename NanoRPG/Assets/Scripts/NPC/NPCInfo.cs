using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCInfo", menuName = "ScriptableObjects/NPCInfo", order = 1)]
public class NPCInfo : ScriptableObject
{
    public string characterName;
    public string characterDisplayName;
    public Sprite characterPortrait;
    public bool isMerchant;

}