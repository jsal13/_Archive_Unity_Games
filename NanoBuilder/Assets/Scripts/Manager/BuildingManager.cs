using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class BuildingManager
{
    private static readonly Lazy<BuildingManager> lazy = new Lazy<BuildingManager>(() => new BuildingManager());
    public static BuildingManager Instance { get { return lazy.Value; } }
    private BuildingManager() { }

    public Tile obstructionTile = Resources.Load<Tile>("Tilesets/Obstruction");
    public Tilemap obstructionTilemap = GameObject.Find("Grid/Obstruction").GetComponent<Tilemap>();
    public Grid worldGrid = GameObject.Find("Grid").GetComponent<Grid>();
}