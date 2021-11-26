using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerManager
{
    private static readonly Lazy<PlayerManager> lazy = new Lazy<PlayerManager>(() => new PlayerManager());
    public static PlayerManager Instance { get { return lazy.Value; } }
    private PlayerManager() { }

    private bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            // TODO: Do other things if false;
        }
    }

    private bool _canMove = true;
    public bool CanMove
    {
        get => _canMove;
        set
        {
            _canMove = value;
        }
    }

    private bool _canAttack = true;
    public bool CanAttack
    {
        get => _canAttack;
        set
        {
            _canAttack = value;
            // TODO: Do other things if false;
        }
    }

    private int maxHealth = 999;
    private int _health = 5;
    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);
            OnPlayerHealthChange?.Invoke(_health);

            if (_health == 0)
            {
                IsAlive = false;
                CanMove = false;
                CanAttack = false;
                OnPlayerDeath?.Invoke();
            }
        }
    }

    private int maxMana = 100;
    private int _mana = 4;
    public int Mana
    {
        get => _mana;
        set
        {
            _mana = Mathf.Clamp(value, 0, maxMana);
            OnPlayerManaChange?.Invoke(_mana);
        }
    }
    private int maxAttackPower = 999;
    private int _attackPower;
    public int AttackPower
    {
        get => _attackPower;
        set
        {
            _attackPower = Mathf.Clamp(value, 0, maxAttackPower);
            OnPlayerAttackPowerChange?.Invoke(_attackPower);
        }
    }
    private int _magicPower = 0;
    public int MagicPower
    {
        get => _magicPower;
        set
        {
            _magicPower = value;
            OnPlayerMagicPowerChange?.Invoke(_magicPower);
        }
    }

    private int maxGold = 999;
    private int _gold;
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = Mathf.Clamp(value, 0, maxGold);
            OnPlayerGoldChange?.Invoke(_gold);
        }
    }

    public static GameObject player = GameObject.Find("Player");

    // Dialog
    public Sprite playerCharacterPortrait = player.GetComponent<SpriteRenderer>().sprite;
    public bool _isInDialog = false;
    public bool IsInDialog
    {
        get => _isInDialog;
        set
        {
            _isInDialog = value;
            if (_isInDialog)
            {
                CanMove = false;
                CanAttack = false;
            }
            else
            {
                CanMove = true;
                CanAttack = true;
            }
        }
    }


    // Events.
    public delegate void PlayerHealthChange(int newVal);
    public static PlayerHealthChange OnPlayerHealthChange;

    public delegate void PlayerAttackPowerChange(int newVal);
    public static PlayerAttackPowerChange OnPlayerAttackPowerChange;

    public delegate void PlayerMagicPowerChange(int newVal);
    public static PlayerMagicPowerChange OnPlayerMagicPowerChange;

    public delegate void PlayerManaChange(int newVal);
    public static PlayerManaChange OnPlayerManaChange;

    public delegate void PlayerGoldChange(int newVal);
    public static PlayerGoldChange OnPlayerGoldChange;

    public delegate void PlayerDeath();
    public static PlayerDeath OnPlayerDeath;
}