using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Put this on the player.
public class Dialog : MonoBehaviour
{
    [SerializeField] private List<GameObject> speakerList;
    [SerializeField] private InputAction dialogAction;


    private void OnEnable()
    {
        dialogAction.Enable();
    }

    private void OnDisable()
    {
        dialogAction.Disable();
    }

    private void Awake()
    {
        dialogAction.performed += OnDialogAction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDialogable>() != null)
        {
            speakerList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IDialogable>() != null)
        {
            speakerList.Remove(other.gameObject);
        }
    }

    private void OnDialogAction(InputAction.CallbackContext obj)
    {

        if (speakerList.Count > 0 && !PlayerManager.Instance.isInDialog)
        {
            NPCController speakerInfo = speakerList[0].GetComponent<NPCController>();
            DialogStateMachine dsm = gameObject.AddComponent<DialogStateMachine>();
            dsm.npc = speakerInfo;
            PlayerManager.Instance.isInDialog = true;
        }
    }


}