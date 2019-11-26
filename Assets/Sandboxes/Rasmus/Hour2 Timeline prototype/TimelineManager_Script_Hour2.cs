using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using System;

public class TimelineManager_Script_Hour2 : Timeline_BaseClass
{
    public float timelineScalar = 0.8f;

    //Boat
    public GameObject Boat;
    public Transform boatStart;
    public Transform boatEnd;

    Vector3 boatPosStart;
    Vector3 boatPosEnd;
    private float boatTravelDistance;
    public int numberOfBoatSegments = 5;
    public float durationOfBoatSegments = 2;

    //Trees
    public ActivateTreeGrowth_Script_Hour2[] treeActivators;
    private int treeCounter = 0;

    //Spraying statues
    public WheatRain_Script_Hour2 wheatSpray;

    //Cam movement
    private GameObject Cam;
    public float sceneLength = 2;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(0, 0, -10);


    //Initialization of variables and start states
    void Awake()
    {
        //Boat variables
        boatPosStart = boatStart.position;
        boatPosEnd = boatEnd.position;
        boatTravelDistance = (boatPosEnd.x - boatPosStart.x) / numberOfBoatSegments;

        //Camera
        Cam = Camera.main.gameObject;
        camPosEnd = new Vector3(sceneLength, 0, -10);
    }

    //Add timeline events and HandleKeys(). Remember HandleKeys() at the end, or nothing works
    void Start()
    {
        //Boat movement
        AddTimelineEvent(0.0f, BoatActions);
        AddTimelineEvent(0.2f, BoatActions);
        AddTimelineEvent(0.4f, BoatActions);
        AddTimelineEvent(0.6f, BoatActions);
        AddTimelineEvent(0.7f, BoatActions);
        AddTimelineEvent(0.9f, BoatActions);

        AddTimelineEvent(0.6f, wheatSpray.StartSpraying);
        //Trees
        for (int i = 0; i < treeActivators.Length; i++)
        {
            AddTimelineEvent(treeActivators[i].GetTimelinePosition(), TreeActivationAction);
        }
        HandleKeys();
    }


    void Update()
    {
        float input = Scroll.scrollValue();
        //Needs to be custom for each Hour --> must be implemented in specific hour instance of timeline_baseclass
        ConvertInputToProgress(input);
        CamAction();
    }

    private void ConvertInputToProgress(float input)
    {
        if (input < 0)
        {
            float speed = Mathf.Abs(input) * timelineScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.001f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            print("Timeline: " + Timeline);
        }
    }


    // GAME OBJECT FUNCTIONS
    private void BoatActions()
    {
        StartCoroutine(MoveBoat());
    }

    private IEnumerator MoveBoat()
    {
        FMOD.Studio.EventInstance boatPaddleInstance = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BoatPaddle");
        boatPaddleInstance.start();

        print("MoveBoat");
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

    private void TreeActivationAction()
    {
        treeActivators[treeCounter].EnableTree();
        treeCounter++;
    }

    private float GetTreeRelativePosition(float xPos)
    {
        return Mathf.InverseLerp(boatPosStart.x, boatPosEnd.x, xPos);
    }

    private void CamAction()
    {
        Cam.transform.position = Vector3.Lerp(camPosStart, camPosEnd, Timeline);
    }
}
