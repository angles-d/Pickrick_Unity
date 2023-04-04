using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateController : MonoBehaviour
{
    public GameObject card;
    public GameObject title;
    private bool cardActive = false;
    [SerializeField]
    TimelineController tc;
    //[Tooltip("Offset from scanned marker image to first card")]
    public float posOffset;


    // Start is called before the first frame update
    void Awake()
    {
        tc = GameObject.Find("Timeline Controller").GetComponent<TimelineController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger: " + other.name);
        if (!cardActive && other.gameObject.CompareTag("MainCamera"))
        {
            //Turn on card
            card.SetActive(true);
            cardActive = true;
           
            tc.ShowNextDate();

            //turn off collider and title text
            gameObject.SetActive(false);
            title.SetActive(false);
        }

    }
}
