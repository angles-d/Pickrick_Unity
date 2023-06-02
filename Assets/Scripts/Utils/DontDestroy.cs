using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to create persistent game objects
//Game objects won't be destroyed between scenes
public class DontDestroy : MonoBehaviour
{
    private void Awake()
    { 
        DontDestroyOnLoad(this.gameObject);
    }
}
