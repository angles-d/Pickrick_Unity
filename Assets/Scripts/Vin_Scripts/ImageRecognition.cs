using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class ImageRecognition : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placablePrefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager ar;
    public void Awake()
    {
        ar = FindObjectOfType<ARTrackedImageManager>();
        foreach (GameObject prefab in placablePrefabs)
        {
            //instantiates a physical version of the prefab
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    public void OnEnable()
    {
        ar.trackedImagesChanged += onImageChanged;
    }

    public void OnDisable()
    {
        ar.trackedImagesChanged -= onImageChanged;
    }

    public void onImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage tracked in args.added)
        {
            SetImage(tracked);
        }

        foreach (ARTrackedImage tracked in args.updated)
        {
           SetImage(tracked);
        }

        foreach (ARTrackedImage tracked in args.removed)
        {
            spawnedPrefabs[tracked.name].SetActive(false);
        }

    }


    private void SetImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;


        GameObject newPrefab = spawnedPrefabs[name];


        newPrefab.transform.position = position;
        newPrefab.transform.rotation = rotation;

        newPrefab.SetActive(true);
  
        foreach (GameObject pref in spawnedPrefabs.Values)
        {
            if (pref.name != name)
            {
                pref.SetActive(false);
            }
        }

    }

    public bool VectorDif(Vector3 cur, Vector3 other, float allowedDifference)
    {
        //var dx = cur.x - other.x;
        //if (Mathf.Abs(dx) > allowedDifference)
        //    return false;

        var dy = cur.y - other.y;
        if (Mathf.Abs(dy) > allowedDifference)
            return false;

        var dz = cur.z - other.z;

        return Mathf.Abs(dz) >= allowedDifference;
    }


}
