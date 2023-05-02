using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEditor;

public class LeftRightNavigation : MonoBehaviour
{
    public Renderer target;
    public Image leftNav;
    public Image rightNav;
    public Camera aRCamera;




    // Hide both left and right arrow images when script activated
    void Start() {
        leftNav.enabled = false;
        rightNav.enabled = false;

    }

    // Check if target is in frame
    // if target is in frame, disable the left and right nav arrows
    // if the target is not in frame, decide if the right or left nav arrow should be enabled depending on camera position
	void Update () {
        if (target.isVisible) {

            leftNav.enabled = false;
            rightNav.enabled = false;
        } else {



            GetArrowDirection();
        }
	}

    // Get the direction the camera needs to go for the target to be on screen, and enable appropriate arrow
    void GetArrowDirection() {
        Vector3 fowardCameraDirection = aRCamera.transform.forward;
        Vector3 upCameraDirection = aRCamera.transform.up;
        Vector3 targetDirection = target.gameObject.transform.position - aRCamera.transform.position;
        Vector3 perp = Vector3.Cross(fowardCameraDirection, targetDirection);
        float dir = Vector3.Dot(perp, upCameraDirection);
        if (dir > 0f) {
            rightNav.enabled = true;
            leftNav.enabled = false;
		} else if (dir < 0f) {
            rightNav.enabled = false;
            leftNav.enabled = true;
		} else {
            rightNav.enabled = false;
            leftNav.enabled = false;
		}
    }



}

