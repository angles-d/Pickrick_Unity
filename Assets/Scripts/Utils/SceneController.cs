using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[DisallowMultipleComponent]
//Scene Controller Component
//Handles general methods used in a scene
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
    //returns true if the raycast intersects with a trackable
    public bool CheckRaycast(ARRaycastManager m_raycastManager, Action<List<ARRaycastHit>> afterHit, Touch touch)
    {
        //We only want to track for intersections w/ planes
        const TrackableType trackableTypes = TrackableType.Planes;

        //Empty list to store any hits
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        //perform the raycast
        if (m_raycastManager.Raycast(touch.position, hits, trackableTypes))
        {
            Debug.Log("Intersection" + hits);
            //if hits is not null call the afterHit method
            afterHit?.Invoke(hits);
            return true;
        }
        return false;
    }

    //checks for an intersection between an input Ray and a trackable object
    //(ex: creating a ray from the camera to check if the user is looking at something)
    //returns true if the raycast intersects with a trackable
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
    //returns true if the raycast intersects with a trackable
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

    //Returns ARRaycastHit
    public ARRaycastHit? RaycastGetHit(ARRaycastManager m_raycastManager, Ray ray)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        m_raycastManager.Raycast(ray, hits, TrackableType.Planes);

        if (hits.Count == 0)
        {
            return null;
        }
        else
        {
            return hits[0];
        }
    }

    //Returns ARRaycastHit
    public ARRaycastHit? RaycastGetHit(ARRaycastManager m_raycastManager, Touch touch)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        m_raycastManager.Raycast(touch.position, hits, TrackableType.Planes);

        //No hits detected; hit list = empty
        if (hits.Count == 0)
        {
            return null;
        }
        else
        {
            return hits[0];
        }
    }

    //Returns ARRaycastHit Plane
    public ARPlane RaycastGetPlane(ARRaycastManager m_raycastManager, ARPlaneManager m_planeManager, Ray ray)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        
        m_raycastManager.Raycast(ray, hits, TrackableType.Planes);

        //No hits detected; hit list = empty
        if (hits.Count == 0)
        {
            return null;
        }

        Debug.Log("Hit Count: " + hits.Count);
        ARRaycastHit hit = hits[0];

        foreach (ARPlane p in m_planeManager.trackables)
        {
            //Check if the current plane is active
            if (p.isActiveAndEnabled && p.trackableId == hit.trackableId)
            {
                return p;
            }
            
        }

        //plane not found
        return null;
    }

    //Returns ARRaycastHit Plane
    public ARPlane RaycastGetPlane(ARRaycastManager m_raycastManager, ARPlaneManager m_planeManager, Touch touch)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        m_raycastManager.Raycast(touch.position, hits, TrackableType.Planes);

        //No hits detected; hit list = empty
        if (hits.Count == 0)
        {
            return null;
        }

        ARRaycastHit hit = hits[0];

        foreach (ARPlane p in m_planeManager.trackables)
        {
            //Check if the current plane is active
            if (p.isActiveAndEnabled && p.trackableId == hit.trackableId)
            {
                return p;
            }

        }

        //plane not found
        return null;
    }


    //Hides active AR planes in plane_manager
    public void HideARPlanes(ARPlaneManager m_planeManager)
    {
        foreach (ARPlane p in m_planeManager.trackables)
        {
            p.gameObject.SetActive(false);

        }
    }
}
