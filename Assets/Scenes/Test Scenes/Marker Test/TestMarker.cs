using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMarker : MonoBehaviour
{
    public GameObject[] toPlace;
    public GameObject arFloor;
    public GameObject markerHolder;

    // Start is called before the first frame update
    void Start()
    {
        markerHolder.transform.position = Camera.main.transform.position;

        arFloor.transform.position = new Vector3(0, LocationInfo.Instance.GetFloorPos().y, 0);
        MoveToGround();

       



    }

    public void MoveToGround()
    {
        float groundHeight = arFloor.transform.position.y;
        foreach (GameObject c in toPlace)
        {
            Vector3 pos = c.transform.position;
            Renderer r = c.GetComponent<Renderer>();
            if (r != null)
            {
                c.transform.position = new Vector3(pos.x, groundHeight + r.bounds.extents.y, pos.z);
            }
            else
            {
                c.transform.position = new Vector3(pos.x, groundHeight + pos.y, pos.z);
            }

            



        }

    }
}