using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerManager
{
    private static readonly Lazy<PlayerManager> lazy = new Lazy<PlayerManager>(() => new PlayerManager());
    public static PlayerManager Instance { get { return lazy.Value; } }
    private PlayerManager() { }

    [Serializable]
    public class PlayerConfigurationData
    {
        public float maxHP;
    }

    [Serializable]
    public class PlayerStateData
    {
        public float HP;
    }

    //    public float Health
    //    {
    //       public get { return stateData.health; }
    //       private set { stateData.health = value; }
    //    }

    public static GameObject player = GameObject.Find("Player");
    public PlayerConfigurationData configuration;
    private PlayerStateData stateData;
    public Sprite playerCharacterPortrait = Resources.Load<Sprite>("Images/Io_Portrait");
    public bool isInDialog = false;
}