using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;

    public void SetState(State state)
    {
        currentState = state;
        StartCoroutine(currentState.Start());
    }
}

public abstract class State
{
    // Put in Statemachine in child.
    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator End()
    {
        yield break;
    }
}