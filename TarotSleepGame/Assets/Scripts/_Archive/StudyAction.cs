// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class StudyAction : TurnAction
// {
//     public StudyAction((Stat stat, int change)[] statChangeTuples, int coinChange) : base(statChangeTuples, coinChange) { }

//     public override bool CanExecuteAction()
//     {
//         return PlayerManager.stats.GetStatistic(Stat.Coin).Value >= coinChange;
//     }

//     public override void PostStatAction() { }
//     public override void PreStatAction() { }
// }
