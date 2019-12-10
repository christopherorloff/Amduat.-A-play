using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline_BaseClass : MonoBehaviour
{
    //TODO: Higher level of security needed!
    private float timeline = 0;

    protected float Timeline
    {
        get { return timeline; }
        set { timeline = value; }
    }

    // Handles all timings and what Actions/functions to be called
    protected SortedDictionary<float, List<Action>> timelineEvents = new SortedDictionary<float, List<Action>>();
    protected Queue<float> keys = new Queue<float>();
    protected bool timelineEventsEmpty = false;

    void LateUpdate()
    {
        if (!timelineEventsEmpty)
            CheckForTimelineEvents();
    }


    // --------------------------------- //
    //    Timeline related behaviour     //
    // --------------------------------- //


    protected void CheckForTimelineEvents()
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

    protected void AddTimelineEvent(float percentageInvoke, Action action)
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

    protected void HandleKeys()
    {
        if (timelineEvents.Count == 0)
        {
            print("No timeline events");
            timelineEventsEmpty = true;
            return;
        }
        float[] tempKeys = new float[timelineEvents.Keys.Count];
        timelineEvents.Keys.CopyTo(tempKeys, 0);
        for (int i = 0; i < tempKeys.Length; i++)
        {
            keys.Enqueue(tempKeys[i]);
        }
    }

    public float GetTimeline()
    {
        return Timeline;
    }
}
