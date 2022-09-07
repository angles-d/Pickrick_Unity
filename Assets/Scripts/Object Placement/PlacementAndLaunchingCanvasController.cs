using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class PlacementAndLaunchingCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private PlacementObject placedPrefab;

    private GameObject placedObject;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    [SerializeField]
    private bool displayCanvas = true;

    [SerializeField]
    private ARPlaneManager arPlaneManager;

    void Awake() 
    {
        dismissButton.onClick.AddListener(Dismiss);
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && placedObject == null)
        {
            
            ARPlane arPlane = args.added[0];
            placedObject = Instantiate(placedObject, arPlane.transform.position, Quaternion.identity);
        }
    }

    private void Dismiss() => welcomePanel.SetActive(false);

    void Update()
    {
        // do not capture events unless the welcome panel is hidden
        if(welcomePanel.activeSelf)
            return;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                // if we got a hit meaning that it was selected
                if(Physics.Raycast(ray, out hitObject))
                {
                    PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                    MeshRenderer placementObjectMeshRenderer = placedObject.GetComponent<MeshRenderer>();
                    if(placementObject != null)
                    {
                        placementObject.Selected = true;
                        placementObjectMeshRenderer.material.color = activeColor;
                        
                        if(displayCanvas) 
                        {
                            placementObject.ToggleCanvas();
                        }
                    }
                } // nothing selected so set the sphere color to inactive
                else
                {
                    PlacementObject placementObject = placedObject.GetComponent<PlacementObject>();
                    MeshRenderer placementObjectMeshRenderer = placedObject.GetComponent<MeshRenderer>();
                    if(placementObject != null)
                    {
                        placementObject.Selected = false;
                        placementObjectMeshRenderer.material.color = inactiveColor;

                        if(displayCanvas) 
                        {
                            placementObject.ToggleCanvas();
                        }
                    }
                }
            }
        }
    }
}
