using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(GameManager.mouse);
    }
}
