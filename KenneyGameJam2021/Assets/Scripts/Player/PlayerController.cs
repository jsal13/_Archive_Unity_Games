using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{

    private void Start()
    {
        if (LevelManager.Instance.CurrentLevel == 1)
        {
            PlayerManager.Instance.Health = 6;
            PlayerManager.Instance.AttackPower = 1;
            PlayerManager.Instance.Gold = 0;
        }
    }
}