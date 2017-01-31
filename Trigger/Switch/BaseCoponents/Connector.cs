using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class Connector : Trigger, IBinary
{
    [HideInInspector]
    public OneWaySwitch connectedTo;

    [SerializeField]
    private bool state;

    public bool State
    {
        get { return state; }
        set { state = value; }
    }

    public void TurnOn()
    {
        if (!State)
        {
            State = true;
            Broadcast();
        }
    }
    public void TurnOff()
    {
        if (State)
        {
            State = false;
            Broadcast();
        }
    }

    protected override void Broadcast()
    {
        connectedTo.Activate();
    }
}