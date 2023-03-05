using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    bool placed = false;
    bool tracking = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    GameObject[] toPlace;
    //[SerializeField]
    //GameObject anim;
    [SerializeField]
    GameObject arFloor;


    Camera arCamera;

    void Awake()
    {
        foreach (GameObject g in toPlace)
        {
            //g.SetActive(false);
        }

        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        m_planeManager.enabled = false;
    }

    void Start()
    {
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
       
    }


    void Update()
    {
        if (tracking && !placed)
        {
            //StartCoroutine(Ground());
            FindGroundPlane();
        }
        
    }

    IEnumerator Ground()
    {
        if (!placed && m_planeManager.trackables.count >= 1)
        {
            Debug.Log("TRACKED");
            MoveToGround();
        }
        yield return null;
    }

    public void StartTracking()
    {
        if (!placed)
        {
            tracking = true;
            m_planeManager.enabled = true;
        }
       
    }

    public void FindGroundPlane()
    {
        if (!placed && m_planeManager.trackables.count >= 1)
        {
            Debug.Log("TRACKED");
            float groundHeight = float.MaxValue;
            //set arplane to ground
            foreach (ARPlane plane in m_planeManager.trackables)
            {
                if (plane.transform.position.y < groundHeight)
                {
                    groundHeight = plane.transform.position.y;
                    Debug.Log("Ground Height: " + groundHeight);
                }

            }

            if (groundHeight != float.MaxValue)
            {
               
                arFloor.transform.position = new Vector3(arFloor.transform.position.x, groundHeight + 0.1f, arFloor.transform.position.z);
                Debug.Log("MOVE TO GROUND");
                MoveToGround();
            }
           
        }

       
    }

    public void MoveToGround()
    {
        float groundHeight = arFloor.transform.position.y;
        foreach (GameObject g in toPlace)
        {
            //for all children in each gameobject
            foreach (Transform c in g.transform)
            {
                Vector3 pos = c.position;
                Renderer r = c.GetComponent<Renderer>();
                if (r != null)
                {
                    c.position = new Vector3(pos.x, groundHeight + r.bounds.extents.y, pos.z);
                }
                else
                {
                    c.position = new Vector3(pos.x, groundHeight + pos.y, pos.z);
                }

                //g.SetActive(true);
            }

        }

            

        //change trackng flags
        placed = true;
        tracking = false;

        //turn off plane manager
        m_planeManager.SetTrackablesActive(false);
        m_planeManager.enabled = false;
    }

       
    

    
}
