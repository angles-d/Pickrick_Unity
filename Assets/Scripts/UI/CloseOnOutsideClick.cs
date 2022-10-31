using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOnOutsideClick : MonoBehaviour
{
    public GameObject infoUI;
    public GameObject title;

    private void Start()
    {
        infoUI = transform.parent.gameObject;
    }
    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !EventSystem.current.IsPointerOverGameObject())
        {
            title.SetActive(false);
            //panel.SetActive(false);
            infoUI.SetActive(false);
        }
    }

}
