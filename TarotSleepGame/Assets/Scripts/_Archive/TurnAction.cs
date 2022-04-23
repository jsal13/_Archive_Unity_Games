// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TurnAction2
// {
//     protected (Stat stat, int change)[] statChangeTuples;
//     protected int coinChange;
//     public static Action OnActionExecuted;

//     protected TurnAction(statChangeTuples, int coinChange)
//     {
//         this.statChangeTuples = statChangeTuples;
//         this.coinChange = coinChange;
//     }

//     public void ExecuteStatChange()
//     {
//         PlayerManager.stats.AddToStatistic(Stat.Coin, coinChange);
//         foreach (var statChangeTuple in statChangeTuples)
//         {
//             PlayerManager.stats.AddToStatistic(statChangeTuple.stat, statChangeTuple.change);
//         }
//     }
// }
