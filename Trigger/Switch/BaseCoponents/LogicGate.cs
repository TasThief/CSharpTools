using UnityEngine;
using System.Collections;
using System;

public enum LogicGatesTypes
{
    AND,
    OR,
    XOR
}

[RequireComponent(typeof(Connector))]
public sealed class LogicGate : Switch, IBinary
{
    private delegate bool GateVerification();

    private GateVerification gateVerification;

    [SerializeField]
    private LogicGatesTypes GateType;

    private Connector[] avaliableConnectors;

    public bool State { get; set; }

    protected override void OnStart()
    {
        avaliableConnectors = GetComponents<Connector>();
        switch (GateType)
        {
            case LogicGatesTypes.AND:
                gateVerification = ANDGate;
                break;
            case LogicGatesTypes.OR:
                gateVerification = ORGate;
                break;
            case LogicGatesTypes.XOR:
                gateVerification = XORGate;
                break;
        }
    }

    protected override void Action()
    {
        bool newState = gateVerification();
        if ((State && !newState) || (newState && !State))
        {
            State = newState;
            Broadcast();
        }
    }
    public void TurnOn()
    {
        if (!State)
            Broadcast();
    }
    public void TurnOff()
    {
        if (State)
            Broadcast();
    }

    private bool XORGate()
    {
        bool result = false;
        bool done = false;

        for (int i = 0; i < avaliableConnectors.Length && !done; i++)
            if (avaliableConnectors[i])
            {
                result = true;
                for (int j = i + 1; j < avaliableConnectors.Length && result; j++)
                    if (avaliableConnectors[i])
                        result = false;
                done = true;
            }

        return result;
    }
    private bool ORGate()
    {
        bool result = false;
        foreach (var item in avaliableConnectors)
            result |= item.State;

        return result;
    }
    private bool ANDGate()
    {
        bool result = true;
        foreach (var item in avaliableConnectors)
            result &= item.State;

        return result;
    }
    protected override void DrawIcon()
    {
        switch (GateType)
        {
            case LogicGatesTypes.AND:
                Gizmos.DrawIcon(transform.position, "SW_AND.png", true);
                break;
            case LogicGatesTypes.OR:
                Gizmos.DrawIcon(transform.position, "SW_OR.png", true);
                break;
            case LogicGatesTypes.XOR:
                Gizmos.DrawIcon(transform.position, "SW_XOR.png", true);
                break;
        }
    }

}
