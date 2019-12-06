using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class ArmControl_Script : MonoBehaviour
{

    //private
    float input;
    Transform root;

    ThrowScript throwScript;
    float snapBack;

    bool inputIsActive = false;

    float maxAngle = 10;
    float minAngle = -6;

    //public
    public float scalar = 1;
    public float deltaThreshold = 0.3f;
    public float drag = 0.9f;
    public float snapBackSpeed = 0;

    public GameObject snake;
    SnakeStateScript snakeState;

    void OnEnable()
    {
        EventManager.turnOffInputEvent += TurnOffInput;
        EventManager.turnOnInputEvent += TurnOnInput;
    }

    void OnDisable()
    {
        EventManager.turnOffInputEvent -= TurnOffInput;
        EventManager.turnOnInputEvent -= TurnOnInput;
    }

    void TurnOffInput()
    {
        inputIsActive = false;
    }

    void TurnOnInput()
    {
        inputIsActive = true;
    }

    void Start()
    {
        root = FindComponentInChildWithTag(this.gameObject, "Root");
        throwScript = GetComponentInChildren<ThrowScript>();
        snakeState = snake.GetComponent<SnakeStateScript>();
    }

    void Update()
    {
        if (inputIsActive)
        {
            TakeInput();
            ProcessInput();
        }      
    }


    private void TakeInput()
    {
        //input = Scroll.scrollValueAccelerated() * scalar * drag;
        input = (Scroll.scrollValue() * scalar * drag);
    }

    private void ProcessInput()
    {
        root.Rotate(0, 0, input);
        float z = root.rotation.eulerAngles.z;
        z = (z > 180) ? (z - 360) : z;

        if (z > maxAngle)
        {
            if (throwScript == null)
            
            {
                throwScript = GetComponentInChildren<ThrowScript>();
            }

            if (z * Time.deltaTime > deltaThreshold && throwScript != null)
            {
                throwScript.ThrowKnife(snake.transform.position);
            }
            root.rotation = Quaternion.Euler(0, 0, maxAngle);
        }
        else if (z < minAngle)
        {
            root.rotation = Quaternion.Euler(0, 0, minAngle + 360);
        }
        else
        {
            snapBack = Mathf.Lerp(z, 0, snapBackSpeed * Time.deltaTime);
            root.rotation = Quaternion.Euler(0, 0, snapBack + 360);
        }
    }

    public Transform FindComponentInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<Transform>();
            }
        }
        return null;
    }
}