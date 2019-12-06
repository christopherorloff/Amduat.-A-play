using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using System;

public class Scenemanager_hour5_script : MonoBehaviour
{

    public GameObject boat;
    public CheckCollisonBoat_Script triggerScript;
    public SnakeMoveTowardsBoat_script[] snakeMoveTowardsBoat_Script;
    public GameObject wings;
    [Range(0.0f, 1.0f)]
    public float normalizedTime;
    public bool pushSnakesAway = false;

    private int numberOfSnakes;
    private float boatMoveSpeedX;
    private Animator animator;
    private WingState wingState = WingState.down;

    private float animationFrame = 1.0f / 41.0f;
    private float[] frameStates;
    private bool running = false;

    enum WingState
    {
        down, mid, up
    };

    void OnEnable()
    {
        Scroll.OnScrollEnter += TakeInput;
    }

    void OnDisable()
    {
        Scroll.OnScrollEnter -= TakeInput;
    }

    void Start()
    {
        frameStates = new float[] { 1 * animationFrame, 15 * animationFrame, 27 * animationFrame };
        boatMoveSpeedX = 0.7f;
        animator = wings.GetComponent<Animator>();
        animator.speed = 0;
    }

    void Update()
    {
        // Consider doing a multiplication approach
        boat.transform.position += new Vector3(boatMoveSpeedX * Time.deltaTime, 0, 0);

        if (triggerScript.numberOfSnakes >= 2)
        {
            boatMoveSpeedX = 0.5f;
        }
        if (triggerScript.numberOfSnakes > 5)
        {
            boatMoveSpeedX = 0.4f;
        }
        if (triggerScript.numberOfSnakes > 7)
        {
            boatMoveSpeedX = 0.2f;
        }
        if (triggerScript.numberOfSnakes > 10)
        {
            boatMoveSpeedX = 0.1f;
        }
        if (triggerScript.numberOfSnakes > 15)
        {
            boatMoveSpeedX = 0.0f;
        }
        else if (triggerScript.numberOfSnakes < 2)
        {
            boatMoveSpeedX = 0.7f;

        }

    }

    private void TakeInput()
    {
        if (!running)
        {
            float input = -Scroll.scrollValue();
            print(input);
            switch (wingState)
            {
                case WingState.down:
                    if (input > 0)
                    {
                        StartCoroutine(WingMovement(wingState, WingState.up, 0.7f));
                        SoundManager.Instance.PlayWingCharge();
                    }
                    break;

                case WingState.mid:
                    if (input > 0)
                    {
                        StartCoroutine(WingMovement(wingState, WingState.up, 0.7f));
                    }
                    break;

                case WingState.up:
                    if (input < 0)
                    {
                        StartCoroutine(WingMovement(wingState, WingState.down, 0.55f));
                        SoundManager.Instance.PlayWingThrust();
                    }
                    break;
            }
        }
    }

    private IEnumerator WingMovement(WingState fromState, WingState toState, float duration)
    {
        running = true;
        float t = 0;
        float startTime = Time.time;
        float from = frameStates[(int)fromState];
        float to = (frameStates[(int)toState] < from) ? 1 : frameStates[(int)toState];

        while (t < 1)
        {
            t = (Time.time - startTime) / duration;
            t = Mathf.SmoothStep(0, 1, t);
            t = Mathf.Clamp(t, 0, 1);
            float timeStep = Mathf.Lerp(from, to, t);
            animator.Play(0, 0, timeStep);

            yield return null;
        }
        running = false;
        ChangeState(toState);
    }

    private void ChangeState(WingState newState)
    {
        wingState = newState;
        if (newState == WingState.down)
        {
            StartCoroutine(TurnBoolOff());
        }
    }

    public IEnumerator TurnBoolOff()
    {
        pushSnakesAway = true;
        yield return null;
        pushSnakesAway = false;
    }





}
