using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Event_AR : Timeline_Event {
    
    // Reference to page controller
    Menu.PageController pageController;
    // Reference to the timeline event manager
    TimelineEventManager timelineEventManager;

    // Reference to page giving more info about event
    public Menu.Page moreInfoPage;

    void GetMenuVariables() {
         // Try to find Page Controller
        pageController = FindObjectOfType<Menu.PageController>();
        if (pageController == null) {
            throw new Exception("Could not find Page Controller. Make sure PageController is in scene");
        }

        // Try to get timeline event manager
        timelineEventManager =  FindObjectOfType<TimelineEventManager>();
        if (timelineEventManager == null) {
            throw new Exception("Could not find TimelineEventManager. Make sure object with TimelineEventManager is in scene");
        }
    }

    public override void LaunchEvent() {
        GetMenuVariables();
        
        // Remove Info Timeline Event if one is on screen
        if (timelineEventManager.currentInfoTimelineEvent != null) {
            // Get page script attached to event we want to turn off
            Menu.Page oldEventPageScript = timelineEventManager.currentInfoTimelineEvent.gameObject.GetComponent<Menu.Page>();
            pageController.TurnPageOff(oldEventPageScript.type);
            // Set currentInfoTimelineEvent to null
            timelineEventManager.currentInfoTimelineEvent = null;
        }

        // Deactivate currentARTimelineEvent if this value isn't null
        if (timelineEventManager.currentARTimelineEvent != null) {
            timelineEventManager.currentARTimelineEvent.gameObject.SetActive(false);
        }

        // Set AR Timeline event active
        gameObject.SetActive(true); 

        // Update currentARTimelineEvent
        timelineEventManager.currentARTimelineEvent = this;

        // Add current More Info Page to screen and remove previous more info page if neccesary
        if (timelineEventManager.currentMoreInfoPage != null) {
            pageController.TurnPageOff(timelineEventManager.currentMoreInfoPage.type, moreInfoPage.type);
        } else {
            pageController.TurnPageOn(moreInfoPage.type);
        }
        timelineEventManager.currentMoreInfoPage = moreInfoPage;

        // // Add the feature where audio plays if you are looking at the Pickrick restuarant if the AR Event is Event_AR_MaddoxThreatensMinisters
        // if (gameObject.tag == "Event_AR_MinistersEnterResturant")
        // {
        //     pageController.TurnPageOn(Menu.PageType.Navigation);
        // }
        // else 
        // {
        //     pageController.TurnPageOff(Menu.PageType.Navigation);
        // }


    }

    // Show all the extra info and hide the more info button
    public void ShowMoreInfo() {
        pageController.TurnPageOff(Menu.PageType.Timeline);

        foreach(Transform child in moreInfoPage.transform) {
            if (child.gameObject.tag == "MoreInfo") {
                child.gameObject.SetActive(false);
            } else {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Hide all the extra info and show the more info button
    public void HideMoreInfo() {
        pageController.TurnPageOn(Menu.PageType.Timeline);

        foreach(Transform child in moreInfoPage.transform) {
            if (child.gameObject.tag == "MoreInfo") {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
    }
}
