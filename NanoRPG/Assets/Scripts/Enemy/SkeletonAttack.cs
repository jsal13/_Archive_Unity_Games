using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    private GameObject boulder;
    [SerializeField] private float frequency;
    private List<Vector3> dirList;

    // TODO: WE NEED TO DO OBJECT POOLING HERE.
    private void Start()
    {
        frequency = Random.Range(2, 8);
        boulder = Resources.Load<GameObject>("Prefabs/Boulder");
        dirList = new List<Vector3>() { Vector3.left, Vector3.down, Vector3.right, Vector3.up };
        InvokeRepeating(nameof(ThrowBolder), frequency, frequency);
    }

    private void ThrowBolder()
    {
        Vector3 randDirection = dirList[Random.Range(0, 4)];
        GameObject go = Instantiate(boulder, transform.position, Quaternion.identity);
        go.GetComponent<BoulderController>().SetDirection(randDirection);
    }
}
