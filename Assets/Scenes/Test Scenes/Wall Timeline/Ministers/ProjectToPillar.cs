using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ProjectToPillar : MonoBehaviour
{
    bool placed = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    ARRaycastManager m_raycastManager;

   
    bool scanning = false;

    void Awake()
    {

    }

    private void Start()
    {

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
        Vector3 planePos = hits[0].pose.position;
        Quaternion rot = hits[0].pose.rotation;
        scanning = false;


        m_planeManager.enabled = false;

        foreach (ARPlane p in m_planeManager.trackables)
        {
            if (p.trackableId.Equals(hits[0].trackableId))
            {
                foreach (Transform c in gameObject.transform)
                {
                    Vector3 cPos = c.position;

                    c.rotation = rot;
                    c.position = new Vector3(planePos.x,cPos.y, planePos.z);
                    
                }
            }
            else
            {
                p.gameObject.SetActive(false);
            }

        }

       

    }

    

}
