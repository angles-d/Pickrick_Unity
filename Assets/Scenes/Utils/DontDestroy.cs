using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //Number of objects you want to keep between scenes
    static int numDontDestroy = 2;

    private void Awake()
    {
        //Tag objects w. don't destroy in inspector
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

        //2 GameObjects: AR Session Origin, AR Session (if adding more 
        if (gameObject.scene.buildIndex != 0)
        {
            Debug.Log("Destroyed" + gameObject.name);
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
