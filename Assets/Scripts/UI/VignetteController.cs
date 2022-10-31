using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VignetteController : MonoBehaviour
{
    public ImageRecognition image_rec;
    public ARTrackedImageManager ar_track;
    //public GameObject rotate_gif;
    public GameObject vinToArInfo;
    public GameObject launchArInfo;
    public GameObject toLandscapeInfo;
    //public GameObject toPortraitInfo;
    public GameObject scanArInfo;
    public GameObject vignettes;
    public GameObject introInfo;
    public bool landscape = false;
    public bool portrait = true;

    public State state = State.TO_LANDSCAPE;

   

    public enum State
    {
        TO_LANDSCAPE,
        IMAGE_TRACKING,
        TO_PORTRAIT
    }
    // Start is called before the first frame update
    void Start()
    {
        image_rec.enabled = true;
        ar_track.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.TO_LANDSCAPE:
                toLandscape();
                break;
            case State.IMAGE_TRACKING:
                trackImage();
                break;
            //case State.TO_PORTRAIT:
            //    toPortrait();
            //    break;
            default:
                break;

        }
        
    }

    void toLandscape()
    {
        //ipad is in landscape mode

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
            Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            landscape = true;
            image_rec.enabled = true;
            ar_track.enabled = true;
            portrait = false;
            toLandscapeInfo.SetActive(false);
            scanArInfo.SetActive(true);
            Screen.orientation = ScreenOrientation.Landscape;
            
            state = State.IMAGE_TRACKING;
        }
    }

    void trackImage()
    {
        if (scanArInfo.activeSelf && image_rec.firstImageScanned)
        { 
            scanArInfo.SetActive(false);
        }

        if (!vinToArInfo.activeSelf && image_rec.vidDone)
        {
            vinToArInfo.SetActive(true);
           
        }
    }

   //public void toPortrait()
   // {

   //     if (Input.deviceOrientation == DeviceOrientation.Portrait ||
   //         Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
   //     {
   //         portrait = true;
   //         landscape = false;
   //         image_rec.enabled = false;
   //         ar_track.enabled = false;
   //         scanArInfo.SetActive(false);
   //     }

   // }

   // //Set the rotate to portrait true when pressing the buttton
   // void setToPortrait()
   // {
   //     toPortraitInfo.SetActive(true);
   // }

    //start the go to AR UI

    public void toAR()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void startVignettes()
    {
        introInfo.SetActive(false);
        vignettes.SetActive(true);
    }

}
