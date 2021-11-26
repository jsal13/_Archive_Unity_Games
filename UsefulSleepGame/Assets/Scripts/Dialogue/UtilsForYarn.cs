using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class UtilsForYarn : MonoBehaviour
{

    DialogueRunner dialogueRunner;

    private void Awake()
    {
        dialogueRunner = this.GetComponent<DialogueRunner>();
    }

    private void Start()
    {
        dialogueRunner.AddFunction("random", () => { return GetRandomValue(); });

        dialogueRunner.AddCommandHandler<string>("add_to_player_inventory", AddToPlayerInventory);
    }

    public float GetRandomValue()
    {
        return Random.value;
    }

    public void AddToPlayerInventory(string item)
    {
        Debug.Log($"Added {item} to inventory!");
    }
}
