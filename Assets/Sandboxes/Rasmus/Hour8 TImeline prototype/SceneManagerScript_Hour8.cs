using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript_Hour8 : MonoBehaviour
{
    public Transform boat;
    public Transform blessedDeadParent;
    public float boatMaxSpeed;
    public float slowDownScalar = 1;

    private int numberOfBlessedDead;
    private int blessedDeadPushingCount = 0;
    private int minimumAmountForPushing = 3;

    private void Start()
    {
        numberOfBlessedDead = blessedDeadParent.childCount;
    }

    private void Update()
    {
        MoveBoat();
    }

    public void AddBlessedDeadPushing()
    {
        blessedDeadPushingCount++;
    }

    private void MoveBoat()
    {
        float speed = 0;

        if (blessedDeadPushingCount == numberOfBlessedDead)
        {
            speed = boatMaxSpeed * Time.deltaTime;
        }
        else
        {
            speed = boatMaxSpeed * ((float)blessedDeadPushingCount / (float)numberOfBlessedDead) * slowDownScalar * Time.deltaTime;
        }

        if (blessedDeadPushingCount > minimumAmountForPushing)
        {
            //here if you want to add some game feel:
            boat.transform.position += new Vector3(speed, 0, 0);
        }
    }

    public float GetBlessedDeadSpeed() { 
        return (float)blessedDeadPushingCount / (float)numberOfBlessedDead;
    }
}
