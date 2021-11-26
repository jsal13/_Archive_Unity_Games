using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int _currentLevel = -1;
    public static int CurrentLevel
    {
        get => _currentLevel;
        set
        {
            _currentLevel = value;
        }
    }

    public Dictionary<int, string> levelDescriptions;
    public static Dictionary<int, Relic> levelRelic;
    public bool isCurrentRelicReal;
    public GameObject tomeBackground;
    public TMP_Text tomeTextComponent;
    private TMP_Text levelTextComponent;
    public GameObject GUIcanvas;
    public GameObject resultBoard;
    private bool canOpenTome;
    public GameObject statusButtons;
    public bool relicNameReady;
    private string currentRelicName;

    public delegate void OnPlayerReadTome();
    public static event OnPlayerReadTome PlayerReadTome;

    private void Awake()
    {
        levelDescriptions = new Dictionary<int, string>() {
        { 0, "First, let's take a piece of Iron and see how it reacts.\n\nYou can drag up to TWO potions into the bowl on the left before dragging the mixture to the bowl on the right.  You may empty either bowl by right-clicking.\n\nWhen you are done playing with the Iron, click 'Genuine'."},
        { 1, "Let's try a Rock this time.  Rocks are inert: they will not react with anything!  Try it out.  Click 'Genuine' when you're finished."},
        { 2, "Let's test some gold!  Make sure you see how it reacts and click 'Genuine' when you're satisfied."},
        { 3, "The last leg of your training: an Emerald.  This is on loan, so be careful with it!  When you're satisfied, click 'Genuine', and proceed."},
        { 4, "Your first real customer: a merchant has brought in a Gold Crown with Emeralds.  Or so he claims.  Use what you've learned to see if this ring is a genuine relic or a forgery!"}
        };

        levelRelic = new Dictionary<int, Relic>() {
            { 0, Resources.Load<Relic>("ScriptableObjects/Relics/IronPure") },
            { 1, Resources.Load<Relic>("ScriptableObjects/Relics/RockPure") },
            { 2, Resources.Load<Relic>("ScriptableObjects/Relics/GoldPure") },
            { 3, Resources.Load<Relic>("ScriptableObjects/Relics/EmeraldPure") },
            { 4, Resources.Load<Relic>("ScriptableObjects/Relics/EmeraldGoldCrownFake_1") },
        };
    }

    private void OnEnable()
    {
        TomeManager.TomeOpened += HandleTomeOpened;
        LevelController.NewRelic += HandleNewRelic;
        SceneManager.sceneLoaded += HandleSceneLoaded;
        LevelController.PlayerCorrect += HandlePlayerCorrect;
    }

    private void OnDisable()
    {
        TomeManager.TomeOpened -= HandleTomeOpened;
        LevelController.NewRelic -= HandleNewRelic;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        LevelController.PlayerCorrect -= HandlePlayerCorrect;
    }

    private void HandlePlayerCorrect(bool value)
    {
        StartCoroutine(MakeResultBoard(value));
    }

    public IEnumerator MakeResultBoard(bool value)
    {
        var go = Instantiate(resultBoard, GUIcanvas.transform, false);
        if (value)
        {
            go.GetComponentInChildren<TMP_Text>().text = "Correct!";
        }
        else
        {
            go.GetComponentInChildren<TMP_Text>().text = "Incorrect!";
            go.GetComponentInChildren<TMP_Text>().color = Color.red;
        }
        yield return new WaitForSeconds(1.25f);
        Destroy(go);
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode _)
    {
        if (scene.name == "Main")
        {
            canOpenTome = true;
            statusButtons = GameObject.Find("GUI/Canvas/StatusButtons");
            tomeBackground = GameObject.Find("GUI/Canvas/TomeBackground");
            resultBoard = Resources.Load<GameObject>("Prefabs/GUI/ResultBoard");
            GUIcanvas = GameObject.Find("GUI/Canvas");
            HandleNewRelic();
        }

        if (scene.name != "Main")
        {
            canOpenTome = false;
        }

    }

    private void HandleNewRelic()
    {
        if (canOpenTome)
        {
            CurrentLevel += 1;
            HandleTomeOpened(true);
        }
    }

    void HandleTomeOpened(bool value)
    {
        if (value)
        {
            statusButtons.SetActive(false);
            tomeBackground.SetActive(true);
            tomeTextComponent = tomeBackground.transform.Find("Description").GetComponent<TMP_Text>();
            levelTextComponent = tomeBackground.transform.Find("LevelNumber").GetComponent<TMP_Text>();

            levelTextComponent.text = $"Level: {CurrentLevel}";

            if (CurrentLevel <= 4)
            {
                // Get from pre-written descriptions.
                StartDialog(levelDescriptions[CurrentLevel]);
            }
            else
            {
                StartDialog("Another relic has come in!  Let's see if it's genuine or not.");
            }
            PlayerReadTome?.Invoke();
        }
        else
        {
            tomeBackground.SetActive(false);
            statusButtons.SetActive(true);
        }

    }

    private void StartDialog(string sentence)
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        tomeTextComponent.text = "";
        yield return new WaitForSeconds(0.1f);
        foreach (char letter in sentence.ToCharArray())
        {
            tomeTextComponent.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}