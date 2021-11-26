using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementLeftRight : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float pauseDuration;
    [SerializeField] private float distance;
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
            animator.SetBool("isWalking", _isMoving);
        }
    }
    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true && !PlayerManager.IsInTransition)
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
}