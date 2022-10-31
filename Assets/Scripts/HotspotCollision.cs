using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotCollision : MonoBehaviour
{
    public GameObject hotspotInfo;
    // Start is called before the first frame update
    void Start()
    {
        hotspotInfo.SetActive(false);
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision???");
            hotspotInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision???");
            hotspotInfo.SetActive(false);
        }

    }

    public void TestHotspot()
    {
        hotspotInfo.SetActive(true);
    }
}
