using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TimelineController : MonoBehaviour
{
    public GameObject[] dates;
    public GameObject wallPosRef;
    //public GameObject[] animations;
    [SerializeField]
    int curDate = 0;
    
    [SerializeField]
    private ARTrackedImageManager _m_trackedImage;

    public bool trackingImage = false;

    public Quaternion wallRefRot;
    public Vector3 wallRefPos;

    GameObject dateParent;

    void Awake()
    {
        //hide all the dates
        foreach (GameObject d in dates)
        {
            d.SetActive(false);
        }

        //Show first 3 dates
        dates[0].SetActive(true);
        dates[1].SetActive(true);
        dates[2].SetActive(true);

        //hide the dates
        dateParent = dates[0].transform.parent.gameObject;
        dateParent.SetActive(false);

        //wall marker reference for testing
        wallRefPos = wallPosRef.transform.position;
    }

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
                Debug.Log("IMAGE SCANNED");
                SetWallPosition(tracked);
                ShowTimeline();
                trackingImage = false;
                _m_trackedImage.enabled = false;
            }

   
        }

    }

    public void SetWallPosition(ARTrackedImage trackedImage)
    {
        wallRefPos = trackedImage.transform.position;
        wallRefRot = trackedImage.transform.rotation;
        

    }

    public void ShowTimeline()
    {
        //dateParent.transform.position = new Vector3(wallRefPos.x, 0, wallRefPos.z);
        dateParent.transform.rotation = wallRefRot;
        dateParent.transform.Rotate(0,90,0);
        dateParent.SetActive(true);
    }

    //public void TestWallPosition()
    //{

    //}


    //Show the marker at the current index
    public void ShowNextDate()
    {
        if (curDate < dates.Length - 3)
        {
            dates[curDate+3].SetActive(true);
        }


    }

}
