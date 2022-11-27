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
    //public Image progress;
    public GameObject progress;
    public GameObject barStart;
    public GameObject play;
    public GameObject pause;
    float progressAmount;
    GameObject barRect;

    [SerializeField]
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    Camera arCamera;
    float barY;
    float barZ;
    float barRectScaleX;

    private void Start()
    {
        barY = progress.transform.position.y;
        barZ = progress.transform.position.z;
        barRect = progress.transform.GetChild(0).gameObject;
        barRectScaleX = barRect.transform.localScale.x*10;
        progress.transform.position = new Vector3(barStart.transform.position.x,
                                                        barY, barZ);

    }


    void Update()
    {
        ARTouchButton();

        //move video bar
        if (player.frameCount > 0)
        {
            progressAmount = (float)player.frame / player.frameCount;
            progress.transform.localScale = new Vector3(progressAmount, 1, 1);
            progress.transform.position = new Vector3((progress.transform.localScale.x * barRectScaleX / 2.0f) + barStart.transform.position.x,
                                                        barY, barZ);
        }
    }
  
    void ARTouchButton()
    {
        if (Input.touchCount == 0)
            return;
        RaycastHit hit;
        Ray r = arCamera.ScreenPointToRay(Input.GetTouch(0).position);

        if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(r, out hit))
                {
                    if (hit.collider.gameObject.name == "Play")
                    {
                        print("Hit Play");
                        if (player != null)
                        {
                            player.Play();
                            play.SetActive(false);
                            pause.SetActive(true);
                        }
                    } else if (hit.collider.gameObject.name == "Pause")
                    {
                        print("Hit Pause");
                        if (player != null)
                        {
                            player.Pause();
                            play.SetActive(true);
                            pause.SetActive(false);
                        }
                            
                    }else if (hit.collider.gameObject.name == "Progress Bar")
                    {
                        print("Hit Parent Bar");
                        float barLength = hits[0].pose.position.x - barStart.transform.position.x/barRectScaleX;
                        progress.transform.localScale = new Vector3(barLength, 1, 1);
                        progress.transform.position = new Vector3((progress.transform.localScale.x * barRectScaleX / 2.0f) + barStart.transform.position.x,
                                                                    barY, barZ);

                        float frame = player.frameCount * barLength;
                        player.frame = (long)frame;
                    } else if (hit.collider.gameObject.name == "Bar Rect")
                    {
                        print("Hit Child Bar");
                    }

                }

            }
        }

    }
}
