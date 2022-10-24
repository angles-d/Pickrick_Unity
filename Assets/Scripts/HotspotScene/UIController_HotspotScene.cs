using System;
using UnityEngine;
using TMPro;

public class UIController_HotspotScene : MonoBehaviour {

    // The UI button that appears which launches the Main AR Scene
    public GameObject launchARButton;
    // The UI for the images showing the player where to go to launch the AR Scene
    public GameObject images;
    // Reference to the arrow object
    public GameObject arrow;
    // Reference to instructions UI
    public GameObject instructions;
    // Reference to the compass object
    public GameObject compass;
    //Reference to AR ARmarker image
    public GameObject arMarker;
    // Reference to the where button Text
    public TextMeshProUGUI whereButtonText;
    // A reference to the TMPRO object for the instructions
    public TextMeshProUGUI instructionsText;
    // A Reference to the AR camera
    public Camera arCamera;
    // Reference to page script in this game object
    public Menu.Page page;
    // Reference to page controller
    public Menu.PageController pageController;
    // Reference to PlaceObjectInFrontOfCameraScript
    public PlaceObjectInFrontOfCamera placeObjectInFrontOfCamera;


    // Colors for the whereButonText
    private Color32 blue;
    private Color32 white;
    // Determines if the hotspot if activated
    [SerializeField]
    private bool hotSpotActivated;
    // Determines if the AR Scene is calibrated correctly
    [SerializeField]
    private bool arCalibrated;
    // reference to IsCompassSetUpCorrectly Script
    //private IsCompassSetUpCorrectly isCompassSetUpCorrectly;

    // Start is called before the first frame update
    void Start() {
        // Hide and Unhide UI elements on launch
        launchARButton.SetActive(false);
        images.SetActive(false);
        instructions.SetActive(true);
        compass.SetActive(false);

        // Initialize/Set Color Values
        blue = new Color32(46, 152, 229, 255);
        white = new Color32(255, 255, 255, 255);
        whereButtonText.color = blue;

        // Check if page type is introduction
        if (page.type != Menu.PageType.Hotspot) {
            throw new Exception("Make sure the Page Component in this game object is of type Hotspot");
        }
        // enable location updates by calling this
        Input.location.Start();

        // get reference to IsCompassSetUpCorrectly Script
        //isCompassSetUpCorrectly = compass.GetComponent<IsCompassSetUpCorrectly>();
        //if (isCompassSetUpCorrectly == null) {
        //    throw new Exception("Make sure this object: " + compass +" has a IsCompassSetUpCorrectly Component");
        //}
    }

    void Update() {
        // What to do when the user is in the hotspot
        if (arCalibrated) {
            launchARButton.SetActive(true);
            arMarker.SetActive(true);
            arrow.SetActive(false);
            images.SetActive(false);
            whereButtonText.gameObject.SetActive(false);
            //compass.SetActive(false);
            instructionsText.text = "The AR experience is properly calibrated! Press the LAUNCH AR BUTTON.";
            instructionsText.fontSize = 75f;
        } else {
            if (hotSpotActivated) {
                arrow.SetActive(false);
                images.SetActive(false);
                whereButtonText.gameObject.SetActive(false);
                whereButtonText.color = blue;
                arCalibrated = true;
                //compass.SetActive(true);
                //instructionsText.text = "In order to calibrate the AR experience, angle your device fowards and rotate northeast using the compass.";
                //instructionsText.fontSize = 75f;
                //if (isCompassSetUpCorrectly.isCompassSetUpCorrectly) {
                //    arCalibrated = true;
                //} else {
                //    launchARButton.SetActive(false);
                //    placeObjectInFrontOfCamera.PlaceInFrontOfCamera();
                //}
            // What to do when the user is not in the hotspot
            } else {
                arrow.SetActive(true);
                launchARButton.SetActive(false);
                whereButtonText.gameObject.SetActive(true);
                //compass.SetActive(false);
                instructionsText.text = "This App is based on the location, please go to the 881 Hemphill Street NW, Atlanta, GA, 30318 (Now in Georgia Tech campus, EcoCommons site).";
                instructionsText.fontSize = 58f;
            }
        }
    }

    // Method runs when the user reachers the hotspot
    public void OnReachHotspot() {
        hotSpotActivated = true;
    }

    // Method runs when the user exits the hotspot
    public void OnExitHotspot() {
        hotSpotActivated = false;
    }

    // Method runs when the user presses the launch scene button
    public void PressLaunchSceneButton() {
        //arrow.SetActive(false);
        //gameObject.SetActive(false);
        //arMarker.SetActive(false);
        placeObjectInFrontOfCamera.PlaceInFrontOfCamera();
        pageController.TurnPageOff(Menu.PageType.Hotspot, Menu.PageType.Timeline);
    }

    // Method Runs When the Where Button is Pressed
    public void PressWhereButton() {
        whereButtonText.color = white;
        images.SetActive(true);
    }

    // Method Runs When the Close Button is pressed for the images
    public void PressImageCloseButton() {
        whereButtonText.color = blue;
        images.SetActive(false); 
    }
}