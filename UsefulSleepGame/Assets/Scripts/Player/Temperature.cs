using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    [SerializeField] private List<ITemperature> temperatureList;
    private bool isCalculatingTemperature;
    private float calculationDuration = 0.1f;

    private void Awake()
    {
        temperatureList = new List<ITemperature>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ITemperature>() != null)
        {
            temperatureList.Add(other.GetComponent<ITemperature>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<ITemperature>() != null)
        {
            temperatureList.Remove(other.GetComponent<ITemperature>());
        }
    }

    private void Update()
    {
        if (temperatureList.Count > 0 && !isCalculatingTemperature)
        {
            isCalculatingTemperature = true;
            StartCoroutine(CalculateTemperature());
        }
        else if (temperatureList.Count == 0)
        {
            StopAllCoroutines();
            isCalculatingTemperature = false;
        }
    }

    private IEnumerator CalculateTemperature()
    {
        while (true)
        {
            PlayerManager.ambientTemperature = temperatureList.Aggregate(0, (acc, x) => acc + x.GetTemperature());

            yield return new WaitForSeconds(calculationDuration);
            PlayerManager.Temperature += Mathf.CeilToInt(PlayerManager.ambientTemperature * calculationDuration);

            yield return null;
        }
    }
}