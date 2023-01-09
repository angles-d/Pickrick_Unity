using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDate: MonoBehaviour
{
    public Text textElement;
    public float timeDelay = 3.0f;
    void OnEnable()
    {
        Invoke("CleanText", timeDelay);
    }


    void CleanText()
    {
        textElement.gameObject.SetActive(false);
    }

    // private void OnDisable()
    // {
    //     textElement.gameObject.SetActive(true);
    // }
}
