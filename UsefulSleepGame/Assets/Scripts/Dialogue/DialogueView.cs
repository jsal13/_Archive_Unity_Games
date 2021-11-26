using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InterruptionFlag
{
    public bool Interrupted { get; private set; } = false;
    public void Set() => Interrupted = true;
    public void Clear() => Interrupted = false;
}

public class DialogueView : DialogueViewBase
{
    [SerializeField] private InputAction continueAction;

    [SerializeField] private GameObject dialogueUIObj;
    [SerializeField] private TextMeshProUGUI lineText = null;
    [SerializeField] private TextMeshProUGUI characterNameText = null;
    [SerializeField] private Image characterPortrait;

    [SerializeField] private InterruptionFlag interruptionFlag = new InterruptionFlag();

    private LocalizedLine currentLine = null;
    [SerializeField] private bool userReleasedTalkButton = false;

    public delegate void InDialogue(bool value);
    public static InDialogue OnInDialogue;


    // OPTIONS
    private GameObject optionsContainer;
    [SerializeField] private GameObject optionsContainerPrefab;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private List<TMP_Text> optionTextList;
    [SerializeField] private int currentOptionIdx = 0;
    [SerializeField] private float optionScrollWait = 0.25f;
    private int numOptions;

    private Action<int> OnOptionSelected;


    private void OnEnable()
    {
        continueAction.performed += HandleContinueAction;
        continueAction.canceled += HandleCanceledAction;
    }

    private void OnDisable()
    {
        continueAction.performed -= HandleContinueAction;
        continueAction.canceled -= HandleCanceledAction;
    }

    private void HandleCanceledAction(InputAction.CallbackContext obj)
    {
        if (!userReleasedTalkButton)
        {
            userReleasedTalkButton = true;
        }
    }

    private void HandleContinueAction(InputAction.CallbackContext obj)
    {
        if (!userReleasedTalkButton) { return; }

        if (currentLine == null)
        {
            return;
        }
        ReadyForNextLine();
    }

    // Overriding DialogueBaseView.

    public override void DialogueStarted()
    {
        Debug.Log("Started dialogue...");
        dialogueUIObj.SetActive(true);
        PlayerManager.IsSpeaking = true;
        OnInDialogue?.Invoke(true);
        continueAction.Enable();

    }

    public override void DialogueComplete()
    {
        continueAction.Disable();
        userReleasedTalkButton = false;
        PlayerManager.IsSpeaking = false;
        OnInDialogue?.Invoke(false);
        dialogueUIObj.SetActive(false);
    }

    public override void DismissLine(Action onDismissalComplete)
    {
        currentLine = null;
        onDismissalComplete();
    }


    public override void NodeComplete(string nextNode, Action onComplete)
    {
        //
    }

    public override void OnLineStatusChanged(LocalizedLine dialogueLine)
    {
        switch (dialogueLine.Status)
        {
            case LineStatus.Presenting:
                break;
            case LineStatus.Interrupted:
                interruptionFlag.Set();
                break;
            case LineStatus.FinishedPresenting:
                break;
            case LineStatus.Dismissed:
                break;
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        currentLine = dialogueLine;

        interruptionFlag.Clear();

        string charName = dialogueLine.CharacterName;
        characterNameText.text = charName;
        characterPortrait.sprite = DialogueManager.GetCharacterPortrait(charName);

        lineText.text = dialogueLine.TextWithoutCharacterName.Text;

        onDialogueLineFinished();
    }

    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        optionsContainer = Instantiate(optionsContainerPrefab, dialogueUIObj.transform);

        numOptions = dialogueOptions.Length;

        for (int idx = 0; idx < numOptions; idx += 1)
        {
            TMP_Text optionText = Instantiate(optionPrefab, optionsContainer.transform).GetComponentInChildren<TMP_Text>();

            optionText.text = dialogueOptions[idx].Line.Text.Text;

            // Add this to a list to highlight easier below.
            optionTextList.Add(optionText);
        }

        // Gets the user's selection and does the action.
        StartCoroutine(GetPlayerOptionSelection(onOptionSelected));
    }

    private IEnumerator GetPlayerOptionSelection(Action<int> onOptionSelected)
    {
        UpdateOptionSelection();
        yield return new WaitForSeconds(optionScrollWait);

        while (true)
        {
            // TODO: Integrate this with continueAction button?            
            if (GameManager.gamepad.buttonWest.wasPressedThisFrame)
            {
                break;
            }

            Vector2 dir = GameManager.gamepad.leftStick.ReadValue();

            if (dir.y > 0.5)
            {
                // Selection moves up.
                currentOptionIdx = currentOptionIdx == 0 ? numOptions - 1 : currentOptionIdx - 1;
                UpdateOptionSelection();
                yield return new WaitForSeconds(optionScrollWait);
            }
            else if (dir.y < -0.5)
            {
                // Selection moves down.
                currentOptionIdx = (currentOptionIdx + 1) % numOptions;
                UpdateOptionSelection();
                yield return new WaitForSeconds(optionScrollWait);
            }

            yield return null;
        }

        yield return null;
        Destroy(optionsContainer);
        optionTextList = new List<TMP_Text>();
        onOptionSelected?.Invoke(currentOptionIdx);
        yield return null;
    }

    private void UpdateOptionSelection()
    {
        for (int idx = 0; idx < optionTextList.Count; idx += 1)
        {
            if (idx == currentOptionIdx)
            {
                optionTextList[idx].color = new Color(0, 0.49f, 1);
            }
            else
            {
                optionTextList[idx].color = Color.white;
            }
        }
    }
}