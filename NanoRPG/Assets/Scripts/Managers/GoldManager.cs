using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    private GameObject oneGoldPrefab;
    private GameObject fiveGoldPrefab;
    private GameObject tenGoldPrefab;
    private Dictionary<int, GameObject> valueToPrefab;

    private void Awake()
    {
        oneGoldPrefab = Resources.Load<GameObject>("Prefabs/GoldOne");
        fiveGoldPrefab = Resources.Load<GameObject>("Prefabs/GoldFive");
        tenGoldPrefab = Resources.Load<GameObject>("Prefabs/GoldTen");

        // TODO: Prob should do some error checking.
        valueToPrefab = new Dictionary<int, GameObject>() {
            {1, oneGoldPrefab},
            {5, fiveGoldPrefab},
            {10, tenGoldPrefab}
        };
    }

    private void OnEnable()
    {
        EnemyController.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EnemyController.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(Vector3 pos, int goldValue)
    {
        Instantiate(valueToPrefab[goldValue], pos, Quaternion.identity);
    }
}
