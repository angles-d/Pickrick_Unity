using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextEleven : MonoBehaviour
{
    public string textValue;
    public Text textElement;
    void OnEnable(){
        SetTextToDisplayAndCleanIt("February 22, 1965");
    }

    public void SetTextToDisplayAndCleanIt(string textValue){
        textElement.text=textValue;
        Invoke("CleanText",3f);
    }

    void CleanText(){
        //textElement.enabled=false;
        textElement.gameObject.SetActive(false);
    }

    // private void OnDisable()
    // {
    //     textElement.gameObject.SetActive(true);
    // }
}
