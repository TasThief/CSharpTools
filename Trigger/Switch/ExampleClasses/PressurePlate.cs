using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BinarySwitch),typeof(Collider2D))]
public class PressurePlate : MonoBehaviour
{
    BinarySwitch bSwitch;

	void Start ()
    {
        bSwitch = GetComponent<BinarySwitch>();
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        bSwitch.TurnOn();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        bSwitch.TurnOff();
    }
}
