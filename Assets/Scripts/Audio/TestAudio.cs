  
using UnityEngine;

namespace Audio {
    public class TestAudio : MonoBehaviour
    {
    public Audio.AudioController audioController;

#region Unity Functions
#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyUp(KeyCode.T)) {
            audioController.PlayAudio(AudioType.MinistersEnterResturant, true, 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.G)) {
            audioController.StopAudio(AudioType.MinistersEnterResturant, true, 0.0f);
        }
        if (Input.GetKeyUp(KeyCode.B)) {
            audioController.RestartAudio(AudioType.MinistersEnterResturant);
        }
    }

#endif
#endregion

    }
}