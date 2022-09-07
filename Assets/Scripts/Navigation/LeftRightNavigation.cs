using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEditor;

public class LeftRightNavigation : MonoBehaviour
{
    public Renderer target;
    public Image leftNav;
    public Image rightNav;
    public Camera aRCamera;

    // If you want to play a sound if the user is looking at the target than make this variable true
    [HideInInspector]
    public bool playSound;

    // Show sound in inspector if playSound bool is true
    [HideInInspector]
    public Audio.AudioType sound;

    [HideInInspector]
    // Reference to audio controller (only show if play sound bool is true)
    public Audio.AudioController audioController;

    // if a sound if currently playing or not
    private bool soundPlaying;

    // Hide both left and right arrow images when script activated
    void Start() {
        leftNav.enabled = false;
        rightNav.enabled = false;
        soundPlaying = false;
    }

    // Check if target is in frame
    // if target is in frame, disable the left and right nav arrows
    // if the target is not in frame, decide if the right or left nav arrow should be enabled depending on camera position
	void Update () {
        if (target.isVisible) {

            // Play audio if playSound bool is true and soundPlaying is false (the sound is not playing yet)
            if (playSound && !soundPlaying) {
                audioController.PlayAudio(sound, true, 0.0f);
                // set soundPlaying to true
                soundPlaying = true;
            }

            leftNav.enabled = false;
            rightNav.enabled = false;
        } else {

            // Stop audio if playSound bool is true and soundPlaying is true (the sound is already playing)
            if (playSound && soundPlaying) {
                audioController.StopAudio(sound, true, 0.0f);
                // set sound playing to false
                soundPlaying = false;
            }

            GetArrowDirection();
        }
	}

    // Get the direction the camera needs to go for the target to be on screen, and enable appropriate arrow
    void GetArrowDirection() {
        Vector3 fowardCameraDirection = aRCamera.transform.forward;
        Vector3 upCameraDirection = aRCamera.transform.up;
        Vector3 targetDirection = target.gameObject.transform.position - aRCamera.transform.position;
        Vector3 perp = Vector3.Cross(fowardCameraDirection, targetDirection);
        float dir = Vector3.Dot(perp, upCameraDirection);
        if (dir > 0f) {
            rightNav.enabled = true;
            leftNav.enabled = false;
		} else if (dir < 0f) {
            rightNav.enabled = false;
            leftNav.enabled = true;
		} else {
            rightNav.enabled = false;
            leftNav.enabled = false;
		}
    }

    // stop the audio when this game object is disabled and set soundPlaying to false
    void OnDisable() {
        if (playSound) {
            audioController.StopAudio(sound, true, 0.0f);
            soundPlaying = false;
        }
    }

}


 #if UNITY_EDITOR
// Hide and unhide sound variable based on playSound bool
[CustomEditor(typeof(LeftRightNavigation))]
 public class LeftRightNavigationEditor : Editor
 {
   override public void OnInspectorGUI() {
       
       DrawDefaultInspector();

       var LeftRightNavigation = target as LeftRightNavigation;
       
       LeftRightNavigation.playSound = EditorGUILayout.Toggle("Play Sound", LeftRightNavigation.playSound);
       
       if (LeftRightNavigation.playSound) {
           LeftRightNavigation.sound = (Audio.AudioType)EditorGUILayout.EnumPopup("Sound", LeftRightNavigation.sound);
           LeftRightNavigation.audioController = (Audio.AudioController)EditorGUILayout.ObjectField("Audio Controller", LeftRightNavigation.audioController, typeof(Audio.AudioController), true);
        }
    }
}
#endif