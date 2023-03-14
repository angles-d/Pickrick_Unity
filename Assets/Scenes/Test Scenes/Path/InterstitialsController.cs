using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System;

public class InterstitialsController : MonoBehaviour
{
    
    public int curInter = 0;
    //bool scanning = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    ARRaycastManager m_raycastManager;


    public bool trackingImage = false;

    //interstitial gameobject prefabs
    public GameObject[] inters;
    //interstitial pin markers
    public GameObject[] markers;

    //Variables for the timer
    float timer = 0;
    public float timeToNext;
    public bool timerOn = false;

    //button to show next animation
    public GameObject nextButtonToAnim;
    public GameObject WalkToSign;

    //array of dates to put on the button for next anim
    string[] dates = {"July 3rd, 1964", "Aug 11th, 1964", "Sep 26th, 1964" , "Jan 29th, 1965", "Feb 22th, 1965" };
    public TextMeshProUGUI dateText;

    private void Start()
    {
        dateText = nextButtonToAnim.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }


    

    private void Update()
    {
        if (timerOn)
        {
            InterstitialTimer();
        }
    }


    public void TrackPillar()
    {
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

        //scanning = true;

        StartCoroutine(Timer(0.8f, CheckPillarRaycast));

    }


    IEnumerator Timer(float t, Action DoneWait) { 
    
        print("WaitAndPrint " + Time.time);
        yield return new WaitForSeconds(t);
        print("WaitAndPrint Done " + Time.time);
        DoneWait?.Invoke();

    }


    public void CheckPillarRaycast()
    {
        Debug.Log("Wall Raycast");
        //DetectPillar();
        CheckRaycast(DetectPillar);
    }


    public bool CheckRaycast(Action<List<ARRaycastHit>> afterHit)
    {
        Ray fromCamera = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        const TrackableType trackableTypes = TrackableType.Planes;


        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_raycastManager.Raycast(fromCamera, hits, trackableTypes))
        {
            afterHit?.Invoke(hits);
            return true;
        }
        //if there's no hit detected
        else
        {
            Debug.Log("No Hit");
            Vector3 position = inters[curInter].transform.position;
            inters[curInter].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            inters[curInter].SetActive(true);

            Debug.Log(inters[curInter].name);

            //Start the countdown timer
            timerOn = true;
        }

