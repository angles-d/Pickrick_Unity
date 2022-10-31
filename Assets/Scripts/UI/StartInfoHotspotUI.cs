using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInfoHotspotUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    public GameObject title;
    public GameObject panel;
    void OnEnable()
    {
        panel.SetActive(false);
        button.SetActive(true);
        title.SetActive(true);
    }
}
