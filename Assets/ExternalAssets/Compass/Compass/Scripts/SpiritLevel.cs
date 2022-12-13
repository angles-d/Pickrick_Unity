using UnityEngine;
using System.Collections;

// This class is attached to the level meter component

public class SpiritLevel : MonoBehaviour {

	public GameObject spiritLevel;

	Vector3 accVector = Vector3.zero;

	// References to the component's parts
	// initiialize them once in the start method
	// to avoid FindChilds every time in the update
	GameObject levelImgGreen;
	GameObject levelImgRed;
	GameObject exclImg;
	GameObject checkedImg;
	GameObject circleBorderGreenImg;
	GameObject circleBorderRedImg;

	bool isProperSpiritLevel;



	//
	// void Start ()
	//
	// Initialize the references to the spirit's components
	//

	void Start () {
		// set the references to the component's parts
		levelImgGreen = transform.Find ("spirit").Find ("levelImgGreen").gameObject;
		levelImgRed = transform.Find ("spirit").Find ("levelImgRed").gameObject;
		exclImg = transform.Find ("statusIcon").Find ("exclImg").gameObject;
		checkedImg = transform.Find ("statusIcon").Find ("checkedImg").gameObject;
		circleBorderGreenImg = transform.Find ("circleBorderGreenImg").gameObject;
		circleBorderRedImg = transform.Find ("circleBorderRedImg").gameObject;
	}



	//
	// void Update ()
	//
	// basically check the spirit level
	//

	void Update () {
		checkSpiritLevel ();
	}


	//
	// public void checkSpiritLevel() 
	//
	// This method reads the accelerometer input
	// and animated the spirit (the bubble)
	//

	public void checkSpiritLevel() {
		// get the device acceleration data
		// Do a spheric interpolation with the data to make it smooth
		// basically, this line of code implements a low pass filter
		accVector = Vector3.Slerp(accVector, Input.acceleration, 0.05f);

		// Clamp the data to the value range of [-0.4..0.4]
		accVector.x = Mathf.Clamp(accVector.x,-0.4f,0.4f);
		accVector.y = Mathf.Clamp(accVector.y,-0.4f,0.4f);
		// set the green dot in the middle according to the calculated sensor data
		spiritLevel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-accVector.x, -accVector.y)*175;

		// now let's do some math
		// if the bubble is in the crosshair's center,
		// draw it green and show the user everything is ok
		if (Mathf.Abs (accVector.x * 175) < 25 && Mathf.Abs (accVector.y * 175) < 25) {
			levelImgGreen.SetActive (true);
			levelImgRed.SetActive (false);
			exclImg.SetActive (false);
			checkedImg.SetActive (true);
			circleBorderGreenImg.SetActive (true);
			circleBorderRedImg.SetActive (false);
			isProperSpiritLevel = true;
		} 
		// the bubble is not in the crosshair, 
		// user should reorientate the device to make the compass work
		else {
			levelImgGreen.SetActive (false);
			levelImgRed.SetActive (true);
			exclImg.SetActive (true);
			checkedImg.SetActive (false);
			circleBorderGreenImg.SetActive (false);
			circleBorderRedImg.SetActive (true);
			isProperSpiritLevel = false;
		}
	}

	 // Getter for isProperSpiritLevel
    public bool getIsProperSpiritLevel() {
        return isProperSpiritLevel;
    }
}
