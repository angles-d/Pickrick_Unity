using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    bool placed = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    GameObject prefab;

    Camera arCamera;
    void Start()
    {
        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    

    public void OnEnable()
    {
        m_planeManager.planesChanged += OnPlaneChanged;
    }

    public void OnDisable()
    {
        m_planeManager.planesChanged -= OnPlaneChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPlaneChanged(ARPlanesChangedEventArgs args)
    {
        Debug.Log("PLANE" + args);
        //foreach(ARPlane plane in args.added)
        //{

        //}

        //foreach (ARPlane plane in args.updated)
        //{

        //}

        //foreach (ARPlane plane in args.removed)
        //{

        //}

    }

    void MoveToGround(GameObject prefab)
    {
        float groundHeight = float.MaxValue;

        foreach (ARPlane plane in m_planeManager.trackables)
        {
            if (plane.transform.position.y < groundHeight) {
                groundHeight = plane.transform.position.y;
            }
        }

        if (groundHeight != float.MaxValue)
        {
            Vector3 pos = prefab.transform.position;
            prefab.transform.position = new Vector3(pos.x, groundHeight, pos.z);
        }
    }
}
