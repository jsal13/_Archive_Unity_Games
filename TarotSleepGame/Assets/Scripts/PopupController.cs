using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject workOptionDialog;
    [SerializeField] private GameObject studyOptionDialog;
    [SerializeField] private GameObject mapDialog;

    private void OnEnable()
    {
        TurnManager.OnActionTypeChoice += HandleActionTypeChoice;
        TurnActionController.OnTurnActionComplete += HandleTurnActionComplete;
    }

    private void OnDisable()
    {
        TurnManager.OnActionTypeChoice -= HandleActionTypeChoice;
        TurnActionController.OnTurnActionComplete -= HandleTurnActionComplete;
    }

    private void HandleActionTypeChoice(ActionType actionType)
    {
        switch (actionType)
        {

            case (ActionType.Rest):
                break;

            case (ActionType.Work):
                workOptionDialog.SetActive(true);
                break;

            case (ActionType.Study):
                studyOptionDialog.SetActive(true);
                break;

            case (ActionType.Travel):
                mapDialog.SetActive(true);
                break;

            default:
                Debug.LogError("PopupManager: actionType not known ActionType.");
                break;
        }
    }

    private void HandleTurnActionComplete()
    {
        // Close all possible open windows.
        workOptionDialog.SetActive(false);
        studyOptionDialog.SetActive(false);
        mapDialog.SetActive(false);
        TurnManager.TurnNumber += 1;
    }
}
