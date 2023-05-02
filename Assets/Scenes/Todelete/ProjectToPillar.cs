using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ProjectToPillar : MonoBehaviour
{
    //#region AR
    //public GameObject pillarLocationRef;
    //public PathSceneController sc;

    ////UI Elements
    //[Header("UI Elements")]
    //[SerializeField]
    //GameObject ministersMenu;
    //[SerializeField]
    //GameObject tapPillarText;
    //[SerializeField]
    //GameObject pointPillarText;

    //bool scanning = false;

    //[SerializeField]
    //ARPlaneManager m_planeManager;
    //[SerializeField]
    //ARRaycastManager m_raycastManager;

    //private void Awake()
    //{
    //    m_raycastManager = LocationInfo.Instance.GetRaycastManager();
    //    m_planeManager = LocationInfo.Instance.GetPlaneManager();

    //    m_planeManager.enabled = true;
    //}

    //void Update()
    //{
    //    //no touch events
    //    if (Input.touchCount == 0)
    //    {
    //        return;
    //    }

    //    // Save the found touch event
    //    if (scanning)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        CheckPillarRaycast(touch);
    //    }

    //}

   
    //public void StartScanning()
    //{
    //    //detect vertical planes
    //    m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;
    //    //show point camera to pillar text
    //    pointPillarText.SetActive(true);
    //    //track the pillar location
    //    StartCoroutine(sc.Timer(2f, TrackPillar));
    //}


    //public void TrackPillar()
    //{
    //    pointPillarText.SetActive(false);
    //    tapPillarText.SetActive(true);
    //    m_planeManager.enabled = true;
    //    m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;
    //    scanning = true;
    //}

    //public void CheckPillarRaycast(Touch touch)
    //{
    //    Debug.Log("Check Raycast Intersection");
    //    sc.CheckRaycast(m_raycastManager, DetectPillar, touch);
    //}


    //public void DetectPillar(List<ARRaycastHit> hits)
    //{
    //    print(hits[hits.Count - 1].ToString() + " " + hits[0].trackableId);
    //    scanning = false;


    //    m_planeManager.enabled = false;
    //    float farthestDist = -1;
    //    ARPlane farthestPlane = null;

    //    //Camera position w/ y = 0
    //    Vector3 camNoY = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);

    //    foreach (ARPlane p in m_planeManager.trackables)
    //    {
    //        //Plane position w/ y = 0
    //        Vector3 planeNoY = new Vector3(p.transform.position.x, 0, p.transform.position.z);

    //        //get distance in forward camera direction
    //        Vector3 zDist = planeNoY - camNoY;
    //        Vector3 zDir = Camera.main.transform.TransformDirection(Vector3.forward).normalized;
    //        float proj = Vector3.Dot(zDist, zDir);

    //        //check if plane is farthes away
    //        if (proj > farthestDist){
    //            farthestDist = proj;
    //            farthestPlane = p;
    //        }

    //        p.gameObject.SetActive(false);
    //    }

    //    //set the plane based on the plane farthest distance from the camera
    //    Vector3 planePos = farthestPlane.transform.position;
    //    Quaternion rot = farthestPlane.transform.rotation;

    //    pillarLocationRef.transform.position = planePos;
    //    pillarLocationRef.transform.rotation = rot;
    //    pillarLocationRef.transform.Rotate(90, 0, 0);


    //    //move the AR
    //    sc.PositionAR();

    //    //move the minsiters to the floor
    //    PositionMinsiters();

    //    //Turn on the Ministers' Introduction Text
    //    transform.GetChild(0).gameObject.SetActive(true);

    //    //Turn on Ministers canvas
    //    ministersMenu.SetActive(true);

    //    //turn off tap the pillar
    //    tapPillarText.SetActive(false);

    //}




    //public void PositionMinsiters()
    //{
    //    float groundHeight = LocationInfo.Instance.GetFloorPos().y;

    //    //set the minister position 
    //    foreach (Transform c in gameObject.transform)
    //    {
    //        Vector3 cPos = c.position;
    //        c.rotation = pillarLocationRef.transform.rotation;

    //        //pillar&image has been rotated 90f z = right and left
    //        c.position = pillarLocationRef.transform.position;
    //        c.position = new Vector3(pillarLocationRef.transform.position.x, groundHeight + cPos.y, pillarLocationRef.transform.position.z);

    //    }
    //}

    //#endregion AR


}
