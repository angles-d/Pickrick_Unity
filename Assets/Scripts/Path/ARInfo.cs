using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//Stores AR info that needs to remain between scenes
[DisallowMultipleComponent]
public class ARInfo: MonoBehaviour
{
    GameObject arSessionOrigin;

    //Singleton Instance
    public static ARInfo Instance;
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
        arSessionOrigin = GameObject.Find("AR Session Origin");
        //Set default floor position
        floorPos = new Vector3(0, 0, 0);
        DontDestroyOnLoad(this.gameObject);
    }

    //Set the user's floor position
    public void SetFloorPosition(GameObject floor)
    {
        floorPos = floor.transform.position;
    }

    //Get the user's floor position
    public Vector3 GetFloorPosition()
    {
        return floorPos;
    }

    //Gets the AR plane manager
    //ensures that the plane manager is the same between scenes
    public ARPlaneManager GetPlaneManager()
    {
        return arSessionOrigin.GetComponent<ARPlaneManager>();
    }

    //Gets the AR plane manager
    //ensures that the  raycast manager is the same between scenes
    public ARRaycastManager GetRaycastManager()
    {
        return arSessionOrigin.GetComponent<ARRaycastManager>();
    }
}
