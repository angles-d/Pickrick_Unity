using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Linq;

public class FindWall : MonoBehaviour
{
    bool placed = false;

    public enum State{
        ScanningFloor,
        ScanningWall,
        InTimeline
    }

    public State state = State.ScanningFloor;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    ARRaycastManager m_raycastManager;

    [SerializeField]
    GameObject[] cards;

    [SerializeField]
    GameObject[] doors;

    [SerializeField]
    GameObject[] ministers;

    [SerializeField]
    GameObject[] lightrays;

    public GameObject[] popup; //point floor, tap floor, scan wall, tap wall

    public GameObject timeline;

   
    public GameObject dummy;

    [SerializeField]
    GameObject arFloor;


    public GameObject[][] toPlace = new GameObject[4][];

    bool scanning = false;

    public GameObject dates;

    void Awake()
    {
        m_raycastManager = gameObject.GetComponent<ARRaycastManager>();
        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        m_planeManager.enabled = false;

        
    }

    private void Start()
    {
       
        dates = timeline.transform.GetChild(1).gameObject;

        //populate to place
        toPlace[0] = doors;
        toPlace[1] = cards;
        toPlace[2] = lightrays;
        toPlace[3] = ministers;

        //hide timelin
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
                    CheckWallRaycast(touch);
                    break;
                case State.InTimeline:
                    break;

            }
        }
        
       

    }

    IEnumerator Timer(float t, Action DoneWait)
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

    public void CheckWallRaycast(Touch touch)
    {
        Debug.Log("Wall Raycast");
        CheckRaycast(DetectWall, touch);
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



    public void DetectWall(List<ARRaycastHit> hits)
    {
        print(hits[0].ToString() + " " + hits[0].trackableId);
        Vector3 pos = hits[0].pose.position;
        Quaternion rot = hits[0].pose.rotation;
        scanning = false;


        m_planeManager.enabled = false;
        state = State.InTimeline;

        foreach (ARPlane p in m_planeManager.trackables)
        {
            if (p.trackableId.Equals(hits[0].trackableId))
            {
                //reposition timeline
                //timeline.transform.position = pos;
                timeline.transform.rotation = rot;
                timeline.transform.Rotate(-90, 0, 0);
                timeline.transform.Rotate(0, 90, 0);
                timeline.transform.Rotate(0, 0, 180);

                timeline.transform.position = pos - dummy.transform.position;

                //move dates up
                dates.transform.position = new Vector3(dates.transform.position.x, Camera.main.transform.position.y - 0.1f, dates.transform.position.z);
                
            }
            else
            {
                p.gameObject.SetActive(false);
            }

        }

        MoveToGround();

        //turn off tap wall plane popup
        popup[3].SetActive(false);

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

    public void StartAR()
    {
        //Start AR
        StartCoroutine(Timer(2f, TrackFloor));

    }

}
