using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Initialization : MonoBehaviour
{
    private void Awake()
    {
        GameManager.dialogueRunner = GameObject.FindObjectOfType<DialogueRunner>();
    }

    private void Start()
    {
        Debug.Log("Initializing values...");
        PlayerManager.stats = new Statistics(
            new List<Statistic>() {
                new Statistic(Stat.Coin, 100, 1000),
                new Statistic(Stat.Health, 100),
                new Statistic(Stat.Stress, 1),
                new Statistic(Stat.Strength, 1),
                new Statistic(Stat.Dexterity, 1),
                new Statistic(Stat.Magic, 1),
            }
        );

        TurnManager.TurnNumber = 0;
        TurnManager.CalendarMonth = 0;
        TurnManager.CalendarDay = 0;
    }
}
