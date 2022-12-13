using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchAR : MonoBehaviour
{
    // The UI button that appears which launches the Main AR Scene
    //public GameObject launchARButton;

    //refence to arMakrer image
    //public GameObject arMarker;

   
    // A Reference to the AR camera
    public Camera arCamera;
    public GameObject UI;
    //public GameObject launchArInfo;
    public GameObject arObjects;
    public Menu.PageController pageController;
    [Range(0, 100)]
    public float distance;



    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        UI.SetActive(false);
    }

    //places the ARobjects in front of the AR camera
    public void PlaceInFrontOfCamera()
    {
        // Put the object in front of the camera
        arObjects.transform.position = arCamera.transform.position + arCamera.transform.forward * distance;
        // Make the y rotation equal to the cameras y rotation
        Vector3 eulerRotation = new Vector3(arObjects.transform.eulerAngles.x, arCamera.transform.eulerAngles.y, arObjects.transform.eulerAngles.z);
        arObjects.transform.rotation = Quaternion.Euler(eulerRotation);
    }

    // Method runs when the user presses the launch scene button
    public void PressLaunchSceneButton()
    {
        PlaceInFrontOfCamera();
        UI.SetActive(true);
        pageController.TurnPageOff(Menu.PageType.Hotspot, Menu.PageType.Timeline);

        gameObject.SetActive(false);
    }

    
}
