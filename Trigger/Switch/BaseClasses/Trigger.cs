using UnityEngine;

public abstract class Trigger : MonoBehaviour {
    private TriggerSideEffect[] interatorList;
    protected abstract void Broadcast();
    public bool on = true;

    protected virtual void Action() {
        Broadcast();
    }

    public void Activate() {
        if(on) {
            Action();

            foreach(TriggerSideEffect interator in interatorList)
                interator.Activate(this);
        }
    }

    void Start() {
        interatorList = GetComponents<TriggerSideEffect>();
        OnStart();
    }

    public void Sleep(float sleepDuration) {
        on = false;
        Invoke("Wake", sleepDuration);
    }
    public void Ibernate() {
        on = false;
    }
    public void Wake() {
        on = true;
    }
    protected virtual void OnStart() { }
}

public abstract class Switch : Trigger {
    [SerializeField]
    protected Trigger[] connectedList;

    protected override void Broadcast() {
        foreach(Trigger connected in connectedList)
            connected.Activate();
    }

    protected virtual void DrawIcon() { }

    void OnDrawGizmos() {
        DrawIcon();
        if(connectedList != null && connectedList.Length != 0) {
            Gizmos.color = Color.black;
            foreach(Trigger connected in connectedList) {
                Gizmos.DrawLine(transform.position, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.left * 0.04f, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.up * 0.04f, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.down * 0.04f, connected.transform.position);
                Gizmos.DrawLine(transform.position + Vector3.right * 0.04f, connected.transform.position);
            }
        }
    }
}

public interface IBinary {
    bool State { get; set; }
    void TurnOn();
    void TurnOff();
}

[RequireComponent(typeof(Trigger))]
public abstract class TriggerSideEffect : MonoBehaviour {
    public abstract void Activate(Trigger activator);
}
