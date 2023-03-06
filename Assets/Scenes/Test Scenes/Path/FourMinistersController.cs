using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourMinistersController : MonoBehaviour
{
    public GameObject firstMarker;
    public GameObject[] ministers;
    public bool[] visited = {false, false, false, false};

    bool allVisited = false;

    public GameObject moveToNext;
    public GameObject arPlane;
    public GameObject meetMinisterText;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!allVisited)
        {
            CheckAllVisited();
        }
        
    }

    public void StartInnerPath()
    {
        Debug.Log("First Marker on");

        firstMarker.SetActive(true);
        arPlane.SetActive(true);

        for (int i = 0; i < ministers.Length; i++)
        {
            ministers[i].SetActive(false);
            
        }
    }


    public void ShowMinister(int index)
    {
        for (int i = 0; i < ministers.Length; i++)
        {
            if (i == index)
            {
                visited[i-1] = true;
                ministers[i].SetActive(true);
            } else
            {
                ministers[i].SetActive(false);
            }
           
        }
       

    }

    public void CheckAllVisited()
    {
        foreach(bool min in visited)
        {
            if (!min)
            {
                return;
            } 
        }
        Debug.Log("all visited");
        allVisited = true;
        meetMinisterText.SetActive(false);
        moveToNext.SetActive(true);

    }
}
