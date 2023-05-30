using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controller for the Historical Cards
public class CardController : MonoBehaviour
{
    //AR Card object
    [SerializeField] GameObject card;
    //Title text 
    [SerializeField] GameObject title;

    //If the card is shown
    bool cardShown = false;

    TimelineSceneController sc;

    private void Start()
    {
        sc = GameObject.Find("Scene Controller").GetComponent<TimelineSceneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the user collides with the card collider
        if (!cardShown && other.gameObject.CompareTag("MainCamera"))
        {
            //Turns on card
            card.SetActive(true);

            //turn off collider and title text
            gameObject.SetActive(false);
            title.SetActive(false);

            //Increment current date
            sc.IncrementDateCount();

            cardShown = true;
        }

    }
}
