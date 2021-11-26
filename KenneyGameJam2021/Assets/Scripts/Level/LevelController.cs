using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class LevelController : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    private string areaName;

    public delegate void LevelChangeComplete();
    public static LevelChangeComplete OnLevelChangeComplete;

    private void Awake()
    {
        dialogueRunner = GameObject.Find("Overlays/Dialogue System").GetComponent<DialogueRunner>();
    }

    private void Start()
    {
        if (LevelManager.Instance.CurrentLevel == 1)
        {
            StartCoroutine(LoadFirstLevel());
        }
        else
        {
            GameManager.CurrentEasternDir = new Vector3(1, 0, 0);
            areaName = $"Area_{(LevelManager.Instance.CurrentLevel).ToString().PadLeft(2, '0')}";
            dialogueRunner.StartDialogue(areaName);
        }
    }

    private void OnEnable()
    {
        LevelManager.OnLevelChange += HandleLevelChange;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelChange -= HandleLevelChange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Do I want this automatic?
        if (other.gameObject.name == "Player")
        {
            LevelManager.Instance.CurrentLevel += 1;
        }
    }

    private void HandleLevelChange(int newVal)
    {
        StartCoroutine(LoadNewLevel());
    }

    private IEnumerator LoadNewLevel()
    {
        areaName = $"Area_{(LevelManager.Instance.CurrentLevel).ToString().PadLeft(2, '0')}";
        SceneManager.LoadScene(areaName);
        OnLevelChangeComplete?.Invoke();
        yield return null;
    }

    private IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(0.1f);
        areaName = $"Area_{(LevelManager.Instance.CurrentLevel).ToString().PadLeft(2, '0')}";
        dialogueRunner.StartDialogue(areaName);
        yield return null;
    }

}
