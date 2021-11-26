using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BonfireController : MonoBehaviour, ITemperature
{
    private Animator animator;
    [InlineEditor(Expanded = true)]
    [SerializeField] private BonfireInfo info;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(BurnForDuration());
    }

    private IEnumerator BurnForDuration()
    {
        yield return new WaitForSeconds(info.duration);
        animator.SetTrigger("isExtinguished");
        yield return new WaitForSeconds(info.emberDuration);
        Destroy(this.gameObject);
    }

    public int GetTemperature()
    {
        return info.temperature;
    }
}
