using UnityEngine;
using System.Collections;
using System;

public class Repeater : Switch, IBinary
{
    public bool State { get; set; }

    public float interval;

    public void TurnOn()
    {
        if (!State)
        {
            State = true;
            InvokeRepeating("Broadcast", 0.0f, interval);
        }
    }
    public void TurnOff()
    {
        if (State)
        {
            State = false;
            CancelInvoke("Broadcast");
        }
    }

    protected override void Action()
    {
        if (State)
            TurnOff();

        else
            TurnOn();
    }
    protected override void DrawIcon()
    {
        Gizmos.DrawIcon(transform.position, "SW_REPEATER.png", true);
    }
}
