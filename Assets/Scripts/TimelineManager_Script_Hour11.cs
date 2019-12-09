using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TimelineManager_Script_Hour11 : Timeline_BaseClass
{
    public float timelineScalar = 0.8f;
    

    public GameObject Boat;
    public AnimationCurve boatFloatStep;
    private float _animationTimePosition;

    float startYPosition;
    Vector3 boatPosStart = new Vector3(-9.5f, -2.5f, 0);
    Vector3 boatPosEnd = new Vector3(17.5f, -2.5f, 0);
    private float boatTravelDistance;
    public int numberOfBoatSegments = 9;
    public float durationOfBoatSegments = 4;

    public GameObject[] blessedDeath;
    public int numberOfBlessedSegemnts = 8;
    private int currentNumberOfBlessedSegements = 0;
    private float[] blessedTravelDistance;
    public float moveBlessedDeathThreshold = 0.0001f;

    private GameObject Cam;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(0, 0, -10);
    private float camTravelDistance;
    public float sceneLength;
    public float firstMoveDelay = 2.5f;
    public float cooldown = 0.5f;
    public bool cooldownOn = false;
    private float timeStamp = 0f;
    private bool waiting = false;


    void Awake()
    {
        
        boatTravelDistance = (boatPosEnd.x - boatPosStart.x) / numberOfBoatSegments;

        camPosEnd = new Vector3(sceneLength, 0, -10);
        camTravelDistance = (camPosEnd.x - camPosStart.x) / numberOfBoatSegments;
        Cam = Camera.main.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startScene(firstMoveDelay));
        AddTimelineEvent(0.1f, BoatActions);
        AddTimelineEvent(0.19f, BoatActions);
        AddTimelineEvent(0.29f, BoatActions);
        AddTimelineEvent(0.39f, BoatActions);
        AddTimelineEvent(0.49f, BoatActions);
        AddTimelineEvent(0.59f, BoatActions);
        AddTimelineEvent(0.69f, BoatActions);
        AddTimelineEvent(0.79f, BoatActions);
        AddTimelineEvent(0.89f, BoatActions);



        startYPosition = Boat.gameObject.transform.position.y;
        boatPosStart = new Vector3(boatPosStart.x, startYPosition, boatPosStart.z);
        boatPosEnd = new Vector3(boatPosEnd.x, startYPosition, boatPosEnd.z);

        HandleKeys();
    }
    private IEnumerator startScene(float delay)
    {
        waiting = true;
        yield return new WaitForSeconds(delay); 
        Timeline = 0.1f;
        waiting = false;

    }


    // Update is called once per frame
    void Update()
    {
        float input = Scroll.scrollValue();

        //Needs to be custom for each Hour --> must be implemented in specific hour instance of timeline_baseclass
        swipeBlessedDeath(input);

        //Kepri move by him self not as part of boat.?
    }

    private void swipeBlessedDeath(float input)
    {
        if (timeStamp <= Time.time)
        {
            cooldownOn = false;
        }
        if (input > moveBlessedDeathThreshold && Timeline >= 0.1f && !cooldownOn && !waiting)
        {
            Debug.Log("move Blessed");
            BlessedDeathToShore();
            timeStamp = Time.time + cooldown;
            cooldownOn = true;
            //change sprite and move a bit
        }
        else if (input < moveBlessedDeathThreshold)
        {
            Debug.Log("rubber band blessed death");
            //rubber band move blessed death
        }
    }

    private void BoatActions()
    {
        StartCoroutine(MoveBoat());
        StartCoroutine(MoveCam());
    }

    private IEnumerator MoveBoat()
    {
        float xStart = Boat.transform.position.x;
        float xEnd = xStart + boatTravelDistance;
        //float startTime = Time.time;

        while (Boat.transform.position.x < xEnd)
        {
            _animationTimePosition += Time.deltaTime;
            //float t = (Time.time - startTime) / durationOfBoatSegments;
            float step = Mathf.SmoothStep(xStart, xEnd, boatFloatStep.Evaluate(_animationTimePosition/durationOfBoatSegments));
            Boat.transform.position = new Vector3(step, boatPosStart.y, 0);
            yield return null;
        }
        if(Boat.transform.position.x >= xEnd)
        {
            _animationTimePosition = 0;
        }
        Boat.transform.position = new Vector3(xEnd, boatPosStart.y, 0);
    }

    private void BlessedDeathToShore()
    {
        
        if(currentNumberOfBlessedSegements-1 <= blessedDeath.Length)
        {
            blessedDeath[currentNumberOfBlessedSegements].GetComponent<Hour11_BlessedDeath_MoveToShore_Script>().moveToShore();
        }
        if (blessedDeath[currentNumberOfBlessedSegements].GetComponent<Hour11_BlessedDeath_MoveToShore_Script>().going == true)
        {
            Timeline += 0.1f;
            currentNumberOfBlessedSegements++;
        }   
    }

    private IEnumerator MoveCam()
    {
        float xStart = Cam.transform.position.x;
        float xEnd = xStart + camTravelDistance;
        float startTime = Time.time;

        while (Cam.transform.position.x < xEnd)
        {
            float t = (Time.time - startTime) / durationOfBoatSegments;
            float step = Mathf.SmoothStep(xStart, xEnd, t);
            Cam.transform.position = new Vector3(step, camPosStart.y, -10);
            yield return null;
        }
        Cam.transform.position = new Vector3(xEnd, camPosStart.y, -10);
    }
}
