using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ConfigureAR : MonoBehaviour
{
    bool placed = false;

    public enum State{
        ScanningFloor,
        ScanningWall,
        InTimeline
    }

    public State state = State.ScanningFloor;

    [SerializeField]
    protected ARPlaneManager m_planeManager;

    [SerializeField]
    protected ARRaycastManager m_raycastManager;

    [SerializeField]
    protected GameObject[] cards;

    [SerializeField]
    protected GameObject[] doors;

    [SerializeField]
    protected GameObject[] lightrays;


    [SerializeField]
    protected GameObject intro;

    public GameObject timeline;

   
    public GameObject dummy;

    [SerializeField]
    protected GameObject arFloor;


    public GameObject[][] toPlace = new GameObject[2][];

    protected bool scanning = false;

    public GameObject dates;

    //UI Screens
    public GameObject[] popup; //point floor, tap floor, scan wall, tap wall

    //Check if user clicked right area
    public GameObject confirmPositionText;
    public GameObject yesPositionedButton;
    public GameObject noPositionedButton;


    //cube that surrounds the configure wall image
    public GameObject positionObject;

    //inside the first car holder
    public GameObject line;

    //Location of the finalHit on the wall
    ARRaycastHit finalHit;

    //Wall plane
    ARPlane finalPlane;

    //if the user positioned the object
    bool positioned = false;


    void Awake()
    {
       

        
    }

    private void Start()
    {
        m_raycastManager = LocationInfo.Instance.GetRaycastManager();
        m_planeManager = LocationInfo.Instance.GetPlaneManager();
        m_planeManager.enabled = false;

        dates = timeline.transform.GetChild(1).gameObject;

        //populate to place
        toPlace[0] = doors;
        //toPlace[1] = cards;
        toPlace[1] = lightrays;

        //hide timeline
        timeline.SetActive(false);
    }




    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        // Save the found touch event
        if (scanning) {
            Touch touch = Input.GetTouch(0);
            switch (state)
            {
                case State.ScanningFloor:
                    CheckFloorRaycast(touch);
                    break;
                case State.ScanningWall:
                    if (!positioned)
                    {
                        VerifyWallPosition(touch);
                    }
                    break;
                case State.InTimeline:
                    break;

            }
        }
        
       

    }

    public IEnumerator Timer(float t, Action DoneWait)
    {
        //print("WaitAndPrint " + Time.time);
        yield return new WaitForSeconds(t);
        //print("WaitAndPrint " + Time.time);
        DoneWait?.Invoke();
       
    }


    public void TrackFloor()
    {
        Debug.Log("Track Floor");
       
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;

        //turn off point to floor
        popup[0].SetActive(false);
        //turn on tap
        popup[1].SetActive(true);
        scanning = true;
    }

    public void CheckFloorRaycast(Touch touch)
    {
        Debug.Log("Floor Raycast");
        CheckRaycast(DetectFloor, touch);
    }


    public void StartWallTracking()
    {
        yesPositionedButton.SetActive(false);
        noPositionedButton.SetActive(false);
        confirmPositionText.SetActive(false);
        positionObject.SetActive(false);

        StartCoroutine(Timer(1f, () => {
            m_planeManager.enabled = true;
            popup[3].SetActive(true);
            scanning = true;
        }
        ));
    }

    public bool CheckRaycast(Action <List<ARRaycastHit>> afterHit, Touch touch)
    {
        //Ray fromCamera = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        const TrackableType trackableTypes = TrackableType.Planes;


        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes)) {
            afterHit?.Invoke(hits);
            return true;
        }

        return false;
    }

    public void DetectFloor(List<ARRaycastHit> hits)
    {
        print("Floor" + hits[0].ToString() + " " + hits[0].trackableId);
        Vector3 pos = hits[0].pose.position;
        Quaternion rot = hits[0].pose.rotation;
        scanning = false;

        foreach (ARPlane p in m_planeManager.trackables)
        {
            if (p.trackableId.Equals(hits[0].trackableId))
            {
                arFloor.transform.position = p.transform.position;
                Debug.Log("ar position:" + arFloor.transform.position);
                LocationInfo.Instance.SetFloorPos(arFloor);

            }
            else
            {
                p.gameObject.SetActive(false);
               
            }

        }

        //turn off tap floor
        popup[1].SetActive(false);
        //turn on point to wall
        popup[2].SetActive(true);

        //Time Delay
        StartCoroutine(Timer(0.7f, SwitchToWall));
      

        //switch scanning to wall
        void SwitchToWall(){

            state = State.ScanningWall;
            m_planeManager.enabled = false;
            StartCoroutine(Timer(2f, TrackWall));
           
        }
      
    }

    public void TrackWall()
    {
        Debug.Log("Track Wall");
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

        //turn off point to wall 
        popup[2].SetActive(false);
        //turn on tap wall
        popup[3].SetActive(true);
        scanning = true;
    }

    public void MoveToGround()
    {
        float groundHeight = arFloor.transform.position.y;
        
        foreach (GameObject[] go in toPlace)
        {
            //for all children in each gameobject
            foreach (GameObject g in go)
            {
                Vector3 pos = g.transform.position;
                Renderer r = g.GetComponent<Renderer>();
                if (r != null)
                {
                    g.transform.position = new Vector3(pos.x, groundHeight + r.bounds.extents.y, pos.z);
                }
                else
                {
                    g.transform.position = new Vector3(pos.x, groundHeight + pos.y, pos.z);
                }

               
            }

        }



    }

    public void VerifyWallPosition(Touch touch)
    {
        //turn off pop up
        popup[3].SetActive(false);

        const TrackableType trackableTypes = TrackableType.Planes;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            m_planeManager.enabled = false;
            scanning = false;

            foreach (ARPlane p in m_planeManager.trackables)
            {
                if (p.trackableId.Equals(hits[0].trackableId))
                {
                    finalPlane = p;
                    finalHit = hits[0];
                    positionObject.transform.position = finalHit.pose.position;
                    positionObject.transform.rotation = finalHit.pose.rotation;
                    positionObject.SetActive(true);
                }

                p.gameObject.SetActive(false);

            }

        }

        //turn on  buttons
        yesPositionedButton.SetActive(true);
        noPositionedButton.SetActive(true);
        confirmPositionText.SetActive(true);
    }

    
    //called by yes button
    public void PositionObjects()
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

        //switch scanning to wall
        void SwitchToTimeline()
        {
            state = State.InTimeline;
            timeline.SetActive(true);
        }

    }

    public void StartAR()
    {
        //Start AR
        StartCoroutine(Timer(2f, TrackFloor));

    }

}



