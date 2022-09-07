using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class CompassTargetDirection : MonoBehaviour {
    // The range in degrees for what you want the user to point the compass
    public Vector2 compassTargetDegreesRange;

    // Image that indicates if the compass is facing the right direction
    public Image targetDirectionIndicator;

    // Reference to the compass script
    public Compass compass;

    // If the user is at the correct Direction of not
    private bool isCorrectDirection;
   

 
    void Start() {
        isCorrectDirection = false;
        targetDirectionIndicator.color = Color.red;
    }
 
    void Update() {
        CheckCurrentDirection();
    }

    // Check the current direction of the player and update the color of the target direction indicator if neccesary
    private void CheckCurrentDirection() {
        float direction = compass.getDegrees();
        if (isCorrectDirection) {
            if (direction < compassTargetDegreesRange.x || direction > compassTargetDegreesRange.y) {
                targetDirectionIndicator.color = Color.red;
                isCorrectDirection = false;
            }
        } else {
            if (direction >= compassTargetDegreesRange.x && direction <= compassTargetDegreesRange.y) {
                targetDirectionIndicator.color = Color.green;
                isCorrectDirection = true;
            }
        }
    }

    // Getter for isCorrectDirection
    public bool getIsCorrectDirection() {
        return isCorrectDirection;
    }
}
