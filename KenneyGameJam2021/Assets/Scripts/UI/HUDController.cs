using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text healthValue;
    [SerializeField] private TMP_Text attackValue;
    [SerializeField] private TMP_Text goldValue;
    [SerializeField] private TMP_Text levelValue;

    private void Awake()
    {
        healthValue = transform.Find("UpperLeftDiv/Health/Value").GetComponent<TMP_Text>();
        attackValue = transform.Find("UpperLeftDiv/Attack/Value").GetComponent<TMP_Text>();
        goldValue = transform.Find("UpperLeftDiv/Gold/Value").GetComponent<TMP_Text>();
        levelValue = transform.Find("UpperLeftDiv/Level/Value").GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerAttackPowerChange += HandlePlayerAttackPowerChange;
        PlayerManager.OnPlayerHealthChange += HandlePlayerHealthChange;
        PlayerManager.OnPlayerGoldChange += HandlePlayerGoldChange;
        LevelManager.OnLevelChange += HandleLevelChange;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerAttackPowerChange -= HandlePlayerAttackPowerChange;
        PlayerManager.OnPlayerHealthChange -= HandlePlayerHealthChange;
        PlayerManager.OnPlayerGoldChange -= HandlePlayerGoldChange;
        LevelManager.OnLevelChange -= HandleLevelChange;
    }

    private void HandleLevelChange(int newVal)
    {
        levelValue.text = newVal.ToString();
    }

    private void HandlePlayerHealthChange(int newVal)
    {
        healthValue.text = newVal.ToString();
    }

    private void HandlePlayerAttackPowerChange(int newVal)
    {
        attackValue.text = newVal.ToString();
    }

    private void HandlePlayerGoldChange(int newVal)
    {
        goldValue.text = newVal.ToString();
    }
}
