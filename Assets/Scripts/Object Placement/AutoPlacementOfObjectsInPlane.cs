
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlacementOfObjectsInPlane : MonoBehaviour
{
    private bool isObjectPlaced;
    
    [SerializeField]
    private GameObject placedObject;

    [SerializeField]
    private Button dismissButton;
        
    [SerializeField]
    private ARPlaneManager arPlaneManager;




    void Awake() 
    {
        dismissButton.onClick.AddListener(Dismiss);
    }   

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if(args.added != null && !isObjectPlaced) {
            ARPlane arPlane = args.added[0];
            placedObject.SetActive(true);
            placedObject.transform.position = arPlane.transform.position;
            placedObject.transform.rotation = Quaternion.identity;
            isObjectPlaced = true;
        }
    }

    public void Dismiss()
    {
        Debug.Log("Putting AR Scene on Screen Now");
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }
}
