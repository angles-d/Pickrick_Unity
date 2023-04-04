using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MinisterHotspot : MonoBehaviour { 


    //public GameObject ministers;
    public GameObject walkToSign;
    public GameObject ministerCanvas;
    public GameObject scene;
    public GameObject positionObj;

    // Start is called before the first frame update

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    ARRaycastManager m_raycastManager;
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        if (m_planeManager.isActiveAndEnabled)
        {
            if (Input.touchCount == 0)
            {
                return;
            }

            Touch touch = Input.GetTouch(0);
            CheckRaycast(touch);
        }
    }


    public bool CheckRaycast(Touch touch)
    {
        //Ray fromCamera = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        const TrackableType trackableTypes = TrackableType.Planes;


        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            Instantiate(positionObj, hits[0].pose.position, hits[0].pose.rotation);
        }

        foreach(ARPlane p in m_planeManager.trackables)
        {
            p.gameObject.SetActive(false);
        }

        m_raycastManager.enabled = false;
        m_planeManager.enabled = false;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            //ministers.SetActive(true);
            TurnOffWalkToSign();
            ministerCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void TurnOffWalkToSign()
    {
        walkToSign.SetActive(false);
    }

    public void TurnOnWalkToSign()
    {
        walkToSign.SetActive(true);
    }

    public void MoveScene(GameObject pillarRef)
    {

        scene.transform.rotation = pillarRef.transform.rotation;
        scene.transform.Rotate(90, 0, 0);
        scene.transform.Rotate(0, 90, 0);

        Vector3 pillarPos = new Vector3(pillarRef.transform.position.x, 0, pillarRef.transform.position.z);
        scene.transform.position = pillarPos;
        //Debug.Log("Ref Pos:" + posReference.transform.position);

        TurnOnWalkToSign();
    }


}
