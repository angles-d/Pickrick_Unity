using UnityEngine;
using TMPro;

//Controller for the Path Scene methods
public class PathSceneController : SceneController
{
    //Game objects that need to be placed on the floor (anim, anim markers, inter marker)
    //Game objects to place on the floor
    [SerializeField] GameObject[] toPlace;
    //Reference to arFloor
    [SerializeField] GameObject arFloor;
    //Reference to the pillar position (outside the "Scene Objects" game object)
    [SerializeField] GameObject pillarRef;
    //Reference to the scene position (inside the "Scene Object" game object)
    [SerializeField] GameObject scenePosReference;
    //Reference to the path markers
    [SerializeField] GameObject path;
    //Scene objects
    [SerializeField] GameObject scene;

    //Controller scripts
    [Header("Controller Scripts")]
    [SerializeField] InterstitialsPathController ic;
    [SerializeField] AnimationPathController ac;
    [SerializeField] FourMinistersController fmc;

    //UI elements
    [Header("UI Elements")]
    [SerializeField] GameObject WalkToSign;
    [SerializeField] GameObject EndSceneUIGooger;

    [SerializeField] float googerWaitTime = 20.0f;

    [SerializeField] GameObject animTextGameObject;

    //the current marker index
    int curMarkerIndex = 0;

    //anim marker index
    [HideInInspector] public int animMarkerIndex = 0;
    //if the end credit scene has started
    bool endSceneStarted = false;

    private void Start()
    {
        //start scanning for the pillar
        fmc.StartScanning();

        //Move objects to ground
        MoveToGround(ARInfo.Instance.GetFloorPosition().y, toPlace);

        curMarkerIndex = 0;
        animMarkerIndex = 0;
    }

    //Show the next interstital marker
    public void ShowNextIntersitialMarker()
    {
        //hide prev index assets
        if(curMarkerIndex == 1) {
            //hide jul 9 and show jul 22
            ic.HideInterstitial(1);
        }
        else {
            //hide prev animation
            ac.HideAnimMarker(animMarkerIndex - 1);
            ac.HideAnimation(animMarkerIndex - 1);
            ac.TurnOffAnimDateUI(animMarkerIndex - 1);
        }
        //increment counter
        curMarkerIndex++;

        //show next itnerstitial marker
        ic.ShowIntersitialMarker(curMarkerIndex);
        //IC CHANGE change to show UI screen for next interstitial card
    }

    //Path Marker specific methods
    //Show the marker at the current index
    public void ShowNextAnimMarker()
    {
        //Sept 26 anim - hide aug 11 anim
        if(animMarkerIndex == 2) {
            //hide prev animation
            ac.HideAnimMarker(animMarkerIndex - 1);
            ac.HideAnimation(animMarkerIndex - 1);
            ac.TurnOffAnimDateUI(animMarkerIndex - 1);
        }
        else {
            //hide the current interstitial
            ic.HideInterstitial(curMarkerIndex);
        }
        //increments later after anim UI
        //turn on next animation marker
        ac.ShowAnimMarker(animMarkerIndex);

        if (animMarkerIndex == 0)
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
        scene.transform.rotation = pillarRef.transform.rotation;
        scene.transform.Rotate(0, 90, 0);

        Vector3 pillarPos = new Vector3(pillarRef.transform.position.x, 0, pillarRef.transform.position.z);
        scene.transform.position = pillarPos;
        path.SetActive(true);
    }

    //Starts the inner path
    //Shows the first path marker
    //SPRING 2024 IMPLEMENTATION
    public void StartInnerPathAR()
    {
        ic.ShowIntersitialMarker(0);

        //IC CHANGE change to show first UI

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
        //renders UI screen poster
        ic.TrackPillar(curMarkerIndex);
    }

    //Shows the animation at the current marker index
    public void ShowAnimation()
    {
        ac.TurnOnAnimDateUI(animMarkerIndex);
        ac.ShowAnimation(animMarkerIndex);
        if(animMarkerIndex == 4) EndSceneTimer();
        animMarkerIndex++;
    }

    //Turn on the Newspaper Date UI Overlay for animations
    public void TurnOnAnimDateUI()
    {
        ac.TurnOnAnimDateUI(animMarkerIndex);
    }

    //Starts the credit scene with Jack Googer
    public void ShowEndSceneGooger()
    {
        //SPRING 2024 IMPLEMENTATION
        ac.HideAnimMarker(animMarkerIndex - 1);
        ac.HideAnimation(animMarkerIndex - 1);
        ac.TurnOffAnimDateUI(animMarkerIndex - 1);
        animTextGameObject.SetActive(false);

        endSceneStarted = true;
        EndSceneUIGooger.SetActive(true);
    }

    //Timer to trigger the end scene if there's no collision with the restaurant door
    public void EndSceneTimer()
    {
        StartCoroutine(Timer(googerWaitTime,CheckEndSceneTriggered));

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
