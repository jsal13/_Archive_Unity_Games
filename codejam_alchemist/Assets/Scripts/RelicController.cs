using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicController : MonoBehaviour
{
    public Relic relic;

    public delegate void OnRelicReaction(Potion.ReactionNames reactionName);
    public static event OnRelicReaction RelicReaction;

    private void OnEnable()
    {
        MixingContainerController.PourIntoBowl += HandlePourIntoBowl;
    }

    private void OnDisable()
    {
        MixingContainerController.PourIntoBowl -= HandlePourIntoBowl;
    }

    private void HandlePourIntoBowl(List<Potion> potionList)
    {
        foreach (RawMaterial rawMaterial in relic.materialComposition)
        {
            foreach (Potion potion in rawMaterial.reactivePotions)
            {
                if (potionList.Contains(potion))
                {
                    RelicReaction?.Invoke(potion.reactions[0]);
                }

            }
        }
    }
}


