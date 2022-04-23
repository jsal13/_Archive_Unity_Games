using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;

public class DebugEditor : OdinEditorWindow
{

    [MenuItem("GameDebug/Debug")]
    private static void OpenWindow()
    {
        GetWindow<DebugEditor>().Show();
    }

    [VerticalGroup("StatSplit")]
    [BoxGroup("StatSplit/PlayerStats"), ShowInInspector, PropertyRange(0, 100)]
    public int Health
    {
        get => PlayerManager.health.Value;
        set
        {
            PlayerManager.health.Value = value;
        }
    }

    [BoxGroup("StatSplit/PlayerStats"), ShowInInspector, PropertyRange(0, 100)]
    public int Attack
    {
        get => PlayerManager.attack.Value;
        set
        {
            PlayerManager.attack.Value = value;
        }
    }

    [BoxGroup("StatSplit/PlayerStats"), ShowInInspector, PropertyRange(0, 100)]
    public int Magic
    {
        get => PlayerManager.magic.Value;
        set
        {
            PlayerManager.magic.Value = value;
        }
    }

    [BoxGroup("StatSplit/PlayerStats"), ShowInInspector, PropertyRange(0, 100)]
    public int Dexterity
    {
        get => PlayerManager.dexterity.Value;
        set
        {
            PlayerManager.dexterity.Value = value;
        }
    }

    [VerticalGroup("StatSplit")]
    [BoxGroup("StatSplit/DateStats"), ShowInInspector, PropertyRange(0, 29)]
    public int Day
    {
        get => DateManager.Day;
        set
        {
            DateManager.Day = value;
        }
    }
    [BoxGroup("StatSplit/DateStats"), ShowInInspector, PropertyRange(0, 3)]
    public int Season
    {
        get => DateManager.SeasonIDX;
        set
        {
            DateManager.SeasonIDX = value;
        }
    }

    [VerticalGroup("Map")]
    [BoxGroup("Map/Nodes"), Button("Randomize Node Location")]
    public void RandomizeNodeLocation()
    {
        NodeController.OnRefreshMapNodes?.Invoke();
    }


}