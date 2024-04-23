using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Controller for the minister pillar interaction
public class FourMinistersController : MonoBehaviour
{
    //Location of the pillar (the reference object outside the Scene game object)
    [SerializeField] GameObject pillarLocationRef;

    //Minister panel overlays for the pillar
    //Order of the ministers should match the order of the ministers in the ministercanvas
    [SerializeField] GameObject[] ministers;

    [SerializeField] PathSceneController sc;
  
    //UI Elements
    [Header("UI Elements")]
    [SerializeField] GameObject pointPillarText;
    [SerializeField] GameObject tapPillarText;
    [SerializeField] GameObject moveToNext;

    //Minister Canvas Objects
    //Minister Canvas Game object
    [SerializeField] GameObject ministersMenu;
    [SerializeField] GameObject meetMinisterText;
    //set of indivdual minister picture  canvas groups
    [SerializeField] CanvasGroup[] ministerCanvas = new CanvasGroup[4];

    ARPlaneManager m_planeManager;
    ARRaycastManager m_raycastManager;

    //flag booleans
    //true if current minister button is pressed
    bool[] buttonPressed = new bool[4];
    //true if the minister button has been visited by the user
    bool[] visited = { false, false, false, false };
    //true if scanning for the pillar
    bool scanning = false;
    //true if all the ministers have been visited
    bool allVisited = false;

    public float ministersOffsetY;

    private void Start()
    {
        m_raycastManager = ARInfo.Instance.GetRaycastManager();
        m_planeManager = ARInfo.Instance.GetPlaneManager();

        m_planeManager.enabled = true;
    }

    void Update()
    {
        //if no touch events are detected
        if (Input.touchCount == 0)
        {
            return;
        }

        // If the Camera is scanning for an AR interaction
        if (scanning)
        {
            // Save the found touch event
            Touch touch = Input.GetTouch(0);

            //check for a raycast interaction w/ the current AR planes
            CheckPillarRaycast(touch);
        }

    }

    //Prepares the camera & user to detect the middle pillar
    public void StartScanning()
    {
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;
        //show point camera to pillar text
        pointPillarText.SetActive(true);
        //Turn on the plane detection & scanning
        StartCoroutine(sc.Timer(2f, TrackPillar));
    }

    //Prepares the camera & user to detect the pillar
    public void TrackPillar()
    {
        //Turn on detect pillar UI popups
        pointPillarText.SetActive(false);
        tapPillarText.SetActive(true);

        //Activate scanning
        m_planeManager.enabled = true;
        scanning = true;
    }

    //Check for a raycast w/ the pillar
    public void CheckPillarRaycast(Touch touch)
    {
        sc.CheckRaycast(m_raycastManager, DetectPillar, touch);
    }


    //Finds the farthest plane saved by the ar_plane_manager at the time of the tap
    public void DetectPillar(List<ARRaycastHit> hits)
    {
        scanning = false;
        m_planeManager.enabled = false;

        //Variables to save the farthest plane
        float farthestDist = -1;
        ARPlane farthestPlane = null;

        //Camera position vector w/ the y value set to 0
        Vector3 camNoY = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);

        foreach (ARPlane p in m_planeManager.trackables)
        {
            //Get the plane position w/ the y value set to 0
            Vector3 planeNoY = new Vector3(p.transform.position.x, 0, p.transform.position.z);

            //Get distance to the plane from the camera in the forward camera direction
            Vector3 zDist = planeNoY - camNoY;
            Vector3 zDir = Camera.main.transform.TransformDirection(Vector3.forward).normalized;
            float projDist = Vector3.Dot(zDist, zDir); //projected distance in the camera direction to the plane

            //check if the current plane is farthest away
            if (projDist > farthestDist)
            {
                farthestDist = projDist;
                farthestPlane = p;
            }

            p.gameObject.SetActive(false);
        }

        //set the pillar location based on the plane farthest distance from the camera
        Vector3 planePos = farthestPlane.transform.position;
        Quaternion rot = farthestPlane.transform.rotation;

        //Transform the scene based on the detcted pillar position
        pillarLocationRef.transform.position = planePos;
        pillarLocationRef.transform.rotation = rot;
        pillarLocationRef.transform.Rotate(90, 0, 0);


        //move the AR objects
        sc.PositionAR();

        //move the minsiters to the floor
        PositionMinsiters();

        //Turn on the Ministers' Introduction Text
        transform.GetChild(0).gameObject.SetActive(true);

        //Turn on Ministers canvas
        ministersMenu.SetActive(true);

        //turn off tap the pillar
        tapPillarText.SetActive(false);

    }

    //Position the minister pillar panels so that the ministers' feet are on the ground
    public void PositionMinsiters()
    {
        float groundHeight = ARInfo.Instance.GetFloorPosition().y;

        //set the minister position 
        foreach (Transform c in gameObject.transform)
        {
            Vector3 cPos = c.position;
            c.rotation = pillarLocationRef.transform.rotation;

            //pillar&image are rotated 90 degrees so z = right and left
            c.position = pillarLocationRef.transform.position;
            c.position = new Vector3(pillarLocationRef.transform.position.x, groundHeight + cPos.y + ministersOffsetY, pillarLocationRef.transform.position.z);
        }
    }

    //Activates the selected AR Minister panel on the pillar
    public void ShowMinister(int index)
    {
        for (int i = 0; i < ministers.Length; i++)
        {
            if (i == index)
            {
                if (index > 0)
                {
                    visited[i - 1] = true;
                }
                ministers[i].SetActive(true);
            }
            else
            {
                ministers[i].SetActive(false);
            }

        }


    }

    //Hides the AR minister cards
    public void HideMinisterCards()
    {
        for (int i = 0; i < ministers.Length; i++)
        {
            ministers[i].SetActive(false);

        }

        //turn off script (ministers section has been completed)
        this.enabled = false;
    }

    //Minister Canvas Methods

    //Greys out the non-selected minister images
    public void LowerImageAlpha(int index)
    {
        ministerCanvas[index].alpha = 1f;

        //greyout all the non preseed buttons
        for (int i = 0; i < 4; i++)
        {
            if (i != index && buttonPressed[i])
            {
                ministerCanvas[i].alpha = 0.6f;
            }
        }

        if (!buttonPressed[index])
        {
            buttonPressed[index] = true;
        }
    }

    //Checks to see if all the ministers have been visited
    //Called when the minister UI images are pressed
    public void CheckAllVisited()
    {
        if (!allVisited)
        {
            foreach (bool minVisited in visited)
            {
                if (!minVisited)
                {
                    return;
                }
            }

            allVisited = true;

            //Turn off meet the minister text
            meetMinisterText.SetActive(false);

            //Turn on the tap to continue button
            moveToNext.SetActive(true);
        }
    }
    
}
