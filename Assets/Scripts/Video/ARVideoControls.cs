using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARVideoControls : MonoBehaviour
{
    public VideoPlayer player;
    public GameObject progress;
    public GameObject barStart;
    public GameObject slider;
    public GameObject play;
    public GameObject pause;
    float progressAmount;
    GameObject barRect;

    Camera arCamera;
    float barY;
    float barZ;
    float barRectScaleX;

    private void Start()
    {
        barY = progress.transform.localPosition.y;
        barZ = progress.transform.localPosition.z;
        barRect = progress.transform.GetChild(0).gameObject;
        barRectScaleX = barRect.transform.localScale.x*10;
        progress.transform.position = new Vector3(barStart.transform.localPosition.x,
                                                        barY, barZ);
        //Finds camera in the scene; tag camera as MainCamera
        arCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

    }


    void Update()
    {
        ARTouchButton();

        //move video bar
        if (player.frameCount > 0)
        {
            progressAmount = (float)player.frame / player.frameCount;
            progress.transform.localScale = new Vector3(progressAmount, 1, 1);

            float x = progress.transform.localScale.x * barRectScaleX ; //size x of bar

            progress.transform.localPosition = new Vector3(x/2.0f + barStart.transform.localPosition.x, barY, barZ);
            slider.transform.localPosition = new Vector3(x + barStart.transform.localPosition.x, slider.transform.localPosition.y, slider.transform.localPosition.z);
        }
    }
  
    void ARTouchButton()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray r = arCamera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(r, out hit))
            {
                if (hit.collider.gameObject.name == "Play")
                {
                    Debug.Log("Hit Play");
                    if (player != null)
                    {
                        play.SetActive(false);
                        pause.SetActive(true);
                        player.Play();
                    }
                }
                else if (hit.collider.gameObject.name == "Pause")
                {
                    Debug.Log("Hit Pause");
                    if (player != null)
                    {
                        play.SetActive(true);
                        pause.SetActive(false);
                        player.Pause();
                    }

                }
                else if (hit.collider.gameObject.name == "Progress Bar Back")
                {
                    Debug.Log("Hit Progress Bar");
                    slider.transform.localPosition = new Vector3(hit.point.x, slider.transform.localPosition.y, slider.transform.localPosition.z);
                    float barLength = (hit.point.x - barStart.transform.localPosition.x) / barRectScaleX;
                    progress.transform.localScale = new Vector3(barLength, 1, 1);
                    progress.transform.localPosition = new Vector3((progress.transform.localScale.x * barRectScaleX / 2.0f) + barStart.transform.localPosition.x,
                                                                barY, barZ);                                       
                    float frame = player.frameCount * barLength;
                    player.frame = (long)frame;

                }

            }
        }

    }
     
}
