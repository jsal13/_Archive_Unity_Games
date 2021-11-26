using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;


public class Dialogue : MonoBehaviour
{
    [SerializeField] private InputAction dialogueAction;

    [SerializeField] private List<GameObject> speakerList;
    [SerializeField] private bool _isNearSpeaker;
    private GameObject currentSpeaker;

    public bool IsNearSpeaker
    {
        get => _isNearSpeaker;
        set
        {
            _isNearSpeaker = value;
            PlayerManager.canAttack = !_isNearSpeaker;
        }
    }

    private void OnEnable()
    {
        dialogueAction.Enable();
        dialogueAction.performed += HandleDialogueAction;
        DialogueView.OnInDialogue += HandleInDialogue;
    }

    private void OnDisable()
    {
        dialogueAction.Disable();
        dialogueAction.performed -= HandleDialogueAction;
        DialogueView.OnInDialogue -= HandleInDialogue;
    }

    private void HandleInDialogue(bool value)
    {
        if (!value)
        {
            TradeInventory currentInventory = currentSpeaker.GetComponent<NPCController>().info.inventory;

            // If the dialogue has ended...
            if (currentInventory != null && currentInventory.inventory.Count > 0)
            {
                // If they're a trader...
                TradeManager.ToggleTradeDialog(currentInventory.inventory, true);

            }
        }
    }

    private void HandleDialogueAction(InputAction.CallbackContext obj)
    {
        if (PlayerManager.canSpeak && IsNearSpeaker && !PlayerManager.IsSpeaking && PlayerManager.isOnGround)
        {
            currentSpeaker = speakerList[0];
            GameObject.FindObjectOfType<DialogueRunner>().StartDialogue(currentSpeaker.GetComponent<NPCController>().info.yarnNodeName);
        }
    }

    private void Awake()
    {
        speakerList = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ISpeakable>() != null)
        {
            speakerList.Add(other.gameObject);
            IsNearSpeaker = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ISpeakable>() != null)
        {
            speakerList.Remove(other.gameObject);
            if (speakerList.Count == 0)
            {
                IsNearSpeaker = false;
            }
        }
    }
}
