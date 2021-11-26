using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class DialogBoxContoller : MonoBehaviour
{
    private GameObject dialogBoxObj;
    private VisualElement root;
    private VisualElement portraitUI;
    private TextElement nameUI;
    private TextElement textUI;

    private void Start()
    {
        dialogBoxObj = transform.Find("Dialog").gameObject;
        root = dialogBoxObj.GetComponent<UIDocument>().rootVisualElement;
        portraitUI = root.Q("CharacterPortrait");
        nameUI = root.Q<TextElement>("Name");
        textUI = root.Q<TextElement>("Dialog");
    }

    private void OnEnable()
    {
        DialogState.OnBeginDialog += HandleBeginDialog;
        DialogState.OnBeginDialogCycle += HandleBeginDialogCycle;
        DialogState.OnEndDialog += HandleEndDialog;
    }

    private void OnDisable()
    {
        DialogState.OnBeginDialog -= HandleBeginDialog;
        DialogState.OnBeginDialogCycle -= HandleBeginDialogCycle;
        DialogState.OnEndDialog -= HandleEndDialog;
    }

    private void HandleBeginDialog()
    {
        root.Q("DialogContainer").style.visibility = Visibility.Visible;
    }

    private void HandleBeginDialogCycle(NPCController npc)
    {
        StartCoroutine(DialogCycle(npc));
    }

    IEnumerator DialogCycle(NPCController npc)
    {
        List<string> dialog = npc.GetDialog();
        Sprite characterPortrait = npc.GetCharacterPortrait();

        for (int idx = 0; idx < dialog.Count; idx += 1)
        {
            // Sentence is like "Speaker|Text Goes Here...".  Split this on |.
            string txt = dialog[idx];
            string[] txtSplit = txt.Split('|');
            string speaker = txtSplit[0];
            string sentence = txtSplit[1];

            Sprite speakerPortrait = speaker == "Io" ? PlayerManager.Instance.playerCharacterPortrait : characterPortrait;
            portraitUI.style.backgroundImage = new StyleBackground(speakerPortrait);
            nameUI.text = speaker;

            yield return null; // Skip a frame;

            Coroutine typing = StartCoroutine(TypeSentence(sentence));
            yield return new WaitUntil(() => GameManager.gamepad.buttonWest.wasPressedThisFrame);
            StopCoroutine(typing);
        }
        PlayerManager.Instance.isInDialog = false;
    }

    IEnumerator TypeSentence(string sentence)
    {
        textUI.text = "";
        yield return new WaitForSeconds(0.1f);
        foreach (char letter in sentence.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void HandleEndDialog()
    {
        root.Q("DialogContainer").style.visibility = Visibility.Hidden;
        nameUI.text = "";
        textUI.text = "";
    }

}




// private void EndDialog()
// {
//     IsSpeaking = false;
//     canSpeak = true;

//     if (!string.IsNullOrWhiteSpace(targetNPCInfo.inventoryType))
//     {
//         OnPlayerTrading?.Invoke(targetNPCInfo);
//     }
// }