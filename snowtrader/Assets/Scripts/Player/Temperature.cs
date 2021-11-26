using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    private PlayerManager player;
    private bool canChangeTemperature = true;

    private readonly float initAmbientTemperature = -2f;
    List<Collider2D> temperatureColliders;
    
    private float playerTemperatureChangePerSecond = 0;
    private float playerTemperatureChangePerSecondClamped;
    private float _currentPlayerTemperature;
    public float CurrentPlayerTemperature
    {
        get => _currentPlayerTemperature;
        set
        {
            if (!canChangeTemperature) return;
            else
            {
                _currentPlayerTemperature = Mathf.Clamp(value, 0, 100);
                PersistenceManager.Instance.currentPlayerTemperature = _currentPlayerTemperature;
                OnPlayerTemperatureChange?.Invoke(value);
            }
        }
    }

    public delegate void PlayerTemperatureChange(float newTemp);
    public static event PlayerTemperatureChange OnPlayerTemperatureChange;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        temperatureColliders = new List<Collider2D>();
        CurrentPlayerTemperature = PersistenceManager.Instance.currentPlayerTemperature;
    }

    private void Start()
    {
        RecalculateTemperature();
        OnPlayerTemperatureChange?.Invoke(CurrentPlayerTemperature);
    }

    private void Update()
    {
        playerTemperatureChangePerSecondClamped = Mathf.Clamp(playerTemperatureChangePerSecond, -10, 10);
        CurrentPlayerTemperature += playerTemperatureChangePerSecondClamped * Time.deltaTime;
        if (CurrentPlayerTemperature <= 0 & !player.IsDead)
        {
            player.IsDead = true;
        }
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerDied += HandlePlayerDeath;
        Dialog.OnPlayerSpeaking += HandlePlayerSpeaking;
        Dialog.OnPlayerTrading += HandlePlayerTrading;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDied -= HandlePlayerDeath;
        Dialog.OnPlayerSpeaking -= HandlePlayerSpeaking;
        Dialog.OnPlayerTrading -= HandlePlayerTrading;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!temperatureColliders.Contains(collision) && (collision.CompareTag("Warm") || collision.CompareTag("Cold")))
        {
            temperatureColliders.Add(collision);
            RecalculateTemperature();
        };
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Warm") || collision.CompareTag("Cold")))
        {
            temperatureColliders.Remove(collision);
            RecalculateTemperature();
        }
    }

    private void RecalculateTemperature()
    {
        // Add up the current colliders value, or set the value to zero if there are none.
        playerTemperatureChangePerSecond = initAmbientTemperature;
        foreach (Collider2D col in temperatureColliders)
        {
            playerTemperatureChangePerSecond += col.GetComponent<TemperatureController>().temperature;
        }
    }

    private void HandlePlayerDeath(bool value)
    {
        if (value)
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        CurrentPlayerTemperature = 90.0f;
    }

    private void HandlePlayerSpeaking(bool value)
    {
        canChangeTemperature = !value;
    }

    private void HandleTradeMenuEnd()
    {
        canChangeTemperature = true;
    }

    private void HandlePlayerTrading(NPCController.NPC _)
    {
        canChangeTemperature = false;
    }
}
