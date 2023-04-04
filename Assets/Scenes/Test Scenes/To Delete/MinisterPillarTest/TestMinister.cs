using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMinister : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    MinisterHotspot mh;
    bool used = false;
    void OnEnable()
    {
        mh = GameObject.FindGameObjectWithTag("MinistersMarker").GetComponent<MinisterHotspot>();
        
    }

    private void Update()
    {
        if (!used)
        {
            mh.MoveScene(gameObject);
            used = true;
        }
        
    }
}
