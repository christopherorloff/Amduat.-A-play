using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeStateScript : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 secondaryPosition;
    float yMax;
    float yMin;


    public bool snakeAlive = true;
    public bool snakeRising = false;

    float startTime;
    public float duration = 3.0f;

    int knifeHits = 0;
    public int knifeHitLimit = 6;

    BackGround_Object_Movement_Script bgMovement;

    public FadeUIScript fadeScript;

    void Awake()
    {
        initialPosition = this.transform.position;
        secondaryPosition = new Vector3(initialPosition.x, -10, 0);
        yMax = initialPosition.y;
        yMin = secondaryPosition.y;
        bgMovement = GetComponent<BackGround_Object_Movement_Script>();
        bgMovement.enabled = false;
        snakeRising = true;
        StartCoroutine(SnakeIntro());
    }



    IEnumerator SnakeIntro()
    {
        startTime = Time.time;
        transform.position = secondaryPosition;
        print((Time.time - startTime));
        while ((Time.time - startTime) <= duration)
        {
            print((Time.time - startTime));
            float t = (Time.time - startTime) / duration;
            transform.position = new Vector3(initialPosition.x, Mathf.SmoothStep(yMin, yMax, t), 0);
            yield return null;
        }
        snakeRising=false;
        print("Fade in snake done.");
        bgMovement.enabled = true;
        EventManager.turnOnInputEvent();
    }

    public void hitPlus()
    {
        knifeHits++;
    }

    private void Update()
    {
        if (knifeHits >= knifeHitLimit && snakeAlive == true)
        {

            SoundManager.Instance.EndApopisTheme();
            SoundManager.Instance.apopisIdleInstance.start();
            SoundManager.Instance.apopisAppearInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SoundManager.Instance.caveWaterAmbInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            SoundManager.Instance.caveAmbInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            fadeScript.StartFadeOut();
            snakeAlive = false;
            print("den ær døj");
            EventManager.turnOffInputEvent();
           // EventManager.snakeDeadEvent();
        }
    }
    
}
