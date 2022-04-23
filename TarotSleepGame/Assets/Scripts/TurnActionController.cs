using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TurnActionController : MonoBehaviour
{
    public static Action OnTurnActionComplete;

    private void Start()
    {
        GameManager.dialogueRunner.AddCommandHandler(
            "end_turn_action", EndTurnAction
        );
    }

    private void OnEnable()
    {
        ActionOptionController.OnTurnOptionChoice += HandleTurnOptionChoice;
    }

    private void OnDisable()
    {
        ActionOptionController.OnTurnOptionChoice -= HandleTurnOptionChoice;
    }

    private void HandleTurnOptionChoice(TurnAction turnAction)
    {
        // TODO: Can it be executed?
        // TODO: What if it can't be executed?

        // Yarn node names will look like: Work_Name_of_Job.
        GameManager.dialogueRunner.StartDialogue($"{turnAction.actionType.ToString()}_{turnAction.actionName.Replace(' ', '_')}");

        // Remove or Add Coin, then Change Stats.
        PlayerManager.stats.AddToStatistic(Stat.Coin, turnAction.actionCoinCost);
        ExecuteStatChanges(turnAction.actionStatChange);
    }

    private void ExecuteStatChanges(Dictionary<Stat, int> statChanges)
    {
        foreach (KeyValuePair<Stat, int> statChange in statChanges)
        {
            PlayerManager.stats.AddToStatistic(statChange.Key, statChange.Value);
        }
    }

    [YarnCommand("end_turn_action")]
    public void EndTurnAction()
    {
        OnTurnActionComplete?.Invoke();
    }

}
