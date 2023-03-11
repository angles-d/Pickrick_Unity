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



    public GameObject pillarRef;

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
        ptp.StartScanning();
        MoveToGround();

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
        Debug.Log("pillar Pos" + pillarRef.transform.position);

        transform.rotation = pillarRef.transform.rotation;
        transform.Rotate(0, 90, 0);

        Vector3 pillarPos = new Vector3(pillarRef.transform.position.x, 0, pillarRef.transform.position.z);
        transform.position = pillarPos;
        //Debug.Log("Ref Pos:" + posReference.transform.position);

        //Vector3 noY = pillarPos - posReference.transform.localPosition;
        //transform.Translate(noY.x,0, noY.z,Space.World);
        //Debug.Log("Scene:" + transform.position);
        //Debug.Log("Ref Pos" + posReference.transform.position);

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
                g.transform.position = new Vector3(pos.x, groundHeight, pos.z);
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
