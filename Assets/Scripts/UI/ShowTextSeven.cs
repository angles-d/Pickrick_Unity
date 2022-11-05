using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextSeven : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    void OnEnable(){
        SetTextToDisplayAndCleanIt("September 26, 1964");
    }

    public void SetTextToDisplayAndCleanIt(string textValue){
        textElement.text=textValue;
        Invoke("CleanText",3f);
    }

    void CleanText(){
        //textElement.enabled=false;
        textElement.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        textElement.enabled = true;
    }
}
