using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ProjectToPillar : MonoBehaviour
{
    bool tracking = false;

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

        //turn off pint to wall 
      
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
        print(hits[0].ToString() + " " + hits[0].trackableId);
        //Vector3 planePos = hits[0].pose.position;
        //Quaternion rot = hits[0].pose.rotation;
        scanning = false;


        m_planeManager.enabled = false;
        
        foreach (ARPlane p in m_planeManager.trackables)
        {
            if (p.trackableId.Equals(hits[0].trackableId))
            {
                Vector3 planePos = p.transform.position;
                Quaternion rot = p.transform.rotation;

                pillar.transform.position = planePos;
                pillar.transform.rotation = rot;
                pillar.transform.Rotate(90, 0, 0);

    
               
            }
            p.gameObject.SetActive(false);

        }

        //move the AR
        //from Pillar
        Debug.Log("POSITION FROM PILLAR");

        sc.PositionAR();

        //float groundHeight = LocationInfo.Instance.GetFloorPos().y;

        ////set the minister position 
        //foreach (Transform c in gameObject.transform)
        //{
        //    Vector3 cPos = c.position;
        //    c.rotation = pillar.transform.rotation;
        //    //Renderer r = c.GetComponent<Renderer>();
        //    //if (r != null)
        //    //{
        //    //    c.position = new Vector3(pillar.transform.position.x - 0.02f, groundHeight + r.bounds.extents.y, pillar.transform.position.z);
        //    //}
        //    //else
        //    //{
        //    c.position = new Vector3(pillar.transform.position.x - 1f, pillar.transform.position.y, pillar.transform.position.z);
        //    Debug.Log(c.name+ " Move to ground:" + c.position);
        //    //}


        //}

        PositionMinsiters();

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
            //Renderer r = c.GetComponent<Renderer>();
            //if (r != null)
            //{
            //    c.position = new Vector3(pillar.transform.position.x - 0.02f, groundHeight + r.bounds.extents.y, pillar.transform.position.z);
            //}
            //else

            //pillar&image has been rotated 90f z = right and left
            c.position = pillar.transform.position;
            c.position = new Vector3(pillar.transform.position.x, groundHeight + (cPos.y), pillar.transform.position.z);
            //c.transform.Translate(-0.5f,0,0);
            //c.position = new Vector3(pillar.transform.position.x, c.position.y + groundHeight, pillar.transform.position.z -0.5f);
            Debug.Log(c.name + " Move to ground:" + c.position);
            //}


        }
    }

    

}
