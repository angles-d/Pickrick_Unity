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
    public PlaceObjectInFrontOfCamera arObjects;
    public Menu.PageController pageController;

    // Reference to PlaceObjectInFrontOfCameraScript
    //public PlaceObjectInFrontOfCamera placeObjectInFrontOfCamera;


    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        UI.SetActive(false);
    }

    void Update()
    {
       
    }

    //public void toAR()
    //{
       
    //    Screen.orientation = ScreenOrientation.Portrait;
    //    //launchArInfo.SetActive(true);

    //    //vignettes.gameObject.SetActive(false);

    //}


    // Method runs when the user presses the launch scene button
    public void PressLaunchSceneButton()
    {
        //arMarker.SetActive(false);
        //SceneManager.LoadScene("Main_Scene");
        arObjects.PlaceInFrontOfCamera();
        UI.SetActive(true);
        pageController.TurnPageOff(Menu.PageType.Hotspot, Menu.PageType.Timeline);

        gameObject.SetActive(false);
    }

    
}
