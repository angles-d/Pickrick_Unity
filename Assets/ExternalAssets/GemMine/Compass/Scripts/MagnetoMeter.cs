using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagnetoMeter : MonoBehaviour {

	// Reference to the value text// dispalyed in the component
	public Text magneticValueTxt;

	// This is the magnetic value determined by the sensor
	float magneticValue;

	bool isProperMagneticValue;

	// publicly accessible Property
	// Compass will read this value if 
	// magnetometer component is attached
	public float MagneticValue {
		get { return magneticValue; }
	}

	public bool IsInterference {
		get { return (magneticValue >= 60);}
	}

	// References to the component's parts
	// initiialize them once in the start method
	// to avoid FindChilds every time in the update
	GameObject circleBorderRedImg;
	GameObject circleBorderGreenImg;
	GameObject exclImg;
	GameObject checkedImg;



	//
	// void Start ()
	//
	// Initialize the references to the spirit's components
	// Initialize the Input module for accessing the sensor data
	//

	void Start () {
		// set the references to the component's parts
		circleBorderRedImg = transform.Find ("circleBorderRedImg").gameObject;
		circleBorderGreenImg = transform.Find ("circleBorderGreenImg").gameObject;
		exclImg = transform.Find ("statusIcon").Find ("exclImg").gameObject;
		checkedImg = transform.Find ("statusIcon").Find ("checkedImg").gameObject;

		// start the location service
		Input.location.Start ();
		// enable the compass
		Input.compass.enabled = true;
		// enable the gyro
		Input.gyro.enabled = true;
	}



	//
	// void Update ()
	//
	// basically check the Interference
	//

	void Update () {
		checkInterference ();
	}


	//
	// public void checkInterference()
	//
	// this method checks the magnetometer Sensor each update
	//

	public void checkInterference() {
		// read the magnetometer value
		magneticValue = Input.compass.rawVector.magnitude; 
		// magnetometer needs to be calibrated,
		// if value is above 60
		// value in natural environment should not exceed 45-50
		if (magneticValue > 60) {
			//interferenceImg.gameObject.SetActive (true);
			circleBorderRedImg.SetActive (true);
			circleBorderGreenImg.SetActive (false);
			exclImg.SetActive (true);
			checkedImg.SetActive (false);
			isProperMagneticValue = false;
		}
		// everything is ok
		else {
			//interferenceImg.gameObject.SetActive (false);
			circleBorderGreenImg.SetActive (true);
			circleBorderRedImg.SetActive (false);
			exclImg.SetActive (false);
			checkedImg.SetActive (true);
			isProperMagneticValue = true;
		}
		magneticValueTxt.text = magneticValue.ToString ("F0");
	}

	// getter for isProperMagneticValue
	public bool getIsProperMagneticValue() {
		return isProperMagneticValue;
	}
}
