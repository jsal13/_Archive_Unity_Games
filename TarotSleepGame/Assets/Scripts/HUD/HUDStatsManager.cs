using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDStatsManager : MonoBehaviour
{
    private Transform skillPanel;
    private Transform bioPanel;

    // Stats which are contained in either the skill or the bio panel.
    private List<Stat> skillPanelStats = new List<Stat>() {
        Stat.Strength,
        Stat.Dexterity,
        Stat.Magic,
    };
    private List<Stat> bioPanelStats = new List<Stat>() {
        Stat.Coin,
        Stat.Health,
        Stat.Stress,
    };

    private void Awake()
    {
        // TODO: Get rid of these explicit paths.  Why isn't dropping in inspector working?

        skillPanel = GameObject.Find("HUD/Skill_Panel").transform;
        bioPanel = GameObject.Find("HUD/Right_Panel/Bio_Panel").transform;
    }

    private void OnEnable()
    {
        Statistic.OnStatValueChange += HandleStatValueChange;
    }

    private void OnDisable()
    {
        Statistic.OnStatValueChange -= HandleStatValueChange;
    }

    private void HandleStatValueChange(Stat statName, int val)
    {
        bool isInSkillPanel = skillPanelStats.Contains(statName);
        bool isInBioPanel = bioPanelStats.Contains(statName);

        if (isInSkillPanel)
        {
            skillPanel.Find($"{statName.ToString()}/Value").GetComponent<TMP_Text>().text = val.ToString();
        }
        else if (isInBioPanel)
        {
            bioPanel.Find($"{statName.ToString()}/Value").GetComponent<TMP_Text>().text = val.ToString();
        }
        else
        {
            Debug.LogError($"HUDStatsManager :: {statName.ToString()} cannot be found in skill or bio panel.");
        }
    }
}
