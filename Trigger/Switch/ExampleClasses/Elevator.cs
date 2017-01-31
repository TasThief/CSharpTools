using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    [SerializeField]
    Transform[] points;
    int actualPoint;
    [SerializeField]
    float speed;

	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, points[actualPoint].position, speed * Time.deltaTime);
	}
    public void Swap()
    {
        if (actualPoint == 0)
            actualPoint = 1;

        else
            actualPoint = 0;

    }
}
