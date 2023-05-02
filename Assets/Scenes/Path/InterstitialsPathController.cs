using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class InterstitialsPathController : MonoBehaviour
{
    [SerializeField] PathSceneController sc;

    //interstitial gameobject prefabs
    [SerializeField] GameObject[] inters;
    //interstitial pin markers
    [SerializeField] GameObject[] markers;
    //Variables for the timer
    [SerializeField] float timeToNext;

    [Header("UI Elements")]
    //button to show next animation
    public GameObject nextButtonToAnim;
  
    //array of dates to put on the next button to the animations
    string[] dates = {"July 3rd, 1964", "Aug 11th, 1964", "Sep 26th, 1964" , "Jan 29th, 1965", "Feb 22th, 1965" };
    TextMeshProUGUI dateText;

    [SerializeField]
    ARPlaneManager m_planeManager;
    [SerializeField]
    ARRaycastManager m_raycastManager;

    private void Awake()
    {
        m_raycastManager = LocationInfo.Instance.GetRaycastManager();
        m_planeManager = LocationInfo.Instance.GetPlaneManager(); 

    }
    
    private void Start()
    {
        dateText = nextButtonToAnim.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    //Shows the input index interstial marker
    public void ShowIntersitialMarker(int index)
    {
       //set transform and pos (in front of pillar)
        markers[index].SetActive(true);
    }

    //Hides the input index interstial marker
    public void HideInterstitial(int index)
    {
        inters[index].SetActive(false);
    }

    //Starts the set up to detect the pillar
    public void TrackPillar(int index)
    {
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

        StartCoroutine(sc.Timer(0.8f, CheckPillarRaycast, index));
    }


    public void CheckPillarRaycast(int index)
    {
        Ray fromCamera = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(!sc.CheckRaycast(m_raycastManager, PlaceOnPillar, fromCamera, index))
        {
            Debug.Log("No Hit");
            Vector3 position = inters[index].transform.position;
            inters[index].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            inters[index].SetActive(true);


            //Start the countdown timer
            StartCoroutine(sc.Timer(timeToNext, ShowNextButton, index));
        }

    }

    //Places the interstial cards on the pillar
    void PlaceOnPillar(List<ARRaycastHit> hits, int index)
    {
        m_planeManager.enabled = false;
        ARRaycastHit hit = hits[0];
        ARPlane hitPlane = null;

        foreach (ARPlane p in m_planeManager.trackables)
        {
            //Check if the current plane is active
            if (p.isActiveAndEnabled && p.trackableId == hit.trackableId)
            {
                hitPlane = p;
            }
            p.gameObject.SetActive(false);

        }

        if (hitPlane == null)
        {
            Debug.Log("No Planes Hit");
            Vector3 position = inters[index].transform.position;
            inters[index].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
        }
        else
        {
            //check if hit pose up is perpendicular to up direction (aka a vertical plane)
            //If the hitplane is vertical this value should be close to 0

            float normalDir = Mathf.Abs(Vector3.Dot(transform.up, hitPlane.normal));

            Debug.Log("Hit plane " + hitPlane);
            Debug.Log("plane normal" + hitPlane.normal);
            Debug.Log("Normal compare to Horizontal:" + normalDir); 

            if (normalDir < 0.1)
            {
                Debug.Log("Hit a vertical plane");

                //get the position of plane
                Vector3 position = hitPlane.transform.position;
                //get the position of plane
                Quaternion rotation = hitPlane.transform.rotation;

                //set new position & transformation of tracked image
                inters[index].transform.rotation = rotation;
                inters[index].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            }
            else
            {
                Debug.Log("Did not hit a vertical plane");

                Vector3 position = inters[index].transform.position;
                inters[index].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            }
        }

        inters[index].SetActive(true);
        StartCoroutine(sc.Timer(timeToNext, ShowNextButton, index));

    }

    //Updates and shows the next button to the animation events
    void ShowNextButton(int index)
    {
        dateText.text = dates[index];
        nextButtonToAnim.SetActive(true);
    }

}
