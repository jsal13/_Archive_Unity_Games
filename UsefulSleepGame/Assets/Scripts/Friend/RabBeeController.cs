using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabBeeController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float xVel = 12;
    [SerializeField] private float duration = 2;
    private bool _isMoving;
    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            if (_isMoving)
            {
                animator.SetTrigger("Hop");
            }
        }
    }
    private int _direction = 1;
    public int Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            sprite.flipX = _direction == -1;
        }
    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!IsMoving)
        {
            StartCoroutine(HopAround());
        }
    }

    private IEnumerator HopAround()
    {
        IsMoving = true;
        float t = 0;
        while (t < duration)
        {
            this.transform.position += new Vector3(Direction * xVel * Time.deltaTime, 0, 0);
            t += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        IsMoving = false;
        Direction = -1 * Direction;
        yield return null;
    }

}
