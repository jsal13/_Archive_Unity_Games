using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private GameObject relicPrefab;
    private GameObject relicObj;
    [SerializeField] private Relic currentRelic;
    public List<Relic> genuineRelics;
    public List<Relic> fakeRelics;

    public enum Status { Genuine, Forgery };
    public static Status currentStatus;

    public delegate void OnNewRelic();
    public static event OnNewRelic NewRelic;

    public delegate void OnPlayerCorrect(bool value);
    public static event OnPlayerCorrect PlayerCorrect;

    private void Awake()
    {
        relicPrefab = Resources.Load<GameObject>("Prefabs/Relics/Relic");
    }

    private void OnEnable()
    {
        LevelController.NewRelic += HandleNewRelic;
        LevelManager.PlayerReadTome += HandlePlayerReadTome;
    }

    private void OnDisable()
    {
        LevelController.NewRelic -= HandleNewRelic;
        LevelManager.PlayerReadTome -= HandlePlayerReadTome;
    }

    private void HandlePlayerReadTome()
    {
        HandleNewRelic();
    }

    public void GetNewRelic()
    {
        NewRelic?.Invoke();
    }

    private void HandleNewRelic()
    {
        if (relicObj != null)
        {
            Destroy(relicObj);
        }

        // This is when we have standard relics (in tutorial, for example).;
        if (LevelManager.CurrentLevel <= 3)
        {
            currentRelic = LevelManager.levelRelic[LevelManager.CurrentLevel];
            currentStatus = Status.Genuine;
        }
        else if (LevelManager.CurrentLevel == 4)
        {
            currentRelic = LevelManager.levelRelic[LevelManager.CurrentLevel];
            currentStatus = Status.Forgery;
        }

        else
        {
            if (UnityEngine.Random.value < 0.5)
            {
                currentRelic = genuineRelics[UnityEngine.Random.Range(0, genuineRelics.Count - 1)];
                currentStatus = Status.Genuine;
            }
            else
            {
                currentRelic = fakeRelics[UnityEngine.Random.Range(0, fakeRelics.Count - 1)];
                currentStatus = Status.Forgery;
            }
        }

        relicObj = Instantiate(relicPrefab);
        relicObj.name = "Relic";
        relicObj.GetComponent<RelicController>().relic = currentRelic;
        relicObj.GetComponent<SpriteRenderer>().sprite = currentRelic.sprite;
    }

    // This is because the inspector won't let me submit a Status parameter.
    // TODO: Merge these.
    public void PlayerClickedGenuineButton()
    {
        if (currentStatus == Status.Genuine)
        {
            StartCoroutine(PauseBetweenResults(true));
        }
        else
        {
            StartCoroutine(PauseBetweenResults(false));
        }
    }

    public void PlayerClickedForgeryButton()
    {
        if (currentStatus == Status.Forgery)
        {
            StartCoroutine(PauseBetweenResults(true));
        }
        else
        {
            StartCoroutine(PauseBetweenResults(false));
        }
    }

    public IEnumerator PauseBetweenResults(bool value)
    {
        PlayerCorrect?.Invoke(value);
        yield return new WaitForSeconds(1.5f);
        NewRelic?.Invoke();
    }


}
