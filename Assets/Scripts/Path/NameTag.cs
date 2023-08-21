using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds a name tag to a game object
//Name tag will rotate to face the camera
public class NameTag : MonoBehaviour
{
    //Name tag text
    public GameObject TextName;

    void Update()
    {
        if (TextName != null)
        {
            //Rotates the name tag in the direction of the camera
            TextName.transform.LookAt(Camera.main.transform.position);
            TextName.transform.Rotate(0, 180, 0);
        }
    }
}
