using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Scenemanager_hour5_script : MonoBehaviour
{

    public GameObject Boat;
    public GameObject wing1;

    Animation anim;
    public AnimationClip wingUp;
    public AnimationClip wingDown;

    public CheckCollisonBoat_Script TriggerScript;

    int numberOfSnakes;
    float boatMoveSpeedX;
    float animSpeed = 0.5f;

    bool readyToFlap = false;
    public bool pushSnakesAway = false;

    void Start()
    {
        anim = wing1.GetComponent<Animation>();
        boatMoveSpeedX = 0.7f;
    }

    void Update()
    {

        Boat.transform.position += new Vector3(boatMoveSpeedX * Time.deltaTime, 0, 0);
        print(boatMoveSpeedX);

        if (TriggerScript.numberOfSnakes >= 2)
        {
            boatMoveSpeedX = 0.5f;
        }
        if (TriggerScript.numberOfSnakes > 5)
        {
            boatMoveSpeedX = 0.4f;
        }
        if (TriggerScript.numberOfSnakes > 7)
        {
            boatMoveSpeedX = 0.2f;
        }
        if (TriggerScript.numberOfSnakes > 10)
        {
            boatMoveSpeedX = 0.1f;
        }
        if (TriggerScript.numberOfSnakes > 15)
        {
            boatMoveSpeedX = 0.0f;
        }
        else if (TriggerScript.numberOfSnakes < 2)
        {
            boatMoveSpeedX = 0.7f;

        }
        


        if (Scroll.scrollValueAccelerated() < 0 && !readyToFlap)
        {
            anim.clip = wingUp;
            anim["wingUp"].speed = animSpeed;
            anim.Play();
        }

        if (Scroll.scrollValueAccelerated() == 0 && !readyToFlap)
        {
            anim.clip = wingUp;
            anim["wingUp"].speed = 0;
            anim.Play();
        }

        if (anim["wingUp"].normalizedTime >= 0.99f && !readyToFlap)
        {
            readyToFlap = true;
        }


        if (Scroll.scrollValueAccelerated() > 0 && readyToFlap)
        {
            anim.clip = wingDown;
            anim["wingDown"].speed = animSpeed;
            anim.Play();
        }

        if (anim["wingDown"].normalizedTime >= 0.99f)
        {
            StartCoroutine(TurnBoolOff());
        }

        


    }


    public IEnumerator TurnBoolOff()
    {
        pushSnakesAway = true;
        yield return new WaitForSeconds(1f);
        pushSnakesAway = false;
        readyToFlap = false;


    }




}
