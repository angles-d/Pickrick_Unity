using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FindWall : MonoBehaviour
{
    bool placed = false;
    bool wallFound = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    ARRaycastManager m_raycastManager;

    [SerializeField]
    GameObject[] cards;
    ARPlane wall;

    public GameObject timeline;

   
    public GameObject dummy;

    public PlaceOnGround pg;

    //[SerializeField]
    //GameObject anim;

    void Awake()
    {
        m_raycastManager = gameObject.GetComponent<ARRaycastManager>();
        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        //m_planeManager.enabled = false;
    }



    void Update()
    {
        //if (wallFound)
        //{
        //    RemoveOtherPlanes();
        //}
            //foreach (ARPlane p in m_planeManager.trackables)
            //{

            //    Debug.Log("NEW PLANE DETECTED");
            //    Debug.Log("NEW PLANE Rotation:" + p.transform.rotation);
            //    Debug.Log("NEW PLANE Position:" + p.transform.position);
            //    foreach (GameObject card in cards)
            //    {

            //        Instantiate(dummy, p.transform.position, p.transform.rotation);

            //        //not working??
            //        card.transform.position = new Vector3(p.transform.position.x, card.transform.position.y, card.transform.position.z);
            //        //rotation being weird as hell
            //        //card.transform.rotation = p.transform.rotation;

            //    }


            //    //placed = true;
            //    //tracking = false;
            //    //m_planeManager.enabled = false;
            //}
        

    }



    public void TrackWall()
    {
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;
    }

    public void CheckWallRaycast()
    {
        Ray fromCamera = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        //var touch = Input.GetTouch(0);
        const TrackableType trackableTypes = TrackableType.Planes;


        List<ARRaycastHit> hits = new List<ARRaycastHit>();


        if (m_raycastManager.Raycast(fromCamera, hits, trackableTypes)) {
            print(hits[0].ToString() + " "+  hits[0].trackableId);
            Vector3 pos = hits[0].pose.position;
            Quaternion rot = hits[0].pose.rotation;

            

            wallFound = true;
            m_planeManager.enabled = false;
            
            foreach (ARPlane p in m_planeManager.trackables)
            {
                if (p.trackableId.Equals(hits[0].trackableId))
                {
                    timeline.transform.position = pos;
                    timeline.transform.rotation = rot;
                    timeline.transform.Rotate(-90, 0, 0);
                    timeline.transform.Rotate(0, 90, 0);
                    timeline.transform.Rotate(0, 0, 180);

                    wall = p;
                    cards[0].transform.position = pos;
                } else {
                    p.gameObject.SetActive(false);
                }

            }
           
        }

        
    }

    public void SetUpTestWallRotate()
    {
        Vector3 pos = dummy.transform.position;
        Quaternion rot = dummy.transform.rotation;

        timeline.transform.rotation = rot;
        timeline.transform.position = pos;

    }
    public void TestWallRotate()
    {
        timeline.transform.Rotate(90, 0, 0);

    }
}
