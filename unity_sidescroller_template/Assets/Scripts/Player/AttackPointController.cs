using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointController : MonoBehaviour
{
    public List<GameObject> objsInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IAttackable>() != null)
        {
            objsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IAttackable>() != null)
        {
            objsInRange.Remove(other.gameObject);
        }
    }
}
