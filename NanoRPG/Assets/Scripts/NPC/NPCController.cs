using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, ISpeaker
{
    public NPCInfo npcInfo;

    public Sprite GetCharacterPortrait()
    {
        return npcInfo.characterPortrait;
    }

    public string GetCharacterName()
    {
        return npcInfo.characterName;
    }

    public string GetCharacterDisplayName()
    {
        return npcInfo.characterDisplayName;
    }

    public bool IsMerchant()
    {
        return npcInfo.isMerchant;
    }

}
