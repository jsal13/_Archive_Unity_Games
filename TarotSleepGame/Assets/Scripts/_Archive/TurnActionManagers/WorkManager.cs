// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WorkManager : MonoBehaviour
// {
//     // Workaction is a tuple of stat changes, followed by coin increase.
//     // TODO: We're gonna want scriptable objects here.
//     private Dictionary<WorkOption, WorkAction> workActionsData = new Dictionary<WorkOption, WorkAction>() {
//         {WorkOption.NightGuard, new WorkAction(new (Stat, int)[] {
//             (Stat.Strength, 2),
//             (Stat.Stress, 4)
//         }, 10)},

//         {WorkOption.Locksmith, new WorkAction(new (Stat, int)[] {
//             (Stat.Dexterity, 2),
//             (Stat.Stress, 3)
//         }, 8)},

//         {WorkOption.TarotReader, new WorkAction(new (Stat, int)[] {
//             (Stat.Magic, 2),
//             (Stat.Stress, 4)
//         }, 20)},
//     };


//     private void OnEnable()
//     {
//         WorkDialogController.OnWorkOptionSelected += HandleWorkOptionSelected;
//     }

//     private void OnDisable()
//     {
//         WorkDialogController.OnWorkOptionSelected -= HandleWorkOptionSelected;
//     }

//     private void HandleWorkOptionSelected(WorkOption workOption)
//     {
//         try
//         {
//             workActionsData[workOption].ExecuteStatChange();
//             // TODO: On turn complete?
//         }
//         catch (KeyNotFoundException)
//         {
//             Debug.LogError($"WorkManager :: Key \"{workOption.ToString()}\" is not found.");
//         }
//     }
// }