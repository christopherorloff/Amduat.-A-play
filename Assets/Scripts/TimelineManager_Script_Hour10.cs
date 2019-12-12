using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager_Script_Hour10 : Timeline_BaseClass
{
    public Transform boat;
    public Transform[] positions;

    public Transform[] coneTransforms;
    public Transform[] endStatueTransforms;
    private bool coroutineRunning = false;

    private int progress = 0;

    void Start()
    {

        HandleKeys();

    }

    void Update()
    {
        // HandleInput();
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(LerpBoatToNextPosition(2, boat.position, positions[progress].position));
            progress++;
        }
    }

    private void HandleInput()
    {
        throw new NotImplementedException();
    }

    IEnumerator LerpBoatToNextPosition(float duration, Vector3 startPos, Vector3 endPos)
    {
        coroutineRunning = true;
        float startTime = Time.time;
        float t = 0;

        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0, 1, t);
            t = Mathf.Clamp(t, 0, 1);

            boat.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
    }
}
