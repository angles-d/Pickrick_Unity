using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;


//applying changes for Spring 2024 version, removing AR for intersitials and changing content to 2D UI on screens

//Controller for the AR Interstitials panels on the pillars
public class InterstitialsPathController : MonoBehaviour
{
    [SerializeField] PathSceneController sc;

    //interstitial gameobject prefabs
    [SerializeField] GameObject[] inters;
    //interstitial pin markers
    [SerializeField] GameObject[] markers;
    //Variables for the timer
    [SerializeField] float timeToNext;

    [Header("UI Elements")]
    //button to show next animation
    public GameObject nextButtonToAnim;
    //button to show next interstitial
    public GameObject nextButtonToInter;
  
    //array of dates to put on the next button to the animations
    string[] dates = {"July 3rd, 1964", "Aug 11th, 1964", "Sep 26th, 1964" , "Jan 29th, 1965", "Feb 22th, 1965" };
    string extraInterDate = "Jul 22nd, 1964";
    TextMeshProUGUI dateText;
    TextMeshProUGUI interDateText;
    
    private void Start()
    {
        //Get the text compenent in the "NEW EVENT" button to update the date
        dateText = nextButtonToAnim.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        interDateText = nextButtonToInter.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    //Starts the set up to detect the pillar
    //Spring 2024 implementation
    public void TrackPillar(int index)
    {
        inters[index].SetActive(true);

        //Start the countdown timer to show the next button
        StartCoroutine(sc.Timer(timeToNext, ShowNextButton, index));
    }

    //Shows the input index interstial marker
    public void ShowIntersitialMarker(int index)
    {
        inters[index].SetActive(true);

        //Start the countdown timer to show the next button
        StartCoroutine(sc.Timer(timeToNext, ShowNextButton, index));
    }

//called from pathscenecontroller ShowNextAnimMarker function
    //Hide the input index interstial marker
    public void HideInterstitial(int index)
    {
        inters[index].SetActive(false);
        //IC CHANGE 
    }

    //Updates and shows the next button to the animation events
    void ShowNextButton(int index)
    {
        //Jul 9 inter -> July 22 inter
        if(index == 1) {
            interDateText.text = extraInterDate;
            nextButtonToInter.SetActive(true);
        }
        else {
            //Jul 22 inter -> Aug 11 anim
            if(index == 2) index--;
            //inter -> anim
            dateText.text = dates[index];
            nextButtonToAnim.SetActive(true);
        }
    }

}
