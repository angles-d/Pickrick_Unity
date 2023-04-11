using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneDoorCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneController sc;
  

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        //if user walks into the door
        if (other.gameObject.CompareTag("MainCamera"))
        {
            sc.ShowEndSceneGooger();
        }
    }
}
