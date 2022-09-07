using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;




public class menuControl : MonoBehaviour
{
    public GameObject[] gallery;
    /*public Image displayImage;*/
    public Button nextImg;
    /*public Button prevImg;*/
    public GameObject finalCanvas;
    public GameObject FirstCanvas;
    public int i = 0;

    public void BtnNext()
    {
        if (i + 1 < gallery.Length)
        {
            i++;
            Debug.Log(i);
            gallery[i].SetActive(true);
            gallery[i - 1].SetActive(false);

            if (i == 4)
            {
                finalCanvas.SetActive(true);
                FirstCanvas.SetActive(false);
            }
        }
       
    }

 /*   public void BtnPrev()
    {
        if (i >= 1) 
        {
            i--;
            Debug.Log("clicked-");
            gallery[i].SetActive(false);
            gallery[i-1].SetActive(true);


        }
        else
        {
            Debug.Log("i=0");
            gallery[1].SetActive(true);
        }
       
}*/
   /* void Update()
    {
        gallery[i].SetActive(true);
    }*/
}
