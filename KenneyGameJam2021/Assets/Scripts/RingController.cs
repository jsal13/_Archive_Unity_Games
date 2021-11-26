using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Yarn.Unity;

public class RingController : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;

    private void Awake()
    {
        dialogueRunner = GameObject.Find("Overlays/Dialogue System").GetComponent<DialogueRunner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerManager.Instance.CanMove = false;
            SceneManager.LoadScene("EndScene");
        }
    }
}
