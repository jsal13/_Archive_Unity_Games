using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStatsController : MonoBehaviour
{
    private VisualElement root;

    private Label health;
    private Label attack;
    private Label magic;
    private Label dexterity;
    private Label gold;

    private Label season;
    private Label dayOfSeason;

    private Button btnIncrHealth;
    private Button btnDecrAttack;
    private Button btnIncrMagic;
    private Button btnIncrDexterity;
    private Button btnDay;

    private Dictionary<string, Label> statMap;

    private void Awake()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;

        // Query for Labels.
        health = root.Q<Label>("lbl-health-value");
        attack = root.Q<Label>("lbl-attack-value");
        magic = root.Q<Label>("lbl-magic-value");
        dexterity = root.Q<Label>("lbl-dexterity-value");
        gold = root.Q<Label>("lbl-gold-value");

        season = root.Q<Label>("lbl-season");
        dayOfSeason = root.Q<Label>("lbl-day-of-season");

        // Query for Buttons.
        btnIncrHealth = root.Q<Button>("btn-health");
        btnDecrAttack = root.Q<Button>("btn-attack");
        btnIncrMagic = root.Q<Button>("btn-magic");
        btnIncrDexterity = root.Q<Button>("btn-dexterity");
        btnDay = root.Q<Button>("btn-day-of-season");

        statMap = new Dictionary<string, Label>(){
            {"gold", gold},
            { "health", health },
            { "attack", attack },
            { "magic", magic },
            { "dexterity", dexterity }
        };
    }

    private void OnEnable()
    {
        Stat.OnStatChange += HandleStatChange;
        DateManager.OnDateChange += HandleDayChange;
    }

    private void OnDisable()
    {
        Stat.OnStatChange -= HandleStatChange;
        DateManager.OnDateChange -= HandleDayChange;
    }

    private void HandleStatChange(string statName, int value)
    {
        Label valueLabel = statMap[statName];
        valueLabel.text = value.ToString();
    }

    private void HandleDayChange()
    {
        season.text = DateManager.season;
        dayOfSeason.text = (DateManager.Day + 1).ToString();
    }

}
