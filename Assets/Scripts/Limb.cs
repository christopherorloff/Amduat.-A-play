using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    //TARGET VARIABLES
    public Transform target;
    public float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;

    public bool isMoving;
    public bool isActive;

    private void Update()
    {
        if (isMoving) {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        }

        if (isActive) { 
            
        }
    }
}
