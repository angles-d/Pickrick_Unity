using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateCollision : MonoBehaviour
{
    public GameObject card;
    private bool animActive = false;
    [SerializeField]
    TimelineController tc;


    // Start is called before the first frame update
    void Awake()
    {
       
        tc = GameObject.Find("Timeline").GetComponent<TimelineController>();
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
            Debug.Log("COLLIDE");
            card.SetActive(true);
            animActive = true;
            gameObject.SetActive(false);
            tc.ShowNextDate();
        }

    }
}
