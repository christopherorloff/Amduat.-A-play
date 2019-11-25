using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class FoliageRiser_Hour2 : MonoBehaviour
{
    public float finalYPosition;
    private Vector3 startingPosition;

    public TimelineManager_Script_Hour2 timeline;
    
    private void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(startingPosition.x, startingPosition.y + finalYPosition * timeline.GetTimeline(), startingPosition.z);
    }
}
