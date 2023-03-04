using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float rotationSpeed;

    private Quaternion rotation;
    private Vector3 direction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointArrowAt(target);
    }

    public void PointArrowAt(GameObject toPointAt)
    {
        direction = (toPointAt.transform.position - transform.position).normalized;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

    }
}
