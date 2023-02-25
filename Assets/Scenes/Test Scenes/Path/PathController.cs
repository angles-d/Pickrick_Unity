using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] markers;
    public GameObject[] animations;
    [SerializeField]
    int curMarker = 0;
    [SerializeField]
    GameObject nextButton;
    bool firstPlayed = false;
    [SerializeField]
    InterstitialsController ic;

    //public GameObject nextMarker;


    void Awake()
    {
        //hide all the markers
        foreach (GameObject m in markers)
        {
            m.SetActive(false);
        }

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
    public void ShowNextMarker()
    {
        //hide the current interstitial
        ic.HideCurrent();

       
        
        //turn on next marker
        markers[curMarker].SetActive(true);
        
    }



    //called after the animaiton has been played once
    //through the signal emitter on the timeline
    //Make sure signal only emits once
    public void MoveToNext()
    {
        nextButton.SetActive(true);
        firstPlayed = true;
        
    }
}
