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
    GameObject prefab;
    [SerializeField]
    GameObject anim;
    [SerializeField]
    GameObject arFloor;


    Camera arCamera;

    void Awake()
    {
        //anim.SetActive(false);
        prefab.SetActive(false);
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
            MoveToGround(prefab);
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
            MoveToGround(prefab);
        }

       
    }

    void MoveToGround(GameObject prefab)
    {
        float groundHeight = float.MaxValue;

        foreach (ARPlane plane in m_planeManager.trackables)
        {
            if (plane.transform.position.y < groundHeight) {
                groundHeight = plane.transform.position.y;
                Debug.Log("Ground Height: " + groundHeight);
            }
        }

        if (groundHeight != float.MaxValue)
        {
            Vector3 pos = prefab.transform.position;
            //Instantiate(prefab, new Vector3(pos.x, groundHeight, pos.z), Quaternion.identity);
            prefab.transform.position = new Vector3(pos.x, groundHeight + pos.y, pos.z);
            prefab.SetActive(true);

            arFloor.transform.position = new Vector3(arFloor.transform.position.x, groundHeight + pos.y, arFloor.transform.position.z);

            //change tracking flags
            placed = true;
            tracking = false;

            anim.transform.position = new Vector3(anim.transform.position.x, groundHeight, anim.transform.position.z);
           

            m_planeManager.SetTrackablesActive(false);
            m_planeManager.enabled = false;
        }

       
    }

    
}
