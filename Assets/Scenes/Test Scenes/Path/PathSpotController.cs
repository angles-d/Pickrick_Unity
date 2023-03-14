using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpotController : MonoBehaviour
{
    public GameObject arAnim;
    private bool animActive = false;

    InterstitialsController ic;
    public SceneController sc;

    // Start is called before the first frame update
    private void Awake()
    {
        if (tag.Equals("Inter Marker"))
        {
            ic = transform.parent.GetComponent<InterstitialsController>();
        }

        //FIX LATER --> move to UI controller
        sc = transform.parent.parent.parent.GetComponent<SceneController>();
    }
  

    // Update is called once per frame
    void Update()
    {
        
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
                    Debug.Log("TURN OFF");

                    //TODO fix this nonsense; create UI controller??
                    sc.ic.TurnOffWalkToSign();
                }
                arAnim.SetActive(true);
                sc.TurnOnAnimDateUI();
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
                    ic.TurnOffWalkToSign();
                }
                ic.TrackPillar();

                
            }
            gameObject.SetActive(false);
        }
       
    }
}
