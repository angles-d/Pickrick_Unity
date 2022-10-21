using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimeline : MonoBehaviour
{
    // Start is called before the first frame update
    // Reference to page controller
    public Menu.PageController pageController;
    // Reference to PlaceObjectInFrontOfCameraScript
    public PlaceObjectInFrontOfCamera placeObjectInFrontOfCamera;
    void Awake()
    {
        print("hellooo");
        placeObjectInFrontOfCamera.PlaceInFrontOfCamera();
        pageController.TurnPageOff(Menu.PageType.Hotspot, Menu.PageType.Timeline);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
