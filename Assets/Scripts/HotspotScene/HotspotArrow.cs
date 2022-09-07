using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Attach this script to the arrow 3d model and make the child of the camera object
public class HotspotArrow : MonoBehaviour {
    
    // A reference to the gameobject the arrow will point to
    public GameObject targetObject;  
    Camera referenceCamera;
    public float rotationSpeed;

    private Quaternion rotation;
    private Vector3 direction;



    private void Start() {
        
        // Try to get the reference camera
        try {
            referenceCamera = transform.parent.gameObject.GetComponent<Camera>();
        } catch {
            throw new Exception("Could not find the camera. Make sure to make this gameObject the child of an object with a camera");
        }

        transform.position = referenceCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/15, 1));
    }

	void LateUpdate () {
        direction = (targetObject.transform.position - transform.position).normalized;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        // var dir = referenceCamera.WorldToScreenPoint(targetObject.transform.position) - 
        // referenceCamera.WorldToScreenPoint(transform.position);
        // var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}

}
