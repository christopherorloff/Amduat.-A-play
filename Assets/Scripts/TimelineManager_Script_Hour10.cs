﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ScrollManager;

public class TimelineManager_Script_Hour10 : MonoBehaviour
{
    public float LaserDuration = 1;
    public float inputStep = 1;
    public float inputDownStep = 1;
    public float boatLerpDuration = 4;

    public Transform boat;
    public Transform[] positions;

    public Transform[] coneTransforms;
    public Transform[] endStatueTransforms;

    public FadeUIScript fadeUIScript;


    private bool coroutineRunning = false;
    private Vector3 camOffset;
    private int boatProgress = 0;
    private int coneProgress = 0;
    private float velocity;
    private float input;
    private float minConeScaleY = 0.07f;
    private float maxConeScaleY = 1.0f;
    private bool ready = false;
    private bool endAnimationStarted = false;
    private Quaternion endBow;

    void Awake()
    {
        for (int i = 0; i < coneTransforms.Length; i++)
        {
            coneTransforms[i].localScale = new Vector3(0, minConeScaleY, 1);
        }
        endBow = Quaternion.Euler(0, 0, 28);
    }

    void Start()
    {
        StartCoroutine(LerpBoatToNextPosition(boatLerpDuration, boat.position, positions[0].position));
    }

    void Update()
    {
        if (ready && !endAnimationStarted)
        {
            HandleInput();
            camOffset = Camera.main.transform.position - boat.position;
        }
    }

    private void HandleInput()
    {
        input = Scroll.scrollValue();

        //Scale up/down light cones
        if (input > 0)
        {
            velocity += inputStep * Time.deltaTime;
        }
        else
        {
            // velocity -= inputDownStep * Time.deltaTime;
        }

        // constrain velocity
        velocity = Mathf.Clamp(velocity, minConeScaleY, maxConeScaleY);

        //Only change scale when velocity is being changed
        if (velocity > 0)
        {
            if (coneProgress == 0)
            {
                coneTransforms[0].localScale = new Vector3(transform.localScale.x, velocity, 1);
            }
            else if (coneProgress == 1)
            {
                coneTransforms[1].localScale = new Vector3(transform.localScale.x, velocity, 1);
                coneTransforms[2].localScale = new Vector3(transform.localScale.x, velocity, 1);

            }
            else if (coneProgress == 2)
            {
                coneTransforms[3].localScale = new Vector3(transform.localScale.x, velocity, 1);
                coneTransforms[4].localScale = new Vector3(transform.localScale.x, velocity, 1);

            }

        }

        if (velocity == 1)
        {
            //Prepare for next setup 
            //----------------------
            print("boat " + boatProgress + " cone " + coneProgress);
            // - Reset velocity for next cone(s)
            velocity = 0;

            // - Increment boatprogress and coneprogress
            coneProgress++;
            // - Start Boat Coroutine
            // - Start Beam Coroutine

            if (coneProgress == 1)
            {
                StartCoroutine(LerpBoatToNextPosition(boatLerpDuration, boat.transform.position, positions[boatProgress].position));
                StartCoroutine(BeamLerp(1, LaserDuration));
                StartCoroutine(BeamLerp(2, LaserDuration));
            }
            else if (coneProgress == 2)
            {
                StartCoroutine(LerpBoatToNextPosition(boatLerpDuration, boat.transform.position, positions[boatProgress].position));
                StartCoroutine(BeamLerp(3, LaserDuration));
                StartCoroutine(BeamLerp(4, LaserDuration));
            }
            else if (coneProgress == 3)
            {
                StartCoroutine(LerpBoatToNextPosition(boatLerpDuration, boat.transform.position, positions[boatProgress].position));

            }
        }
    }

    IEnumerator LerpBoatToNextPosition(float duration, Vector3 startPos, Vector3 endPos)
    {
        float startTime = Time.time;
        float t = 0;

        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0, 1, t);
            t = Mathf.Clamp(t, 0, 1);

            boat.position = Vector3.Lerp(startPos, endPos, t);
            if (boatProgress > 1)
            {
                Camera.main.transform.position = boat.position + camOffset;
            }

            yield return null;
        }

        if (boatProgress == 0)
        {
            camOffset = Camera.main.transform.position - boat.position;
            StartCoroutine(BeamLerp(0, LaserDuration));
            ready = true;
        }

        if (coneProgress == 3 && !endAnimationStarted)
        {
            StartEndAnimation();
        }
        boatProgress++;
    }

    private void StartEndAnimation()
    {
        endAnimationStarted = true;
        print("End animation started");
        StartCoroutine(StatuesBow(4));

    }

    IEnumerator BeamLerp(int index, float duration)
    {
        coroutineRunning = true;

        float startTime = Time.time;
        float t = 0;

        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0, 1, t);
            t = Mathf.Clamp(t, 0, 1);

            coneTransforms[index].localScale = new Vector3(t, coneTransforms[index].localScale.y, 1);

            yield return null;
        }
        coroutineRunning = false;
    }

    IEnumerator StatuesBow(float duration)
    {
        float startTime = Time.time;
        float t = 0;
        Quaternion startRotation = endStatueTransforms[0].rotation;

        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0, 1, t);
            t = Mathf.Clamp(t, 0, 1);

            endStatueTransforms[0].rotation = Quaternion.Lerp(startRotation, endBow, t);
            endStatueTransforms[1].rotation = Quaternion.Lerp(startRotation, endBow, t);

            yield return null;
        }
        FadeInDustNScarab();
    }

    //DEN HER NIKOLAJ!!
    private void FadeInDustNScarab()
    {
        //Den her til aller sidst!
        fadeUIScript.StartFadeOut();

    }
}
