using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        HarvestRersource.OnChoppingForest += HandleChoppingForest;
    }

    private void OnDisable()
    {
        HarvestRersource.OnChoppingForest -= HandleChoppingForest;
    }

    private void HandleChoppingForest(bool value, int quantity)
    {
            animator.SetBool("isChopped", value);
    }
}
