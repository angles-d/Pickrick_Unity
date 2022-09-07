using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class TimelineEventManager : MonoBehaviour {

    // A list of TimelineEvent instances representing events in the timeline 
    public TimelineEvent[] eventList;
    
    // The current Timeline event that can be pressed
    [HideInInspector]
    public TimelineEvent currentEvent;

    // A reference to current timeline infopage timeline event event on screen
    [HideInInspector]
    public Event_InfoPage currentInfoTimelineEvent;
    
    // A reference to current more infopage for each AR event on screen
    [HideInInspector]
    public Menu.Page currentMoreInfoPage;

    // A reference to current timeline AR timeline event event on screen
    [HideInInspector]
    public Event_AR currentARTimelineEvent;

    // How long the animation should take when changing the current event
    public float changeTimelineEventAnimationSpeed;

    // The Marker that shows what timeline event is selected
    public GameObject selectionMarker;

    // Start is called before the first frame update
    void Start() {
        // The default position of the timeline is the first event in the timeline
        transform.localPosition = new Vector3(eventList[0].positionWhereCentered,gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
        // The current event is the first event on the timeline
        currentEvent = eventList[0];
        // set currentInfoTimelineEvent because there is no info timeline event on screen
        currentInfoTimelineEvent = null;
        currentARTimelineEvent = null;
    }

    // This method changes the current event to the new current event specified in the parameter
    public void ChangeCurrentEvent() {
        LeanTween.moveLocalX(gameObject, currentEvent.positionWhereCentered , changeTimelineEventAnimationSpeed);
        selectionMarker.SetActive(true);
        selectionMarker.transform.localPosition = new Vector3(-currentEvent.positionWhereCentered, selectionMarker.transform.localPosition.y, selectionMarker.transform.localPosition.z);
    }
}