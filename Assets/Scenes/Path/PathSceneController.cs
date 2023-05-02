using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using System;

public class PathSceneController : SceneController
{
    //Game objects that need to be placed on the floor (anim, anim markers, inter marker)
    [SerializeField] GameObject[] toPlace;
    [SerializeField] GameObject arFloor;
    [SerializeField] GameObject pillarRef;
    [SerializeField] GameObject scenePosReference;
    [SerializeField] GameObject path;

    //marker controllers
    [Header("Controller Scripts")]
    [SerializeField] InterstitialsPathController ic;
    [SerializeField] AnimationPathController ac;
    [SerializeField] FourMinistersController fmc;

    //UI elements
    [Header("UI Elements")]
    [SerializeField] GameObject WalkToSign;
    [SerializeField] GameObject EndSceneUIGooger;

    //the current marker index
    int curMarkerIndex = 0;
    //if the end credit scene has started
    bool endSceneStarted = false;

    private void Start()
    {
        fmc.StartScanning();
        MoveToGround(LocationInfo.Instance.GetFloorPosition().y, toPlace);
    }

    public void ShowNextIntersitialMarker()
    {
        //hide current animation
        ac.HideAnimMarker(curMarkerIndex);
        ac.HideAnimation(curMarkerIndex);
        ac.TurnOffAnimDateUI(curMarkerIndex);

        //increment counter
        curMarkerIndex++;

        //show next itnerstitial marker
        ic.ShowIntersitialMarker(curMarkerIndex);
    }

    //Path Marker specific methods
    //Show the marker at the current index
    public void ShowNextAnimMarker()
    {
        //hide the current interstitial
        ic.HideInterstitial(curMarkerIndex);

        //turn on next marker
        ac.ShowAnimMarker(curMarkerIndex);

        if (curMarkerIndex == 0)
        {
            ShowWalkToSign();
        }
    }

    //called by the ar animation event emitter
    //component attached directly to the timeline gameobjects
    public void ShowNextInterButton()
    {
        ac.ShowNextInterButton(curMarkerIndex);
    }

    //position the scene to be in the right location
    //based on scanned pillar position
    public void PositionAR()
    {
        Debug.Log("Path AR Positioned");
        Debug.Log("pillar Pos" + pillarRef.transform.position);

        transform.rotation = pillarRef.transform.rotation;
        transform.Rotate(0, 90, 0);

        Vector3 pillarPos = new Vector3(pillarRef.transform.position.x, 0, pillarRef.transform.position.z);
        transform.position = pillarPos;
        path.SetActive(true);
    }

    //Starts the inner path
    //Shows the first path marker
    public void StartInnerPathAR()
    {
        ic.ShowIntersitialMarker(0);
        WalkToSign.SetActive(true);
        fmc.HideMinisterCards();
    }

    //Hides the walk to marker sign
    public void HideWalkToSign()
    {
        WalkToSign.SetActive(false);
    }

    //Shows the walk to marker sign
    public void ShowWalkToSign()
    {
        WalkToSign.SetActive(true);
    }

    //Show the interstitial at the current marker index
    public void ShowInterstitial()
    {
        ic.TrackPillar(curMarkerIndex);

    }

    //Shows the animation at the current marker index
    public void ShowAnimation()
    {
        ac.ShowAnimation(curMarkerIndex);

        //if last animation has been viewed
        if(curMarkerIndex == 4)
        {
            EndSceneTimer();
        }
    }

    //Turn on the Newspaper Date UI Overlay for animations
    public void TurnOnAnimDateUI()
    {
        ac.TurnOnAnimDateUI(curMarkerIndex);
    }

    //Starts the credit scene with Jack Googer
    public void ShowEndSceneGooger()
    {
        endSceneStarted = true;
        EndSceneUIGooger.SetActive(true);
    }

    //Timer to trigger the end scene if there's no collision with the restaurant door
    public void EndSceneTimer()
    {
        StartCoroutine(Timer(45.0f,CheckEndSceneTriggered));

        void CheckEndSceneTriggered()
        {
            if (!endSceneStarted)
            {
                endSceneStarted = true;
                ShowEndSceneGooger();
            }
        }
    }

    //Getters & Setters
    public int GetCurrentMarkerIndex()
    {
        return curMarkerIndex;
    }

}
