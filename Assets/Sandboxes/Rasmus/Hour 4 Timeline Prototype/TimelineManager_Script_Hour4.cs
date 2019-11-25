using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using System;

public class TimelineManager_Script_Hour4 : Timeline_BaseClass
{
    // General
    bool nextIsDraw = true;
    bool coroutineRunning = false;
    public int numberOfPulls = 5;
    private Vector3 boatMoveVector;

    //Boat
    public GameObject boat;
    public Transform startPosBoat;
    public Transform endPosBoat;
    private Vector3 boatDistanceToGoddesses;

    //Goddesses
    public Transform goddessesParent;
    private Transform[] goddesses;
    public float drawDuration = 2;
    public float pullDuration = 3;
    public float rotationAfterDraw = 25;
    public float rotationAfterPull = -25;


    void Awake()
    {
        goddesses = new Transform[goddessesParent.childCount];
        for (int i = 0; i < goddessesParent.childCount; i++)
        {
            goddesses[i] = goddessesParent.GetChild(i);
        }

        //Boat can only move on x-axis. If y-axis is needed, create similar float to travelDistanceX and input in boatMoveVector
        float travelDistanceX = (endPosBoat.position.x - startPosBoat.position.x) / numberOfPulls;
        boatMoveVector = new Vector3(travelDistanceX, 0, 0);
        boatDistanceToGoddesses = new Vector3(goddessesParent.position.x - boat.transform.position.x, goddessesParent.position.y - boat.transform.position.y, 0);
    }

    void OnEnable()
    {
        Scroll.OnScrollEnter += ConvertInputToProgress;
    }

    void OnDisable()
    {
        Scroll.OnScrollEnter -= ConvertInputToProgress;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Add timeline events here 

        HandleKeys();
    }

    void ConvertInputToProgress()
    {
        float input = Scroll.scrollValue();
        bool sign = GetSignBool(input);

        //For debug purposes
        if (Mathf.Approximately(input, 0.0f))
        {
            print("Input is zero!");
        }

        if (Mathf.Approximately(Timeline, 1) && !coroutineRunning)
        {
            //TRIGGER FADE OUT
            //CHANGE TO NEXT LEVEL
            //ALL THAT JAZZ...
            return;
        }

        //Check if correct order of up/down scroll
        if (nextIsDraw == sign)
        {
            //Only continue if the coroutine is not already running
            if (!coroutineRunning)
            {
                //Add percentage to timeline
                Timeline += (1 / numberOfPulls / 2);
                if (nextIsDraw)
                {
                    StartCoroutine(Draw());
                    nextIsDraw = false;
                }
                else
                {
                    StartCoroutine(Pull());
                    nextIsDraw = true;
                }
            }
        }
        else
        {
            //HERE FEEDBACK FOR WRONG SCROLL CAN BE PROCESSED
        }


    }

    private IEnumerator Draw()
    {
        FMOD.Studio.EventInstance collectEnergyInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 4/CollectEnergy");
        collectEnergyInstance.start();

        coroutineRunning = true;
        print("Pull coroutine");
        float startTime = Time.time;
        Quaternion startRotation = goddesses[0].rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, rotationAfterDraw);
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = (t * t * t);
            print(t);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddesses[i].rotation = Quaternion.Lerp(startRotation, endRotation, t);
                yield return null;
            }

        }
        coroutineRunning = false;
    }

    private IEnumerator Pull()
    {
        FMOD.Studio.EventInstance towBoatInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 4/TowBoat");
        towBoatInstance.start();

        coroutineRunning = true;
        print("Draw coroutine");
        float startTime = Time.time;
        Quaternion startRotation = goddesses[0].rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, rotationAfterPull);
        Vector3 startPositionGoddesses = goddessesParent.transform.position;
        Vector3 endPositionGoddesses = startPositionGoddesses + boatMoveVector;
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = (t * t * t * t * t);
            print(t);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddesses[i].rotation = Quaternion.Lerp(startRotation, endRotation, t);
                goddessesParent.position = Vector3.Lerp(startPositionGoddesses, endPositionGoddesses, t);
                boat.transform.position = Vector3.Lerp(startPositionGoddesses - boatDistanceToGoddesses, endPositionGoddesses - boatDistanceToGoddesses, t);
                yield return null;
            }

        }
        coroutineRunning = false;
        yield return null;
    }

    bool GetSignBool(float input)
    {
        if (input < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
