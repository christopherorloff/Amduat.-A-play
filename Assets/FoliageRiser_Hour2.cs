using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class FoliageRiser_Hour2 : MonoBehaviour
{
    float finalYPosition;
    private Vector3 startingPosition;

    public TimelineManager_Script_Hour2 timeline;
    
    private void Start()
    {
        startingPosition = transform.position;
        finalYPosition = Random.Range(3f, 10f);

    }

    void Update()
    {
        transform.position = new Vector3(startingPosition.x, startingPosition.y + finalYPosition * timeline.GetTimeline(), startingPosition.z);
    }
}
