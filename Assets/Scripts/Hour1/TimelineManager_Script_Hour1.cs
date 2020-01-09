using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TimelineManager_Script_Hour1 : Timeline_BaseClass
{
    public float timelineScalar = 0.8f;
    public FadeUIScript fadeUIScript;

    //Sun
    public GameObject Sun;
    private Vector3 sunPosStart = new Vector3(-7.75f, 1.75f, 0);
    private Vector3 sunPosEnd = new Vector3(7.75f, -2.0f, 0);
    private Color sunColorStart = Color.white;
    public Color sunGlowStart;
    public Color SunGlowEnd;
    public SpriteRenderer secondSun;
    public Color secondSunStartColor;
    public Color secondSunEndColor;

    public Color sunColorEnd;
    private SpriteRenderer SunColor;
    public SpriteRenderer sunGlow;

    //Boat
    public GameObject Boat;

    public AnimationCurve boatFloatStep;
    private float _animationTimePosition;
    float startYPosition;
    Vector3 boatPosStart = new Vector3(-9.9f, -2.5f, 0);
    Vector3 boatPosEnd = new Vector3(10.4f, -2.5f, 0);
    private float boatTravelDistance;
    public int numberOfBoatSegments = 5;
    public float durationOfBoatSegments = 2;
    public float maxBlessedAlpha = 0.65f;

    private IEnumerator boatMovement;
    private bool running = false;
    private float leftoverDistance = 0;

    //Blessed dead
    public BlessedDeadFollow_Script_Hour1 blessedDeadController;
    private SpriteRenderer[] blessedDeadSprites;
    private int blessedDeadCounter = 0;
    private float blessedDeadFadeDuration = 2.5f;

    //Baboons
    public RaiseStatue_Script_Hour1 SolarBaboons;

    //Background
    public SpriteRenderer Background;
    private Color BGColorStart;
    public Color BGColorEnd;

    //LightCone
    public SpriteRenderer LightCone;
    private Color lightConeColorStart;
    public Color lightConeColorEnd;

    //Cam movement
    private GameObject Cam;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(0, 0, -10);
    public float sceneLength;
    private bool fadeHasStarted = false;

    //Wrong input
    public General_WrongInput_Movement_Script wrongInput;
    public GameObject rubberMarkerEnd;
    public GameObject rubberMarkerStart;

    public float rubberMarkerDelta = -0.25f;

    void Awake()
    {

        sunPosEnd += new Vector3(sceneLength, 0, 0);
        boatPosEnd += new Vector3(sceneLength, 0, 0);

        lightConeColorStart = LightCone.color;
        BGColorStart = Background.color;

        ColorUtility.TryParseHtmlString("#EC1E1E", out sunColorEnd);
        // ColorUtility.TryParseHtmlString("#64371B", out BGColorEnd);
        // ColorUtility.TryParseHtmlString("#FFE8C5", out lightConeColorEnd);
        lightConeColorEnd = lightConeColorStart;
        lightConeColorEnd.a = 0.7f;

        SunColor = Sun.GetComponent<SpriteRenderer>();

        boatTravelDistance = (boatPosEnd.x - boatPosStart.x) / numberOfBoatSegments;


        camPosEnd = new Vector3(sceneLength, 0, -10);
        Cam = Camera.main.gameObject;

    }

    //This is where all timeline events should be added
    //HandleKeys call must be the last thing AFTER all events are added
    void Start()
    {
        blessedDeadSprites = new SpriteRenderer[blessedDeadController.numberOfBlessedDead];
        blessedDeadSprites = blessedDeadController.GetBlessedDeadSprites();


        //Add all timeline events
        AddTimelineEvent(0.001f, BoatActions);
        AddTimelineEvent(0.3f, BoatActions);
        AddTimelineEvent(0.5f, BoatActions);
        AddTimelineEvent(0.7f, BoatActions);
        AddTimelineEvent(0.99f, BoatActions);

        AddTimelineEvent(0.6f, SolarBaboons.StartRaisingStatues);

        AddTimelineEvent(0.2f, SoundManager.Instance.PlayTheme);

        // Blessed dead sprites
        for (int i = 0; i < blessedDeadSprites.Length; i++)
        {
            AddTimelineEvent(UnityEngine.Random.Range(0.4f, 0.8f), StartBlessedDeadFadeIn);
        }

        //After all timeline events are added
        HandleKeys();

        //Setting starting y position for boat
        startYPosition = Boat.gameObject.transform.position.y;
        boatPosStart = new Vector3(boatPosStart.x, startYPosition, boatPosStart.z);
        boatPosEnd = new Vector3(boatPosEnd.x, startYPosition, boatPosEnd.z);
    }

    //Order: Take input --> convert input --> CheckForTimelineEvents --> Apply linear functions
    void Update()
    {
        float input = Scroll.scrollValueAccelerated();

        //Needs to be custom for each Hour --> must be implemented in specific hour instance of timeline_baseclass

        if (wrongInput.wrongInput == false)
        {
            ConvertInputToProgress(input);
            SunActions();
            BGActions();
            CamAction();
            rubberAction();
        }
        CheckTimelineProgress();
    }

    private void CheckTimelineProgress()
    {
        if (Timeline == 1 && !fadeHasStarted)
        {
            StartCoroutine(StartFadeOut());
            fadeHasStarted = true;
        }
    }

    private IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(durationOfBoatSegments);
        fadeUIScript.StartFadeOut();
    }



    // Functions directly connected to the timeline variable below

    private void ConvertInputToProgress(float input)
    {
        if (input > 0)
        {
            float speed = Scroll.scrollValueAccelerated() * timelineScalar * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.0004f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
        }
    }

    private void StartBlessedDeadFadeIn()
    {
        StartCoroutine(BlessedDeadFadeIn(blessedDeadCounter));
        blessedDeadCounter++;
    }

    IEnumerator BlessedDeadFadeIn(int counter)
    {
        FMOD.Studio.EventInstance blessedDeadAppearInstance = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BlessedDeadAppear");
        blessedDeadAppearInstance.start();

        float startTime = Time.time;
        print("BlessedDead " + blessedDeadCounter + " event, at " + Timeline);
        while (blessedDeadSprites[counter].color.a < maxBlessedAlpha)
        {
            Color col = blessedDeadSprites[counter].color;
            float t = (Time.time - startTime) / blessedDeadFadeDuration;
            float step = Mathf.SmoothStep(0, 1, t);
            step = Mathf.Clamp(step, 0, maxBlessedAlpha);
            Color temp = new Color(col.r, col.g, col.b, step);
            blessedDeadSprites[counter].color = temp;
            yield return null;
        }
    }

    private void SunActions()
    {
        secondSun.color = Color.Lerp(secondSunStartColor, secondSunEndColor, Timeline);
        Sun.transform.position = Vector3.Lerp(sunPosStart, sunPosEnd, Timeline);
        SunColor.color = Color.Lerp(sunColorStart, sunColorEnd, Timeline);
        sunGlow.color = Color.Lerp(sunGlowStart, SunGlowEnd, Timeline);
    }

    private void BGActions()
    {
        Background.color = Color.Lerp(BGColorStart, BGColorEnd, Timeline);
        LightCone.color = Color.Lerp(lightConeColorStart, lightConeColorEnd, Timeline);
    }

    // Timeline Event functions below
    private void BoatActions()
    {
        if (running)
        {
            StopCoroutine(boatMovement);
            running = false;
        }
        boatMovement = MoveBoat();
        StartCoroutine(MoveBoat());
    }

    private IEnumerator MoveBoat()
    {
        running = true;
        FMOD.Studio.EventInstance boatPaddleInstance = FMODUnity.RuntimeManager.CreateInstance("event:/GENERAL SOUNDS/BoatPaddle");
        boatPaddleInstance.start();

        float xStart = Boat.transform.position.x;
        float xEnd = xStart + boatTravelDistance + leftoverDistance;
        leftoverDistance = 0;
        float startTime = Time.time;

        while (Boat.transform.position.x < xEnd)
        {
            _animationTimePosition += Time.deltaTime;
            float t = (Time.time - startTime) / durationOfBoatSegments;
            t = 1 - (1 - t) * (1 - t);
            float step = Mathf.SmoothStep(xStart, xEnd, t);//boatFloatStep.Evaluate(_animationTimePosition / durationOfBoatSegments));
            Boat.transform.position = new Vector3(step, boatPosStart.y, 0);
            leftoverDistance = xEnd - step;
            yield return null;
        }
        if (Boat.transform.position.x >= xEnd)
        {
            _animationTimePosition = 0;
        }
        leftoverDistance = 0;
        Boat.transform.position = new Vector3(xEnd, boatPosStart.y, 0);

    }

    private void CamAction()
    {
        Cam.transform.position = Vector3.Lerp(camPosStart, camPosEnd, Timeline);
    }

    private void rubberAction()
    {
        rubberMarkerEnd.transform.position = Vector3.Lerp(sunPosStart + new Vector3(rubberMarkerDelta, 0, 0), sunPosEnd + new Vector3(rubberMarkerDelta, 0, 0), Timeline);
        rubberMarkerStart.transform.position = Vector3.Lerp(sunPosStart, sunPosEnd, Timeline);
    }
}

