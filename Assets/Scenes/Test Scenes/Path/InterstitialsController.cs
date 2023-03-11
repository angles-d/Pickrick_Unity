using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class InterstitialsController : MonoBehaviour
{
    
    public int curInter = 0;

    [SerializeField]
    int nextPillar = 1;
    int pillarDir = 1; //1 = pillar # going up , -1 = pillar # going down

    [SerializeField]
    private ARTrackedImageManager _m_trackedImage;

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


    public void OnEnable()
    {
        _m_trackedImage.trackedImagesChanged += onImageChanged;
    }

    public void OnDisable()
    {
        _m_trackedImage.trackedImagesChanged -= onImageChanged;
    }

    public void onImageChanged(ARTrackedImagesChangedEventArgs _m_trackedImagegs)
    {
        if (trackingImage)
        {
            //If an image m_m_trackedImageker is recognized
            foreach (ARTrackedImage tracked in _m_trackedImagegs.added)
            {
                UpdateIntersitials(tracked);
            }

            foreach (ARTrackedImage tracked in _m_trackedImagegs.updated)
            {
                UpdateIntersitials(tracked);
            }

            foreach (ARTrackedImage tracked in _m_trackedImagegs.removed)
            {
                //spawnedPrefabs[tracked.name].SetActive(false);
            }
        }

    }

    private void Update()
    {
        if (timerOn)
        {
            InterstitialTimer();
        }
    }


    //path order: pillar1, pillar2, pillar3, pillar2, pillar1

    //Adds object to the same position as m_m_trackedImageker
    private void UpdateIntersitials(ARTrackedImage trackedImage)
    {
        //get the name of the physical m_m_trackedImageker
        string name = trackedImage.referenceImage.name;
        //names of the pillar reference images should follow the format "Pillar#"
        if (name == "Pillar" + nextPillar)
        {
            //Since image has been tracked update the current active interstitial
                
            //curInter += 1;
            //get the position of m_m_trackedImageker image
            Vector3 position = trackedImage.transform.position;
            //get rotation of m_m_trackedImageker image
            Quaternion rotation = trackedImage.transform.rotation;
            //set new position & transformation of tracked image
            inters[curInter].transform.position = position;
            inters[curInter].transform.rotation = rotation;
            inters[curInter].SetActive(true);

            //turn off tracking
            Debug.Log(inters[curInter].name);
            //turn off tracking
            trackingImage = false;

            //Start the countdown timer
            timerOn = true;


        }

        
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
            Debug.Log("Show Next");
            //set transform and pos (in front of pillar)
            markers[curInter + 1].SetActive(true);
            //trackingImage = true;
            print("marker active: Pillar" + nextPillar);
            Debug.Log("prev inter:" + curInter);
        }
    }

    public void HideCurrent()
    {
        Debug.Log("Hide Current");
        trackingImage = false;
        inters[curInter].SetActive(false);

        //update the next pillar info
        NextPillar();
        Debug.Log("the next pillar is:" + nextPillar);

    }


    //for 6 markers
    //private void NextPillar()
    //{
    //    nextPillar += 1;

    //}

    //For 3 markers
    private void NextPillar()
    {
        if (nextPillar == 3)
        {
            pillarDir = -1;
        }
        else if (nextPillar == 0)
        {
            pillarDir = 1;
        }

        nextPillar += pillarDir;

    }

    public void StartTracking()
    {
        trackingImage = true;
    }
}