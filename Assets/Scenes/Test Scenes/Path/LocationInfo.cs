using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfo: MonoBehaviour
{
    public static LocationInfo Instance;

    private Vector3 doorPos;
    private Quaternion doorRot;

    private Vector3 floorPos;
    private Vector3 scenePos;
    private Quaternion sceneRot;

    [SerializeField]
    private GameObject pillarPositionRef;

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

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Pillar Position");
        pillarPositionRef = Instantiate(temp[0], temp[0].transform.position, temp[0].transform.rotation);
        pillarPositionRef.SetActive(false);
        DontDestroyOnLoad(pillarPositionRef);
        DontDestroyOnLoad(gameObject);


    }

   

    public void SetFloorPos(GameObject floor)
    {
        floorPos = floor.transform.position;
    }


    public void SetPillarLocation(GameObject p)
    {
        pillarPositionRef.transform.rotation = p.transform.rotation;
        pillarPositionRef.transform.position = p.transform.position;
    }

    public void SetPillarPosition(Vector3 pos)
    {
        pillarPositionRef.transform.position = pos;
    }

    public void SetPillarRotation(Quaternion rot)
    {
        pillarPositionRef.transform.rotation = rot;
    }

    public GameObject GetPillarPosRef()
    {
        return pillarPositionRef;
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

    public void SetScenePos(GameObject scene)
    {
        scenePos = scene.transform.position;
        sceneRot = scene.transform.rotation;
    }

    public Quaternion GetSceneRotation()
    {
        return sceneRot;
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
