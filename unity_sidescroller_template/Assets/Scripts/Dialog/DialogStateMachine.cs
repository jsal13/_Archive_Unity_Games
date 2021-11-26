using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCController))]
public class DialogStateMachine : StateMachine
{
    public DialogStateMachine(NPCController npc)
    {
        this.npc = npc;
    }

    public NPCController npc;

    private void Start()
    {
        SetState(new Enter(this));
    }
}

public class DialogState : State
{
    protected DialogStateMachine DialogStateMachine;

    public delegate void BeginDialog();
    public static BeginDialog OnBeginDialog;

    public delegate void BeginDialogCycle(NPCController npc);
    public static BeginDialogCycle OnBeginDialogCycle;

    public delegate void EndDialog();
    public static EndDialog OnEndDialog;

    public DialogState(DialogStateMachine dialogStateMachine)
    {
        DialogStateMachine = dialogStateMachine;
    }
}

#region States

public class Enter : DialogState
{
    public Enter(DialogStateMachine dialogStateMachine) : base(dialogStateMachine) { }

    public override IEnumerator Start()
    {
        OnBeginDialog?.Invoke();
        DialogStateMachine.SetState(new DialogCycle(DialogStateMachine));
        yield return null;
    }
}

public class DialogCycle : DialogState
{
    public bool isComplete = false;

    public DialogCycle(DialogStateMachine DialogStateMachine) : base(DialogStateMachine)
    {
    }

    public override IEnumerator Start()
    {
        OnBeginDialogCycle?.Invoke(DialogStateMachine.npc);

        while (PlayerManager.Instance.isInDialog)
        {
            yield return null;
        }

        if (DialogStateMachine.npc.GetDialogType() == "Trader")
        {
            DialogStateMachine.SetState(new Trade(DialogStateMachine));
        }
        else
        {
            DialogStateMachine.SetState(new Exit(DialogStateMachine));
        }
        yield return null;
    }
}

public class Trade : DialogState
{

    public Trade(DialogStateMachine DialogStateMachine) : base(DialogStateMachine)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("Start trade!");
        yield return new WaitForSeconds(1f);
        DialogStateMachine.SetState(new Exit(DialogStateMachine));
    }
}

public class Exit : DialogState
{
    public Exit(DialogStateMachine DialogStateMachine) : base(DialogStateMachine)
    {
    }

    public override IEnumerator Start()
    {
        OnEndDialog?.Invoke();
        yield return null;
        GameObject.Destroy(DialogStateMachine);
    }
}
#endregion