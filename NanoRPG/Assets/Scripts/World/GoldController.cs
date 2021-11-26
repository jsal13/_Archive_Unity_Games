using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    [SerializeField] private int value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerManager.Instance.Gold += value;
            Destroy(gameObject);
        }
    }

}
