// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class StudyManager : MonoBehaviour
// {
//     // StudyAction is a tuple of stat changes, followed by coin increase.
//     // TODO: We're gonna want scriptable objects here.
//     private Dictionary<StudyOption, StudyAction> studyActionsData = new Dictionary<StudyOption, StudyAction>() {
//         {StudyOption.Swordsmanship, new StudyAction(new (Stat, int)[] {
//             (Stat.Strength, 4),
//             (Stat.Stress, 2)
//         }, -10)},

//         {StudyOption.Locksmithing, new StudyAction(new (Stat, int)[] {
//             (Stat.Dexterity, 2),
//             (Stat.Stress, 1)
//         }, -8)},

//         {StudyOption.Divination, new StudyAction(new (Stat, int)[] {
//             (Stat.Magic, 4),
//             (Stat.Stress, 3)
//         }, -15)},
//     };

//     private void OnEnable()
//     {
//         StudyDialogController.OnStudyOptionSelected += HandleStudyOptionSelected;
//     }

//     private void OnDisable()
//     {
//         StudyDialogController.OnStudyOptionSelected -= HandleStudyOptionSelected;
//     }

//     private void HandleStudyOptionSelected(StudyOption studyOption)
//     {
//         try
//         {
//             studyActionsData[studyOption].ExecuteStatChange();
//             // TODO: On turn complete?
//         }
//         catch (KeyNotFoundException)
//         {
//             Debug.LogError($"StudyManager :: Key \"{studyOption.ToString()}\" is not found.");
//         }
//     }
// }
