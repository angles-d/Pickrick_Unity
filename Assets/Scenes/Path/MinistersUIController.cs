using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinistersUIController : MonoBehaviour
{
    public GameObject[] ministers;

    //store the minister button canvas groups so we can change the transparency
    CanvasGroup[] cg = new CanvasGroup[4];
    //if the user has clicked that minister's buttom
    bool[] buttonVisited = new bool[4];

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            cg[i] = ministers[i].GetComponentInChildren<CanvasGroup>();
        }
    }

    public void LowerImageAlpha(int index)
    {
        cg[index].alpha = 1f;

        //turn all the visited buttons grey and not the current one
        for (int i  = 0; i < 4; i++){
            if (i != index && buttonVisited[i])
            {
                cg[i].alpha = 0.6f;
            }
        }

        if (!buttonVisited[index])
        {
            buttonVisited[index] = true;
        }


    }
}
