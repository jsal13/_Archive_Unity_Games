using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PersistenceManager
{
    private PersistenceManager() { }

    public class Resource
    {
        public int Owns { get; set; }
        public int Cap { get; set; }

        public Resource(int Owns, int Cap)
        {
            this.Owns = Owns;
            this.Cap = Cap;
        }
    }

    public class Checkpoint
    {
        public Vector3 loc;
        public string sceneName;

        public Checkpoint(Vector3 loc, string sceneName)
        {
            this.loc = loc;
            this.sceneName = sceneName;
        }
    }

    private static readonly Lazy<PersistenceManager> lazy = new Lazy<PersistenceManager>(() => new PersistenceManager());
    public static PersistenceManager Instance
    {
        get => lazy.Value;
    }

    public float currentPlayerTemperature = 80f;
    public int maxCountJump = 1;

    public Dictionary<string, Resource> resourceDict = new Dictionary<string, Resource>() {
        { "Coin", new Resource(0, 20) },
        { "Wood", new Resource(6, 12) },
        { "Wool", new Resource(4, 4) },
    };

    public Checkpoint checkpoint = new Checkpoint(new Vector3(0,0,0), "0_Mimas_1/0_Mimas_1_L1");

    // Upgrades.
    public bool jumpBoots = false;


    // Level Loading.
    public static Dictionary<string, Vector3> levelStartLocs = new Dictionary<string, Vector3>()
    {
        { "_Debug_Mimas1_ByDoor", new Vector3 (0, 0, 0) },
        { "0_Mimas_1_L0", new Vector3(-54.0f, 0f, 0) },
        { "0_Mimas_1_L1", new Vector3(17f, -37.8f, 0) }
    };
    
    // Transitions
    public static Dictionary<string, Vector3> transitionLocations = new Dictionary<string, Vector3>()
    {
        { "", new Vector3 (0, 0, 0) },
        { "0_Mimas_1_L0 East", new Vector3(132.5f, -18f, 0) },
        { "0_Mimas_1_L1 Top-Left", new Vector3(-0.5f, 0f, 0f) }
    };

    public static Vector3 initLevelLocation;


    // Player Obstruction Layers.
    public static ContactFilter2D contactFilter;
    public static int ObstructionLayer { get; set; }

    public static ContactFilter2D GetPlayerToGroundContactFilter() {
        ObstructionLayer = GetPlayerToGroundObstructionLayer();
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(ObstructionLayer);
        contactFilter.useNormalAngle = true;
        contactFilter.SetNormalAngle(89.995f, 90.005f);

        return contactFilter;
    }

    public static int GetPlayerToGroundObstructionLayer()
    {
        ObstructionLayer = 1 << LayerMask.NameToLayer("Obstruction");
        return ObstructionLayer;
    }



}
