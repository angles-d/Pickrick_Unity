using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScrubber: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gbCanvas;
    public GameObject screen;
    Canvas canvas;
    public GameObject bar;
    public GameObject button;
    void Start()
    {
       
        canvas = gbCanvas.GetComponent<Canvas>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
