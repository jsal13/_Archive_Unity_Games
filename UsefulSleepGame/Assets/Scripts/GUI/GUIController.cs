using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Canvas> canvases;

    private void OnEnable()
    {
        PopulateCanvasCameras();
        SceneManager.activeSceneChanged += HandleActiveSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= HandleActiveSceneChanged;
    }

    private void HandleActiveSceneChanged(Scene _arg0, Scene _arg1)
    {
        PopulateCanvasCameras();
    }

    private void PopulateCanvasCameras()
    {
        mainCamera = Camera.main;
        foreach (Canvas canvas in this.GetComponentsInChildren<Canvas>())
        {
            canvas.worldCamera = mainCamera;
        }
    }
}
