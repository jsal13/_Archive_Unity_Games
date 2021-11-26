using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDebugManager : MonoBehaviour
{
    public List<GameObject> debugObjects;

    private void Awake()
    {
        for (int idx = 0; idx < debugObjects.Count; idx += 1)
        {
            if (GameObject.Find(debugObjects[idx].name) == null)
            {
                debugObjects[idx].SetActive(true);
            }
        }
    }
}
