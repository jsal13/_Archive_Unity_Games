using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointController : MonoBehaviour
{
    public List<GameObject> objsInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Make a general attackable thing.
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnvironmentResource") || other.GetComponent<ISlashable>() != null)
        {
            objsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnvironmentResource") || other.GetComponent<ISlashable>() != null)
        {
            objsInRange.Remove(other.gameObject);
        }
    }
}
