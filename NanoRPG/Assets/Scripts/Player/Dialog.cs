using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

// Put this on the player.
public class Dialog : MonoBehaviour
{
    [SerializeField] private List<GameObject> speakerList;
    [SerializeField] private InputAction dialogAction;

    public delegate void NextToNPC(bool value);
    public static NextToNPC OnNextToNPC;
    private DialogueRunner dialogSystem;

    private void Awake()
    {
        dialogSystem = GameObject.Find("Dialog/DialogueSystem").GetComponent<DialogueRunner>();

        dialogAction.performed += OnDialogAction;
    }

    private void OnEnable()
    {
        dialogAction.Enable();
    }

    private void OnDisable()
    {
        dialogAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ISpeaker>() != null)
        {
            speakerList.Add(other.gameObject);
            OnNextToNPC?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ISpeaker>() != null)
        {
            speakerList.Remove(other.gameObject);
            if (speakerList.Count == 0)
            {
                OnNextToNPC?.Invoke(false);
            }
        }
    }

    private void OnDialogAction(InputAction.CallbackContext obj)
    {
        if (speakerList.Count > 0 && !PlayerManager.Instance.IsInDialog)
        {
            NPCController speakerInfo = speakerList[0].GetComponent<NPCController>();
            dialogSystem.StartDialogue(speakerInfo.GetCharacterName());

            PlayerManager.Instance.IsInDialog = true;
        }
    }

    public void OnDialogEnd()
    {
        PlayerManager.Instance.IsInDialog = false;
    }


}