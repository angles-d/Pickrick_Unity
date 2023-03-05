using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    bool firstPlayed = false;

    //Game objects thate need to be placed on the floor (anim, anim markers, inter marker)
    public GameObject[] toPlace;

    public GameObject[] markers;
    public GameObject[] animations;
   
    //the current anim marker
    [SerializeField]
    int curMarker = 0;

    [SerializeField]
    public GameObject posReference;

    [SerializeField]
    public GameObject path;

    [SerializeField]
    GameObject nextButtonToInter;

    [SerializeField]
    InterstitialsController ic;


    [SerializeField]
    ProjectToPillar ptp; 

    //called regardless if object = enabled
    void Awake()
    {
        //hide all the markers
        foreach (GameObject m in markers)
        {
            m.SetActive(false);
        }

        

    }

    private void Start()
    {

        PositionAR();
        //PositionAR();
        ptp.StartScanning();

    }

    public void ShowNextIntersitial()
    {
        //hide current animation
        animations[curMarker].SetActive(false);
        //show next itnerstitial marker
        ic.ShowNextIntersitial();

        ic.curInter += 1;
        curMarker++;
    }

  

    //Path Marker specific methods
    //Show the marker at the current index
    public void ShowNextAnimMarker()
    {
        //hide the current interstitial
        ic.HideCurrent();

        //turn on next marker
        markers[curMarker].SetActive(true);
        
    }



    //called after the animaiton has been played once
    //through the signal emitter on the timeline
    //Make sure signal only emits once
    public void ShowNextInterButton()
    {
        nextButtonToInter.SetActive(true);
        firstPlayed = true;
        
    }

    //position the path 
    public void PositionAR()
    {
        //move to place to ground
        Debug.Log("Path AR Positioned");
        MoveToGround();

        Debug.Log("Door: " + LocationInfo.Instance.GetDoorPosition());
        transform.position = LocationInfo.Instance.GetDoorPosition() - posReference.transform.position;
        Debug.Log("Scene:" + transform.position);

        path.SetActive(true);
       

    }


    public void MoveToGround()
    {
        Debug.Log("Move to Ground");

        float groundHeight = LocationInfo.Instance.GetFloorPos().y;
        foreach (GameObject g in toPlace)
        {
            //set ar floor
            if (g.name.Equals("AR Floor"))
            {
                Vector3 pos = g.transform.position;
                Renderer r = g.GetComponent<Renderer>();
                g.transform.position = new Vector3(pos.x, groundHeight + r.bounds.extents.y, pos.z);
                continue;
            }

            //for all children in each gameobject
            foreach (Transform c in g.transform)
            {
                Vector3 pos = c.position;
                Renderer r = c.GetComponent<Renderer>();
                if (r != null)
                {
                    c.position = new Vector3(pos.x, groundHeight + r.bounds.extents.y, pos.z);
                }
                else
                {
                    c.position = new Vector3(pos.x, groundHeight + pos.y, pos.z);
                }
            }

        }
    }



}
