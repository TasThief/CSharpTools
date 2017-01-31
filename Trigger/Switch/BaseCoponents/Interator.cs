using UnityEngine;
using System.Collections;

public class Interator : Switch
{
    private int actualSwitch;

    public int ActualSwitch
    {
        get { return actualSwitch; }
        set { actualSwitch = Mathf.Clamp(value, 0, connectedList.Length); }
    }

    protected override void Action()
    {
        if (connectedList.Length > 0)
        {
            connectedList[actualSwitch].Activate();
            actualSwitch++;
        }
    }
    protected override void DrawIcon()
    {
        Gizmos.DrawIcon(transform.position, "SW_INTERATOR.png", true);
    }

}
