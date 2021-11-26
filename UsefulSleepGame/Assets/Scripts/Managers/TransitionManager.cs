using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 targetPos;

    private void Awake()
    {
        animator = GameObject.Find("GameManager/TransitionScreen").GetComponent<Animator>();
    }

    public void TransitionPlayer(string _targetScene, Vector3 _targetPos)
    {
        targetScene = _targetScene;
        targetPos = _targetPos;

        StartCoroutine(TransitionScene());
    }

    private IEnumerator TransitionScene()
    {
        animator.SetTrigger("TransitionTo");
        yield return new WaitForSeconds(1f);

        PlayerManager.IsInTransition = true;
        yield return null;

        SceneManager.LoadScene(targetScene);
        yield return null;

        GameObject.Find("Player").transform.position = targetPos;

        animator.SetTrigger("TransitionFrom");
        yield return new WaitForSeconds(0.5f);

        PlayerManager.IsInTransition = false;
        if (PlayerManager.IsDead)
        {
            // TODO: Restrict movement if they're dead.
            PlayerManager.IsDead = true;
        }

        yield return null;
    }

}
