using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : MonoBehaviour, IReaction
{
    public GameObject bubblesPrefab;
    private GameObject bubblesObj;
    private Transform bowl;

    private void Awake()
    {
        bubblesPrefab = Resources.Load<GameObject>("Prefabs/Bubbles");
        bowl = GameObject.Find("World/Experiment/Bowl").transform;
    }

    public void DoEffect()
    {
        bubblesObj = Instantiate<GameObject>(bubblesPrefab, bowl, false);
    }

    public void ReverseEffect()
    {
        Destroy(bubblesObj);
        Destroy(this);
    }
}