using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfo: MonoBehaviour
{
    public static LocationInfo Instance;

    private Vector3 doorPos;
    private Quaternion doorRot;

    private Vector3 floorPos;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        doorPos = new Vector3(0, 0, 0);
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

    public void SetDoorPos(GameObject door)
    {
        doorPos = door.transform.position;
        doorRot = door.transform.rotation;
    }

    public Quaternion GetDoorRotation()
    {
        return doorRot;
    }

    public Vector3 GetDoorPosition()
    {
        return doorPos;
    }

    public void PrintDoorPos()
    {
        Debug.Log("Pos:"+ doorPos);
        Debug.Log("Rot:" + doorRot);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
