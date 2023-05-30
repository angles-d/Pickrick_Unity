using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controller for user collision with AR Path Markers
public class PathMarkerController : MonoBehaviour
{
    PathSceneController sc;

    private void Awake()
    {
        //get a reference to the scene controller
        sc = GameObject.Find("Scene Controller").GetComponent<PathSceneController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //If the user colliders w/ the pin marker
        if (other.gameObject.CompareTag("MainCamera"))
        {
            //check if it's the first marker
            if (sc.GetCurrentMarkerIndex() == 0)
            {
                //Turn off the "walk to the red marker" UI popup
                sc.HideWalkToSign();
            }

            //If the user collided with an animation marker
            if (gameObject.tag.Equals("Anim Marker"))
            {
                sc.ShowAnimation();
                sc.TurnOnAnimDateUI();
            }

            //If the user collided with an interstitial marker
            if (gameObject.tag.Equals("Inter Marker"))
            {
                sc.ShowInterstitial();
            }

            //Hide the marker after collision
            gameObject.SetActive(false);
        }
       
    }
}
