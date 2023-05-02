using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[DisallowMultipleComponent]
public class SceneController : MonoBehaviour
{
    //Timer waits t seconds then calls DoneWait method
    public IEnumerator Timer(float t, Action DoneWait)
    {
        yield return new WaitForSeconds(t);
        DoneWait?.Invoke();

    }

    //Timer with a parameter
    public IEnumerator Timer<T>(float t, Action<T> DoneWait, T param)
    {
        yield return new WaitForSeconds(t);
        DoneWait?.Invoke(param);

    }

    //Opens the link in a website
    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }

    //Loads a new scene
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //Moves objects to a specified height
    //Bottom of the object will be at the set height
    public void MoveToGround(float height, GameObject[] toPlace)
    {
        //for all children in each gameobject
        foreach (GameObject g in toPlace)
        {
            Vector3 pos = g.transform.position;
            Renderer r = g.GetComponent<Renderer>();

            //If there's a renderer use the render bounds (slightly more accurate)
            if (r != null)
            {
                g.transform.position = new Vector3(pos.x, height + r.bounds.extents.y, pos.z);
            }
            //otherwise use the y position
            else
            {
                g.transform.position = new Vector3(pos.x, height + pos.y, pos.z);
            }
        }

    }


    //Moves objects to a specified height
    //Use if there are multiple sets of objects to be placed
    public void MoveToGround(float height, GameObject[][] toPlace)
    {
        foreach (GameObject[] go in toPlace)
        {
            MoveToGround(height, go);
        }
    }

    //Creates a ray from a 2D screen touch position and checks if there's an
    //intersection w/ any trackable objects
    //Use when you want to detect a hit from a user's tap
    //returns true if the raycast was successful else false
    public bool CheckRaycast(ARRaycastManager m_raycastManager, Action<List<ARRaycastHit>> afterHit, Touch touch)
    {
        //We only want to track for intersections w/ planes
        const TrackableType trackableTypes = TrackableType.Planes;

        //Empty list to store any hits
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        //perform the raycast
        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            //if hits is not null call the afterHit method
            afterHit?.Invoke(hits);
            return true;
        }

        return false;
    }

    //checks for an intersection between an input Ray and a trackable object
    //(ex: creating a ray from the camera to check if the user is looking at something)
    public bool CheckRaycast(ARRaycastManager m_raycastManager, Action<List<ARRaycastHit>> afterHit, Ray ray)
    {
        const TrackableType trackableTypes = TrackableType.Planes;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_raycastManager.Raycast(ray, hits, trackableTypes))
        {
            afterHit?.Invoke(hits);
            return true;
        }

        return false;
    }

    //checks for an intersection between an input Ray and a trackable object
    //(ex: creating a ray from the camera to check if the user is looking at something)
    public bool CheckRaycast<T>(ARRaycastManager m_raycastManager, Action<List<ARRaycastHit>, T> afterHit, Ray ray, T param)
    {
        const TrackableType trackableTypes = TrackableType.Planes;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (m_raycastManager.Raycast(ray, hits, trackableTypes))
        {
            afterHit?.Invoke(hits, param);
            return true;
        }

        return false;
    }
}
