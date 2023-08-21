using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//End Scene Door Collider Script
public class EndSceneDoorCollider : MonoBehaviour
{  
    [SerializeField] PathSceneController sc;
  
    //Starts the end scene when the user walks through the door collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            sc.ShowEndSceneGooger();
        }
    }
}
