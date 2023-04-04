using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System;

public class InterstitialsPathController : MonoBehaviour
{
    
    public int curInterIndex = 0;


    [SerializeField]
    ARPlaneManager m_planeManager;
    
    [SerializeField]
    ARRaycastManager m_raycastManager;


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

    //for testing
    public void StartTimer()
    {
        timerOn = true;

    }

    public void ShowNextIntersitial()
    {
        if (curInterIndex < markers.Length - 1)
        {
            curInterIndex += 1;

            Debug.Log("Show Next");
            //set transform and pos (in front of pillar)
            markers[curInterIndex].SetActive(true);
            Debug.Log("prev inter:" + curInterIndex);
        }
    }

    public void HideCurrent()
    {
        Debug.Log("Hide Current");
        inters[curInterIndex].SetActive(false);

    }


    public void TrackPillar()
    {
        m_planeManager.enabled = true;
        m_planeManager.requestedDetectionMode = PlaneDetectionMode.Vertical;

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
            Vector3 position = inters[curInterIndex].transform.position;
            inters[curInterIndex].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            inters[curInterIndex].SetActive(true);

            Debug.Log(inters[curInterIndex].name);

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
            Vector3 position = inters[curInterIndex].transform.position;
            inters[curInterIndex].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
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
                inters[curInterIndex].transform.rotation = Quaternion.identity;
                inters[curInterIndex].transform.rotation = rotation;
                inters[curInterIndex].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            }
            else
            {
                Debug.Log("HIT NO PLANE");
                Vector3 position = inters[curInterIndex].transform.position;
                inters[curInterIndex].transform.position = new Vector3(position.x, Camera.main.transform.position.y, position.z);
            }
        }

        inters[curInterIndex].SetActive(true);

        Debug.Log(inters[curInterIndex].name);

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
            dateText.text = dates[curInterIndex];
            nextButtonToAnim.SetActive(true);


        }

    }

}