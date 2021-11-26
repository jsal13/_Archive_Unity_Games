using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneManager : MonoBehaviour
{
    public static List<GameObject> gameObjectsToKeep;

    private void Awake()
    {
        gameObjectsToKeep = new List<GameObject>(){
            GameObject.Find("GameManager"),
            // GameObject.Find("Main Camera"),
            // GameObject.Find("Player"),
            // GameObject.Find("GUI")
        };

        foreach (GameObject go in gameObjectsToKeep)
        {
            if (go.activeInHierarchy)
            {
                DontDestroyOnLoad(go);
            }
        }
    }
}
