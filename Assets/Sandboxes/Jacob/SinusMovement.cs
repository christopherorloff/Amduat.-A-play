using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class SinusMovement : MonoBehaviour
{
    private bool isMoving;
    private bool isReady = true;

    private float[] sines = new float[5];
    private float[] timers = new float[5];
    private bool[] timerIsReady = new bool[5];
    private bool[] timerIsMoving = new bool[5];
    private float[] timeSinceLast = new float[5];

    private float cooldown;
    public float maxCooldown = 1;

    public float magnitude = 0.05f;
    public float speed = 1;

    public float velocity;

    private void Start()
    {
        velocity = transform.position.y;

        for(int i = 0; i < sines.Length; i++) {
            timerIsReady[i] = true;
        }
    }

    void Update() {
        if (!isReady) cooldown += Time.deltaTime;
        if (cooldown >= maxCooldown) {
            isReady = true;
            cooldown = 0;
        }

        /*if (Scroll.scrollValueAccelerated() > 0) {
            for (int i = 0; i < sines.Length; i++)
            {
                if (timerIsReady[i] && !timerIsMoving[i] && isReady)
                {
                    timerIsMoving[i] = true;
                    timerIsReady[i] = false;
                    timeSinceLast[i] = Time.time;
                    isReady = false;
                    return;
                }
            }
        }*/
        
        //SETTING THE READY SINE TO MOVE
        if (Input.GetKeyDown(KeyCode.Space) && isReady) { 
            for(int i = 0; i < sines.Length; i++) {
                if(timerIsReady[i] && !timerIsMoving[i]) {
                    timerIsMoving[i] = true;
                    timerIsReady[i] = false;
                    timeSinceLast[i] = Time.time;
                    isReady = false;
                    return;
                }
            }
        }

        //IF THE PARTICULAR SINE IS READY THEN SET A 0 POINT
        for (int i = 0; i < sines.Length; i++) { 
            if (timerIsMoving[i]) {
                timers[i] += Time.time - timeSinceLast[i];
            }

            //SETTING THE VALUE OF SINES
            //ADDING THOSE TO THE VELOCITY
            sines[i] = Mathf.Sin(timers[i] * speed) * (Time.deltaTime * magnitude);
            velocity += sines[i];

            //RESETTING SINES AND TIMERS
            if(sines[i] <= 0f) {
                timerIsReady[i] = true;
                timerIsMoving[i] = false;
                timers[i] = 0;
            }
        }

        transform.position = new Vector3(transform.position.x, velocity, transform.position.z);
    }



}
