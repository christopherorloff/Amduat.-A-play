using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using System;

public class TimelineManager_Script_Hour4 : Timeline_BaseClass
{
    // General
    bool nextIsDraw = true;
    bool coroutineRunning = false;
    public int numberOfPulls = 5;
    private Vector3 boatMoveVector;
    public FadeUIScript fadeUIScript;

    //Boat
    public GameObject boat;
    public Transform startPosBoat;
    public Transform endPosBoat;
    private Vector3 boatDistanceToGoddesses;

    //Goddesses
    public Transform goddessesParent;
    private Transform[] goddesses;
    private SpriteRenderer[] goddessesSprite;
    private Animator[] goddessAnimators;
    const float numberOfAnimationFrames = 36;
    const float animationFrameLength = 1 / numberOfAnimationFrames;
    public float drawDuration = 2;
    public float pullDuration = 3;
    public float rotationAfterDraw = 25;
    public float rotationAfterPull = -25;
    public float fadeInTime = 1;

    //From old scenemanager --> make more sustainable solution
    public SpriteRenderer GoddessIcon;
    float fade = 0.15f;
    public ParticleSystem StartParticles;
    private bool sceneHasEnded = false;

    public float WrongInputDuration;
    public float WrongInputDraw;
    public float WrongInputPull;
    private bool firstWrong = true;

    void Awake()
    {
        goddesses = new Transform[goddessesParent.childCount];
        goddessesSprite = new SpriteRenderer[goddessesParent.childCount];
        goddessAnimators = new Animator[goddessesParent.childCount];

        for (int i = 0; i < goddessesParent.childCount; i++)
        {
            goddesses[i] = goddessesParent.GetChild(i);
            goddessesSprite[i] = goddesses[i].GetComponent<SpriteRenderer>();
            goddessAnimators[i] = goddesses[i].GetComponent<Animator>();

            //Initial values
            goddessAnimators[i].Play(0, 0, 18 * animationFrameLength);
            goddessAnimators[i].speed = 0;
            goddessesSprite[i].color *= new Vector4(1, 1, 1, 0);
        }

        //Boat can only move on x-axis. If y-axis is needed, create similar float to travelDistanceX and input in boatMoveVector
        float travelDistanceX = (endPosBoat.position.x - startPosBoat.position.x) / numberOfPulls;
        boatMoveVector = new Vector3(travelDistanceX, 0, 0);
        boatDistanceToGoddesses = new Vector3(goddessesParent.position.x - boat.transform.position.x, goddessesParent.position.y - boat.transform.position.y, 0);
        Scroll.setDeltaMultiplier(2000);
    }

    void OnEnable()
    {
        Scroll.OnScrollEnter += ConvertInputToProgress;
    }

    void OnDisable()
    {
        Scroll.OnScrollEnter -= ConvertInputToProgress;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Add timeline events here 

        HandleKeys();
        StartCoroutine(WaitAndStart());
    }

    void ConvertInputToProgress()
    {
        float input = Scroll.scrollValue();
        bool sign = GetSignBool(input);

        //For debug purposes
        if (Mathf.Approximately(input, 0.0f))
        {
            // print("Input is zero!");
        }

        if (Mathf.Approximately(Timeline, 1) && !sceneHasEnded)
        {
            fadeUIScript.StartFadeOut();
            sceneHasEnded = true;
        }

        //Check if correct order of up/down scroll
        if (nextIsDraw == sign && !sceneHasEnded)
        {
            //Only continue if the coroutine is not already running
            if (!coroutineRunning)
            {
                //Add percentage to timeline
                Timeline += (1.0f / (float)numberOfPulls / 2.0f);
                //print("Timeline: " + Timeline);
                if (nextIsDraw)
                {
                    StartCoroutine(DrawAnimation(18 * animationFrameLength, 1));
                    nextIsDraw = false;
                    firstWrong = false;
                }
                else
                {
                    StartCoroutine(PullAnimation(0, 18 * animationFrameLength));
                    nextIsDraw = true;
                }
            }
        }
        else
        {
            if (nextIsDraw)
            {
                if (firstWrong)
                {
                    StartCoroutine(WrongInput(WrongInputDraw));
                }
                else
                {
                    StartCoroutine(WrongInput(WrongInputDraw));
                }
            }
            else
            {
                StartCoroutine(WrongInput(WrongInputPull));
            }
        }
    }

