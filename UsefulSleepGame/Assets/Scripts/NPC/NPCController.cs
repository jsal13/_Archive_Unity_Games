using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NPCController : MonoBehaviour, ISpeakable
{
    [InlineEditor(Expanded = true)]
    public NPCInfo info;
}