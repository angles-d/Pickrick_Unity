using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place the object this script is attached to in front of the camera
public class PlaceObjectInFrontOfCamera : MonoBehaviour {
    
    // A Reference to the AR camera
    public Camera arCamera;
    //  How far you want to object from the camera
    [Range(0,100)]
    public float distance;

    // Run this function when you want to place the object in front of camera
    public void PlaceInFrontOfCamera() {
        // Put the object in front of the camera
        transform.position = arCamera.transform.position + arCamera.transform.forward * distance;
        // Make the y rotation equal to the cameras y rotation
        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, arCamera.transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerRotation);   
    }
}
