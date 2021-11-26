using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDestroyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> dontDestroyList;

    private void Awake()
    {
        dontDestroyList = new List<GameObject>() {
        GameObject.Find("Manager"),
        GameObject.Find("Overlays")
        };
    }

    private void Start()
    {
        foreach (GameObject go in dontDestroyList)
        {
            DontDestroyOnLoad(go);
        }
    }

}
