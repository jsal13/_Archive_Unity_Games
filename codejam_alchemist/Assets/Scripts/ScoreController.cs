using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int _correct = -1;
    private int Correct
    {
        get => _correct;
        set
        {
            _correct = value;
            if (value >= 10)
            {
                EndGame?.Invoke("Good");
            }
        }
    }

    private int _incorrect = 0;
    public int Incorrect
    {
        get => _incorrect;
        set
        {
            _incorrect = value;
            if (value >= 3)
            {
                EndGame?.Invoke("Bad");
            }
        }
    }

    // "Bad" or "Good".
    public delegate void OnEndGame(string endingType);
    public static event OnEndGame EndGame;

    private void Awake()
    {
        HandlePlayerCorrect(true);
    }

    private void OnEnable()
    {
        LevelController.PlayerCorrect += HandlePlayerCorrect;
    }

    private void OnDisable()
    {
        LevelController.PlayerCorrect -= HandlePlayerCorrect;
    }

    private void HandlePlayerCorrect(bool value)
    {
        if (value)
        {
            Correct += 1;
        }
        else
        {
            Incorrect += 1;
        }

        gameObject.GetComponent<TMP_Text>().text = $"Correct:\t\t{Correct.ToString().PadLeft(2, ' ')}\nIncorrect:\t{Incorrect.ToString().PadLeft(2, ' ')}";
    }
}
