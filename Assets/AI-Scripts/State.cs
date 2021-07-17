using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State 
{
    public Action ActiveAction;
    //the action that is called whenever we enter this state.
    public Action OnEnterAction;

    public Action OnExitAction;

    public State(Action active, Action OnEnter, Action OnExit)
    {
        ActiveAction = active;
        OnEnterAction = OnEnter;
        OnExitAction = OnExit;
    }

    public void Execute()
    {
        //if there is no active action that we can go to then dont invoke
        if (ActiveAction != null)
            ActiveAction.Invoke();
    }

    public void OnEnterExecute()
    {
        //same as above if there is no OnEnter state we can go to then dont Invoke
        if (OnEnterAction != null)
            OnEnterAction.Invoke();
    }

    public void OnExit()
    {
        if (OnExitAction != null)
            OnExitAction.Invoke();
    }
}
