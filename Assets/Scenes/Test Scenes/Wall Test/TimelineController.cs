using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TimelineController : MonoBehaviour
{
    public GameObject[] dates;
    public GameObject[] doors =  new GameObject[3];

    [SerializeField]
    int curDate = 0;


    GameObject timeline;

    //variable related to opening door
    bool doorOpen = false;
    float timer = 0;
    public float timeToOpenDoor = 2;

    void Awake()
    {
        //hide all the dates
        foreach (GameObject d in dates)
        {
            d.SetActive(false);
        }

        //Show first 3 dates
        dates[0].SetActive(true);
        dates[1].SetActive(true);
        dates[2].SetActive(true);

        //hide the dates
        timeline = dates[0].transform.parent.gameObject;
 
    }


    private void Update()
    {
        if (!doorOpen && curDate == dates.Length)
        {
            if (timer == 0)
            {
                Debug.Log("Door timer started");
            }
           
            if(timer < timeToOpenDoor){
                timer += Time.deltaTime;
            } else
            {
                Debug.Log("Doors open");
                OpenDoor();
                doorOpen = true;
            }
        }
    }

    
    public void OpenDoor()
    {
        StartCoroutine(OpenDoorCo());
    }

    IEnumerator OpenDoorCo()
    {
        //Open doors

        Vector3 offset = new Vector3(0,0,-1);
        float duration = 4.0f;
        float time = 0.0f;

        Vector3[] ogPos = {doors[0].transform.position,
                           doors[1].transform.position,
                           doors[2].transform.position};
        

        while (time <= duration)
        {
            time  = time + Time.deltaTime;
            float percent = Mathf.Clamp01(time / duration);
      
            doors[0].transform.position = Vector3.Lerp(ogPos[0], ogPos[0] + offset, percent);
            doors[1].transform.position = Vector3.Lerp(ogPos[1], ogPos[1] + offset, percent);
            doors[2].transform.position = Vector3.Lerp(ogPos[2], ogPos[2] + offset, percent);

            yield return null;
        }
      
    }


    //Show the marker at the current index
    public void ShowNextDate()
    {
        if (curDate < dates.Length - 3)
        {
            dates[curDate+3].SetActive(true);
        }

        curDate += 1;


    }


   

}
