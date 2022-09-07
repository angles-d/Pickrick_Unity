using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Direction
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

public class MoveCompass : MonoBehaviour
{
    [Header("Camera")]
    public Camera arCamera;

    [Header("Debug")]
    public bool debug;
    [SerializeField] [Range(0f, 360f)] private float northHeading;
    private float tempNorthHeading;

    [Header("Output Values")]
    [SerializeField] private float myHeading;
    [SerializeField] private float degrees;
    [SerializeField] public Direction direction;

    [Header("Compass UI")]
    public TextMeshProUGUI degreesText;
    public TextMeshProUGUI directionText;
    [Header("Compass Image")]
    public GameObject compass;

    
    private void Start() {
        debug = false;
        Input.compass.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Input.location.Start();
        // only use the Y component of the objects orientation
        // always returns a value between 0 and 360
        myHeading = arCamera.transform.eulerAngles.y;
        // also this is always a value between 0 and 360
        if (!debug) {
            northHeading = Input.compass.trueHeading;
        }


        degrees = myHeading - northHeading;
        // wrap the value so it is always between 0 and 360
        if (degrees < 0) degrees += 360f;

        if (degrees >= 23 && degrees < 67) {
            direction = Direction.NorthEast;
        } else if (degrees >= 67 && degrees < 113) {
            direction = Direction.East;
        } else if (degrees >= 113 && degrees < 158) {
            direction = Direction.SouthEast;
        } else if (degrees >= 158 && degrees < 202) {
            direction = Direction.South;
        } else if (degrees >= 202 && degrees < 247) {
            direction = Direction.SouthWest;
        } else if (degrees >= 247 && degrees < 292) {
            direction = Direction.West;
        } else if (degrees >= 292 && degrees < 337) {
            direction = Direction.NorthWest;
        } else {
            direction = Direction.North;
        }

          // Update the Compass UI Variables
            // Note: "\u00B0" is how to get the degree symbol to show
            degrees = Mathf.RoundToInt(degrees); // Round degrees before displaying
            degreesText.text = degrees.ToString() + "\u00B0";
            directionText.text = direction.ToString();

        // // Assignment to prevent the jitter degree from changing more than two
        // if (Mathf.Abs(tempNorthHeading-tempNorthHeading)>3)
        // {
            tempNorthHeading = northHeading;
            compass.transform.eulerAngles = new Vector3(0, 0, northHeading);
      
        //}
    }
}