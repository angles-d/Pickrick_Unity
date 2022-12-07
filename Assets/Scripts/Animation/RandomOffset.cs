using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOffset : MonoBehaviour
{
    Animator anim;
    float offset;
    // Start is called before the first frame update

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Offset", Random.Range(0.0f, 1.0f));
        anim.speed = Random.Range(0.5f, 1.2f);
    }
    
}
