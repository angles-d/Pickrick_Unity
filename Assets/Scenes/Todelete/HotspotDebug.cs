using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotDebug : MonoBehaviour
{
    public void OnReachHotspot() {
        Debug.Log("You Have Entered the Hotspot!");
    }

    public void OnExitHotspot() {
        Debug.Log("You Have Exited the Hotspot!");
    }
}
