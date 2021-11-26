using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string type;
    public string inventoryType;
    public string dialogType;
    public NPC NPCInfo;
    public class NPC
    {
        public string type;
        public string inventoryType;
        public string dialogType;

        public NPC(string type, string inventoryType, string dialogType)
        {
            this.type = type;
            this.inventoryType = inventoryType;
            this.dialogType = dialogType;
        }
    }

    private void Awake()
    {
        NPCInfo = new NPC(type, inventoryType, dialogType);
    }
}
