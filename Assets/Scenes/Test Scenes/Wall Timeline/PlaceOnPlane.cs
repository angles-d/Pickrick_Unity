using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceOnPlane : MonoBehaviour
{
    // Start is called before the first frame update
    bool placed = false;
    bool tracking = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    GameObject toPlace;
    //[SerializeField]
    //GameObject anim;
    //[SerializeField]
    //GameObject arFloor;

    void Awake()
    {
       
        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        //m_planeManager.enabled = false;
    }



    void Update()
    {
        if (!placed & m_planeManager.trackables.count == 1)
        {
            foreach(ARPlane p in m_planeManager.trackables)
            {
                Debug.Log("NEW PLANE DETECTED");
                Debug.Log("NEW PLANE Rotation:" + p.transform.rotation);
                Debug.Log("NEW PLANE Position:" + p.transform.position);
                toPlace.transform.position = p.transform.position;
                toPlace.transform.rotation= p.transform.rotation;
                placed = true;
            }
        }

    }

   

    public void StartTracking()
    {
        if (!placed)
        {
            tracking = true;
            m_planeManager.enabled = true;
        }

    }
    
}
