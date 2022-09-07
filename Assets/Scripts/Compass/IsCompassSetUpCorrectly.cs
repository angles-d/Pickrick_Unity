using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Determines if the compass is in the right position, at the right level, and have appropriate magnetic value
public class IsCompassSetUpCorrectly : MonoBehaviour {

    // bool if compass is set up right
    public bool isCompassSetUpCorrectly;
    // References to compass scripts
    public CompassTargetDirection compassTargetDirection;
    public MagnetoMeter magnetoMeter;
    // Referemce to AR camera
    public Camera arCamera;


    // Start is called before the first frame update
    void Start() {
        isCompassSetUpCorrectly = false;
    }

    // Update is called once per frame
    void Update() {
        float cameraXRotation = arCamera.transform.eulerAngles.x;
        if ((cameraXRotation >= 0 && cameraXRotation <= 20) || (cameraXRotation >= 340 && cameraXRotation <= 360)) {
            if (compassTargetDirection.getIsCorrectDirection() && magnetoMeter.getIsProperMagneticValue()) {
                isCompassSetUpCorrectly = true;
            } else {
                isCompassSetUpCorrectly = false;
            }
        } else {
            isCompassSetUpCorrectly = false;
        }
    }
}
