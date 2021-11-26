using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RawMaterial", menuName = "RawMaterial")]
public class RawMaterial : ScriptableObject
{

    public new string name;
    public Sprite sprite;

    public List<Potion> reactivePotions;
}