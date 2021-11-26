using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class TransitionController : MonoBehaviour
{
    private TransitionManager transitionManager;
    [SerializeField] private string sourceSceneName;
    [SerializeField] private string targetSceneName;
    [SerializeField] private Vector3 targetPos;

    private void Start()
    {
        transitionManager = GameObject.Find("GameManager").GetComponent<TransitionManager>();

        targetPos = LocationManager.transitionPositions.GetTransitionPosition(sourceSceneName, targetSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            transitionManager.TransitionPlayer("Scenes/" + targetSceneName, targetPos);
        }
    }
}
