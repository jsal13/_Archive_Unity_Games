using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionController : MonoBehaviour
{
    [SerializeField] private GameObject relic;
    private List<IReaction> reactionList;
    [SerializeField] private List<string> reactionListStr;

    private void Awake()
    {
        reactionList = new List<IReaction>();
        reactionListStr = new List<string>();
    }

    private void OnEnable()
    {
        RelicController.RelicReaction += HandleRelicReaction;
        BowlController.BowlFull += HandleBowlFull;
    }

    private void OnDisable()
    {
        RelicController.RelicReaction -= HandleRelicReaction;
        BowlController.BowlFull -= HandleBowlFull;
    }

    private void GetCurrentRelic()
    {
        relic = GameObject.Find("Relic");
    }

    private void HandleRelicReaction(Potion.ReactionNames reactionName)
    {
        GetCurrentRelic();
        IReaction rct = null;
        string rName = reactionName.ToString();

        if (!reactionListStr.Contains(rName))
        {
            reactionListStr.Add(rName);

            switch (rName)
            {
                case nameof(Glows):
                    rct = relic.AddComponent<Glows>();
                    break;
                case nameof(Bubbles):
                    rct = relic.AddComponent<Bubbles>();
                    break;
                case nameof(SolutionColorToClear):
                    rct = relic.AddComponent<SolutionColorToClear>();
                    break;
                case nameof(Sparkles):
                    rct =
                    rct = relic.AddComponent<Sparkles>();
                    break;
                default:
                    Debug.LogError($"There is no reaction: {rName}.");
                    break;
            }

            reactionList.Add(rct);
            rct.DoEffect();
        }
    }

    private void HandleBowlFull(bool value)
    {
        // If we empty the bowl, remove all reactions.
        if (!value)
        {
            foreach (IReaction reaction in reactionList)
            {
                reaction.ReverseEffect();
            }
            reactionList = new List<IReaction>();
            reactionListStr = new List<string>();
        }
    }
}
