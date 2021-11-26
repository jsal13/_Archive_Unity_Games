using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public GameObject testingBowl;
    private GameObject currentRelic;
    private bool canGetNewRelic = true;

    public delegate void OnRelicCreation(GameObject relicPrefab);
    public static event OnRelicCreation RelicCreation;

    public static void GetNewRelic(GameObject relicPrefab) => RelicCreation?.Invoke(relicPrefab);

    private void OnEnable()
    {
        RelicManager.RelicCreation += HandleRelicCreation;
    }

    private void OnDisable()
    {
        RelicManager.RelicCreation -= HandleRelicCreation;
    }

    public void HandleRelicCreation(GameObject relicPrefab)
    {
        if (canGetNewRelic)
        {
            if (currentRelic != null)
            {
                Destroy(currentRelic);
            }
            currentRelic = Instantiate(relicPrefab, testingBowl.transform, false);
        }

    }
}


