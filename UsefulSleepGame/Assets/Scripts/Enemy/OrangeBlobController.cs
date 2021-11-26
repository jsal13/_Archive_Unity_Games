using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBlobController : EnemyController
{
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer sprite;
    private bool isMoving = false;
    [SerializeField] private float xForce;
    [SerializeField] private float yForce = 10;
    [SerializeField] private int currentHealth;
    private int direction = 1;
    private bool isHit;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
        currentHealth = info.health;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        // Initial pause.
        yield return new WaitForSeconds(5 * Random.value);

        while (true)
        {
            if (!isHit)
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
                    yield return null;
                }
            }
            yield return null;
        }
    }

    public override void IsHit()
    {
        // Stop movement, get thrown backward.
        Vector3 hitVel = info.knockbackVelXForce * (this.transform.position - player.position).normalized;
        hitVel.x *= info.knockbackVelXForce;
        hitVel.y *= info.knockbackVelY;
        // TODO: Should we have negative y vel?
        hitVel = new Vector3(Mathf.Clamp(hitVel.x, -130, 130), Mathf.Clamp(hitVel.y, -130, 130));
        StartCoroutine(Combat.FlashRed(sprite, 0.4f));
        currentHealth -= 1;

        if (currentHealth > 0)
        {
            StartCoroutine(KnockbackPlayer(hitVel));
        }
        else
        {
            Death();
        }
    }

    private IEnumerator KnockbackPlayer(Vector3 hitVel)
    {
        isHit = true;
        rb.AddForce(hitVel, ForceMode2D.Impulse);
        yield return new WaitForSeconds(info.knockbackDuration);
        isHit = false;
        yield return null;
    }

    public override int GetTemperature()
    {
        return info.temperature;
    }

    public override void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return null;
        // TODO: Set an animation maybe.
        Destroy(this.gameObject);
    }
}
