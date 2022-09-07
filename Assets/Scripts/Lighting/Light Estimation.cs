using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class NewBehaviourScript : MonoBehaviour {
    [SerializeField]
    private ARCameraManager arCameraManager;

    [SerializeField]
    private Text brightnessVal;

    [SerializeField]
    private Text tempVal;

    [SerializeField]
    private Text colorCorrectionVal;

    private Light currentLight;

    private void Awake() {
        currentLight = GetComponent<Light>();
    }

    private void OnEnable() {
        arCameraManager.frameReceived += FrameUpdated;
    }
    
    private void OnDisable() {
        arCameraManager.frameReceived -= FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs args) {
        if (args.lightEstimation.averageBrightness.HasValue) {
            brightnessVal.text = $"Brightness: {args.lightEstimation.averageBrightness.Value}";
            currentLight.intensity = args.lightEstimation.averageBrightness.Value;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue) {
            brightnessVal.text = $"Temp: {args.lightEstimation.averageColorTemperature.Value}";
            currentLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue) {
            brightnessVal.text = $"Temp: {args.lightEstimation.colorCorrection.Value}";
            currentLight.color = args.lightEstimation.colorCorrection.Value;
        }
    }
}
