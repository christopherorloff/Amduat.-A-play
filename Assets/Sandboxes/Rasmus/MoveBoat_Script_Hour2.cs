using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class MoveBoat_Script_Hour2 : MonoBehaviour
{
    public float speedMultiplier = 400;
    public float maxSpeed = 3;
    public float drag = 0.99f;

    public float timeBeforeRampDown = 0.5f;

    void Update()
    {
        float x = Mathf.Clamp(Scroll.scrollValueAccelerated(drag) * Time.deltaTime * speedMultiplier, 0, maxSpeed);
        
        if (x > 0)
        {
            print("Input: " + x);
            transform.position += new Vector3(x, 0, 0);
        }
    }
}