        return false;
    }


    public void DetectPillar(List<ARRaycastHit> hits)
    {
        Debug.Log("Detect Pillar");
        //scanning = false;
        m_planeManager.enabled = false;
        ARRaycastHit hit = hits[0];
        ARPlane hitPlane = null;

        Debug.Log("hit" + hit+ ", "+hit.trackableId);
        foreach (ARPlane p in m_planeManager.trackables)
        {

            //set a distance check??
            Debug.Log(p.normal + " " + p.isActiveAndEnabled);
            Debug.Log("g active" + " " + p.gameObject.activeSelf);
            //Check if the current plane is active
            if (p.isActiveAndEnabled && p.trackableId == hit.trackableId)
            {
                Debug.Log("This plane" + p.normal);
                hitPlane = p;
            }
            p.gameObject.SetActive(false);

        }

        if (hitPlane == null)
        {
            Debug.Log("HIT NO PLANE");
            Vector3 position = inters[curInter].transform.position;
            inters[curInter].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
        }
        else
        {
            //check if hit pose up is perpendiculat to up direction (aka a vertical plane)
            float normalDir = Mathf.Abs(Vector3.Dot(transform.up, hitPlane.normal));

            Debug.Log("Hit plane " + hitPlane);
            Debug.Log("norm" + hitPlane.normal);
            Debug.Log("Normal compare:" + normalDir);


            if (normalDir < 0.1)
            {
                Debug.Log("Plane active");

                //get the position of plane
                Vector3 position = hitPlane.transform.position;
                //get the position of plane
                Quaternion rotation = hitPlane.transform.rotation;

                //set new position & transformation of tracked image
                inters[curInter].transform.rotation = Quaternion.identity;
                inters[curInter].transform.rotation = rotation;
                inters[curInter].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            }
            else
            {
                Debug.Log("HIT NO PLANE");
                Vector3 position = inters[curInter].transform.position;
                inters[curInter].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            }
        }

        inters[curInter].SetActive(true);

        Debug.Log(inters[curInter].name);

        //Start the countdown timer
        timerOn = true;
        
    }


    void InterstitialTimer()
    {
        if (timer < timeToNext)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("timerDone");
            timer = 0;
            timerOn = false;
            dateText.text = dates[curInter];
            nextButtonToAnim.SetActive(true);


        }

    }

    //for testing
    public void StartTimer()
    {
        timerOn = true;

    }

    public void ShowNextIntersitial()
    {
        if (curInter < markers.Length - 1) {
            curInter += 1;

            Debug.Log("Show Next");
            //set transform and pos (in front of pillar)
            markers[curInter].SetActive(true);
            Debug.Log("prev inter:" + curInter);
        }
    }

    public void HideCurrent()
    {
        Debug.Log("Hide Current");
        trackingImage = false;
        inters[curInter].SetActive(false);

        //update the next pillar info
        //NextPillar();
        

    }

    public void TurnOffWalkToSign()
    {
        WalkToSign.SetActive(false);
    }

    public void TurnOnWalkToSign()
    {
        WalkToSign.SetActive(true);
    }



    //Tracked image manager stuff


    //[SerializeField]
    //private ARTrackedImageManager _m_trackedImage;
    //[SerializeField]
    //int nextPillar = 1;
    //int pillarDir = 1; //1 = pillar # going up , -1 = pillar # going down

    //public void OnEnable()
    //{
    //    _m_trackedImage.trackedImagesChanged += onImageChanged;
    //}

    //public void OnDisable()
    //{
    //    _m_trackedImage.trackedImagesChanged -= onImageChanged;
    //}

    //public void onImageChanged(ARTrackedImagesChangedEventArgs _m_trackedImagegs)
    //{
    //    if (trackingImage)
    //    {
    //        //If an image m_m_trackedImageker is recognized
    //        foreach (ARTrackedImage tracked in _m_trackedImagegs.added)
    //        {
    //            UpdateIntersitials(tracked);
    //        }

    //        foreach (ARTrackedImage tracked in _m_trackedImagegs.updated)
    //        {
    //            UpdateIntersitials(tracked);
    //        }

    //        foreach (ARTrackedImage tracked in _m_trackedImagegs.removed)
    //        {
    //            //spawnedPrefabs[tracked.name].SetActive(false);
    //        }
    //    }

    //}

    //path order: pillar1, pillar2, pillar3, pillar2, pillar1

    //Adds object to the same position as m_m_trackedImageker
    //private void UpdateIntersitials(ARTrackedImage trackedImage)
    //{
    //    //get the name of the physical m_m_trackedImageker
    //    string name = trackedImage.referenceImage.name;
    //    //names of the pillar reference images should follow the format "Pillar#"
    //    if (name == "Pillar" + nextPillar)
    //    {
    //        //Since image has been tracked update the current active interstitial

    //        //curInter += 1;
    //        //get the position of m_m_trackedImageker image
    //        Vector3 position = trackedImage.transform.position;
    //        //get rotation of m_m_trackedImageker image
    //        Quaternion rotation = trackedImage.transform.rotation;
    //        //set new position & transformation of tracked image
    //        inters[curInter].transform.position = position;
    //        inters[curInter].transform.rotation = rotation;
    //        //flip it upside down

    //        inters[curInter].SetActive(true);

    //        //turn off tracking
    //        Debug.Log(inters[curInter].name);
    //        //turn off tracking
    //        trackingImage = false;

    //        //Start the countdown timer
    //        timerOn = true;


    //    }


    //}

    //public void StartTracking()
    //{
    //    trackingImage = true;
    //}

    //for 6 markers
    //private void NextPillar()
    //{
    //    nextPillar += 1;

    //}

    //For 3 markers
    //private void NextPillar()
    //{
    //    if (nextPillar == 3)
    //    {
    //        pillarDir = -1;
    //    }
    //    else if (nextPillar == 0)
    //    {
    //        pillarDir = 1;
    //    }

    //    nextPillar += pillarDir;

    //}


}