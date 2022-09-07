using UnityEngine;
public class GetARCameraDirection : MonoBehaviour {
    private float northHeading;

    private float myHeading;
    private float dif;

    // Update is called once per frame
    private void Update()
    {
        // only use the Y component of the objects orientation
        // always returns a value between 0 and 360
        myHeading = transform.eulerAngles.y;
        // also this is always a value between 0 and 360
        northHeading = Input.compass.magneticHeading;

        dif = myHeading - northHeading;
        // wrap the value so it is always between 0 and 360
        if (dif < 0) dif += 360f;
    }

    // Only for debug and demo
    // draw a pointer towards north
    private void OnDrawGizmos()
    {
        var northDirection = (Quaternion.Euler(0, northHeading, 0) * Vector3.forward).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + northDirection);

        var objectDirection = (Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward).normalized;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + objectDirection);
    }
}
