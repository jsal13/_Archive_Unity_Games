using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireController : MonoBehaviour
{
    private Animator animator;
    private readonly float timeBeforeExistinguishSecs = 5f;
    private readonly float timeBeforeDestroySecs = 2f;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator ExtinguishCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeExistinguishSecs);
        animator.SetBool("isExtinguished", true);

        yield return new WaitForSeconds(timeBeforeDestroySecs);
        Destroy(gameObject);
    }

    private void Update()
    {
        StartCoroutine(ExtinguishCoroutine());
    }
}
