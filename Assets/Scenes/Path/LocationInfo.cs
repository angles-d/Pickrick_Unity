using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LocationInfo: MonoBehaviour
{
    public static LocationInfo Instance;

    private Vector3 floorPos;
    private Vector3 scenePos;
    private Quaternion sceneRot;
    public GameObject arSessionOrigin;

    private Vector3 sessionStartPosition;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        floorPos = new Vector3(0, 0, 0);
 
        DontDestroyOnLoad(gameObject);

        arSessionOrigin = GameObject.Find("AR Session Origin"); 

    }

   

    public void SetFloorPos(GameObject floor)
    {
        floorPos = floor.transform.position;
    }


   
    public Vector3 GetFloorPos()
    {
        return floorPos;
    }

   

    public void SetScenePos(GameObject scene)
    {
        scenePos = scene.transform.position;
        sceneRot = scene.transform.rotation;
    }

    public Quaternion GetSceneRotation()
    {
        return sceneRot;
    }


    public ARPlaneManager GetPlaneManager()
    {
        return arSessionOrigin.GetComponent<ARPlaneManager>();
    }

    public ARRaycastManager GetRaycastManager()
    {
        return arSessionOrigin.GetComponent<ARRaycastManager>();
    }
}
