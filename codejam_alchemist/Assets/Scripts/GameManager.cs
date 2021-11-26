using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool managerAlreadyExists = false;
    public static bool pauseInput;

    public int HandleLoadScene { get; private set; }

    private void Awake()
    {
        if (managerAlreadyExists)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void BeginGame()
    {
        LevelManager.CurrentLevel = -1;
        SceneManager.LoadScene("Main");
    }

    public void PauseInput(float secs)
    {
        StartCoroutine(PauseInputForTime(secs));
    }

    public IEnumerator PauseInputForTime(float secs)
    {
        pauseInput = true;
        yield return new WaitForSeconds(secs);
        pauseInput = false;
    }

    private void OnEnable()
    {
        LevelController.PlayerCorrect += HandlePlayerCorrect;
        ScoreController.EndGame += HandleEndGame;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        LevelController.PlayerCorrect -= HandlePlayerCorrect;
        ScoreController.EndGame -= HandleEndGame;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode _)
    {
        if (scene.name == "EndScreenBad" || scene.name == "EndScreenGood")
        {
            Button startBTN = GameObject.Find("Canvas/Start").GetComponent<Button>();
            startBTN.onClick.AddListener(BeginGame);
        }
    }

    private void HandleEndGame(string endingType)
    {
        if (endingType == "Bad")
        {
            SceneManager.LoadScene("EndScreenBad");
        }
        else
        {
            SceneManager.LoadScene("EndScreenGood");
        }
    }

    private void HandlePlayerCorrect(bool value)
    {
        PauseInput(2);
    }

    public void RestartGame()
    {
        LevelManager.CurrentLevel = -1;
        SceneManager.LoadScene("StartMenu");
    }
}
