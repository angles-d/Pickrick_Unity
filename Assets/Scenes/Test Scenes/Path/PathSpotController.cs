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

            //For Animation Marjers
            if(gameObject.tag.Equals("Anim Marker"))
            {
                //Detec the first animation marker by name
                if (gameObject.name.Equals("Car Scene Marker"))
                {
                    sc.TurnOffWalkToSign();
                }
                sc.ShowAnimation();
                sc.ac.TurnOnAnimDateUI();
            }

            //For interstitials
            if (gameObject.tag.Equals("Inter Marker"))
            {
                //if its the first interstitial
                print(gameObject.name);

                //Detect the first intersitial marker by name
                if (gameObject.name.Equals("1. July 3"))
                {
                    Debug.Log("TURN OFF");
                    sc.TurnOffWalkToSign();
                }
                sc.ic.TrackPillar();

                
            }
            gameObject.SetActive(false);
        }
       
    }
}
