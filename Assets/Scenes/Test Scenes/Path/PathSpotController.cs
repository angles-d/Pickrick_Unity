using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpotController : MonoBehaviour
{
    public GameObject arAnim;
    private bool animActive = false;

    public InterstitialsPathController ic;
    public SceneController sc;

    // Start is called before the first frame update
    private void Awake()
    {
        sc = transform.parent.parent.parent.GetComponent<SceneController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger: " + other.name);
        if (!animActive && other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("COLLIDE: " + gameObject.name);

            //For Animation Marjers
            if(gameObject.tag.Equals("Anim Marker"))
            {
                //TODO change this from hardcoded
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

                //do not change the name
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
