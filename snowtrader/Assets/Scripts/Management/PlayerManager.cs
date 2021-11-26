using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //> STRUCTURES.
    private BoxCollider2D playerCollider;
    private ContactFilter2D contactFilter;

    //> TRANSITIONS.
    private TransitionController currentTransitionInfo;
    private bool canTransition;

    //> STATUS BOOLEANS.
    private bool _isCrafting = false;
    public bool IsCrafting
    {
        get => _isCrafting;
        set
        {
            _isCrafting = value;
            OnPlayerOpenedCrafting?.Invoke(_isCrafting);
        }
    }

    public bool IsOnGround { get; set; }
    public bool CanWalk { get; set; }

    [SerializeField] private bool _canMove;
    public bool CanMove
    {
        get => _canMove;
        set
        {
            _canMove = value;
            CanWalk = _canMove;
        }
    }

    private bool _isDead;
    public bool IsDead
    {
        get => _isDead;
        set
        {
            _isDead = value;
            OnPlayerDied?.Invoke(_isDead);
            if (_isDead) HandlePlayerDied();
        }
    }

    public delegate void LevelTransition(TransitionController.Transition transition);
    public static event LevelTransition OnLevelTransition;

    public delegate void PlayerOpenedCrafting(bool value);
    public static event PlayerOpenedCrafting OnPlayerOpenedCrafting;

    public delegate void PlayerDied(bool value);
    public static event PlayerDied OnPlayerDied;

    private void Awake()
    {
        //if (PersistenceManager.initLevelLocation != null)
        //{
        //    transform.position = PersistenceManager.initLevelLocation;
        //}

        playerCollider = GetComponent<BoxCollider2D>();
        contactFilter = PersistenceManager.GetPlayerToGroundContactFilter();

        CanMove = true;
        CanWalk = true;
    }

    private void Update()
    {
        IsOnGround = playerCollider.IsTouching(contactFilter);
        if (InputManager.gamepad.leftStick.up.wasPressedThisFrame && canTransition)
        {
            OnLevelTransition?.Invoke(currentTransitionInfo.transition);
        }

        if (InputManager.gamepad.buttonNorth.wasPressedThisFrame)
        {
            IsCrafting = !IsCrafting;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint")) {
            SetCheckpoint(
                collision.gameObject.transform.position + new Vector3(0, 0.5f), 
                SceneManager.GetActiveScene().name
            );
        }

        if (collision.CompareTag("Transition"))
        {
            canTransition = true;
            currentTransitionInfo = collision.GetComponent<TransitionController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Transition"))
        {
            canTransition = false;
            currentTransitionInfo = null;
        }
    }

    private void OnEnable()
    {
        HarvestRersource.OnChoppingForest += HandleChoppingForest;
        Dialog.OnPlayerSpeaking += HandlePlayerSpeaking;
        Dialog.OnPlayerTrading += HandlePlayerTrading;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
    }

    private void OnDisable()
    {
        HarvestRersource.OnChoppingForest -= HandleChoppingForest;
        Dialog.OnPlayerSpeaking -= HandlePlayerSpeaking;
        Dialog.OnPlayerTrading -= HandlePlayerTrading;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
    }

    private void SetCheckpoint(Vector3 loc, string sceneName)
    {
        PersistenceManager.Instance.checkpoint.loc = loc;
        PersistenceManager.Instance.checkpoint.sceneName = sceneName;
    }

    private void HandleChoppingForest(bool value, int _)
    {
        // If chopping wood, don't let the player move.
        CanMove = !value;
    }

    private void HandlePlayerSpeaking(bool value)
    {
        // If speaking, don't let the player move.
        CanMove = !value;
    }

    private void HandlePlayerTrading(NPCController.NPC _)
    {
        CanMove = false;
    }

    private void HandleTradeMenuEnd()
    {
        CanMove = true;
    }

    private void HandlePlayerDied()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        CanMove = false;

        SceneManager.LoadSceneAsync(PersistenceManager.Instance.checkpoint.sceneName);
        //TODO: DO I NEED TO WAIT FOR THIS ASYNC?  HOW.
        yield return new WaitForSeconds(1.5f);
        transform.position = PersistenceManager.Instance.checkpoint.loc;
        IsDead = false;
        CanMove = true;
    }

}
