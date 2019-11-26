using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TimelineManager_Script_Hour11 : Timeline_BaseClass
{
    public float timelineScalar = 0.8f;
    

    public GameObject Boat;

    float startYPosition;
    Vector3 boatPosStart = new Vector3(-9.9f, -2.5f, 0);
    Vector3 boatPosEnd = new Vector3(10.4f, -2.5f, 0);
    private float boatTravelDistance;
    public int numberOfBoatSegments = 5;
    public float durationOfBoatSegments = 2;

    public GameObject[] blessedDeath;
    public int numberOfBlessedSegemnts = 7;
    private int currentNumberOfBlessedSegements = 0;
    private float[] blessedTravelDistance;

    private GameObject Cam;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(0, 0, -10);
    public float sceneLength;


    void Awake()
    {
        
        boatTravelDistance = (boatPosEnd.x - boatPosStart.x) / numberOfBoatSegments;

        camPosEnd = new Vector3(sceneLength, 0, -10);
        Cam = Camera.main.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        AddTimelineEvent(0.1f, BoatActions);
        AddTimelineEvent(0.3f, BoatActions);
        AddTimelineEvent(0.5f, BoatActions);
        AddTimelineEvent(0.7f, BoatActions);
        AddTimelineEvent(0.99f, BoatActions);

        AddTimelineEvent(0.4f, BlessedDeathToShore);
        AddTimelineEvent(0.6f, BlessedDeathToShore);
        AddTimelineEvent(0.8f, BlessedDeathToShore);
        AddTimelineEvent(0.9f, BlessedDeathToShore);

        startYPosition = Boat.gameObject.transform.position.y;
        boatPosStart = new Vector3(boatPosStart.x, startYPosition, boatPosStart.z);
        boatPosEnd = new Vector3(boatPosEnd.x, startYPosition, boatPosEnd.z);

        HandleKeys();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Scroll.scrollValueAccelerated();

        //Needs to be custom for each Hour --> must be implemented in specific hour instance of timeline_baseclass
        ConvertInputToProgress(input);

        CamAction();
    }

        private void ConvertInputToProgress(float input)
    {
        if (input > 0)
        {
            float speed = Scroll.scrollValueAccelerated(0.99999f) * timelineScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.001f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            print("Speed: " + speed);
            print("Timeline" + Timeline);
        }
    }

    private void BoatActions()
    {
        StartCoroutine(MoveBoat());
    }

    private IEnumerator MoveBoat()
    {
        //print("MoveBoat");
        float xStart = Boat.transform.position.x;
        float xEnd = xStart + boatTravelDistance;
        float startTime = Time.time;

        while (Boat.transform.position.x < xEnd)
        {
            float t = (Time.time - startTime) / durationOfBoatSegments;
            float step = Mathf.SmoothStep(xStart, xEnd, t);
            Boat.transform.position = new Vector3(step, boatPosStart.y, 0);
            yield return null;
        }
        Boat.transform.position = new Vector3(xEnd, boatPosStart.y, 0);
    }

    private void BlessedDeathToShore()
    {
        
        if(currentNumberOfBlessedSegements-1 <= blessedDeath.Length)
        {
            blessedDeath[currentNumberOfBlessedSegements].GetComponent<Hour11_BlessedDeath_MoveToShore_Script>().moveToShore();
        }
        currentNumberOfBlessedSegements++;
    }


    private void CamAction()
    {
        Cam.transform.position = Vector3.Lerp(camPosStart, camPosEnd, Timeline);
    }    
}
