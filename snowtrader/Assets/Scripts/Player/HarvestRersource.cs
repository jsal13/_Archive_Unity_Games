using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestRersource : MonoBehaviour
{
    private GameObject playerObj;
    private bool _canStartChopping = false;
    public bool CanStartChopping
    {
        get => _canStartChopping;
        set
        {
            if (value) { _canStartChopping = value; }
            else
            {
                _canStartChopping = value;
                ChopProgress = 0;
            }
        }
    }

    private readonly float chopDuration = 1f;
    private readonly int woodPerChop = 1;
    private Coroutine chopCoroutine;
    private float chopStartTime;
    private float _chopProgress;
    public float ChopProgress
    {
        get => _chopProgress;
        set
        {
            if (value < 1)
            {
                _chopProgress = value;
            }
            else
            {
                PopupWoodIcon();
                OnChoppingForest(true, woodPerChop);
                _chopProgress = 0;
            }
        }
    }

    private GameObject woodHarvestedIconPrefab;

    public delegate void ChoppingForest(bool value, int quantity);
    public static event ChoppingForest OnChoppingForest;

    public delegate void PlayerNearInteractable(bool value);
    public static event PlayerNearInteractable OnPlayerNearInteractable;

    private void Awake()
    {
        playerObj = GameObject.Find("Player");
        woodHarvestedIconPrefab = Resources.Load<GameObject>("Prefabs/WoodHarvestedIcon");
    }

    private void Update()
    {
        if (CanStartChopping || ChopProgress > 0)
        {
            ChopAction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Forest"))
        {
            CanStartChopping = true;
            OnPlayerNearInteractable?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Forest"))
        {
            CanStartChopping = false;
            ChopProgress = 0;
            OnChoppingForest?.Invoke(false, 0);
            OnPlayerNearInteractable?.Invoke(false);
        }
    }

    private void ChopAction()
    {
        if (InputManager.gamepad.buttonWest.isPressed && CanStartChopping && ChopProgress == 0)
        {
            OnChoppingForest?.Invoke(true, 0);
            ChopProgress = 0.001f;  // Make slightly non-zero.
            OnChoppingForest?.Invoke(true, 0);
            chopStartTime = Time.time;
            chopCoroutine = StartCoroutine(ChopCoroutine());
        }
        else if (InputManager.gamepad.buttonWest.isPressed && ChopProgress > 0)
        {
            ChopProgress = (Time.time - chopStartTime) / chopDuration;
            OnChoppingForest?.Invoke(true, 0);
        }

        else if (InputManager.gamepad.buttonWest.wasReleasedThisFrame)
        {
            StopCoroutine(chopCoroutine);
            ChopProgress = 0;
            OnChoppingForest?.Invoke(false, 0);
            CanStartChopping = true;
        }
    }

    private IEnumerator ChopCoroutine()
    {
        yield return new WaitForSeconds(chopDuration);
    }

    private void PopupWoodIcon()
    {
        GameObject woodIcon = Instantiate<GameObject>(woodHarvestedIconPrefab);
        woodIcon.transform.parent = playerObj.transform;
        woodIcon.transform.localPosition = new Vector3(0.05f, 0.65f);

        var delay = 0.5f;
        Destroy(woodIcon, delay);
    }
}
