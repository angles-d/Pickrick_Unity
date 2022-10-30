using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextFour : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    void Start(){
        SetTextToDisplayAndCleanIt("July 3, 1964");
    }

    public void SetTextToDisplayAndCleanIt(string textValue){
        textElement.text=textValue;
        Invoke("CleanText",3f);
    }

    void CleanText(){
        textElement.enabled=false;
    }
}
