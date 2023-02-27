using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateController : MonoBehaviour
{
    public GameObject card;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger: " + other.name);
        if (!cardActive && other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("COLLIDE");
            //Game object has been rotated 90 degrees
            //front = positive x
            //Better to work in local coordinates???
            card.transform.position = new Vector3(tc.wallRefPos.x + card.transform.position.x + posOffset, 0, tc.wallRefPos.z);
            card.SetActive(true);
            cardActive = true;
            gameObject.SetActive(false);
            tc.ShowNextDate();
        }

    }
}
