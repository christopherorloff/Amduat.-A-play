using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;
using System;

public class MoveBoat_Script_Hour2 : MonoBehaviour
{
    public float speedMultiplier = 400;
    public float maxVelocity = 0.3f;
    public float drag = 0.99f;

    public float timeBeforeRampDown = 0.5f;

    bool maxVelocityReached = false;

    void Update()
    {
        float input = Mathf.Abs(Scroll.scrollValueMean(10));
        float velocityX = Mathf.Clamp(input,0,maxVelocity);

        if (velocityX >= maxVelocity && !maxVelocityReached)
        {
            OnNewScrollEnter();
            maxVelocityReached = true;
        }
        else if(velocityX < maxVelocity && maxVelocityReached)
        {
            OnNewScrollExit();
            maxVelocityReached = false;
        }

    }

    private void OnNewScrollEnter()
    {
        print("New scroll enter");
    }

    private void OnNewScrollExit()
    {
        print("New scroll exit");
    }
}
