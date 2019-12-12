using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using System;

public class TimelineManager_Script_Hour11 : Timeline_BaseClass
{
    public float timelineScalar = 0.8f;
    public GameObject Boat;
    public AnimationCurve boatFloatStep;
    private float _animationTimePosition;

    float startYPosition;
    Vector3 boatPosStart = new Vector3(-6.9f, -2.5f, 0);
    Vector3 boatPosEnd = new Vector3(20.5f, -2.5f, 0);
    private float boatTravelDistance;
    public int numberOfBoatSegments = 3;
    public float durationOfBoatSegments = 4;

    public GameObject[] blessedDeath;
    public int numberOfBlessedSegemnts = 16;
    private int currentNumberOfBlessedSegements = 0;
    private float[] blessedTravelDistance;
    public float moveBlessedDeathThreshold = 0.0001f;
    public BackGround_Object_Movement_Script dustballMovement;
    public KepriAnimationScript kepriMovement;

    private GameObject Cam;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(0, 0, -10);
    private float camTravelDistance;
    public float sceneLength;
    public float firstMoveDelay = 2.5f;
    public float cooldown = 0.5f;
    public bool cooldownOn = false;
    private float timeStamp = 0f;
    private float timeStampShake = 0f;
    private float shakeDuration = 0.5f;
    private bool waiting = false;
    public bool running = false;
    public bool cooldownOnShake;
    private float currentX;

    public float boatEndX;
    public float speed;


    void Awake()
    {
        
        boatTravelDistance = (boatPosEnd.x - boatPosStart.x) / numberOfBoatSegments;
        boatEndX = Boat.transform.position.x;
        boatEndX += boatTravelDistance;
        camPosEnd = new Vector3(sceneLength, 0, -10);
        camTravelDistance = (camPosEnd.x - camPosStart.x) / numberOfBoatSegments;
        Cam = Camera.main.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentX = Boat.transform.position.x;
        AddTimelineEvent(0.25f, BoatActions);
        AddTimelineEvent(0.5f, BoatActions);
        AddTimelineEvent(0.75f, BoatActions);
        AddTimelineEvent(1f, BoatActions);



        startYPosition = Boat.gameObject.transform.position.y;
        boatPosStart = new Vector3(boatPosStart.x, startYPosition, boatPosStart.z);
        boatPosEnd = new Vector3(boatPosEnd.x, startYPosition, boatPosEnd.z);
        

        HandleKeys();
    }


    // Update is called once per frame
    void Update()
    {
        float input = Scroll.scrollValueAccelerated();
        //Debug.Log(input);
        //Needs to be custom for each Hour --> must be implemented in specific hour instance of timeline_baseclass
        swipeBlessedDeath(input);
        MovingBoat();
        //ConvertInputToProgress(input);
        //Kepri move by him self not as part of boat.?
    }

    private void MovingBoat()
    {
        currentX = Boat.transform.position.x;
        //Debug.Log(currentX + " " + boatEndX);
        if(currentX < boatEndX-1f)
        {
            kepriMovement.enabled = true;
            Boat.transform.position += transform.right * ((Time.deltaTime*speed)*Vector3.Distance(new Vector3(boatEndX,Boat.transform.position.y,Boat.transform.position.z),Boat.transform.position));
            kepriMovement.animationTime =  2f;//(Time.deltaTime*speed)*Vector3.Distance(new Vector3(boatEndX,Boat.transform.position.y,Boat.transform.position.z),Boat.transform.position);
            dustballMovement.rotateSpeed = ((Time.deltaTime*-speed)*Vector3.Distance(new Vector3(boatEndX,Boat.transform.position.y,Boat.transform.position.z),Boat.transform.position))*55f;
            Debug.Log(kepriMovement.animationTime);
        }
        else
        {            
            kepriMovement.enabled = false;//(Time.deltaTime*speed)*Vector3.Distance(new Vector3(boatEndX,Boat.transform.position.y,Boat.transform.position.z),Boat.transform.position);
            Boat.transform.position = Boat.transform.position;
            dustballMovement.rotateSpeed = 0;

        }

        
        //while boatEndX is > currentX
        // Move forward else stop
    }

    private void swipeBlessedDeath(float input)
    {
        if (timeStamp <= Time.time)
        {
            cooldownOn = false;
        }
        if (input > moveBlessedDeathThreshold && !cooldownOn && !waiting)
        {
            Debug.Log("move Blessed");
            //Debug.Log(cooldownOn);
            BlessedDeathToShore();
            timeStamp = Time.time + cooldown;
            cooldownOn = true;
            //change sprite and move a bit
        }
        else if (input < -0.02 && !cooldownOnShake)
        {
            cooldownOnShake = true;            
            BlessedDeathRubberBand();
            StartCoroutine(coolDowner(0.75f));
        }
    }
    private IEnumerator coolDowner(float time)
    {
        yield return new WaitForSeconds(time);
        cooldownOnShake = false;
    }

    private void BoatActions()
    {
        if(!running)
        {
            boatEndX += boatTravelDistance;
        }
        
        StartCoroutine(MoveCam());
    }

    private IEnumerator MoveBoat()
    {
        running = true;
        float xStart = Boat.transform.position.x;
        float xEnd = xStart + boatTravelDistance;

        while (System.Math.Round(Boat.transform.position.x,2) < System.Math.Round(xEnd,2))
        {
            //Debug.Log("Boat X " + System.Math.Round(Boat.transform.position.x,3) + " " + "xEnd " + System.Math.Round(xEnd,3));
            _animationTimePosition += Time.deltaTime;
            float step = Mathf.SmoothStep(xStart, xEnd, boatFloatStep.Evaluate(_animationTimePosition/durationOfBoatSegments));
            Boat.transform.position = new Vector3(step, boatPosStart.y, 0);
            //Debug.Log(_animationTimePosition/durationOfBoatSegments);
            yield return null;
        }
        _animationTimePosition = 0;
        Boat.transform.position = new Vector3(xEnd, boatPosStart.y, 0);
        running = false;
    }

    private void BlessedDeathToShore()
    {
        
        if(currentNumberOfBlessedSegements-1 <= blessedDeath.Length)
        {
            blessedDeath[currentNumberOfBlessedSegements].GetComponent<Hour11_BlessedDeath_MoveToShore_Script>().moveToShore();
        }
        if (blessedDeath[currentNumberOfBlessedSegements].GetComponent<Hour11_BlessedDeath_MoveToShore_Script>().going == true)
        {
            Timeline += 0.05f;
            currentNumberOfBlessedSegements++;
        }   
    }

       private void BlessedDeathRubberBand()
    {
        
        for (int i = 0; i < blessedDeath.Length; i++)
        {
            //Debug.Log("rubber band blessed death");
            blessedDeath[i].GetComponent<Hour11_BlessedDeath_MoveToShore_Script>().Shake(blessedDeath[i].transform.position.x,blessedDeath[i].transform.position.y); 
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
