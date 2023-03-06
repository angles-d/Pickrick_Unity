using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfo: MonoBehaviour
{
    public static LocationInfo Instance;

    private Vector3 floorPos;
    private Vector3 scenePos;
    private Quaternion sceneRot;


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

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
