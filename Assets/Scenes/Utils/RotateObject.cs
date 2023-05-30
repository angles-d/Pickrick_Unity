using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotates an object around its y axis
public class RotateObject : MonoBehaviour
{
    public float speed = 3;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
