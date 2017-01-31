using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public  class BinarySwitch : OneWaySwitch, IBinary
{
    protected override void OnStart()
    {
        if (state)
            onActivate.Invoke();
        else
            onDeactivate.Invoke();
    }
    [SerializeField]
    protected UnityEvent onDeactivate;

    [SerializeField]
    private bool state;

    public bool State
    {
        get{    return state;}
        set{    state = value;}
    }

    protected override void Action()
    {
        if (State)
            TurnOff();

        else
            TurnOn();
    }

    public void TurnOn()
    {
        if (!State)
        {
            State = true;
            onActivate.Invoke();
            Broadcast();
        }
    }
    public void TurnOff()
    {
        if(State)
        {
            State = false;
            onDeactivate.Invoke();
            Broadcast();
        }
    }
}
