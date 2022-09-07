using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIVideo : MonoBehaviour
{

    public GameObject VideoCanvas;
    void Start()
    {
        VideoCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VideoActive()
    {
        VideoCanvas.SetActive(true);
    }

    public void VideoDeActive()
    {
        VideoCanvas.SetActive(false);
    }
}
