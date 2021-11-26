using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WillOTheWispController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private float pauseDuration;
    private float distance;
    private int _direction = 1;
    private int Direction
    {
        get => _direction;
        set
        {
            _direction = value;
            this.transform.localScale = new Vector3(_direction == 1 ? 1 : -1, 1, 1);
        }
    }
    private bool _isMoving;
    private bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            animator.SetBool("isMoving", _isMoving);
        }
    }
    private Animator animator;
    private Rigidbody2D playerRB;
    private bool isNearPlayer;
    private float followDistance = 128f;
    private bool isFollowingPlayer = false;

    private void Awake()
    {
        speed = Random.Range(16, 25);
        pauseDuration = Random.Range(0, 3);
        distance = 16;

        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isNearPlayer = Vector2.Distance(player.transform.position, this.transform.position) < followDistance;

        if (isNearPlayer)
        {
            if (!isFollowingPlayer)
            {
                StartCoroutine(MoveTowardsPlayer());
            }

        }
        else
        {
            if (!isFollowingPlayer && !IsMoving)
            {
                // Patrol the area.
                StartCoroutine(Move());
            }
        }

    }

    private IEnumerator MoveTowardsPlayer()
    {
        isFollowingPlayer = true;
        yield return null;

        Vector2 currentTarget = new Vector2(player.transform.position.x, player.transform.position.y) + playerRB.velocity;

        float duration = Vector2.Distance(currentTarget, this.transform.position) / speed;

        Tween friendTween = transform.DOMove(currentTarget, duration);
        yield return friendTween.WaitForCompletion();
        isFollowingPlayer = false;
    }


    private IEnumerator Move()
    {
        Vector2 newPos = new Vector2(transform.position.x + (distance * Direction), transform.position.y);
        float duration = distance / speed;

        IsMoving = true;
        Tween friendTween = transform.DOMove(newPos, duration);
        yield return friendTween.WaitForCompletion();

        IsMoving = false;
        yield return new WaitForSeconds(pauseDuration);
        Direction = -Direction;
        yield return null;
    }
}
