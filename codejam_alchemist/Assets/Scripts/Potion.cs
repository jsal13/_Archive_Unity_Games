using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion")]
public class Potion : ScriptableObject
{
    public enum ReactionNames { Glows, Bubbles, SolutionColorToClear, Sparkles };

    public new string name;
    public Color color;

    public List<ReactionNames> reactions;
}
