using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStatsController : MonoBehaviour
{
    private VisualElement root;

    private Label health;
    private Label food;
    private Label water;
    private Label salt;
    private Label wood;

    private Dictionary<string, Label> statMap;
    private Dictionary<string, Label> itemMap;

    private void Awake()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;

        // Query for Labels.
        health = root.Query<VisualElement>("HealthContainer").Children<Label>("Value").First();
        food = root.Query<VisualElement>("FoodContainer").Children<Label>("Value").First();
        water = root.Query<VisualElement>("WaterContainer").Children<Label>("Value").First();
        salt = root.Query<VisualElement>("SaltContainer").Children<Label>("Value").First();
        wood = root.Query<VisualElement>("WoodContainer").Children<Label>("Value").First();

        statMap = new Dictionary<string, Label>(){
            { "health", health },
        };

        itemMap = new Dictionary<string, Label>()
        {
            { "food", food },
            { "water", water },
            { "salt", salt },
            { "wood", wood },
        };

    }

    private void OnEnable()
    {
        Stat.OnStatChange += HandleStatChange;
        Item.OnItemChange += HandleItemChange;
    }

    private void OnDisable()
    {
        Stat.OnStatChange -= HandleStatChange;
        Item.OnItemChange -= HandleItemChange;
    }

    private void HandleStatChange(string statName, int value)
    {
        Label valueLabel = statMap[statName];
        valueLabel.text = value.ToString();
    }

    private void HandleItemChange(string itemName, int value)
    {
        Label valueLabel = itemMap[itemName];
        valueLabel.text = value.ToString();
    }

}