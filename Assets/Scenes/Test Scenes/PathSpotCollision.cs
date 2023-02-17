using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpotCollision : MonoBehaviour
{
    public GameObject arAnim;
    private bool animActive = false;
    // Start is called before the first frame update
    void Start()
    {
        arAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!animActive && other.gameObject.CompareTag("Camera"))
        {
            Debug.Log("COLLIDE");
            arAnim.SetActive(true);
            animActive = true;
        }
       
    }
}
