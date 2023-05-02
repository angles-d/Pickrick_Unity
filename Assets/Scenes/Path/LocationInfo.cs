using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[DisallowMultipleComponent]
public class LocationInfo: MonoBehaviour
{
    [SerializeField] GameObject arSessionOrigin;
    //Singleton Instance
    public static LocationInfo Instance;
    //Floor position (used to get floor height)
    private Vector3 floorPos;
  
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        floorPos = new Vector3(0, 0, 0);
        arSessionOrigin = GameObject.Find("AR Session Origin"); 
    }


    public void SetFloorPosition(GameObject floor)
    {
        floorPos = floor.transform.position;
    }

    public Vector3 GetFloorPosition()
    {
        return floorPos;
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
