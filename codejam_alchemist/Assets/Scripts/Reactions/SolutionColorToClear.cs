using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolutionColorToClear : MonoBehaviour, IReaction
{
    private Transform bowl;
    private SpriteRenderer solutionColor;
    private bool isReacting;

    private void Awake()
    {
        bowl = GameObject.Find("World/Experiment/Bowl").transform;
        solutionColor = bowl.transform.Find("Color").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isReacting)
        {
            float r = Mathf.PingPong(1f * Time.time, 1);
            solutionColor.color = new Color(1, 0, 1, r);
        }
    }

    public void DoEffect()
    {
        isReacting = true;
    }

    public void ReverseEffect()
    {
        Destroy(this);
    }
}