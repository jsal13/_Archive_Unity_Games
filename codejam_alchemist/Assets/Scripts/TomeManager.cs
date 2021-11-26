using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TomeManager : MonoBehaviour
{
    public Button tomeButton;
    public bool _tomeOpen;
    public bool TomeOpen
    {
        get => _tomeOpen;
        set
        {
            _tomeOpen = value;
            TomeOpened?.Invoke(value);
        }
    }

    public delegate void OnTomeOpened(bool value);
    public static event OnTomeOpened TomeOpened;

    public void OpenTome()
    {
        if (!GameManager.pauseInput)
        {
            TomeOpen = true;
        }
    }

    public void CloseTome()
    {
        if (!GameManager.pauseInput)
        {
            TomeOpen = false;
        }
    }

    public void ToggleTome()
    {
        if (!GameManager.pauseInput)
        {
            TomeOpen = !TomeOpen;
        }
    }

}
