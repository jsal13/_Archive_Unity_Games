using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusButtonsController : MonoBehaviour
{
    public Button genuineBTN;
    public Button forgeryBTN;
    public LevelController levelController;

    void Awake()
    {
        levelController = GameObject.Find("Managers").gameObject.GetComponent<LevelController>();

        genuineBTN = gameObject.transform.Find("Genuine").GetComponent<Button>();
        forgeryBTN = gameObject.transform.Find("Forgery").GetComponent<Button>();

        genuineBTN.onClick.AddListener(levelController.PlayerClickedGenuineButton);
        forgeryBTN.onClick.AddListener(levelController.PlayerClickedForgeryButton);

    }

    private void OnEnable()
    {
        LevelController.PlayerCorrect += HandlePlayerCorrect;
    }

    private void OnDisable()
    {
        LevelController.PlayerCorrect -= HandlePlayerCorrect;
    }

    private void HandlePlayerCorrect(bool _)
    {
        StartCoroutine(DisableButtonsTemporarily());
    }

    public IEnumerator DisableButtonsTemporarily()
    {
        genuineBTN.interactable = false;
        forgeryBTN.interactable = false;
        yield return new WaitForSeconds(1.5f);
        genuineBTN.interactable = true;
        forgeryBTN.interactable = true;

    }

}
