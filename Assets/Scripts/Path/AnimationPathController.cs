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
    [SerializeField] GameObject nextButtonToAnim;
    [SerializeField] GameObject animHeader;
    [SerializeField] GameObject[] animDates; //text & dates


    //dates for the interstital button
    string[] dates = { "Jul 3rd, 1964", "Jul 9th, 1964", "Jul 22nd, 1964", "Fall, 1964", "February, 1965" };
    string extraAnimDate = "Sept 26th, 1964";
    TextMeshProUGUI dateText;
    TextMeshProUGUI animDateText;

    ///
    private bool playedAug11AnimFlag;
    string[] texts = 
    {"Albert Dunn, George Willis, and Woodrow Lewis arrive at the Pickrick to test compliance with the Civil Rights Act passed the day before. You can view their reception from the back seat of the car by sitting on the edge of the bench facing the pillar.",
    "On July 9,1964 the NAACP brings suit against the Pickrick and Maddox on behalf of Dunn, Willis, and Lewis under the Civil Rights Act. On July 22, 1964 the Court orders Maddox’s Pickrick Restaurant to comply. On August 11, 1964 Dunn and Wells return to the Pickrick with fellow activists Rev. Albert Sampson and Barbara Suarez. You can watch the recreation of the confrontation by turning towards your left.",
    "On Sept 4, 1964 the Court orders Maddox’s Pickrick Restaurant to obey its order to integrate. Maddox closed the restaurant and reopened on Sept 26 as the Lester Maddox Cafeteria. You can see the change in signage by turning towards the bench.",
    "After another confrontation on Sept 28 with ministers Dunne, Willis, Wells, and Lewis, the court orders the Lester Maddox Cafeteria to comply with the Civil Rights Act. Then is a break in active confrontations for the presidential elections of November 1964 and a ruling by the Supreme court in the related Heart of Atlanta Motel case that upholds the Civil Rights Act. On January 28, 1965 ministers Wells, Willis, Dunn, and Lewis were again met with physical aggression and again responded non-violently. You can see the recreation by turning toward your right.",
    "The Court found Lester Maddox in contempt of court for refusing to comply with Civil Rights Act of 1964 and levied a fine of $200/day. On Feb 7,1965 Lester Maddox closed the restaurant. On Feb 22 it reopened under new management as the Gateway Cafeteria and Jack Googer, who worked nearby, was filmed entering at lunchtime. You can see a recreation of this event by turning towards your left. Enter to the restaurant to continue."};
    
    [SerializeField] GameObject animTextGameObject;
    TextMeshProUGUI animText;

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
        animDateText = nextButtonToAnim.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        animText = animTextGameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        playedAug11AnimFlag = false;
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
        animTextGameObject.SetActive(false);
    }

    //called after the animation has been played once
    //through the signal emitter on the timeline
    //Make sure signal only emits once
    public void ShowNextInterButton(int index)
    {
        //aug 11 -> sept 26 anim
        if(index == 2 && !playedAug11AnimFlag) {
            animDateText.text = extraAnimDate;
            nextButtonToAnim.SetActive(true);
            playedAug11AnimFlag = true;
        }
        else if (index + 1 < dates.Length)
        {
            dateText.text = dates[index + 1];
            nextButtonToInter.SetActive(true);
        }
    }

    //turns on the input index marker
    public void ShowAnimMarker(int index)
    {
        markers[index].SetActive(true);
        //SPRING 2024 IMPLEMENTATION
        animText.text = texts[index];
        animTextGameObject.SetActive(true);
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
