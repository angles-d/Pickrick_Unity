using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    private void Awake()
    {
        if (!Application.isEditor)
        {
            gameObject.SetActive(false);
        }
    }
}
