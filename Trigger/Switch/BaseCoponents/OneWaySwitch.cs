using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class OneWaySwitch : Switch
{
    [SerializeField]
    protected UnityEvent onActivate;

    protected override void Action()
    {
        onActivate.Invoke();
        Broadcast();
    }
    protected override void DrawIcon()
    {
        if (connectedList!= null && connectedList.Length == 0)
            Gizmos.DrawIcon(transform.position, "SW_END.png", true);

        else
            Gizmos.DrawIcon(transform.position, "SW_Switch.png", true);

    }
}