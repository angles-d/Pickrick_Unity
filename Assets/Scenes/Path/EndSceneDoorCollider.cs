using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneDoorCollider : MonoBehaviour
{  
    [SerializeField] PathSceneController sc;
  
    //starts the end scene when the user walks through the door collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            sc.ShowEndSceneGooger();
        }
    }
}
