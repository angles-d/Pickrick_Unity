using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controller for the Animation Markers and Animations
public class AnimationPathController : MonoBehaviour
{
    //game objects
    [SerializeField] GameObject[] markers; //path markers
    [SerializeField] GameObject[] animations; //animations to show
    [SerializeField] GameObject restaurant;

    //UI elements
    [Header("UI Elements")]
    [SerializeField] GameObject nextButtonToInter;
    [SerializeField] GameObject animHeader;
    [SerializeField] GameObject[] animDates; //text & dates


    //dates for the interstital button
    string[] dates = { "Jul 3rd, 1964", "Jul 9th, 1964", "Jul 22nd, 1965", "Fall, 1964", "February, 1965" };
    TextMeshProUGUI dateText;

    //called regardless if object = enabled
    void Awake()
    {
        //hide all the markers
        foreach (GameObject m in markers)
        {
            m.SetActive(false);
        }
    }
     
    void Start()
    {
        dateText = nextButtonToInter.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    //Sets the Newspaper UI Overlay
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

    //turns on the input index marker
    public void ShowAnimMarker(int index)
    {
        markers[index].SetActive(true);
    }

    //hides the input index marker
    public void HideAnimMarker(int index)
    {
        markers[index].SetActive(false);
    }

    //shows the input index animation 
    public void ShowAnimation(int index)
    {
        animations[index].SetActive(true);
        restaurant.SetActive(true);
    }

    //hides input index animation
    public void HideAnimation(int index)
    {
        animations[index].SetActive(false);
        restaurant.SetActive(false);
    }


}
