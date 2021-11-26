using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition;

    public delegate void TeleportingPlayer(bool value);
    public static TeleportingPlayer OnTeleportingPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "Player")
        {
            StartCoroutine(TeleportPlayer(other.gameObject, targetPosition));
        }
    }

    private IEnumerator TeleportPlayer(GameObject player, Vector3 targetPosition)
    {
        OnTeleportingPlayer?.Invoke(true);
        yield return null;

        player.SetActive(false);
        player.transform.position = targetPosition;
        yield return new WaitForSeconds(0.5f);

        player.SetActive(true);
        OnTeleportingPlayer?.Invoke(false);
        yield return null;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(targetPosition, new Vector3(16, 16, 1));
    }

}
