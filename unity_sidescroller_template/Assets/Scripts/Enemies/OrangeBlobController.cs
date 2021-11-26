using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBlobController : MonoBehaviour, IAttackable
{
    private Rigidbody2D rb;
    private bool isMoving = false;
    [SerializeField] private float xForce;
    [SerializeField] private float yForce = 10;
    private int direction = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        // Initial pause.
        yield return new WaitForSeconds(5 * Random.value);

        while (true)
        {
            if (isMoving)
            {
                rb.AddForce(new Vector2(direction * xForce, yForce), ForceMode2D.Impulse);
                yield return new WaitForSeconds(5 * Random.value + 0.01f);
                isMoving = false;
            }
            else
            {
                // Get random values for next movement.
                direction = Random.value < 0.5 ? -1 : 1;
                xForce = Random.Range(25, 125);
                yForce = Random.Range(50, 100);
                isMoving = true;
            }

        }


    }

}
