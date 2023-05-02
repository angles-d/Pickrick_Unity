using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;


public class GoogerEndController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject nameText;
    [SerializeField] GameObject quote;
    [SerializeField] GameObject video;
    [SerializeField] GameObject credits;

    Image uiGoogerBackground;
    RawImage uiGoogerVideo;
    TextMeshProUGUI uiName;
    TextMeshProUGUI uiQuote;

    bool vidDone = false;


    private void Awake()
    {
        uiGoogerBackground = GetComponent<Image>();
        uiGoogerVideo = video.GetComponent<RawImage>();
        uiName = nameText.GetComponent<TextMeshProUGUI>();
        uiQuote = quote.GetComponent<TextMeshProUGUI>();
    }

    //When the end Scene Credit object is activated
    private void OnEnable()
    {
        videoPlayer.frame = 0;

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
            uiGoogerVideo.CrossFadeAlpha(0, 1, false);
            uiName.CrossFadeAlpha(0, 1, false);
            uiQuote.CrossFadeAlpha(0, 1, false);

            credits.SetActive(true);
            
            
        }
    }



}
