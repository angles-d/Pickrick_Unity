using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This class is attached to the compass Panel
// and realized the compass functionality

public class Compass : MonoBehaviour {

	// reference to the compass needle
	public Image pointerImg;

	// reference to the interference / calibration picture
	public Image interferenceImg;

	// this is a reference to the magnetometer component
	// you may use it, but you do not have to
	public MagnetoMeter magnetoMeter;

	// Use this for initialization
	void Start () {
		// start the location service
		Input.location.Start ();
		// enable the compass
		Input.compass.enabled = true;
		// enable the gyro
		Input.gyro.enabled = true;
	}


	//
	// void Update()
	//
	// this method is called every frame
	//

	void Update() {
		moveCompass ();
	}
		

	public void moveCompass() {
		// let's see if the user has attached a magnetometer read to the compass
		if (magnetoMeter != null) {
			if (magnetoMeter.IsInterference) {
				interferenceImg.gameObject.SetActive (true);
			} else {
				interferenceImg.gameObject.SetActive (false);
			}
		}
		// read the magnetometer / compass value and generate a quaternion
		Quaternion trueHeading = Quaternion.Euler (0, 0, Input.compass.trueHeading);
		// interpolate between old and new position
		pointerImg.transform.localRotation = Quaternion.Slerp (pointerImg.transform.localRotation, trueHeading, 0.05f);	
	}

	// Get the current direction in degrees
	public float getDegrees() {
		float imagePosition = pointerImg.transform.eulerAngles.z;
		imagePosition = 360f - imagePosition;

		// ADDED THIS CAUSE COMPASS APPEARED TO BE FLIPPED IN THE WRONG DIRECTION
		if (imagePosition > 0 && imagePosition < 180) {
			imagePosition += 180;
		} else if (imagePosition > 180) {
			imagePosition -= 180;
		}
		
		//Debug.Log(imagePosition);
		return imagePosition;
	}

}
