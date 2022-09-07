using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this class on the object storing all of the Introduction UI graphic elements
// Make sure this object has a page script attached with type introduction

public class IntroductionUIController : MonoBehaviour
{

    // Reference to page script in this game object
    Menu.Page page;

    // List of all introduction pages
    List<GameObject> introductionPagesList;

    // Current active introduction page index
    int currentIntroductionPageIndex;

    // Reference to page controller
    Menu.PageController pageController;

    // Start is called before the first frame update
    void Start() {
        
        // Check if page object is attached to this game object
        page = gameObject.GetComponent<Menu.Page>();
        if (page == null) {
            throw new Exception("Make sure the IntroductionUIController script is in a game object with a Page Component");
        }

        // Check if page type is introduction
        if (page.type != Menu.PageType.Introduction) {
            throw new Exception("Make sure the Page Component in this game object is of type Introduction");
        }

        // Try to find Page Controller
        pageController = FindObjectOfType<Menu.PageController>();
        if (pageController == null) {
            throw new Exception("Could not find Page Controller. Make sure PageController is in scene");
        }

        GetIntroductionPages();

        // Set active the first introduction page
        currentIntroductionPageIndex = 0;
        introductionPagesList[currentIntroductionPageIndex].SetActive(true);
    }

    // Get the list of introduction pages
    private void GetIntroductionPages() {
        introductionPagesList = new List<GameObject>();
        foreach(Transform child in transform) {
            introductionPagesList.Add(child.gameObject);
        }
    }

    // Go to the next introduction page or the timeline if you have reached the last introduction page
    public void GoToNextIntroductionPage() {
        try {
            introductionPagesList[currentIntroductionPageIndex].SetActive(false);
            currentIntroductionPageIndex++;
            introductionPagesList[currentIntroductionPageIndex].SetActive(true);

            // Add the Menu UI here after going to second introduction UI page
            if (currentIntroductionPageIndex == 1) {
                pageController.TurnPageOn(Menu.PageType.Menu);
            }

        } catch {
            pageController.TurnPageOff(Menu.PageType.Introduction, Menu.PageType.Hotspot);
        }

    }
}
