// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class StudyDialogController : MonoBehaviour
// {
//     // NOTE: Can't use Enum due to: Dropdown in Button UI.
//     public static event Action<StudyOption> OnStudyOptionSelected;

//     public void PlayerStudyChoice(string studyOptionStr)
//     {

//         // TODO?: To get around not being able to pass in an Enum to a button,
//         // we pass in a string and look for an enum with that value.  Gross.
//         StudyOption studyOption = Enum.GetValues(typeof(StudyOption))
//             .Cast<StudyOption>()
//             .First<StudyOption>(x => x.ToString() == studyOptionStr);

//         OnStudyOptionSelected?.Invoke(studyOption);
//     }
// }

