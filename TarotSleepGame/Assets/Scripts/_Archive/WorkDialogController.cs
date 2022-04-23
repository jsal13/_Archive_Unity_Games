// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class WorkDialogController : MonoBehaviour
// {
//     // NOTE: Can't use Enum due to: Dropdown in Button UI.
//     public static event Action<WorkOption> OnWorkOptionSelected;

//     public void PlayerWorkChoice(string workOptionStr)
//     {
//         // TODO?: To get around not being able to pass in an Enum to a button,
//         // we pass in a string and look for an enum with that value.  Gross.
//         WorkOption workOption = Enum.GetValues(typeof(WorkOption))
//             .Cast<WorkOption>()
//             .First<WorkOption>(x => x.ToString() == workOptionStr);

//         OnWorkOptionSelected?.Invoke(workOption);
//     }
// }
