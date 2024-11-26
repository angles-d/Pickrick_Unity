using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroSceneController : MonoBehaviour
{
    public void BeginExperience()
    {
        Debug.Log("starting AR experience");
        SceneManager.LoadScene(1);
    }
}