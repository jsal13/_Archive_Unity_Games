using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashableController : MonoBehaviour
{
    public Animator slashAnimator;

    private void Awake()
    {
        slashAnimator = GetComponent<Animator>();
    }

    public void PlaySlashAnimationAndDestroy()
    {
        StartCoroutine(PlayAnimationAndWait());
    }

    IEnumerator PlayAnimationAndWait()
    {
        slashAnimator.SetTrigger("IsSlashed");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);  // TODO: Maybe do something else here, this destroys it for the whole game.
    }

}
