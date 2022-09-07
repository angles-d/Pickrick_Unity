using UnityEngine;

// Interface for every event on timeline
public abstract class Timeline_Event : MonoBehaviour {
    // Method that runs when user presses on timeline event
    public abstract void LaunchEvent(); 
}