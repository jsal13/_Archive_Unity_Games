using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    private bool isReviving = false;
    [SerializeField] private TransitionManager transitionManager;

    private void OnEnable()
    {
        PlayerManager.OnPlayerDeath += HandlePlayerDeath;
    }


    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        if (!isReviving)
        {
            Debug.Log("What's up scrubs.");
            isReviving = true;
            StartCoroutine(RevivePlayer());
        }
    }

    private IEnumerator RevivePlayer()
    {
        Vector3 checkpointPosWithOffset = GameManager.checkpoint.position + new Vector3(0, 8f, 0);

        transitionManager.TransitionPlayer(GameManager.checkpoint.sceneName, checkpointPosWithOffset);

        yield return new WaitForSeconds(0.5f);
        PlayerManager.Temperature = 100;

        PlayerManager.IsDead = false;
        yield return new WaitForSeconds(0.5f);

        isReviving = false;
        yield return null;
    }

}

