using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlayController : MonoBehaviour
{
    private GameObject overlayObj;
    private GameObject root;
    private Image heart0;
    private Image heart1;
    private Image heart2;
    private Image magicMeter;
    private TMP_Text goldLabel;
    private TMP_Text attackPowerLabel;
    private TMP_Text magicPowerLabel;

    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;

    [SerializeField] private List<List<Sprite>> heartList;
    [SerializeField] private List<Sprite> magicBar;

    private void Awake()
    {
        root = transform.Find("Root").gameObject;

        // HEARTS
        heart0 = root.transform.Find("Hearts/Heart0").GetComponent<Image>();
        heart1 = root.transform.Find("Hearts/Heart1").GetComponent<Image>();
        heart2 = root.transform.Find("Hearts/Heart2").GetComponent<Image>();

        heartEmpty = Resources.Load<Sprite>("Images/Sprites/Heart_empty");
        heartHalf = Resources.Load<Sprite>("Images/Sprites/Heart_half");
        heartFull = Resources.Load<Sprite>("Images/Sprites/heart_full");

        // MAGIC
        magicMeter = root.transform.Find("MagicMeter").GetComponent<Image>();

        // STATS
        goldLabel = root.transform.Find("Stats/Gold/Label").GetComponent<TMP_Text>();
        attackPowerLabel = root.transform.Find("Stats/MagicPower/Label").GetComponent<TMP_Text>();
        magicPowerLabel = root.transform.Find("Stats/AttackPower/Label").GetComponent<TMP_Text>();

        // LISTS USED FOR HEARTS + MAGIC
        heartList = new List<List<Sprite>>() {
            new List<Sprite>(){ heartEmpty, heartEmpty, heartEmpty},
            new List<Sprite>(){ heartHalf, heartEmpty, heartEmpty},
            new List<Sprite>(){ heartFull, heartEmpty, heartEmpty},
            new List<Sprite>(){ heartFull, heartHalf, heartEmpty},
            new List<Sprite>(){ heartFull, heartFull, heartEmpty},
            new List<Sprite>(){ heartFull, heartFull, heartHalf},
            new List<Sprite>(){ heartFull, heartFull, heartFull},
        };

        magicBar = new List<Sprite>()
        {
            Resources.Load<Sprite>("Images/Sprites/MagicMeter_0"),
            Resources.Load<Sprite>("Images/Sprites/MagicMeter_1"),
            Resources.Load<Sprite>("Images/Sprites/MagicMeter_2"),
            Resources.Load<Sprite>("Images/Sprites/MagicMeter_3"),
            Resources.Load<Sprite>("Images/Sprites/MagicMeter_4")
        };

    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerHealthChange += HandlePlayerHealthChange;
        PlayerManager.OnPlayerManaChange += HandlePlayerManaChange;
        PlayerManager.OnPlayerAttackPowerChange += HandlePlayerAttackPowerChange;
        PlayerManager.OnPlayerMagicPowerChange += HandlePlayerMagicPowerChange;
        PlayerManager.OnPlayerGoldChange += HandlePlayerGoldChange;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerHealthChange -= HandlePlayerHealthChange;
        PlayerManager.OnPlayerManaChange -= HandlePlayerManaChange;
        PlayerManager.OnPlayerAttackPowerChange -= HandlePlayerAttackPowerChange;
        PlayerManager.OnPlayerMagicPowerChange -= HandlePlayerMagicPowerChange;
        PlayerManager.OnPlayerGoldChange -= HandlePlayerGoldChange;
    }

    private void HandlePlayerGoldChange(int newVal)
    {
        goldLabel.text = newVal.ToString();
    }

    private void HandlePlayerMagicPowerChange(int newVal)
    {
        magicPowerLabel.text = newVal.ToString();
    }

    private void HandlePlayerAttackPowerChange(int newVal)
    {
        attackPowerLabel.text = newVal.ToString();
    }

    private void HandlePlayerManaChange(int newVal)
    {
        magicMeter.sprite = magicBar[newVal];
    }

    private void HandlePlayerHealthChange(int newVal)
    {
        List<Sprite> currentHearts = heartList[newVal];
        heart0.sprite = currentHearts[0];
        heart1.sprite = currentHearts[1];
        heart2.sprite = currentHearts[2];
    }
}
