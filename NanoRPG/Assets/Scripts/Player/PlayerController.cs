using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float manaRefillRate = 5f;

    private void Awake()
    {
        PlayerManager.Instance.Health = 5;
        PlayerManager.Instance.MagicPower = 1;
        PlayerManager.Instance.AttackPower = 1;
        PlayerManager.Instance.Gold = 600;

        StartCoroutine(RefillMana());
    }

    private IEnumerator RefillMana()
    {
        while (true)
        {
            // TODO: Would be nice if we could reset timer if they cast magic during the thing.
            if (PlayerManager.Instance.Mana < 4)
            {
                yield return new WaitForSeconds(manaRefillRate);
                PlayerManager.Instance.Mana += 1;
            }
            yield return null;
        }
    }

    [YarnCommand("sleep")]
    public void Sleep()
    {
        StartCoroutine(PlayerManager.Instance.Sleep());
    }
}
