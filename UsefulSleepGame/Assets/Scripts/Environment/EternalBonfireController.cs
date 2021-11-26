using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EternalBonfireController : MonoBehaviour, ITemperature
{
    [SerializeField] private int temperature = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached!");
            GameManager.checkpoint.position = this.transform.position;
            GameManager.checkpoint.sceneName = SceneManager.GetActiveScene().name;
        }
    }

    public int GetTemperature()
    {
        return temperature;
    }
}
