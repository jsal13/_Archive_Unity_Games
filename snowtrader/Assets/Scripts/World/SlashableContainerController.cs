using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlashableContainerController : MonoBehaviour
{
    [System.Serializable]
    public class GrassField
    {
        public int xMin;
        public int xMax;
        public float y;
        public float probability;
        public string sceneName;
    }

    public List<GrassField> grassFields;
    GameObject grassPrefab;
    

    private void Awake()
    {
        grassPrefab = Resources.Load<GameObject>("Prefabs/Slashables/Grass");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode _1)
    {
        foreach (GrassField grassField in grassFields)
        {
            if (scene.name == grassField.sceneName)
            {
                Debug.Log("I got here!");
                for (int idx = 0; idx < (grassField.xMax - grassField.xMin); idx += 1)
                {
                    if (UnityEngine.Random.value < grassField.probability)
                    {
                        var go = Instantiate(grassPrefab, transform, true);
                        go.transform.position = new Vector3(grassField.xMin + idx, grassField.y, 0);
                    }
                }
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
