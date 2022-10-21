using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchAR : MonoBehaviour
{
    // The UI button that appears which launches the Main AR Scene
    public GameObject launchARButton;

    //refence to arMakrer image
    public GameObject arMarker;

   
    // A Reference to the AR camera
    public Camera arCamera;
 
    // Reference to PlaceObjectInFrontOfCameraScript
    public PlaceObjectInFrontOfCamera placeObjectInFrontOfCamera;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Update()
    {
       
    }

   
    // Method runs when the user presses the launch scene button
    public void PressLaunchSceneButton()
    {
        //gameObject.SetActive(false);
        //arMarker.SetActive(false);
        SceneManager.LoadScene("Main_Scene");
        //placeObjectInFrontOfCamera.PlaceInFrontOfCamera();
    }

    
}
