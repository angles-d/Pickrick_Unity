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

    private int noPositionButtonClickCount;
    public int maxClickCount = 3;

    public GameObject[] gameObjectsToDestroy;

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
    Pose wallHitPose;

    //flag booleans
    //if the objects have been correctly positioned
    bool positioned = false;
    //if the camera is scanning for planes
    bool scanning = false;

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
        noPositionButtonClickCount = 0;
    }



    void Update()
    {
        //if the camera is scanning for planes/detecting hits
        if (scanning)
        {

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
                        WallConfiguration(touch);
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
        ARPlane plane = sc.RaycastGetPlane(m_raycastManager, m_planeManager, touch);

        //Turn off scaning
        scanning = false;

        if (plane != null)
        {
            arFloor.transform.position = new Vector3(arFloor.transform.position.x, plane.transform.position.y, arFloor.transform.position.z);
            ARInfo.Instance.SetFloorPosition(arFloor);

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
            StartCoroutine(sc.Timer(0.2f, () => { scanning = true; }));
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
        //SPRING 2024 IMPLEMENTATION - RESTART  IF CANNOT SCAN WALL CORRECTLY {maxClickCount} TIMES
        noPositionButtonClickCount++;
        if(noPositionButtonClickCount == maxClickCount) {
            //error in floor scan, will not scan wall correctly -> restart floor scanning
            foreach(GameObject obj in gameObjectsToDestroy) Destroy(obj);
            sc.LoadScene(0);
        }
        
        //Turn off confirmation UI
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);
        confirmPositionBox.SetActive(false);

        //Turn on scanning
        StartCoroutine(sc.Timer(1f, () =>
        {
            m_planeManager.enabled = true;
            tapWall.SetActive(true);
            scanning = true;
        }
        ));
    }

    //Verify the tapped wall position location is in the right location
    public void WallConfiguration(Touch touch)
    {
        //turn off the tap the wall UI sign
        tapWall.SetActive(false);
        m_planeManager.enabled = false;
        scanning = false;

        ARRaycastHit? finalWallHit = sc.RaycastGetHit(m_raycastManager, touch);

        if (finalWallHit != null && ((ARRaycastHit) finalWallHit).hitType == TrackableType.PlaneWithinPolygon)
        {
            Debug.Log("Wall hit");
            wallHitPose = ((ARRaycastHit)finalWallHit).pose;
            confirmPositionBox.transform.SetPositionAndRotation(wallHitPose.position, wallHitPose.rotation);
            confirmPositionBox.SetActive(true);
        }

        //turn on confirmation buttons
        yesPositionedButton.SetActive(true);
        noPositionedButton.SetActive(true);
        confirmPositionText.SetActive(true);
    }


    public void PlaceObjects()
    {
        //Turn off confirmation buttons
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);

        positioned = true;

        Quaternion rot = wallHitPose.rotation;
        Vector3 pos = wallHitPose.position;

        sc.ConfigureARPlaceObjects(pos, rot);
        sc.GetTimeline().SetActive(true);

        //turn off this script (AR configuration is completed)
        this.enabled = false;
    }


}


