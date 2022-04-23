using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Stat statHealth;
    [SerializeField] private Stat statAttack;
    [SerializeField] private Stat statMagic;
    [SerializeField] private Stat statDexterity;

    [SerializeField] private int currentDay;
    [SerializeField] private string currentSeason;

    [SerializeField] private Towns towns;


    private void Awake()
    {
        // Initialize Stats.
        Stat.OnStatChange?.Invoke("gold", PlayerManager.gold.Value);
        Stat.OnStatChange?.Invoke("health", PlayerManager.health.Value);
        Stat.OnStatChange?.Invoke("attack", PlayerManager.attack.Value);
        Stat.OnStatChange?.Invoke("magic", PlayerManager.magic.Value);
        Stat.OnStatChange?.Invoke("dexterity", PlayerManager.dexterity.Value);

        // Initialize Dates.
        DateManager.Day = 0;
        DateManager.SeasonIDX = 0;
        DateManager.season = DateManager.seasons[DateManager.SeasonIDX];

        // Towns + Prices.
        Town aTown = new Town("Town A", new PlayerActions(), 1);
        Town bTown = new Town("Town B", new PlayerActions(), 0);
        Town cTown = new Town("Town C", new PlayerActions(), 3);
        Town dTown = new Town("Town D", new PlayerActions());

        towns = new Towns(new List<Town> { aTown, bTown, cTown, dTown });
        towns.SetRandomActionPrices();

    }
}
