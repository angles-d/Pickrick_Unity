using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    public GameObject[] dates;
    //public GameObject[] animations;
    [SerializeField]
    int curDate = 0;
    [SerializeField]

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

    }


    //Show the marker at the current index
    public void ShowNextDate()
    {
        if (curDate < dates.Length - 3)
        {
            dates[curDate+3].SetActive(true);
        }


    }

}
