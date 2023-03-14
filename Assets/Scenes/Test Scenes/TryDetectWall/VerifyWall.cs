using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class VerifyWall : ConfigureAR
{
    //if the user verifies the object is positioned
    bool positioned = false;
    public GameObject positionObject;

    public GameObject confirmPositionText;
    public GameObject yesPositionedButton;
    public GameObject noPositionedButton;

    ARRaycastHit finalHit;
    ARPlane finalPlane;

    // Start is called before the first frame update
    void Start()
    {
        m_raycastManager = gameObject.GetComponent<ARRaycastManager>();
        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        m_planeManager.enabled = false;


        dates = timeline.transform.GetChild(1).gameObject;

        //populate to place
        toPlace[0] = doors;
        //toPlace[1] = cards;
        toPlace[1] = lightrays;

        StartWallTracking();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        // Save the found touch event
        if (!positioned && scanning)
        {
            Touch touch = Input.GetTouch(0); 
            VerifyWallPosition(touch);     
        }
    }

    public void VerifyWallPosition(Touch touch)
    {
       //turn off pop up
        popup[0].SetActive(false);

        const TrackableType trackableTypes = TrackableType.Planes;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            m_planeManager.enabled = false;
            scanning = false;

            foreach (ARPlane p in m_planeManager.trackables)
            {
                if (p.isActiveAndEnabled && p.trackableId.Equals(hits[0].trackableId))
                {
                    finalPlane = p;
                }

                p.gameObject.SetActive(false);

            }
            finalHit = hits[0];

            positionObject.transform.position = finalHit.pose.position;
            positionObject.transform.rotation = finalHit.pose.rotation;
            positionObject.SetActive(true);

        }

        //turn on  buttons
        yesPositionedButton.SetActive(true);
        noPositionedButton.SetActive(true);
        confirmPositionText.SetActive(true);
    }

    public void StartWallTracking()
    {
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);

        StartCoroutine(Timer(1f,  () => {
            m_planeManager.enabled = true;
            popup[0].SetActive(true);
            scanning = true;}
        ));
       

    }



    public void Positioned()
    {
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);

        positioned = true;
        state = State.InTimeline;
        PlaceARObjects(finalPlane, finalHit);

    }

    public void PlaceARObjects(ARPlane p, ARRaycastHit hit)
    {
        //reposition timeline
        //check if hit pose is actually right
        Quaternion rot = hit.pose.rotation;
        Vector3 pos = hit.pose.position;
        timeline.transform.rotation = rot;
        timeline.transform.Rotate(-90, 0, 0);
        timeline.transform.Rotate(0, 90, 0);
        timeline.transform.Rotate(0, 0, 180);

        Vector3 userPos = new Vector3(pos.x, Camera.main.transform.position.y, pos.z);
        timeline.transform.position = userPos - dummy.transform.position;

        //move dates up
        dates.transform.position = new Vector3(dates.transform.position.x, Camera.main.transform.position.y - 0.05f, dates.transform.position.z);
        intro.transform.position = new Vector3(intro.transform.position.x, hit.pose.position.y, intro.transform.position.z);

        //move cards to middle of the wall; based on plane center
        foreach (GameObject c in cards)
        {
            Vector3 cardPos = c.transform.position;
            c.transform.position = new Vector3(cardPos.x, hit.pose.position.y, cardPos.z);
        }


        MoveToGround();

        SwitchToTimeline();

        ////Time Delay
        //StartCoroutine(Timer(0.5f, SwitchToTimeline));

        //switch scanning to wall
        void SwitchToTimeline()
        {
            state = State.InTimeline;
            timeline.SetActive(true);


        }


    }
}
