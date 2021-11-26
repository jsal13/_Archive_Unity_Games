using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour, IAttackable
{
    [SerializeField] private List<float> timeToGrow;
    [SerializeField] private float treeRegrowthTime = 8f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gotResourceIndicator;
    private Sprite woodSprite;

    public bool canChop = false;
    [SerializeField] private int startingNumWoodLeftBeforeChopped = 4;
    private int _numWoodLeftBeforeChopped;
    public int NumWoodLeftBeforeChopped
    {
        get => _numWoodLeftBeforeChopped;
        set
        {
            if (value <= 0)
            {
                animator.ResetTrigger("IsHit");
                StartCoroutine(FellTree());
            }
            else { }
            _numWoodLeftBeforeChopped = value;
        }

    }

    [SerializeField] private int startingHP = 8;
    private int _hp = 8;
    public int HP
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                PlayerManager.Wood += 1;

                gotResourceIndicator.GetComponent<Animator>().SetTrigger("GotResource");
                gotResourceIndicator.GetComponent<SpriteRenderer>().sprite = woodSprite;
                _hp = startingHP;
                NumWoodLeftBeforeChopped -= 1;
            }
            else
            {
                _hp = value;
            }
        }
    }

    private void Awake()
    {
        NumWoodLeftBeforeChopped = startingNumWoodLeftBeforeChopped;
        timeToGrow = new List<float>() { 4f, 6f, 10f };
        animator = this.GetComponent<Animator>();
        woodSprite = Resources.Load<Sprite>("Images/MaterialResources/Wood");
        gotResourceIndicator = GameObject.Find("Player/GotResourceIndicator");

    }

    void Start()
    {
        StartCoroutine(GrowTree());
    }

    public void IsHit()
    {
        if (canChop)
        {
            animator.SetTrigger("IsHit");
            HP -= 1;
        }
    }

    private IEnumerator GrowTree()
    {
        for (int idx = 0; idx < 3; idx += 1)
        {
            yield return new WaitForSeconds(timeToGrow[idx]);
            animator.SetTrigger("Grow");
        }
        canChop = true;
    }

    private IEnumerator FellTree()
    {
        canChop = false;
        animator.SetTrigger("Chopped");
        yield return new WaitForSeconds(treeRegrowthTime);
        animator.SetTrigger("Regrow");
        NumWoodLeftBeforeChopped = startingNumWoodLeftBeforeChopped;
        yield return null;
        StartCoroutine(GrowTree());
    }
}
