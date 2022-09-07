using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCanvasShow : MonoBehaviour
{
    public Canvas ShowCanvas;
    void Awake()
    {
        ShowCanvas.enabled = false;

    }

    public void TextOn()
    {
        ShowCanvas.enabled = true;
    }

    public void TextOff()
    {
        ShowCanvas.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
