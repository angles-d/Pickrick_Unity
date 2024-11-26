using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartSceneController : MonoBehaviour
{
    public GameObject confirm;
    public Button restartIconButton;

    public void OnRestartButtonClick()
    {
        Time.timeScale = 0f;
        restartIconButton.interactable = false;
        confirm.SetActive(true);
    }

    public void RestartApplication()
    {
        //Reset AR references
        if (GameObject.Find("AR Session") != null) Destroy(GameObject.Find("AR Session"));
        if (GameObject.Find("AR Session Origin") != null) Destroy(GameObject.Find("AR Session Origin"));
        if (GameObject.Find("LocationInfo") != null) Destroy(GameObject.Find("LocationInfo"));
        SceneManager.LoadScene(0);
    }

    public void ResumeApplication()
    {
        Time.timeScale = 1f;
        restartIconButton.interactable = true;
        confirm.SetActive(false);
    }

    public void StartARExperience()
    {
        Debug.Log("starting AR experience");
        SceneManager.LoadScene(1);
    }
}
