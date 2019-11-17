using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class TimelineManager_Script_Hour1 : MonoBehaviour
{
    private float timeline = 0;

    // Handles all timings and what Actions/functions to be called
    SortedDictionary<float, List<Action>> timelineEvents = new SortedDictionary<float, List<Action>>();
    Queue<float> keys = new Queue<float>();
    private bool timelineEventsEmpty = false;
    public float timelineScalar = 0.8f;

    // Gameobjects for interacting with


    //Sun
    public GameObject Sun;
    private Vector3 sunPosStart = new Vector3(-7.75f, 1.75f, 0);
    private Vector3 sunPosEnd = new Vector3(7.75f, -2.0f, 0);
    private Color sunColorStart = Color.white;
    public Color sunColorEnd;
    private SpriteRenderer SunColor;

    //Boat
    public GameObject Boat;

    Vector3 boatPosStart = new Vector3(-8.9f, -2.25f, 0);
    Vector3 boatPosEnd = new Vector3(7.4f, -2.25f, 0);
    private float boatTravelDistance;
    public int numberOfBoatSegments = 5;
    public float durationOfBoatSegments = 2;

    //Background
    public SpriteRenderer Background;
    private Color BGColorStart;
    private Color BGColorEnd;

    //LightCone
    public SpriteRenderer LightCone;
    private Color lightConeColorStart;
    public Color lightConeColorEnd;

    void Awake()
    {
        lightConeColorStart = LightCone.color;
        BGColorStart = Background.color;

        ColorUtility.TryParseHtmlString("#EC1E1E", out sunColorEnd);
        // ColorUtility.TryParseHtmlString("#64371B", out BGColorEnd);
        // ColorUtility.TryParseHtmlString("#FFE8C5", out lightConeColorEnd);
        lightConeColorEnd = lightConeColorStart;
        lightConeColorEnd.a = 0.7f;

        SunColor = Sun.GetComponent<SpriteRenderer>();

        boatTravelDistance = (boatPosEnd.x - boatPosStart.x) / numberOfBoatSegments;

    }

    void Start()
    {
        AddTimelineEvent(0.2f, BoatActions);
        AddTimelineEvent(0.4f, BoatActions);
        AddTimelineEvent(0.6f, BoatActions);
        AddTimelineEvent(0.8f, BoatActions);
        AddTimelineEvent(0.99f, BoatActions);

        HandleKeys();
    }

    void Update()
    {
        float input = Scroll.scrollValueAccelerated();
        ConvertInputToProgress(input);

        if (!timelineEventsEmpty)
            CheckForTimelineEvents();

        SunActions();
        BGActions();



    }

    // Functions directly connected to the timeline variable below

    private void SunActions()
    {
        Sun.transform.position = Vector3.Lerp(sunPosStart, sunPosEnd, timeline);
        SunColor.color = Color.Lerp(sunColorStart, sunColorEnd, timeline);
    }

    private void BGActions()
    {
        Background.color = Color.Lerp(BGColorStart,BGColorEnd, timeline);
        LightCone.color = Color.Lerp(lightConeColorStart, lightConeColorEnd, timeline);
    }

    // Timeline Event functions below
    private void BoatActions()
    {
        StartCoroutine(MoveBoat());
    }

    private IEnumerator MoveBoat()
    {
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

    // "Abstract" part of class below
    // Handles the timeline and the actions connected to it

    private void CheckForTimelineEvents()
    {
        if (timeline >= keys.Peek())
        {
            foreach (var action in timelineEvents[keys.Peek()])
            {
                action();
            }
            keys.Dequeue();
            if (keys.Count <= 0)
            {
                timelineEventsEmpty = true;
            }

        }
    }

    private void ConvertInputToProgress(float input)
    {
        if (input > 0)
        {
            timeline += Scroll.scrollValueAccelerated(0.99999f) * timelineScalar * Time.deltaTime;
            timeline = Mathf.Clamp(timeline, 0, 1);
            print("Timeline: " + timeline);

        }
    }

    private void AddTimelineEvent(float percentageInvoke, Action action)
    {
        if (timelineEvents.ContainsKey(percentageInvoke))
        {
            timelineEvents[percentageInvoke].Add(action);
        }
        else
        {
            timelineEvents[percentageInvoke] = new List<Action> { action };
        }
    }

    private void HandleKeys()
    {
        float[] tempKeys = new float[timelineEvents.Keys.Count];
        timelineEvents.Keys.CopyTo(tempKeys, 0);
        for (int i = 0; i < tempKeys.Length; i++)
        {
            keys.Enqueue(tempKeys[i]);
        }
    }

    private void TimelineEventInvoked() { print("Like this"); }
    private void TimelineEventInvokedToo() { print("Like this also"); }


}

