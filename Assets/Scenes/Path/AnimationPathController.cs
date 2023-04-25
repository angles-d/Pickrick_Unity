using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimationPathController : MonoBehaviour
{
    //physcial game objects
    public GameObject[] markers; //path markers
    public GameObject[] animations; //animations to show
    public GameObject restaurant;

    //UI elements
    [SerializeField]
    GameObject nextButtonToInter;

    public GameObject animHeader;
    //text & dates
    public GameObject[] animDates;
  

    //dates for the interstital button
    string[] dates = { "Jul 3rd, 1964", "Jul 9th, 1964", "Jul 22nd, 1965", "Fall, 1964", "February, 1965" };
    public TextMeshProUGUI dateText;

    //called regardless if object = enabled
    void Awake()
    {
        //hide all the markers
        foreach (GameObject m in markers)
        {
            m.SetActive(false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        dateText = nextButtonToInter.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();



    }


    //TODO create a children array automatically??
    public void TurnOnAnimDateUI(int index)
    {
        animHeader.SetActive(true);
        animDates[index].SetActive(true);
    }

    //called by to Interstitial button
    public void TurnOffAnimDateUI(int index)
    {
        animHeader.SetActive(false);
        animDates[index].SetActive(false);
    }



    //called after the animation has been played once
    //through the signal emitter on the timeline
    //Make sure signal only emits once
    public void ShowNextInterButton(int index)
    {
        if (index + 1 < dates.Length)
        {
            dateText.text = dates[index + 1];
            nextButtonToInter.SetActive(true);
        }

    }

    //turns on the marker at index
    public void ShowAnimMarker(int index)
    {
        markers[index].SetActive(true);
    }

    //hides the marker at index
    public void HideAnimMarker(int index)
    {
        markers[index].SetActive(false);
    }

    //public show animation
    public void ShowAnimation(int index)
    {
        animations[index].SetActive(true);
        restaurant.SetActive(true);

    }

    //public show animation
    public void HideAnimation(int index)
    {
        animations[index].SetActive(false);
        restaurant.SetActive(false);

    }


}
