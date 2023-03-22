using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ProjectToPillar : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    ARRaycastManager m_raycastManager;

    public GameObject pillar;

    public SceneController sc;

    [SerializeField]
    GameObject ministersMenu;

    [SerializeField]
    GameObject tapPillarText;

    [SerializeField]
    GameObject pointPillarText;

    //TODO REMOVE ONLY FOR TESTING
    [SerializeField]
    GameObject scene;

    bool scanning = false;

    void Awake()
    {

    }

    private void Start()
    {
       
    }

    public void StartScanning()
    {
        pointPillarText.SetActive(true);
        StartCoroutine(Timer(2f,TrackPillar));
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        // Save the found touch event
        if (scanning)
        {
            Touch touch = Input.GetTouch(0);
            CheckPillarRaycast(touch);
        }



    }

    IEnumerator Timer(float t, Action DoneWait)
    {
        //print("WaitAndPrint " + Time.time);
        yield return new WaitForSeconds(t);
        //print("WaitAndPrint " + Time.time);
        DoneWait?.Invoke();

    }


    public void TrackPillar()
    {
        pointPillarText.SetActive(false);
        tapPillarText.SetActive(true);
        Debug.Log("Track Pillar");
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

        //turn off point to wall 
      
        scanning = true;
    }

    public void CheckPillarRaycast(Touch touch)
    {
        Debug.Log("Wall Raycast");
        CheckRaycast(DetectPillar, touch);
        
        

    }




    public bool CheckRaycast(Action<List<ARRaycastHit>> afterHit, Touch touch)
    {
        //Ray fromCamera = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        const TrackableType trackableTypes = TrackableType.Planes;


        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            afterHit?.Invoke(hits);
            return true;
        }

        return false;
    }


    public void DetectPillar(List<ARRaycastHit> hits)
    {
        print(hits[hits.Count - 1].ToString() + " " + hits[0].trackableId);
        ARRaycastHit lastHit = hits[hits.Count - 1];

        scanning = false;


        m_planeManager.enabled = false;
        int count = 0;
        float farthestDist = -1;
        ARPlane farthestPlane = null;
        
        foreach (ARPlane p in m_planeManager.trackables)
        {
            Vector3 camNoY = new Vector3(Camera.main.transform.position.x, 0 , Camera.main.transform.position.z);
            Vector3 planeNoY = new Vector3(p.transform.position.x, 0, p.transform.position.z);



            //float dist = Vector3.Distance(camNoY, planeNoY);
            //get distance in forward camera direction
            Vector3 zDist = planeNoY - camNoY;
            Vector3 zDir = Camera.main.transform.TransformDirection(Vector3.forward).normalized;
            float proj = Vector3.Dot(zDist, zDir);
            if (proj > farthestDist){
                farthestDist = proj;
                farthestPlane = p;
            }

            Debug.Log(count +":" + p.transform.position);
            p.gameObject.SetActive(false);
        }

        //set the plane based on the plane farthest distance from the camera
        //farthestPlane.gameObject.SetActive(true);
        Vector3 planePos = farthestPlane.transform.position;
        Quaternion rot = farthestPlane.transform.rotation;

        pillar.transform.position = planePos;
        pillar.transform.rotation = rot;
        pillar.transform.Rotate(90, 0, 0);


        //move the AR
        //from Pillar
        Debug.Log("POSITION FROM PILLAR");

        sc.PositionAR();

        PositionMinsiters();

        //Turn on Introduction Text
        transform.GetChild(0).gameObject.SetActive(true);

        //Turn on Ministers canvas
        ministersMenu.SetActive(true);
        //turn off tap the pillar
        tapPillarText.SetActive(false);

    }




    public void PositionMinsiters()
    {
        float groundHeight = LocationInfo.Instance.GetFloorPos().y;

        //set the minister position 
        foreach (Transform c in gameObject.transform)
        {
            Vector3 cPos = c.position;
            c.rotation = pillar.transform.rotation;

            //pillar&image has been rotated 90f z = right and left
            c.position = pillar.transform.position;
            c.position = new Vector3(pillar.transform.position.x, groundHeight + (cPos.y), pillar.transform.position.z);

        }
    }

}
