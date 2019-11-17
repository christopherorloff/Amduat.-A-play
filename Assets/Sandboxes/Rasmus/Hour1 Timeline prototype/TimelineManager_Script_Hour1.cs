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

    // Gameobjects for interacting with
    public GameObject Sun;


    public GameObject Boat;
    public SpriteRenderer SunColor;
    public SpriteRenderer Background;
    public SpriteRenderer LightCone;

    void Start()
    {
        AddTimelineEvent(0.3f, (Action)TimelineEventInvokedToo);
        AddTimelineEvent(0.1f, (Action)TimelineEventInvoked);
        AddTimelineEvent(0.6f, (Action)TimelineEventInvoked);

        HandleKeys();
    }

    void Update()
    {
        float input = Scroll.scrollValueAccelerated();
        ConvertInputToProgress(input);

        if (!timelineEventsEmpty)
            CheckForTimelineEvents();


        if (Input.anyKeyDown)
        {
            foreach (KeyValuePair<float, List<Action>> dict in timelineEvents)
            {
                foreach (var item in dict.Value)
                {
                    print(dict.Key);
                    item();
                }
            }
        }
    }

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
            timeline += Scroll.scrollValueAccelerated() * Time.deltaTime;
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

