using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class ArmControl : MonoBehaviour
{
    //private
    float input;
    Transform root;
    public GameObject snake;
    ThrowScript throwScript;

    
    float maxAngle = 10;
    float minAngle = -6;

    //public
    public float scalar = 1;
    public float deltaThreshold = 0.3f;
    public float drag = 0.9f;
    private float velocity = 0;
    
    void Start()
    {
        root = FindComponentInChildWithTag(this.gameObject,"Root");  
        throwScript = GetComponentInChildren<ThrowScript>();  
        
    }

    void Update()
    {
        TakeInput();   
        ProcessInput();
    }


    private void TakeInput()
    {
        input = Scroll.scrollValueAccelerated() * scalar * drag;
    }

    private void ProcessInput()
    {
        root.Rotate(0,0,input);
        float z = root.rotation.eulerAngles.z;
        z = (z > 180) ? (z - 360) : z;
        
        if (z > maxAngle)
        {
            print("max: " + (z * Time.deltaTime) );
            if (z * Time.deltaTime > deltaThreshold)
            {
                throwScript.ThrowKnife(snake.transform.position);
            }
            root.rotation = Quaternion.Euler(0,0,maxAngle);
        } else if (z < minAngle)
        {
            root.rotation = Quaternion.Euler(0, 0, minAngle + 360);
        }

        if(input == 0)
        {

            root.rotation = Quaternion.Euler(0, 0, Mathf.SmoothDamp(transform.position.z, 0f, ref velocity ,2.0f));
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