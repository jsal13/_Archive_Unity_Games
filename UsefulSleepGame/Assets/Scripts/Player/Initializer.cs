using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Initializer : MonoBehaviour
{

    private void Start()
    {
        Application.targetFrameRate = 30;

        // Resources
        PlayerManager.MaxCoin = 20;
        PlayerManager.MaxWood = 12;
        PlayerManager.MaxStone = 8;
        PlayerManager.MaxWool = 4;

        PlayerManager.Coin = 15;
        PlayerManager.Wood = 4;
        PlayerManager.Stone = 0;
        PlayerManager.Wool = 4;

        // Temperature
        PlayerManager.Temperature = 80;
    }

}