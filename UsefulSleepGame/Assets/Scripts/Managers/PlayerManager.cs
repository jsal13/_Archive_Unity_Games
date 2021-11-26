using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public sealed class PlayerManager
{
    [HideInInspector]
    private static readonly Lazy<PlayerManager> lazy = new Lazy<PlayerManager>(() => new PlayerManager());

    [HideInInspector]
    public static PlayerManager Instance { get { return lazy.Value; } }

    private PlayerManager() { }

    [HideInInspector]
    public Sprite playerCharacterPortrait = Resources.Load<Sprite>("Images/Io_Portrait");

    [VerticalGroup("Base")]
    [TitleGroup("Base/PlayerState")]
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool canMove = true;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool canJump = true;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool canSpeak = true;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool canTrade = true;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool canAttack = true;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool canCraft = true;

    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool isOnGround;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool isInWater;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool isOnPlatform;

    [HideInInspector]
    private static bool isHit;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool IsHit
    {
        get => isHit;
        set
        {
            isHit = value;
            canMove = !isHit;
            canJump = !isHit;
            canSpeak = !isHit;
            canTrade = !isHit;
            canCraft = !isHit;
        }
    }

    [HideInInspector]
    private static bool _isSpeaking;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool IsSpeaking
    {
        get => _isSpeaking;
        set
        {
            _isSpeaking = value;
            canMove = !_isSpeaking;
            canJump = !_isSpeaking;
            canSpeak = !_isSpeaking;
            canTrade = !_isSpeaking;
            canCraft = !_isSpeaking;
        }
    }

    [HideInInspector]
    private static bool _isTrading;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool IsTrading
    {
        get => _isTrading;
        set
        {
            _isTrading = value;
            canMove = !_isTrading;
            canJump = !_isTrading;
            canSpeak = !_isTrading;
            canTrade = !_isTrading;
            canCraft = !_isTrading;
        }
    }

    [HideInInspector]
    public delegate void PlayerDeath();
    [HideInInspector]
    public static PlayerDeath OnPlayerDeath;

    [HideInInspector]
    private static bool _isDead;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool IsDead
    {
        get => _isDead;
        set
        {
            _isDead = value;
            canMove = !_isDead;
            canJump = !_isDead;
            canSpeak = !_isDead;
            canTrade = !_isDead;
            canCraft = !_isDead;

            if (_isDead)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }

    [HideInInspector]
    public delegate void PlayerTransition();
    [HideInInspector]
    public static PlayerTransition OnPlayerTransition;

    [HideInInspector]
    private static bool _isInTransition;
    [BoxGroup("Base/PlayerState/PhysicalState")]
    public static bool IsInTransition
    {
        get => _isInTransition;
        set
        {
            _isInTransition = value;
            canMove = !_isInTransition;
            canJump = !_isInTransition;
            canSpeak = !_isInTransition;
            canTrade = !_isInTransition;
            canCraft = !_isInTransition;
        }
    }


    // COIN
    [HideInInspector]
    public delegate void CoinChange(int newValue);
    [HideInInspector]
    public static CoinChange OnCoinChange;
    [HideInInspector]
    public delegate void MaxCoinChange(int newValue);
    [HideInInspector]
    public static MaxCoinChange OnMaxCoinChange;

    [HideInInspector]
    private static int _maxCoin;
    [TitleGroup("Base/Resources")]
    [HorizontalGroup("Base/Resources/Col"), PropertyOrder(-1)]
    [BoxGroup("Base/Resources/Col/Max Resource")]
    public static int MaxCoin
    {
        get => _maxCoin;
        set
        {
            _maxCoin = value;
            OnMaxCoinChange?.Invoke(_maxCoin);
        }
    }

    [HideInInspector]
    private static int _coin;
    [BoxGroup("Base/Resources/Col/Resource")]
    public static int Coin
    {
        get => _coin;
        set
        {
            _coin = Mathf.Clamp(value, 0, MaxCoin);
            OnCoinChange?.Invoke(_coin);
        }
    }

    // WOOD
    [HideInInspector]
    public delegate void WoodChange(int newValue);
    [HideInInspector]
    public static WoodChange OnWoodChange;
    [HideInInspector]
    public delegate void MaxWoodChange(int newValue);
    [HideInInspector]
    public static MaxWoodChange OnMaxWoodChange;

    [HideInInspector]
    private static int _maxWood;
    [BoxGroup("Base/Resources/Col/Max Resource")]
    public static int MaxWood
    {
        get => _maxWood;
        set
        {
            _maxWood = value;
            OnMaxWoodChange?.Invoke(_maxWood);
        }
    }

    [HideInInspector]
    private static int _wood;
    [BoxGroup("Base/Resources/Col/Resource")]
    public static int Wood
    {
        get => _wood;
        set
        {
            _wood = Mathf.Clamp(value, 0, MaxWood);
            OnWoodChange?.Invoke(_wood);
        }
    }

    // STONE
    [HideInInspector]
    public delegate void StoneChange(int newValue);
    [HideInInspector]
    public static StoneChange OnStoneChange;
    [HideInInspector]
    public delegate void MaxStoneChange(int newValue);
    [HideInInspector]
    public static MaxStoneChange OnMaxStoneChange;

    [HideInInspector]
    private static int _maxStone;
    [BoxGroup("Base/Resources/Col/Max Resource")]
    public static int MaxStone
    {
        get => _maxStone;
        set
        {
            _maxStone = value;
            OnMaxStoneChange?.Invoke(_maxStone);
        }
    }

    [HideInInspector]
    private static int _stone;
    [BoxGroup("Base/Resources/Col/Resource")]
    public static int Stone
    {
        get => _stone;
        set
        {
            _stone = Mathf.Clamp(value, 0, MaxStone);
            OnStoneChange?.Invoke(_stone);
        }
    }

    // WOOL
    [HideInInspector]
    public delegate void WoolChange(int newValue);
    [HideInInspector]
    public static WoolChange OnWoolChange;
    [HideInInspector]
    public delegate void MaxWoolChange(int newValue);
    [HideInInspector]
    public static MaxWoolChange OnMaxWoolChange;

    [HideInInspector]
    private static int _maxWool;
    [BoxGroup("Base/Resources/Col/Max Resource")]
    public static int MaxWool
    {
        get => _maxWool;
        set
        {
            _maxWool = value;
            OnMaxWoolChange?.Invoke(_maxWool);
        }
    }

    [HideInInspector]
    private static int _wool;
    [BoxGroup("Base/Resources/Col/Resource")]
    public static int Wool
    {
        get => _wool;
        set
        {
            _wool = Mathf.Clamp(value, 0, MaxWool);
            OnWoolChange?.Invoke(_wool);
        }
    }

    // TEMPERATURE
    [HideInInspector]
    public delegate void TemperatureChange(int newValue);
    [HideInInspector]
    public static TemperatureChange OnTemperatureChange;

    [BoxGroup("Base/PlayerState/Player State")]
    public static int ambientTemperature;

    [HideInInspector]
    private static int _temperature;
    [BoxGroup("Base/PlayerState/Player State")]
    public static int Temperature
    {
        get => _temperature;
        set
        {
            _temperature = Mathf.Clamp(value, 0, 100);
            OnTemperatureChange?.Invoke(_temperature);
            if (_temperature <= 0)
            {
                IsDead = true;
            }
        }
    }
}