using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;


//Scene Controller for the Wall Timeline Scene
public class TimelineSceneController : SceneController
{
    //Timeline date objects (contain collider & card holder); 6 cards rn
    public GameObject[] dates;
    //Doors between walls; 3 rn
    public GameObject[] doors = new GameObject[3];
    //Position reference to the timeline (in the timeline gameobject)
    public GameObject timelinePosRef;
    //Timeline game object
    public GameObject timeline;
    //AR floor game object
    public GameObject arFloor;

    [Header("Videos")]
    //Pickrick intro video game object
    public GameObject intro;
    //Signing the civil rights video gameobject (in the last date gameobject)
    public GameObject doorVid;

    [Header("UI Elements")]
    //Move on to the ministers button (switches scenes)
    public GameObject pathSceneButton;

    //flag booleans
    //if the introduction video is done
    bool introDone = false;
    //if the door to the inner area is open
    bool doorOpen = false;

    //video players
    VideoPlayer doorVidPlayer;
    VideoPlayer introVidPlayer;

    //latest date the user has collided with
    int curDate = 0;


    //Historical cards in the date gameobjects
    GameObject[] cards = new GameObject[6];

    public float SigningVideoWaitTime = 10f;

    private void Awake()
    {
        //hide all the date game objects
        foreach (GameObject d in dates)
        {
            d.SetActive(false);
        }

        //Get video players
        introVidPlayer = intro.GetComponent<VideoPlayer>();
        doorVidPlayer = doorVid.GetComponent<VideoPlayer>();

        timeline.SetActive(false);


    }

    private void Start()
    {
        // gets the cardholder in the date object in the date prefab
        for (int i = 0; i < dates.Length; i++)
        {
            cards[i] = dates[i].transform.GetChild(1).gameObject; 
        }
    }


    private void Update()
    {
        //if intro video hasn't finished
        if (!introDone)
        {
            CheckIntroVidEnd();
        }

        //If the door isn't open & all the cards have been viewed 
        if (!doorOpen && curDate == dates.Length)
        {
            Debug.Log("Video Timer on");
            //Open the door after 10s
            StartCoroutine(Timer(SigningVideoWaitTime, OpenDoor));
            doorOpen = true;

        }
    }

    //Place the AR objects based on user's wall tap position 
    public void ConfigureARPlaceObjects(Vector3 pos, Quaternion rot)
    {
        //reposition timeline
        timeline.transform.rotation = rot;
        timeline.transform.Rotate(-90, 0, 0);
        timeline.transform.Rotate(0, 90, 0);
        timeline.transform.Rotate(0, 0, 180);

        //Get the user's position based on camera
        Vector3 userPos = new Vector3(pos.x, Camera.main.transform.position.y, pos.z);
        timeline.transform.position = userPos - timelinePosRef.transform.position;

        //move the video to tap height
        intro.transform.position = new Vector3(intro.transform.position.x, pos.y, intro.transform.position.z);

        //move cards to middle of the wall; based on plane center
        foreach (GameObject c in cards)
        {
            Vector3 cardPos = c.transform.position;
            c.transform.position = new Vector3(cardPos.x, pos.y, cardPos.z);

        }

        //Scene Controller MovetoGround method
        MoveToGround(arFloor.transform.position.y, doors);

    }

    //Activates the timelie gameobject
    //Called when the video finishes playing
    public void ShowTimeline()
    {
        //Show first 3 dates
        foreach (GameObject d in dates)
        {
            d.SetActive(true);
        }
    }

    //Checks to see if the intro video has finished playing
    void CheckIntroVidEnd()
    {
        //if the intro vid is 6 secs from the end
        if (introVidPlayer.time > introVidPlayer.clip.length - 6)
        {
            introDone = true;
            ShowTimeline();
        }
    }

    //Plays the signing the civil rights act video then starts to open the door
    public void OpenDoor()
    {
        Debug.Log("Video started");
        doorVid.SetActive(true);
        doorVidPlayer.Play();
        StartCoroutine(OpenDoorCoroutine());
    }

    //Translates the doors to open them
    //Uses lerp for transformation
    IEnumerator OpenDoorCoroutine()
    {
        Debug.Log("Door Opening");

        //amount to offset
        Vector3 offset = doors[0].transform.right * 2.6f;
        //how long doors take to open
        float duration = 6.0f;
        //counter for time
        float time = 0.0f;

        //original door pos reference
        Vector3[] ogPos = {doors[0].transform.position,
                           doors[1].transform.position,
                           doors[2].transform.position};

        //wait for signing the civil rights at audio to finish playing
        yield return new WaitForSeconds(8);

        while (time <= duration)
        {
            time = time + Time.deltaTime;
            float percent = Mathf.Clamp01(time / duration);

            //Moves the door in the left direction; based on local direction of doors
            doors[0].transform.position = Vector3.Lerp(ogPos[0], ogPos[0] + offset, percent);
            doors[1].transform.position = Vector3.Lerp(ogPos[1], ogPos[1] + offset, percent);
            doors[2].transform.position = Vector3.Lerp(ogPos[2], ogPos[2] + offset, percent);

            yield return null;
        }

        //Activates button to switch scenes
        pathSceneButton.SetActive(true);
        yield return null;

    }

    //Increment the date value
    public void IncrementDateCount()
    {
        if(curDate < dates.Length)
        {
            curDate++;
        }
       
    }


    //getters and setters
    public GameObject GetTimeline()
    {
        return timeline;
    }

}
