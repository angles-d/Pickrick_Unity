using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_InfoPage : Timeline_Event {

    // Reference to page controller
    Menu.PageController pageController;
    // The Page script attached to this event
    Menu.Page eventPageScript;
    // Reference to the timeline event manager
    TimelineEventManager timelineEventManager;

    void GetMenuVariables() {
         // Try to find Page Controller
        pageController = FindObjectOfType<Menu.PageController>();
        if (pageController == null) {
            throw new Exception("Could not find Page Controller. Make sure PageController is in scene");
        }

        // Try to get the Page script attached to this event
        eventPageScript = gameObject.GetComponent<Menu.Page>();
        if (eventPageScript == null) {
            throw new Exception("Make sure that this object has a page script attached to it");
        }

        // Try to get timeline event manager
        timelineEventManager =  FindObjectOfType<TimelineEventManager>();
        if (timelineEventManager == null) {
            throw new Exception("Could not find TimelineEventManager. Make sure object with TimelineEventManager is in scene");
        }
    }

    public override void LaunchEvent() {
        GetMenuVariables();
        
        // check if info page is on screen already
        if (timelineEventManager.currentInfoTimelineEvent == null) {
            pageController.TurnPageOn(eventPageScript.type);
        } else {
            // Get page script attached to event we want to turn off
            Menu.Page oldEventPageScript = timelineEventManager.currentInfoTimelineEvent.gameObject.GetComponent<Menu.Page>();
            pageController.TurnPageOff(oldEventPageScript.type, eventPageScript.type);
        }

        // Make sure navigation feature is off
        //pageController.TurnPageOff(Menu.PageType.Navigation);

        // Update current info timeline event in timeline event manager
        timelineEventManager.currentInfoTimelineEvent = this;
    }
}