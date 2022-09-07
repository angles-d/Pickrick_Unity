using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class EffectsManager : MonoBehaviour
{
    [SerializeField]
    private Light defaultLight;

    [SerializeField]
    private Button toggleLightButton;

    [SerializeField]
    private Button toggleShadowsButton;

    [SerializeField]
    private Button togglePlaneDetectionButton;

    [SerializeField]
    private ARPlaneManager aRPlaneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if(toggleLightButton == null || toggleShadowsButton == null || togglePlaneDetectionButton == null)
        {
            Debug.LogError("You must set buttons in the inspector");
            enabled = false;
            return;
        }

        if(defaultLight == null)
        {
            Debug.LogError("You must set the light in the inspector");
            enabled = false;
            return;
        }

        toggleLightButton.onClick.AddListener(ToggleLights);
        toggleShadowsButton.onClick.AddListener(ToggleShadows);
        togglePlaneDetectionButton.onClick.AddListener(TogglePlaneDetection);
    }

    public void TogglePlaneDetection()
    {
        aRPlaneManager.enabled = !aRPlaneManager.enabled;
        
        foreach(ARPlane plane in aRPlaneManager.trackables)
        {   
            plane.gameObject.SetActive(aRPlaneManager.enabled);
        }
        togglePlaneDetectionButton.GetComponentInChildren<TextMeshProUGUI>().text = aRPlaneManager.enabled ? "Disable Detection" : "Enable Detection";
    }

    public void ToggleLights()
    {
        defaultLight.enabled = !defaultLight.enabled;
        toggleLightButton.GetComponentInChildren<TextMeshProUGUI>().text = defaultLight.enabled ? "Disable Lights" : "Enable Lights";
    }

    public void ToggleShadows()
    {
        if(defaultLight.enabled)
        {
            float shadowValue = defaultLight.shadowStrength > 0 ? 0 : 1;
            defaultLight.shadowStrength = shadowValue;
            toggleShadowsButton.GetComponentInChildren<TextMeshProUGUI>().text   = shadowValue == 0 ? "Enable Shadows" : "Disable Shadows";
        }
    }
}
