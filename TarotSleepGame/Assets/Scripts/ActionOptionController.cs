using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionOptionController : MonoBehaviour
{
    // Class for data and processing of Turn Options.
    // Eg, Studying Swordsmanship or Working TarotReading.

    [SerializeField] private TurnAction turnAction;
    public static Action<TurnAction> OnTurnOptionChoice;

    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClickPlayerChoice());
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void OnClickPlayerChoice()
    {
        OnTurnOptionChoice?.Invoke(turnAction);
    }

}
