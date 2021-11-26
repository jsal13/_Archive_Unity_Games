using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Animator transitionAnimation;
    private readonly float transitionTime = 1f;
    private readonly string scenePrefix = "Scenes/";
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            transitionAnimation.SetBool("IsLoading", value);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Management"));
        transitionAnimation = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        PlayerManager.OnLevelTransition += HandleLevelTransition;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        PlayerManager.OnLevelTransition -= HandleLevelTransition;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene _0, LoadSceneMode _1)
    {
        StartCoroutine(FinishTransition());
    }
    
    IEnumerator FinishTransition()
    {
        IsLoading = false;
        yield return new WaitForSeconds(transitionTime);
    }

    private void HandleLevelTransition(TransitionController.Transition transition)
    {
        IsLoading = true;
        PersistenceManager.initLevelLocation = transition.destinationLoc;
        SceneManager.LoadScene(scenePrefix + transition.destinationScene);
    }
}
