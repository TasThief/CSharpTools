using UnityEngine;
using System.Collections;

public class Clock : Switch
{
    public bool initialBroadcast;
    public float waitTime;

    protected override void Action()
    {
        if (initialBroadcast)
            Broadcast();

        Invoke("Broadcast", waitTime);
    }
    protected override void DrawIcon()
    {
        Gizmos.DrawIcon(transform.position, "SW_TIME.png", true);
    }
}