//public void CheckWallRaycast(Touch touch)
//{
//    Debug.Log("Wall Raycast");
//    CheckRaycast(DetectWall, touch);
//}

//public void DetectWall(List<ARRaycastHit> hits)
//{
//    print(hits[0].ToString() + " " + hits[0].trackableId);
//    Vector3 pos = hits[0].pose.position;
//    Quaternion rot = hits[0].pose.rotation;
//    scanning = false;


//    m_planeManager.enabled = false;
//    state = State.InTimeline;

//    foreach (ARPlane p in m_planeManager.trackables)
//    {
//        if (p.isActiveAndEnabled && p.trackableId.Equals(hits[0].trackableId))
//        {
//            //reposition timeline
//            //timeline.transform.position = pos;
//            timeline.transform.rotation = rot;
//            timeline.transform.Rotate(-90, 0, 0);
//            timeline.transform.Rotate(0, 90, 0);
//            timeline.transform.Rotate(0, 0, 180);

//            Vector3 userPos = new Vector3(pos.x, Camera.main.transform.position.y, pos.z);
//            timeline.transform.position = userPos - dummy.transform.position;

//            //move dates up
//            dates.transform.position = new Vector3(dates.transform.position.x, Camera.main.transform.position.y - 0.05f, dates.transform.position.z);
//            intro.transform.position = new Vector3(intro.transform.position.x, hits[0].pose.position.y, intro.transform.position.z);

//            //move cards to middle of the wall; based on plane center
//            foreach (GameObject c in cards)
//            {
//                Vector3 cardPos = c.transform.position;
//                c.transform.position = new Vector3(cardPos.x, hits[0].pose.position.y, cardPos.z);
//            }
//        }

//        p.gameObject.SetActive(false);


//    }

//    MoveToGround();

//    //turn off tap wall plane popup
//    popup[3].SetActive(false);

//    SwitchToTimeline();

//    ////Time Delay
//    //StartCoroutine(Timer(0.5f, SwitchToTimeline));

//    //switch scanning to wall
//    void SwitchToTimeline()
//    {
//        state = State.InTimeline;
//        timeline.SetActive(true);


//    }

//}