    private IEnumerator WrongInput(float WrongInputRotation2)
    {
        float startTime = Time.time;

        Quaternion startRotation = goddesses[0].rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, WrongInputRotation2);
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / WrongInputDuration;
            t = (t * t * t);
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddesses[i].rotation = Quaternion.Lerp(startRotation, endRotation, t);
            }
            yield return null;
        }
        if (t >= 1f)
        {
            float t2 = 0;
            while (t2 < 1)
            {
                t2 = (Time.time - startTime) / (WrongInputDuration * 2);
                t2 = (t2 * t2 * t2);
                t2 = Mathf.Clamp(t2, 0, 1);
                for (int i = 0; i < goddesses.Length; i++)
                {
                    goddesses[i].rotation = Quaternion.Lerp(endRotation, startRotation, t2);
                }
                yield return null;
            }
        }


    }



    private IEnumerator DrawAnimation(float start, float end)
    {
        coroutineRunning = true;

        FMOD.Studio.EventInstance collectEnergyInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 4/CollectEnergy");
        collectEnergyInstance.start();

        float startTime = Time.time;
        float t = 0;

        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < goddessAnimators.Length; i++)
            {
                goddessAnimators[i].Play(0, 0, Mathf.Lerp(start, end, t));
            }
            yield return null;
        }
        if (Timeline == 1f)
        {
            StartCoroutine(MoveLast(0, 1f));
        }
        coroutineRunning = false;
    }


    IEnumerator PullAnimation(float start, float end)
    {

        coroutineRunning = true;
        FMOD.Studio.EventInstance towBoatInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 4/TowBoat");
        towBoatInstance.start();
        //  print("Pull coroutine");

        float startTime = Time.time;

        Vector3 startPositionGoddesses = goddessesParent.transform.position;
        Vector3 endPositionGoddesses = startPositionGoddesses + boatMoveVector;
        float t = start;
        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddessesParent.position = Vector3.Lerp(startPositionGoddesses, endPositionGoddesses, t);
                boat.transform.position = Vector3.Lerp(startPositionGoddesses - boatDistanceToGoddesses, endPositionGoddesses - boatDistanceToGoddesses, t);
                goddessAnimators[i].Play(0, 0, Mathf.Lerp(start, end, t));
            }
            yield return null;
        }
        if (Timeline == 1f)
        {
            StartCoroutine(MoveLast(0, 1f));
        }
        coroutineRunning = false;
    }

    bool GetSignBool(float input)
    {
        if (input < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator WaitAndStart()
    {

        yield return new WaitForSeconds(fadeInTime);
        StartParticles.Play();
        SoundManager.Instance.goddessesAppearingInstance.start();
        while (GoddessIcon.color.a > 0)
        {
            GoddessIcon.color = new Color(GoddessIcon.color.r, GoddessIcon.color.g, GoddessIcon.color.b, GoddessIcon.color.a - fade * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(FadeInGoddesses());


        StartParticles.Stop();

    }

    IEnumerator FadeInGoddesses()
    {

        while (goddessesSprite[0].color.a < 1)
        {
            for (int i = 0; i < goddessesSprite.Length; i++)
            {
                goddessesSprite[i].color = new Color(goddessesSprite[i].color.r, goddessesSprite[i].color.g, goddessesSprite[i].color.b, goddessesSprite[i].color.a + fade * Time.deltaTime);

            }
            yield return null;

        }
    }
    private IEnumerator MoveLast(float start, float end)
    {
        float startTime = Time.time;

        Vector3 startPositionGoddesses = goddessesParent.transform.position;
        Vector3 endPositionGoddesses = startPositionGoddesses + new Vector3(3, 0, 0);
        float t = start;

        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddessesParent.position = Vector3.Lerp(startPositionGoddesses, endPositionGoddesses, t);
                boat.transform.position = Vector3.Lerp(startPositionGoddesses - boatDistanceToGoddesses, endPositionGoddesses - boatDistanceToGoddesses, t);
                goddessAnimators[i].Play(0, 0, Mathf.Lerp(start, end, t));
            }
            yield return null;
        }
    }

    ///--------///
    /// Legacy ///
    ///--------///



    private IEnumerator Draw()
    {
        coroutineRunning = true;

        FMOD.Studio.EventInstance collectEnergyInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 4/CollectEnergy");
        collectEnergyInstance.start();
        float startTime = Time.time;

        print("Draw coroutine");
        Quaternion startRotation = goddesses[0].rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, rotationAfterDraw);
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = (t * t * t);
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddesses[i].rotation = Quaternion.Lerp(startRotation, endRotation, t);
            }
            yield return null;
        }
        coroutineRunning = false;
    }



    private IEnumerator Pull()
    {
        FMOD.Studio.EventInstance towBoatInstance = FMODUnity.RuntimeManager.CreateInstance("event:/HOUR 4/TowBoat");
        towBoatInstance.start();

        coroutineRunning = true;
        print("Pull coroutine");
        float startTime = Time.time;
        Quaternion startRotation = goddesses[0].rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, rotationAfterPull);
        Vector3 startPositionGoddesses = goddessesParent.transform.position;
        Vector3 endPositionGoddesses = startPositionGoddesses + boatMoveVector;
        float t = 0;
        while (t < 1)
        {
            t = (Time.time - startTime) / drawDuration;
            t = (t * t * t * t * t);
            t = Mathf.Clamp(t, 0, 1);
            for (int i = 0; i < goddesses.Length; i++)
            {
                goddesses[i].rotation = Quaternion.Lerp(startRotation, endRotation, t);
                goddessesParent.position = Vector3.Lerp(startPositionGoddesses, endPositionGoddesses, t);
                boat.transform.position = Vector3.Lerp(startPositionGoddesses - boatDistanceToGoddesses, endPositionGoddesses - boatDistanceToGoddesses, t);
            }
            yield return null;
        }
        coroutineRunning = false;
    }

}








