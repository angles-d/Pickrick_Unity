using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class SpawnAR : MonoBehaviour
{
    public GameObject scene;
    public GameObject path;
    public GameObject posReference;

    public GameObject dummyPosRef;
    [SerializeField]
    private ARTrackedImageManager _m_trackedImage;

    public bool trackingImage = false;


    //Runs when scene is loaded
    public void Awake()
    {

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
         //If an image m_m_trackedImageker is recognized
        foreach (ARTrackedImage tracked in _m_trackedImagegs.added)
        {
            PositionAR(tracked);
        }

        

    }
 

    public void PositionAR(ARTrackedImage trackedImage)
    {
        Vector3 pos = trackedImage.transform.position;
        Debug.Log("MArker:" + pos);
        scene.transform.position = pos - posReference.transform.position;
        Debug.Log("Scene:" + scene.transform.position);
        //scene.transform.rotation = trackedImage.transform.rotation;

        path.SetActive(true);
        Debug.Log("SPAWNED");

        this.enabled = false;
        print("Positioned");
    }


    public void TestSpawn()
    {
        //Vector3 pos = trackedImage.transform.position;
        scene.transform.position = dummyPosRef.transform.position - posReference.transform.position;
        Debug.Log("Scene:" + scene.transform.position);
        //scene.transform.rotation = trackedImage.transform.rotation;

        path.SetActive(true);
        Debug.Log("SPAWNED");

        this.enabled = false;
        print("Positioned");
    }
    

    
}
