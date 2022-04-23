using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public enum OrbState { white, blue }

public class Orb
{
    // Contains information related to an Orb object.
    private OrbState _state;
    public OrbState State
    {
        get => _state;
        set
        {
            _state = value;
            OnOrbStateChange?.Invoke(value);
        }
    }

    public Action<OrbState> OnOrbStateChange;
}

public class OrbController : MonoBehaviour
{
    private Orb orb;
    [SerializeField] private SpriteRenderer orbSpriteRenderer;
    [SerializeField] private KeyCode associatedKey;
    [SerializeField] private InputAction stateChangeAction;
    [SerializeField] private TMP_Text label;

    private void Awake()
    {
        orb = new Orb();
        orbSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        label = transform.GetComponentInChildren<TMP_Text>();

        // Initialize values;
        stateChangeAction = new InputAction("orbStateChange", binding: $"<Keyboard>/{associatedKey.ToString().ToLower()}");

        switch (associatedKey)
        {
            case KeyCode.Semicolon:
                label.text = ";";
                break;
            case KeyCode.L:
                label.text = "L";
                break;
            default:
                label.text = $"{associatedKey.ToString().ToLower()}";
                break;
        }
    }

    private void Start()
    {
        // Initial states.
        orb.State = OrbState.white;
        label.color = Color.blue;
    }

    private void OnEnable()
    {
        stateChangeAction.Enable();

        stateChangeAction.performed += HandleChangeStateKeyPressed;
        orb.OnOrbStateChange += HandleOrbStateChange;
    }

    private void OnDisable()
    {
        stateChangeAction.Disable();

        stateChangeAction.performed -= HandleChangeStateKeyPressed;
        orb.OnOrbStateChange -= HandleOrbStateChange;
    }

    private void HandleChangeStateKeyPressed(InputAction.CallbackContext obj)
    {
        if (orb.State == OrbState.white)
        {
            orb.State = OrbState.blue;
            label.color = Color.white;
        }
        else if (orb.State == OrbState.blue)
        {
            orb.State = OrbState.white;
            label.color = Color.blue;
        }
    }

    private void HandleOrbStateChange(OrbState orbState)
    {
        switch (orbState)
        {
            case OrbState.white:
                orbSpriteRenderer.color = Color.white;
                break;
            case OrbState.blue:
                orbSpriteRenderer.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
