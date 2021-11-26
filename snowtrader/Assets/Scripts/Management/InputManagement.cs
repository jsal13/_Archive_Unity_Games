using System;
using UnityEngine.InputSystem;

public sealed class InputManager
{
    private InputManager()
    {
    }

    private static readonly Lazy<InputManager> lazy = new Lazy<InputManager>(() => new InputManager());
    public static InputManager Instance
    {
        get => lazy.Value;
    }

    public static Gamepad gamepad = InputSystem.GetDevice<Gamepad>();


}