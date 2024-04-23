using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

//Controller for the ending screen & credits sequence
public class GoogerEndScene : MonoBehaviour
{
    //Video player 
    [SerializeField] VideoPlayer videoPlayer;
    //Googer Video 
    [SerializeField] RawImage uiGoogerVideo;
    //Googer Name
    [SerializeField] TextMeshProUGUI uiName;
    //Googer Quote
    [SerializeField] TextMeshProUGUI uiQuote;

    //Black Background
    Image uiGoogerBackground;

    [SerializeField] PathSceneController pathSceneControllerGameObject;

    //if the googer video is finished
    bool vidDone = false;

    private void Awake()
    {
        uiGoogerBackground = GetComponent<Image>();

    }

    //Runs when the end Scene Credit object is activated
    private void OnEnable()
    {
        //restart the video
        videoPlayer.frame = 0;

        //Cross fade screen elements in 
        uiGoogerBackground.CrossFadeAlpha(0f, 0f, true);
        uiGoogerBackground.CrossFadeAlpha(1, 3, false);

        uiGoogerVideo.CrossFadeAlpha(0f, 0f, true);
        uiGoogerVideo.CrossFadeAlpha(1, 4, false);

        uiName.CrossFadeAlpha(0f, 0f, true);
        uiName.CrossFadeAlpha(1, 4, false);

        uiQuote.CrossFadeAlpha(0f, 0f, true);
        uiQuote.CrossFadeAlpha(1, 4, false);
    }

    private void Update()
    {
        if (!vidDone && videoPlayer.time >= 8.7)
        {
            vidDone = true;

            //Cross fade screen to black
            uiGoogerVideo.CrossFadeAlpha(0, 1, false);
            uiName.CrossFadeAlpha(0, 1, false);
            uiQuote.CrossFadeAlpha(0, 1, false);

            //SPRING 2024 IMPLEMENTATION - MOVED CREDITS TO END OF NEXT SCENE
            //change scene
            pathSceneControllerGameObject.LoadScene(2);
        }
    }



}
