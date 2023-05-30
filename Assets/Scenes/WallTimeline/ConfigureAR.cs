using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


//Configures AR data for the experience
//Sets the ground position and positions the timeline
public class ConfigureAR : MonoBehaviour
{
    [SerializeField] TimelineSceneController sc;

    //cube that surrounds the configure wall image
    [SerializeField] GameObject confirmPositionBox;

    //UI Screens Elements
    [Header("UI Elements")]
    public GameObject pointFloor;
    public GameObject tapFloor;
    public GameObject pointWall;
    public GameObject tapWall;
    public GameObject confirmPositionText;
    public GameObject yesPositionedButton;
    public GameObject noPositionedButton;


    //Location of the finalHit on the wall
    //Based on the location tapped by the user
    ARRaycastHit finalHit;


    //flag booleans
    //if the objects have been correctly positioned
    bool positioned = false;
    //if the camera is scanning for planes
    bool scanning = false;
    //if the floor plane has been detected 
    bool floorDetected = false;

    //reference to the ar floor
    GameObject arFloor;

    //AR Managers
    ARPlaneManager m_planeManager;
    ARRaycastManager m_raycastManager;

    //Scanning state of AR Camera
    State state = State.ScanningFloor;

    //AR Camera scanning state
    public enum State
    {
        ScanningFloor, //confgiuring the floor position
        ScanningWall, //configuring the wall
    }


    private void Start()
    {
        m_raycastManager = ARInfo.Instance.GetRaycastManager();
        m_planeManager = ARInfo.Instance.GetPlaneManager();
        arFloor = sc.arFloor;

        //Turn off plane manager till the user starts the experience
        m_planeManager.enabled = false;
    }



    void Update()
    {
        //if the camera is scanning for planes/detecting hits
        if (scanning) {

            //No touch detected
            if (Input.touchCount == 0)
            {
                return;
            }

            //Get one finger touch interaction (tap)
            Touch touch = Input.GetTouch(0);

            switch (state)
            {
                //Configuring Floor position
                case State.ScanningFloor:
                    CheckFloorRaycast(touch);
                    break;

                //Configuring the wall position
                case State.ScanningWall:
                    //User has not verified wall position
                    if (!positioned)
                    {
                        VerifyWallPosition(touch);
                    }
                    break;
            }
        }
   

    }


    //called when user presses the Start AR button
    public void StartAR()
    {
        StartCoroutine(sc.Timer(1.5f, TrackFloor));
    }

    //Starts the scanning to configure the floor height
    public void TrackFloor()
    {
        //turn off the point to floor UI sign
        pointFloor.SetActive(false);
        //turn on the tap to set floor UI sign
        tapFloor.SetActive(true);

        //turn on the plane manager/ start sccanning for planes
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
        scanning = true;
    }

    //Check if the camera detected a plane from the user's tap
    public void CheckFloorRaycast(Touch touch)
    {
        sc.CheckRaycast(m_raycastManager, DetectFloor, touch);
    }

    //Detects Floor Position
    //Checks for an intersection between user's tap and detected AR planes
    public void DetectFloor(List<ARRaycastHit> hits)
    {
        //Turn off scaning
        scanning = false;

        //Get the roation and position of the nearest hit trackable (ARplane)
        Vector3 pos = hits[0].pose.position;
        Quaternion rot = hits[0].pose.rotation;

        //Iterate through planes and get Hit plane
        //TODO remove for loop? get ar plane directly from hits[0]?
        foreach (ARPlane p in m_planeManager.trackables)
        {
            if (p.trackableId.Equals(hits[0].trackableId))
            {

                //get first plane
                arFloor.transform.position = new Vector3(arFloor.transform.position.x, p.transform.position.y, arFloor.transform.position.z);
                floorDetected = true;
                ARInfo.Instance.SetFloorPosition(arFloor);
                

            }

            p.gameObject.SetActive(false);
        }

        //If there is a detected collision
        if (floorDetected)
        {
            //turn off tap the floor UI sign
            tapFloor.SetActive(false);
            //turn on point camera to wall UI sign
            pointWall.SetActive(true);

            //Switch to scanning the wall
            StartCoroutine(sc.Timer(0.7f, () =>
           {
               state = State.ScanningWall;
               m_planeManager.enabled = false;
               StartCoroutine(sc.Timer(2f, TrackWall));

           }));
        }
        else
        {
            //continue scanning for the floor
            StartCoroutine(sc.Timer(0.2f, () =>{scanning = true; }));
        }

    }


    //Start the wall tracking
    public void TrackWall()
    {
        Debug.Log("Track Wall");
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

        //turn off point to wall 
        pointWall.SetActive(false);
        //turn on tap wall
        tapWall.SetActive(true);
        scanning = true;
    }

    //Prepares the camera to rescan the wall 
    public void ContinueWallTracking()
    {
        //Turn off confirmation UI
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);
        confirmPositionBox.SetActive(false);

        //Turn on scanning
        StartCoroutine(sc.Timer(1f, () => {
            m_planeManager.enabled = true;
            tapWall.SetActive(true);
            scanning = true;
        }
        ));
    }


    //Verify the tapped wall position location is in the right location
    //TODO rewrite with Checkraycast 
    public void VerifyWallPosition(Touch touch)
    {
        //turn off the tap the wall UI sign
        tapWall.SetActive(false);

        const TrackableType trackableTypes = TrackableType.Planes;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            
            m_planeManager.enabled = false;
            scanning = false;

            foreach (ARPlane p in m_planeManager.trackables)
            {
                Debug.Log(p.normal);
                if (p.trackableId.Equals(hits[0].trackableId))
                {
                    Debug.Log("wall hit");
                    finalHit = hits[0];
                    confirmPositionBox.transform.position = finalHit.pose.position;
                    confirmPositionBox.transform.rotation = finalHit.pose.rotation;
                    confirmPositionBox.SetActive(true);
                }

                //p.gameObject.SetActive(false);

            }

        }

        //turn on  buttons
        yesPositionedButton.SetActive(true);
        noPositionedButton.SetActive(true);
        confirmPositionText.SetActive(true);
    }

    
    //Places the AR objects once the wall position has been configured
    //Called by the yes button in the configure wall start game object
    public void PlaceObjects()
    {
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);

        positioned = true;

        Quaternion rot = finalHit.pose.rotation;
        Vector3 pos = finalHit.pose.position;

        sc.ConfigureARPlaceObjects(pos, rot);
        sc.GetTimeline().SetActive(true);

        //turn off script (configuration is completed)
        this.enabled = false;
    }


}


