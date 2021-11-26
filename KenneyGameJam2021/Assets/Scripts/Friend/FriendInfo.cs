using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FriendInfo", menuName = "KenneyJam2021/FriendInfo", order = 0)]
public class FriendInfo : ScriptableObject
{
    public string friendType;
    public int healthRestore;
    public int attackAdd;
    public int goldAdd;
}

