using UnityEngine;

//Controller for the Path Scene methods
public class PathSceneController : SceneController
{
    //Game objects that need to be placed on the floor (anim, anim markers, inter marker)
    //Game objects to place on the floor
    [SerializeField] GameObject[] toPlace;
    //Reference to arFloor
    [SerializeField] GameObject arFloor;
    //Reference to the pillar position (outside the "Scene Objects" game object)
    [SerializeField] GameObject pillarRef;
    //Reference to the scene position (inside the "Scene Object" game object)
    [SerializeField] GameObject scenePosReference;
    //Reference to the path markers
    [SerializeField] GameObject path;
    //Scene objects
    [SerializeField] GameObject scene;

    //Controller scripts
    [Header("Controller Scripts")]
    [SerializeField] InterstitialsPathController ic;
    [SerializeField] AnimationPathController ac;
    [SerializeField] FourMinistersController fmc;

    //UI elements
    [Header("UI Elements")]
    [SerializeField] GameObject WalkToSign;
    [SerializeField] GameObject EndSceneUIGooger;

    //the current marker index
    int curMarkerIndex = 0;
    //if the end credit scene has started
    bool endSceneStarted = false;

    private void Start()
    {
        //start scanning for the pillar
        fmc.StartScanning();

        //Move objects to ground
        MoveToGround(ARInfo.Instance.GetFloorPosition().y, toPlace);
    }

    //Show the next interstital marker
    public void ShowNextIntersitialMarker()
    {
        //hide current animation
        ac.HideAnimMarker(curMarkerIndex);
        ac.HideAnimation(curMarkerIndex);
        ac.TurnOffAnimDateUI(curMarkerIndex);

        //increment counter
        curMarkerIndex++;

        //show next itnerstitial marker
        ic.ShowIntersitialMarker(curMarkerIndex);
    }

    //Path Marker specific methods
    //Show the marker at the current index
    public void ShowNextAnimMarker()
    {
        //hide the current interstitial
        ic.HideInterstitial(curMarkerIndex);

        //turn on next animation marker
        ac.ShowAnimMarker(curMarkerIndex);

        if (curMarkerIndex == 0)
        {
            ShowWalkToSign();
        }
    }

    //called by the ar animation event emitter
    //component attached directly to the timeline gameobjects
    public void ShowNextInterButton()
    {
        ac.ShowNextInterButton(curMarkerIndex);
    }

    //position the scene to be in the right location
    //based on scanned pillar position
    public void PositionAR()
    {
        //Debug.Log("Path AR Positioned");
        //Debug.Log("pillar Pos" + pillarRef.transform.position);

        scene.transform.rotation = pillarRef.transform.rotation;
        scene.transform.Rotate(0, 90, 0);

        Vector3 pillarPos = new Vector3(pillarRef.transform.position.x, 0, pillarRef.transform.position.z);
        scene.transform.position = pillarPos;
        path.SetActive(true);
    }

    //Starts the inner path
    //Shows the first path marker
    public void StartInnerPathAR()
    {
        ic.ShowIntersitialMarker(0);
        WalkToSign.SetActive(true);
        fmc.HideMinisterCards();
    }

    //Hides the walk to marker sign
    public void HideWalkToSign()
    {
        WalkToSign.SetActive(false);
    }

    //Shows the walk to marker sign
    public void ShowWalkToSign()
    {
        WalkToSign.SetActive(true);
    }

    //Show the interstitial at the current marker index
    public void ShowInterstitial()
    {
        ic.TrackPillar(curMarkerIndex);

    }

    //Shows the animation at the current marker index
    public void ShowAnimation()
    {
        ac.ShowAnimation(curMarkerIndex);

        //if last animation has been viewed start the end scene timer
        if(curMarkerIndex == 4)
        {
            EndSceneTimer();
        }
    }

    //Turn on the Newspaper Date UI Overlay for animations
    public void TurnOnAnimDateUI()
    {
        ac.TurnOnAnimDateUI(curMarkerIndex);
    }

    //Starts the credit scene with Jack Googer
    public void ShowEndSceneGooger()
    {
        endSceneStarted = true;
        EndSceneUIGooger.SetActive(true);
    }

    //Timer to trigger the end scene if there's no collision with the restaurant door
    public void EndSceneTimer()
    {
        StartCoroutine(Timer(45.0f,CheckEndSceneTriggered));

        void CheckEndSceneTriggered()
        {
            if (!endSceneStarted)
            {
                endSceneStarted = true;
                ShowEndSceneGooger();
            }
        }
    }

    //Getters & Setters
    public int GetCurrentMarkerIndex()
    {
        return curMarkerIndex;
    }

}
