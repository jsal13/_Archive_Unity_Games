using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceContainerController : MonoBehaviour
{
    [Serializable]
    public class Section
    {
        public TMP_Text value;
        public TMP_Text maxValue;
    }

    [SerializeField] private Section coinSection;
    [SerializeField] private Section woodSection;
    [SerializeField] private Section stoneSection;
    [SerializeField] private Section woolSection;

    private void OnEnable()
    {
        PlayerManager.OnCoinChange += HandleCoinChange;
        PlayerManager.OnWoodChange += HandleWoodChange;
        PlayerManager.OnStoneChange += HandleStoneChange;
        PlayerManager.OnWoolChange += HandleWoolChange;

        PlayerManager.OnMaxCoinChange += HandleMaxCoinChange;
        PlayerManager.OnMaxWoodChange += HandleMaxWoodChange;
        PlayerManager.OnMaxStoneChange += HandleMaxStoneChange;
        PlayerManager.OnMaxWoolChange += HandleMaxWoolChange;
    }

    private void OnDisable()
    {
        PlayerManager.OnCoinChange -= HandleCoinChange;
        PlayerManager.OnWoodChange -= HandleWoodChange;
        PlayerManager.OnStoneChange -= HandleStoneChange;
        PlayerManager.OnWoolChange -= HandleWoolChange;

        PlayerManager.OnMaxCoinChange -= HandleMaxCoinChange;
        PlayerManager.OnMaxWoodChange -= HandleMaxWoodChange;
        PlayerManager.OnMaxStoneChange -= HandleMaxStoneChange;
        PlayerManager.OnMaxWoolChange -= HandleMaxWoolChange;
    }

    private void HandleMaxWoolChange(int newValue)
    {
        woolSection.maxValue.text = "/" + MakePaddedNumber(newValue);
    }

    private void HandleWoolChange(int newValue)
    {
        woolSection.value.text = MakePaddedNumber(newValue);
    }

    private void HandleMaxCoinChange(int newValue)
    {
        coinSection.maxValue.text = "/" + MakePaddedNumber(newValue);
    }

    private void HandleCoinChange(int newValue)
    {
        coinSection.value.text = MakePaddedNumber(newValue);
    }

    private void HandleMaxWoodChange(int newValue)
    {
        woodSection.maxValue.text = "/" + MakePaddedNumber(newValue);
    }

    private void HandleWoodChange(int newValue)
    {
        woodSection.value.text = MakePaddedNumber(newValue);
    }

    private void HandleMaxStoneChange(int newValue)
    {
        stoneSection.maxValue.text = "/" + MakePaddedNumber(newValue);
    }

    private void HandleStoneChange(int newValue)
    {
        stoneSection.value.text = MakePaddedNumber(newValue);
    }

    private string MakePaddedNumber(int val)
    {
        return val.ToString().PadLeft(2, '0');
    }

}
