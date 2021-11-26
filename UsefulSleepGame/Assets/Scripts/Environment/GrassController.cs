using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour, ISlashable
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isGrown;

    private void Awake()
    {
        isGrown = Random.value <= 0.2;
        if (!isGrown)
        {
            Destroy(this.gameObject);
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    public void SlashedAction()
    {
        if (isGrown)
        {
            StartCoroutine(SlashGrass());
        }
    }

    private IEnumerator SlashGrass()
    {
        animator.SetTrigger("IsSlashed");
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
