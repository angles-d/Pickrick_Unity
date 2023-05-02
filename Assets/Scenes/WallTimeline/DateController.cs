using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateController : MonoBehaviour
{
    [SerializeField] GameObject card;
    [SerializeField] GameObject title;

    bool cardShown = false;
    TimelineSceneController sc;

    private void Start()
    {
        sc = GameObject.Find("Scene Controller").GetComponent<TimelineSceneController>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!cardShown && other.gameObject.CompareTag("MainCamera"))
        {
            //Turn on card
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
