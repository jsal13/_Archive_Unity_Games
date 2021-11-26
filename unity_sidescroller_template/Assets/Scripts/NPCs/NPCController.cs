using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IDialogable
{
    public NPCInfo npcInfo;

    public string GetDialogType()
    {
        return npcInfo.dialogType;
    }

    public Sprite GetCharacterPortrait()
    {
        return npcInfo.characterPortrait;
    }

    public List<string> GetDialog()
    {
        return npcInfo.dialog;
    }

}
