using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "Relic")]
public class Relic : ScriptableObject
{
    public Sprite sprite;
    public List<RawMaterial> materialComposition;
}