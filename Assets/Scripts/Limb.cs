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

    private Shake shake;
    private float shakeValue;
    private float increaseSpeed = 0.02f;

    private Wiggle wiggle;

    public GameObject snapParticle;
    private bool particleRunning;

    private void Start()
    {
        shake = GetComponentInParent<Shake>();
        wiggle = GetComponentInParent<Wiggle>();
    }

    private void Update()
    {
        if (isMoving && transform.position != target.position) {
            //Move limb to correct position of target
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);

            //If shake script is on parent, then increase shake
            if (shake != null)
            {
                shakeValue += Time.deltaTime * increaseSpeed;
                if (shakeValue > 0.025f) shakeValue = 0.025f;
                shake.magnitude = shakeValue;
            }

            //Snap (i.e. make smooth time 0) when close to target
            float dist = Vector3.Distance(transform.position, target.position);
            if(dist <= 0.05f) {
                smoothTime = 0;
                if (!particleRunning) {
                    Instantiate(snapParticle, transform.position, Quaternion.identity);
                    particleRunning = true;
                }
            }
            //print("DISTANCE TO TARGET IS : " + dist);
        }

        if (isActive) {
            if(wiggle != null) wiggle.enabled = true;
            if (shake != null) shake.enabled = false;
        } else {
            if (wiggle != null) wiggle.enabled = false;
            if (shake != null) shake.enabled = true;
        }
    }
}
