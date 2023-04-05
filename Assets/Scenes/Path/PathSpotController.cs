using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpotController : MonoBehaviour
{
    public SceneController sc;

    // Start is called before the first frame update
    private void Awake()
    {
        //get a reference to the scene controller
        //Finds the scene object by name
        sc = GameObject.Find("Scene").GetComponent<SceneController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger: " + other.name);
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("COLLIDE: " + gameObject.name);

            //check if it's the first marker
            if (sc.GetCurrentMarkerIndex() == 0)
            {
                sc.TurnOffWalkToSign();
            }

            //For Animation Marjers
            if (gameObject.tag.Equals("Anim Marker"))
            {
                sc.ShowAnimation();
                //TODO FIX THIS
                sc.ac.TurnOnAnimDateUI(sc.GetCurrentMarkerIndex());
            }

            //For interstitials
            if (gameObject.tag.Equals("Inter Marker"))
            {
                sc.ic.TrackPillar();
            }
            gameObject.SetActive(false);
        }
       
    }
}
