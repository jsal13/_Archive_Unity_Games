using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager
{
    private static readonly Lazy<GameManager> lazy = new Lazy<GameManager>(() => new GameManager());
    public static GameManager Instance { get { return lazy.Value; } }
    private GameManager() { }

    public static Dictionary<string, int> layerDict = new Dictionary<string, int>() {
        {"Obstruction", LayerMask.GetMask("Obstruction")}
    };

    public GameObject placementIndicatorPrefab = Resources.Load<GameObject>("Prefabs/PlacementIndicator");

    public GameObject currentSelectedBuildingPrefab;
    public string _currentSelectedBuilding;
    public string CurrentSelectedBuilding
    {
        get => _currentSelectedBuilding;
        set
        {
            _currentSelectedBuilding = value;
            currentSelectedBuildingPrefab = Resources.Load<GameObject>($"Prefabs/{_currentSelectedBuilding}");
        }
    }
}