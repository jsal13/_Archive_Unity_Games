using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    public bool _isFull;
    public bool IsFull
    {
        get => _isFull;
        set
        {
            _isFull = value;
            BowlFull?.Invoke(_isFull);
        }
    }

    private Color _solutionColor;
    public Color SolutionColor
    {
        get => _solutionColor;
        set
        {
            _solutionColor = value;
            solutionColorComp.color = _solutionColor;
        }
    }
    private SpriteRenderer solutionColorComp;

    public delegate void OnBowlFull(bool value);
    public static event OnBowlFull BowlFull;

    private void Awake()
    {
        solutionColorComp = transform.Find("Color").GetComponent<SpriteRenderer>();
        SolutionColor = Color.clear;
    }

    private void OnEnable()
    {
        MixingContainerController.PourIntoBowl += HandlePourIntoBowl;
        RelicManager.RelicCreation += HandleRelicCreation;
        LevelController.NewRelic += HandleNewRelic;
    }

    private void OnDisable()
    {
        MixingContainerController.PourIntoBowl -= HandlePourIntoBowl;
        RelicManager.RelicCreation -= HandleRelicCreation;
        LevelController.NewRelic -= HandleNewRelic;
    }

    private void HandleNewRelic()
    {
        EmptyBowl();
    }

    private void OnMouseOver()
    {
        // Right click to empty bowl.
        if (Input.GetMouseButtonDown(1))
        {
            EmptyBowl();
        }
    }

    private void HandleRelicCreation(GameObject relicPrefab)
    {
        EmptyBowl();
    }

    private void HandlePourIntoBowl(List<Potion> potionList)
    {
        SolutionColor = CalculateSolutionColor(potionList);
        IsFull = true;
    }

    private Color CalculateSolutionColor(List<Potion> potionList)
    {
        // Mixes colors together by averaging them.
        float r = 0;
        float g = 0;
        float b = 0;
        float n = potionList.Count;

        foreach (Potion potion in potionList)
        {
            r += potion.color.r;
            g += potion.color.g;
            b += potion.color.b;
        }

        r /= n;
        g /= n;
        b /= n;

        return new Color(r, g, b, 1);
    }

    public void EmptyBowl()
    {
        SolutionColor = Color.clear;
        IsFull = false;
    }
}
