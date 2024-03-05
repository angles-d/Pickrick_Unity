using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

//Controller for the AR Interstitials panels on the pillars
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
    public string[] dates = {"July 3rd, 1964", "Aug 11th, 1964", "Sep 26th, 1964" , "Jan 29th, 1965", "Feb 22th, 1965" };
    TextMeshProUGUI dateText;

    ARPlaneManager m_planeManager;
    ARRaycastManager m_raycastManager;

    private void Awake()
    {
        m_raycastManager = ARInfo.Instance.GetRaycastManager();
        m_planeManager = ARInfo.Instance.GetPlaneManager(); 
    }
    
    private void Start()
    {
        //Get the text compenent in the "NEW EVENT" button to update the date
        dateText = nextButtonToAnim.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        timeToNext = 0f;
    }

    //Starts the set up to detect the pillar
    public void TrackPillar(int index)
    {
        //m_planeManager.enabled = true;
        //m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

        StartCoroutine(sc.Timer(0.8f, CheckPillarRaycast, index));
    }

    //Checks for a Raycast with the Pillar and places the interstial cards on the pillar if a collision is detected
    //if no collision is detected interstital is placed at the  world position in the editor
    public void CheckPillarRaycast(int index)
    {
        ////Shoot a ray from the camera
        //Ray fromCamera = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //ARPlane p = sc.RaycastGetPlane(m_raycastManager, m_planeManager, fromCamera);

        ////If a raycast collision is detected PlaceonPillar runs
        ////if no collision is detected
        //if (p == null)
        //{
        //    Debug.Log("No Hit");
        //    PlaceAtPresetLocation(index);
        //}
        //else
        //{
        //    //check if hit pose up is perpendicular to up direction (aka a vertical plane)
        //    //If the hitplane is vertical this value should be close to 0
        //    float normalDir = Mathf.Abs(Vector3.Dot(transform.up, p.normal));

        //    if (normalDir < 0.1)
        //    {
        //        Debug.Log("Hit a vertical plane");

        //        //get the position of plane
        //        Vector3 position = p.transform.position;

        //        //set new position & transformation of tracked image
        //        inters[index].transform.SetPositionAndRotation(new Vector3(position.x, Camera.main.transform.position.y, position.z), p.transform.rotation);
        //    }
        //    else
        //    {
        //        Debug.Log("Did not hit a vertical plane");
        //        PlaceAtPresetLocation(index);
        //    }
        //}

        //Show the interstitial
        inters[index].SetActive(true);

        //Start the countdown timer to show the next button
        StartCoroutine(sc.Timer(timeToNext, ShowNextButton, index));
    }

    //Shows the input index interstial marker
    public void ShowIntersitialMarker(int index)
    {
        //set transform and pos (in front of pillar)
        markers[index].SetActive(true);
    }

    //Hide the input index interstial marker
    public void HideInterstitial(int index)
    {
        inters[index].SetActive(false);
    }

    //Places the interstitial at the preset position set in the unity editor scene (estimate of pillar position)
    void PlaceAtPresetLocation(int index)
    {
        Vector3 position = inters[index].transform.position;
        inters[index].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
    }

    //Updates and shows the next button to the animation events
    void ShowNextButton(int index)
    {
        dateText.text = dates[index];
        nextButtonToAnim.SetActive(true);
    }

}
