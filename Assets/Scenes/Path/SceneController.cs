using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{
    //Game objects thate need to be placed on the floor (anim, anim markers, inter marker)
    public GameObject[] toPlace;

    //the current anim marker
    [SerializeField]
    int curMarkerIndex = 0;

    [SerializeField]
    public GameObject posReference;

    [SerializeField]
    public GameObject path;

    //marker controllers
    public InterstitialsPathController ic;
    public AnimationPathController ac;


    [SerializeField]
    ProjectToPillar ptp;

    public GameObject pillarRef;

    //UI elements
    public GameObject WalkToSign;

    public GameObject EndSceneUIGooger;
   

    private void Start()
    {
      

        ptp.StartScanning();
        MoveToGround();

    }



    public void ShowNextIntersitial()
    {
        //hide current animation
        ac.HideAnimMarker(curMarkerIndex);
        ac.HideAnimation(curMarkerIndex);
        ac.TurnOffAnimDateUI(curMarkerIndex);

        //show next itnerstitial marker
        ic.ShowNextIntersitial();
        curMarkerIndex++;
    }



    //Path Marker specific methods
    //Show the marker at the current index
    public void ShowNextAnimMarker()
    {
        //hide the current interstitial
        ic.HideCurrent();

        //turn on next marker
        ac.ShowAnimMarker(curMarkerIndex);

        if (curMarkerIndex == 0)
        {
            TurnOnWalkToSign();
        }
    }

    //called by the ar animation event emitter
    //component attached directly to the timeline gameobjects
    public void ShowNextInterButton()
    {
        ac.ShowNextInterButton(ic.curInterIndex);
    }

    //position the scene to be in the right location
    //based on scanned pillar position
    public void PositionAR()
    {
        Debug.Log("Path AR Positioned");
        Debug.Log("pillar Pos" + pillarRef.transform.position);

        transform.rotation = pillarRef.transform.rotation;
        transform.Rotate(0, 90, 0);

        Vector3 pillarPos = new Vector3(pillarRef.transform.position.x, 0, pillarRef.transform.position.z);
        transform.position = pillarPos;
        path.SetActive(true);
    }

    //Moves any objects in toPlace array to the floor
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

    public void TurnOffWalkToSign()
    {
        WalkToSign.SetActive(false);
    }

    public void TurnOnWalkToSign()
    {
        WalkToSign.SetActive(true);
    }

    public void ShowAnimation()
    {
        ac.ShowAnimation(curMarkerIndex);
    }

    public void ShowEndSceneGooger()
    {
        EndSceneUIGooger.SetActive(true);
    }

    public int GetCurrentMarkerIndex()
    {
        return curMarkerIndex;
    }

    
}
