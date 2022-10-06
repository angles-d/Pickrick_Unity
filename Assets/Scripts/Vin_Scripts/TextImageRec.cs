using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class TextImageRec: MonoBehaviour
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
        string name = "Text"+trackedImage.referenceImage.name;
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

}
