using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    bool placed = false;

    [SerializeField]
    ARPlaneManager m_planeManager;

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject anim;

    Camera arCamera;
    void Start()
    {
        m_planeManager = gameObject.GetComponent<ARPlaneManager>();
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    

    void Update()
    {
        if (!placed)
        {
            StartCoroutine(Ground());
        }
        
    }

    IEnumerator Ground()
    {
        if (!placed && m_planeManager.trackables.count >= 1)
        {
            Debug.Log("TRACKED");
            MoveToGround(prefab);
        }
        yield return null;
    }

    void MoveToGround(GameObject prefab)
    {
        float groundHeight = float.MaxValue;

        foreach (ARPlane plane in m_planeManager.trackables)
        {
            if (plane.transform.position.y < groundHeight) {
                groundHeight = plane.transform.position.y;
            }
        }

        if (groundHeight != float.MaxValue)
        {
            Vector3 pos = prefab.transform.position;
            //Instantiate(prefab, new Vector3(pos.x, groundHeight, pos.z), Quaternion.identity);
            prefab.transform.position = new Vector3(pos.x, groundHeight, pos.z);
            prefab.SetActive(true);
            placed = true;

            anim.transform.position = new Vector3(anim.transform.position.x, groundHeight, anim.transform.position.z);
           

            m_planeManager.SetTrackablesActive(false);
            m_planeManager.enabled = false;
        }

       
    }

    
}
